using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Project;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Services;
public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly ClockifyCloneDbContext _context;

    public ProjectService(IMapper mapper, ClockifyCloneDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Create(PostProjectRequest request)
    {
        Project project = _mapper.Map<Project>(request);

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return "Projeto criado com sucesso!";
    }

    public async Task<string> Delete(int id)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException("Projeto não encontrado!");

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        return "Projeto removido com sucesso!";
    }

    public async Task<GetProjectResponse> Get(int id)
    {
        var project = await _context.Projects
            .ProjectTo<GetProjectResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException("Projeto não encontrado!");

        return project;
    }

    public async Task<List<GetAllProjectResponse>> GetAll()
    {
        var projects = await _context.Projects
            .AsNoTracking()
            .ProjectTo<GetAllProjectResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return projects;
    }

    public async Task<string> Update(int id, UpdateProjectRequest request)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException("Projeto não encontrado!");

        _mapper.Map(request, project);

        await _context.SaveChangesAsync();

        return "Projeto atualizado com sucesso!";
    }
}

public interface IProjectService
{
    Task<string> Update(int id, UpdateProjectRequest request);
    Task<string> Create(PostProjectRequest request);
    Task<string> Delete(int id);
    Task<List<GetAllProjectResponse>> GetAll();
    Task<GetProjectResponse> Get(int id);
}

