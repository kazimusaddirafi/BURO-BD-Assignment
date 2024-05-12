using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiCrud.DTO;
using RestApiCrud.Models;

namespace RestApiCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetEmployees()
        {
            try
            {
                var result = await _context.Employees
            .Select(e => new EmployeeDTO
            {
                Id = e.Id,
                EmployeeName = e.EmployeeName,
                Pin = e.Pin,
                DateOfBirth = e.DateOfBirth,
                IsActive = e.IsActive,
                DepartmentName = e.Department.DepartmentName
            })
            .ToListAsync();
             return StatusCode(200, new { status = true, data = result, message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeDetails(int id)
        {

            try
            {
                var employee = await _context.Employees.FindAsync(id);

                if (employee == null)
                {
                    return StatusCode(404, new { status = false, message = "No employee found", });
                }

                // Fetch department name
                var departmentName = await _context.Departments
                                            .Where(d => d.Id == employee.DepartmentId)
                                            .Select(d => d.DepartmentName)
                                            .FirstOrDefaultAsync();

                var employeeDTO = new EmployeeDTO
                {
                    Id = employee.Id,
                    EmployeeName = employee.EmployeeName,
                    Pin = employee.Pin,
                    DateOfBirth = employee.DateOfBirth,
                    IsActive = employee.IsActive,
                    DepartmentName = departmentName,
                };
                return StatusCode(200, new { status = true, data = employeeDTO, message = "success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }

        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeModel>> UpdateEmployee(int id, EmployeeModel employee)
        {
            try
            {
                var existingEmployee = await _context.Employees.FindAsync(id);

                if (existingEmployee == null)
                {
                    return StatusCode(404, new { status = false, message = "Employee not found" });
                }

                // Check if the updated Pin already exists for another employee
                if (await _context.Employees.AnyAsync(e => e.Pin == employee.Pin && e.Id != id))
                {
                    ModelState.AddModelError("Pin", "Employee pin must be unique.");
                    return BadRequest(ModelState);
                }

                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Pin = employee.Pin;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.IsActive = employee.IsActive;
                existingEmployee.DepartmentId = employee.DepartmentId;

                await _context.SaveChangesAsync();

                return StatusCode(200, new { status = true, message = "Employee updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "An error occurred while updating the employee.", });
            }
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> CreateEmployee(EmployeeModel employeeModel)
        {
            try
            {
                // Check if Pin already exists
                if (await _context.Employees.AnyAsync(e => e.Pin == employeeModel.Pin))
                {
                    ModelState.AddModelError("Pin", "Employee pin must be unique.");
                    return BadRequest(ModelState);
                }
                await _context.Employees.AddAsync(employeeModel);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { status = true, message = "New employee added", });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }
            
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employeeModel = await _context.Employees.FindAsync(id);
                if (employeeModel == null)
                {
                    return StatusCode(404, new { status = false, message = "Employee not found" });
                }


                _context.Employees.Remove(employeeModel);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { status = true, message = "Employee deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = false, message = "Something went wrong", });
            }
        }

       
    }
}
