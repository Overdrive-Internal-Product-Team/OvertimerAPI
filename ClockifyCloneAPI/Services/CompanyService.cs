using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Company;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Services;
public class CompanyService : ICompanyService
{
    private ClockifyCloneDbContext _context;
    private IMapper _mapper;
    public CompanyService(ClockifyCloneDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GetCompanyResponse>> GetAllCompanies()
    {
        var companies = await _context.Companies.
            ProjectTo<GetCompanyResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return companies;
    }

    public async Task<GetCompanyResponse> GetCompany(int companyId)
    {
        var company = await _context.Companies
            .ProjectTo<GetCompanyResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == companyId) 
            ?? throw new NotFoundException("Empresa não encontrada!");

        return company;
    }

    public async Task<string> UpdateCompany(int companyId, PutCompanyRequest request)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == companyId)
            ?? throw new NotFoundException("Empresa não encontrada!");

        _mapper.Map(request, company);

        await _context.SaveChangesAsync();

        return "Dados da empresa alterados com sucesso!";
    }
}

public interface ICompanyService
{
    public Task<string> UpdateCompany(int companyId, PutCompanyRequest request);
    public Task<GetCompanyResponse> GetCompany(int companyId);
    public Task<List<GetCompanyResponse>> GetAllCompanies();
}
