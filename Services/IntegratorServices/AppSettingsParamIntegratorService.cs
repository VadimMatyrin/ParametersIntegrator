using ParametersIntegrator.Utils;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ParametersIntegrator.Services
{
    public class AppSettingsParamIntegratorService : IParamIntegratorService
    {
        private readonly IFileSearcherService _fileSearcherService;
        private readonly IJsonSectionService _jsonSectionService;

        public AppSettingsParamIntegratorService(IFileSearcherService fileSearcherService, IJsonSectionService jsonSectionService)
        {
            _fileSearcherService = fileSearcherService;
            _jsonSectionService = jsonSectionService;
        }

        public void ProcessDirectory(string folder, string replaceSectionName)
        {
            var files = _fileSearcherService.GetMathingFiles(folder, @"deploy\\.+\.JSON");

            var newTokens = SectionParser.GetNewSectionsFromFile();

            foreach (var filePath in files)
            {
                var fileJObject = _fileSearcherService.GetJObjectFromFile(filePath);

                if (_jsonSectionService.EditSection(fileJObject, "appSettings", replaceSectionName, newTokens))
                {
                    bool shouldUseSpaces = ShouldUseSpaces(filePath);
                    using StreamWriter file = File.CreateText(filePath);
                    using JsonTextWriter jsonWriter = new JsonTextWriter(file)
                    {
                        Formatting = Formatting.Indented
                    };
                    if (shouldUseSpaces)
                    {
                        jsonWriter.IndentChar = ' ';
                        jsonWriter.Indentation = 4;
                    }
                    else
                    {
                        jsonWriter.IndentChar = '\t';
                        jsonWriter.Indentation = 1;
                    }

                    fileJObject.WriteTo(jsonWriter);
                    string indentType = shouldUseSpaces ? "spaces" : "tabs";
                    Console.WriteLine($"{filePath} - proccessed, formatted using {indentType}");
                }
                else
                {
                    Console.WriteLine($"{filePath} - ignored");
                }
            }
        }

        private bool ShouldUseSpaces(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            return fileContent.Count(ch => Char.IsWhiteSpace(ch) && ch != '\t') > fileContent.Count(ch => ch == '\t') * 4;
        }
    }
}
