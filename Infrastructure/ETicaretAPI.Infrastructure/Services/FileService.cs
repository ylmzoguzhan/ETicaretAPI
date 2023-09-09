using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<List<(string fileName, string path)>> UploadAsync(string directory, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, directory);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);
            List<(string fileName, string)> datas = new();
            List<bool> results = new();
            foreach (IFormFile file in files)
            {
                var newFileName = await FileRenameAsync(uploadPath, file.Name);
                var result = await SaveAsync($"{uploadPath}\\{newFileName}", file);
                results.Add(result);
                datas.Add((newFileName, $"{directory}\\{newFileName}"));
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;
        }
        private async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = "";
                if (first)
                {
                    var oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameHelper.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    newFileName = Path.GetFileNameWithoutExtension(fileName);
                    char lastChar = newFileName[newFileName.Length - 1];
                    if (char.IsDigit(lastChar))
                    {
                        int index = int.Parse(lastChar.ToString());
                        index++;
                        newFileName = newFileName.Remove(newFileName.Length-1);
                        newFileName += $"{index}{extension}";
                    }
                    else
                    {
                        newFileName += $"2{extension}"; 
                    }
                }
                if (File.Exists($"{path}\\{newFileName}"))
                    return await FileRenameAsync(path, newFileName, false);
                else
                    return newFileName;
            });
            return newFileName;
        }

        public async Task<bool> SaveAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
