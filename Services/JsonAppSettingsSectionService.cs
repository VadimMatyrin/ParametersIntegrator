using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParametersIntegrator.Services
{
    public class JsonAppSettingsSectionService : IJsonSectionService
    {
        public bool EditSection(JObject json, string sectionName, string replaceSectionName, List<JToken> newValues)
        {
            if (json is null || sectionName is null || newValues is null)
            {
                throw new ArgumentNullException();
            }

            JToken jSection;

            if (IsTemplateFile(json))
            {
                jSection = json["resources"]
                    .Children()
                    .FirstOrDefault(c => c["properties"] != null &&
                            c["properties"]["parameters"] != null &&
                            c["properties"]["parameters"][sectionName] != null)
                    ?["properties"]["parameters"][sectionName];
            }
            else
            {
                jSection = json["parameters"][sectionName];
            }
            return ProcessToken(jSection, replaceSectionName, newValues);
        }

        private bool ProcessToken(JToken jSection, string replaceSectionName, List<JToken> newValues)
        {
            if (jSection != null && jSection["value"].Children().Count() != 0)
            {
                if (replaceSectionName != null)
                {
                    var childTokens = jSection["value"].Children();
                    if (childTokens.Count() > 1)
                    {
                        var sectionsToReplace = childTokens.Where(c => c["name"].Value<string>().Contains(replaceSectionName)).ToList();
                        foreach (var serilogValue in sectionsToReplace)
                        {
                            if (sectionsToReplace.Last() == serilogValue)
                            {
                                serilogValue.AddAfterSelf(newValues);
                            }

                            serilogValue.Remove();
                        }
                        if (sectionsToReplace.Count == 0)
                        {
                            jSection["value"].Children().Last().AddAfterSelf(newValues);
                        }
                        return true;
                    }
                }
                else
                {
                    jSection["value"].Children().Last().AddAfterSelf(newValues);
                    return true;
                }
            }
            return false;
        }

        private bool IsTemplateFile(JObject json)
        {
            return json["resources"] != null;
        }

    }
}
