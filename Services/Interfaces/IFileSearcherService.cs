using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ParametersIntegrator.Services
{
    public interface IFileSearcherService
    {
        JObject GetJObjectFromFile(string filePath);
        List<string> GetMathingFiles(string searchDirectory, string fileSearchPattern = null);
    }
}