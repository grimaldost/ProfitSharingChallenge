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

        /// <summary>
        /// Get all employees participation in profit sharing, and a resume of
        /// all values
        /// </summary>
        /// <param name="total_disponibilizado">Total amount of available money for share</param>
        /// <returns>
        /// json with all employees participation, number of employees, total shared,
        /// total available and final balance
        /// </returns>
        [HttpGet("{total_disponibilizado}")]
        public async Task<ActionResult<ReturnItem>> Get(string total_disponibilizado)
        {
            return await _profitSharing.GetParticipation(total_disponibilizado);
        }
    }
}
