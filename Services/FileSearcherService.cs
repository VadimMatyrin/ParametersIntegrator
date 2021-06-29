using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParametersIntegrator.Services
{
    public class FileSearcherService : IFileSearcherService
    {
        public List<string> GetMathingFiles(string searchDirectory, string fileSearchPattern = null)
        {
            if (searchDirectory is null)
            {
                throw new ArgumentNullException();
            }

            List<string> fileNames;
            if (fileSearchPattern != null)
            {
                var reg = new Regex(fileSearchPattern);
                fileNames = Directory.GetFiles(searchDirectory, "*.*", SearchOption.AllDirectories).Where(f => reg.IsMatch(f)).ToList();
            }
            else
            {
                fileNames = Directory.GetFiles(searchDirectory, "*.*", SearchOption.AllDirectories).ToList();
            }

            return fileNames;
        }


        public JObject GetJObjectFromFile(string filePath)
        {
            if (filePath is null)
            {
                throw new ArgumentNullException();
            }

            var fileJObject = JObject.Parse(File.ReadAllText(filePath));
            return fileJObject;
        }
    }
}
