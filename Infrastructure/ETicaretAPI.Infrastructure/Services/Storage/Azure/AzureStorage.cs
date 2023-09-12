using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ETicaretAPI.Application.Abstractions.Storage.Azure;
using ETicaretAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : Storage, IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _blobContainerClient.GetBlobs().Select(b=>b.Name).ToList();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainer, IFormFileCollection files)
        {
            try
            {
                _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainer);
                await _blobContainerClient.CreateIfNotExistsAsync();
                await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
                List<(string fileName, string pathOrContainer)> datas = new();
                foreach (var file in files)
                {
                    string fileNewName = await FileRenameAsync(pathOrContainer, file.Name, HasFile);
                    BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
                    await blobClient.UploadAsync(file.OpenReadStream());
                    datas.Add((fileNewName, $"{pathOrContainer}/{fileNewName}"));
                }
                return datas;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
