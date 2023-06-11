using Microsoft.AspNetCore.Mvc;

namespace PersonService.Api.Controllers
{
    public class BaseController : Controller
    {
        public string UserId => User?.Identity != null ? User.Identity.Name : "";
    }
}
