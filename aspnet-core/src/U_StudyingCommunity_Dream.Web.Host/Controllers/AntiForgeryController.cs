using Microsoft.AspNetCore.Antiforgery;
using U_StudyingCommunity_Dream.Controllers;

namespace U_StudyingCommunity_Dream.Web.Host.Controllers
{
    public class AntiForgeryController : U_StudyingCommunity_DreamControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
