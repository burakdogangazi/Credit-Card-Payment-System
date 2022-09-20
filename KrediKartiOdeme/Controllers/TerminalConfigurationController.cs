using DataAccess;
using DataAccess.Repositories;
using Entities.DbEntities;
using Microsoft.AspNetCore.Mvc;

namespace KrediKartiOdeme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalConfigurationController : GenericBaseController<TerminalConfiguration, EfTerminalConfigurationDAL>
    {

    }
}
