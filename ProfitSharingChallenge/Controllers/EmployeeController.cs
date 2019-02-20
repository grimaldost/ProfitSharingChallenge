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

        /// <summary>
        /// Get all employees in database
        /// </summary>
        /// <returns>
        /// Json with all empployee items in database
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<EmployeeItem[]>> Get()
        {
            return await _data.GetEmployeesAsync();
        }

        /// <summary>
        /// Get single employee by its id
        /// </summary>
        /// <param name="matricula">Id of the employee</param>
        /// <returns>
        /// Json with single employee item difined by matricula
        /// </returns>
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
