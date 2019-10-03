using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace U_StudyingCommunity_Dream.Localization
{
    public static class U_StudyingCommunity_DreamLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(U_StudyingCommunity_DreamConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(U_StudyingCommunity_DreamLocalizationConfigurer).GetAssembly(),
                        "U_StudyingCommunity_Dream.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
