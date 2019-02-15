using Microsoft.AspNetCore.Mvc;
using ProfitSharingChallenge.Models;
using System;

namespace ProfitSharingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitSharingController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly IProfitSharing _profitSharing;
        private readonly IReturnData _returnData;

        public ProfitSharingController(EmployeeContext context, IProfitSharing
            profitSharing, IReturnData returnData)
        {
            _context = context;
            _profitSharing = profitSharing;
            _returnData = returnData;
        }

        //GET api/profitsharing
        [HttpGet]
        public ActionResult<ReturnItem> Get()
        {
            // TODO: Return all profit sharings and resume

            return _profitSharing.GetParticipation();
        }

        //GET api/profitsharing/{matricula}
        [HttpGet("{matricula}")]
        public ActionResult<ReturnItem> Get(string matricula)
        {
            var employee = _context.participation.Find(matricula);
            if (employee == null)
                return NotFound();

            return _profitSharing.GetParticipation(matricula);
        }

        //Post api/profitsharing/{total_disponibilizado}
        [HttpPost("{total_disponibilizado}")]
        public ActionResult<string> Post(string total_disponibilizado)
        {
            _returnData.SetTDisp(Convert.ToDouble(total_disponibilizado));
            _context.SaveChanges();
            return "Total disponibilizado atualizado!";
        }

    }
}
