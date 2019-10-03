using System.Collections.Generic;

namespace U_StudyingCommunity_Dream.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
