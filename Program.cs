using ParametersIntegrator.Services;
using System;

namespace ParametersIntegrator
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileSearcherService fileSearcherService = new FileSearcherService();
            IJsonSectionService jsonSectionService = new JsonAppSettingsSectionService();
            IParamIntegratorService appSettingsService = new AppSettingsParamIntegratorService(fileSearcherService, jsonSectionService);
            Console.WriteLine("Enter folder to search for json param files");
            string folder = Console.ReadLine();
            Console.WriteLine("Enter pattern for value in name to be replaced");
            string replaceSectionName = Console.ReadLine();
            appSettingsService.ProcessDirectory(folder, replaceSectionName);
            Console.ReadKey();
        }
    }
}
