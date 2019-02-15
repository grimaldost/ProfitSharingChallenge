using Microsoft.AspNetCore.Mvc;
using ProfitSharingChallenge.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProfitSharingChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;
        private readonly IProfitSharing _profitSharing;

        public EmployeeController(EmployeeContext context, IProfitSharing
            profitSharing)
        {
            _context = context;
            _profitSharing = profitSharing;
        }

        //GET api/employee
        [HttpGet]
        public ActionResult<DbSet<EmployeeItem>> Get()
        {
            return _context.employees;
        }

        //GET api/employee/{matricula}
        [HttpGet("{matricula}")]
        public ActionResult<EmployeeItem> Get(string matricula)
        {
            var employee = _context.employees.Find(matricula);
            if (employee == null)
                return NotFound();

            return employee;
        }


        //POST api/employee
        [HttpPost]
        public ActionResult<EmployeeItem[]> Post([FromBody] EmployeeItem[] employeeList)
        {
            foreach (EmployeeItem employee in employeeList)
            {
                if (_context.employees.Find(employee.matricula) == null)
                {
                    _context.employees.Add(employee);
                    _context.SaveChanges();
                    _profitSharing.UpdateParticipation(employee.matricula);
                }
            }

            return employeeList;
        }
    }
}
