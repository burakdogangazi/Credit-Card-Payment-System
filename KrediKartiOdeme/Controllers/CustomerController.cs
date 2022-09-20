using Microsoft.AspNetCore.Mvc;

namespace KrediKartiOdeme.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CustomerController: ControllerBase
    {

        [HttpPost]
        [Route("api/customer")]
        public bool Customer(string identityNumber, int Id , string name)
        {
            return true;
        }
    }
}
