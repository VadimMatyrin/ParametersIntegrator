using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ParametersIntegrator.Services.JsonServices
{
    public class JsonSubdivisonService : IJsonSectionService
    {
        public bool EditSection(JObject json, string sectionName, string replaceSectionName, List<JToken> newValues)
        {
            throw new NotImplementedException();
        }

        public bool ProcessJArray(JArray json)
        {
            bool isEdited = false;
            foreach (var jToken in json.Children())
            {
                if (TryGetSubdivisionNameToChange(jToken, out string subdivisionName))
                {
                    isEdited = true;
                    jToken["Name"] = Regex.Replace(subdivisionName, @"\s*\(see also separate country.+\)\s*", "");
                }
            }
            return isEdited;
        }

        private bool TryGetSubdivisionNameToChange(JToken jToken, out string subdivisionName)
        {
            subdivisionName = jToken["Name"]?.Value<string>();
            if (subdivisionName is not null && Regex.IsMatch(subdivisionName, @"\s*\(see also separate country.+\)\s*"))
            {
                return true;
            }
            return false;
        }
    }
}
