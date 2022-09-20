using Entities.DbEntities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace KrediKartiOdeme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardConfigurationController : GenericBaseController<CreditCardConfiguration, EfCreditCardConfigurationDAL>
    {

    }
}
