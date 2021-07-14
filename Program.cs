using ParametersIntegrator.Services;
using ParametersIntegrator.Services.JsonServices;
using System;

namespace ParametersIntegrator
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProcessTemplateFiles();
            ProcessISOFiles();
        }

        private static void ProcessTemplateFiles()
        {
            IFileSearcherService fileSearcherService = new FileSearcherService();
            IJsonSectionService jsonSectionService = new JsonAppSettingsSectionService();
            IParamIntegratorService appSettingsService = new AppSettingsParamIntegratorService(fileSearcherService, jsonSectionService);
            GetUserInputs(appSettingsService);
        }

        private static void ProcessISOFiles()
        {
            IFileSearcherService fileSearcherService = new FileSearcherService();
            IJsonSectionService jsonSectionService = new JsonSubdivisonService();
            IParamIntegratorService appSettingsService = new ISOCodeNameIntegratorService(fileSearcherService, jsonSectionService);
            GetUserInputs(appSettingsService);
        }

        private static void GetUserInputs(IParamIntegratorService appSettingsService)
        {
            Console.WriteLine("Enter folder to search for json param files");
            string folder = Console.ReadLine();
            Console.WriteLine("Enter pattern for value in name to be replaced");
            string replaceSectionName = Console.ReadLine();
            appSettingsService.ProcessDirectory(folder, replaceSectionName);
            Console.ReadKey();
        }
    }
}
