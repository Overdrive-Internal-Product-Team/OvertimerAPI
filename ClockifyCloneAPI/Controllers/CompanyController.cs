using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Services;
using ClockifyCloneAPI.Models.Company;

namespace ClockifyCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ClockifyCloneDbContext _context;
        private ICompanyService _companyService;

        public CompanyController(ClockifyCloneDbContext context, ICompanyService companyService)
        {
            _context = context;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            try
            {
                var companies = await _companyService.GetAll();
                return Ok(companies);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            try
            {
                var company = await _companyService.Get(id);
                return Ok(company);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Company/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyRequest request)
        {
            try
            {
                var message = await _companyService.Update(id, request);
                return Ok(message);
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
