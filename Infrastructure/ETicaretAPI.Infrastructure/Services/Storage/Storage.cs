using ETicaretAPI.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
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
                        newFileName = newFileName.Remove(newFileName.Length - 1);
                        newFileName += $"{index}{extension}";
                    }
                    else
                    {
                        newFileName += $"2{extension}";
                    }
                }
                if (hasFileMethod(pathOrContainerName, newFileName))
                    return await FileRenameAsync(pathOrContainerName, newFileName,hasFileMethod,false);
                else
                    return newFileName;
            });
            return newFileName;
        }
    }
}
