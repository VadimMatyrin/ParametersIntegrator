using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AppSettingParametersIntegrator.Services
{
    public interface IJsonSectionService
    {
        bool EditSection(JObject json, string sectionName, string replaceSectionName, List<JToken> newValues);
    }
}