using Microsoft.AspNetCore.Mvc;
using ProfitSharingChallenge.Models;
using System.Threading.Tasks;

namespace ProfitSharingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitSharingController : Controller
    {
        private readonly IProfitSharing _profitSharing;

        public ProfitSharingController(IProfitSharing profitSharing)
        {
            _profitSharing = profitSharing;
        }

        //GET api/profitsharing/total_disponibilizado
        [HttpGet("{total_disponibilizado}")]
        public async Task<ActionResult<ReturnItem>> Get(string total_disponibilizado)
        {
            return await _profitSharing.GetParticipation(total_disponibilizado);
        }
    }
}
