using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Category;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Services;
public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly ClockifyCloneDbContext _context;
    public CategoryService(IMapper mapper, ClockifyCloneDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<string> Create(PostCategoryRequest request)
    {
        Category category = _mapper.Map<Category>(request);

        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();

        return "Categoria criada com sucesso!";
    }

    public async Task<string> Delete(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Categoria não encontrada!");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return "Categoria deletada com sucesso!";
    }

    public async Task<GetCategoryResponse> Get(int id)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Projects)
            .ProjectTo<GetCategoryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Categoria não encontrada!");

        return category;
    }

    public async Task<List<GetAllCategoryResponse>> GetAll()
    {
        var companies = await _context.Categories
            .AsNoTracking()
            .ProjectTo<GetAllCategoryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return companies;
    }

    public async Task<string> Update(int id, UpdateCategoryRequest request)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Categoria não encontrada!");

        _mapper.Map(request, category);

        await _context.SaveChangesAsync();

        return "Dados da categoria alterados com sucesso!";
    }
}

public interface ICategoryService
{
    public Task<string> Update(int id, UpdateCategoryRequest request);
    public Task<string> Create(PostCategoryRequest request);
    public Task<string> Delete(int id);
    public Task<List<GetAllCategoryResponse>> GetAll();
    public Task<GetCategoryResponse> Get(int id);

}
