namespace AppSettingParametersIntegrator.Services
{
    public interface IAppSettingsParamIntegratorService
    {
        void ProcessDirectory(string folder, string replaceSectionName);
    }
}