using Newtonsoft.Json;
using ParametersIntegrator.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParametersIntegrator.Services
{
    public class ISOCodeNameIntegratorService : IParamIntegratorService
    {
        private readonly IFileSearcherService _fileSearcherService;
        private readonly IJsonSectionService _jsonSectionService;

        public ISOCodeNameIntegratorService(IFileSearcherService fileSearcherService, IJsonSectionService jsonSectionService)
        {
            _fileSearcherService = fileSearcherService;
            _jsonSectionService = jsonSectionService;
        }

        public void ProcessDirectory(string folder, string replaceSectionName)
        {
            var files = _fileSearcherService.GetMathingFiles(folder, @".+\.JSON");

            foreach (var filePath in files)
            {
                var fileJObject = _fileSearcherService.GetJArrayFromFile(filePath);

                if (_jsonSectionService.ProcessJArray(fileJObject))
                {
                    using StreamWriter file = File.CreateText(filePath);
                    using JsonTextWriter jsonWriter = new JsonTextWriter(file)
                    {
                        Formatting = Formatting.Indented
                    };
                    jsonWriter.IndentChar = ' ';
                    jsonWriter.Indentation = 2;

                    fileJObject.WriteTo(jsonWriter);
                    Console.WriteLine($"{filePath} - proccessed");
                }
                else
                {
                    Console.WriteLine($"{filePath} - ignored");
                }
            }
        }
    }
}
