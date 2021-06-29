using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ParametersIntegrator.Utils
{
    public static class SectionParser
    {
        public static List<JToken> GetNewSectionsFromFile()
        {
            string directory = Directory.GetCurrentDirectory();
            string pathToSearchForFile = $"{directory}/sections.txt";
            string fileContent = File.ReadAllText(pathToSearchForFile);
            var fileJObject = JObject.Parse(fileContent);
            return fileJObject["value"].Children().ToList();
        }
    }
}
