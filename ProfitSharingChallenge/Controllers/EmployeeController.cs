using Microsoft.AspNetCore.Mvc;
using ProfitSharingChallenge.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProfitSharingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesData _data;

        public EmployeeController(IEmployeesData data)
        {
            _data = data;
        }

        //GET api/employee
        [HttpGet]
        public async Task<ActionResult<EmployeeItem[]>> Get()
        {
            return await _data.GetEmployeesAsync();
        }

        //GET api/employee/{matricula}
        [HttpGet("{matricula}")]
        public async Task<ActionResult<EmployeeItem>> Get(string matricula)
        {
            var employee = _data.GetEmployeeAsync(matricula);
            if (employee == null)
                return NotFound();
        
            return await employee;
        }

    }
}
