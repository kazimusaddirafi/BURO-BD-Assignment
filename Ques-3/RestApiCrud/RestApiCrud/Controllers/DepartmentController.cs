using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiCrud.Models;

namespace RestApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<DepartmentModel>> GetDepartmentList()
        {
            try
            {
                var result = await _context.Departments.ToListAsync();
                return StatusCode(200,new { status = true, data = result, message = "success" });
            }
            catch (Exception ex) {
                return StatusCode(500,new { status = false, message = "Something went wrong", });
            }  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentModel>> GetDepartmentDetails(int id)
        {
            try { 
            var deptInfo = await _context.Departments.FindAsync(id);

            if (deptInfo == null)
            {
                return StatusCode(404, new { status = false, message = "No department found", }); ;
            }

                return StatusCode(200, new { status = true, data = deptInfo, message = "success" });
            }

            catch (Exception ex) {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateDepartment(DepartmentModel deptData)
        {
            try
            {
                await _context.Departments.AddAsync(deptData);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { status = true, message = "New department created" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentModel deptData)
        {
          
            try
            {

                if (id != deptData.Id)
                {
                    return StatusCode(404, new { status = false, message = "No department found in this id" });
                }
                var deptInfo = await _context.Departments.FindAsync(id);
                if (deptInfo==null)
                {
                    return StatusCode(404, new { status = false, message = "No department found in this id" });
                }

                _context.Entry(deptData).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return StatusCode(201, new { status = true, message = "Department data updated" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }

            
        }
    }
}
