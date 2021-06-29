namespace AppSettingParametersIntegrator.Services
{
    public interface IParamIntegratorService
    {
        void ProcessDirectory(string folder, string replaceSectionName);
    }
}