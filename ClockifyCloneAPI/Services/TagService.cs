using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Project;
using ClockifyCloneAPI.Models.Tag;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Services;
public class TagService : ITagService
{
    private readonly IMapper _mapper;
    private readonly ClockifyCloneDbContext _context;
    public TagService(IMapper mapper, ClockifyCloneDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<string> Create(PostTagRequest request)
    {
        Tag tag = _mapper.Map<Tag>(request);

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        return "Tag criada com sucesso!";
    }

    public async Task<string> Delete(int id)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Tag não encontrada!");

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return "Tag deletada com sucesso!";
    }

    public async Task<List<GetAllTagResponse>> GetAll()
    {
        var tags = await _context.Tags
            .AsNoTracking()
            .ProjectTo<GetAllTagResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return tags;
    }

    public async Task<GetTagResponse> Get(int id)
    {
        var project = await _context.Tags
            .ProjectTo<GetTagResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException("Tag não encontrada!");

        return project;
    }

    public async Task<string> Update(int id, UpdateTagRequest request)
    {
        var tag = await _context.Tags
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new NotFoundException("Tag não encontrada!");

        _mapper.Map(request, tag);

        await _context.SaveChangesAsync();

        return "Dados da tag alterados com sucesso!";
    }
}

public interface ITagService
{
    public Task<string> Update(int id, UpdateTagRequest request);
    public Task<string> Create(PostTagRequest request);
    public Task<string> Delete(int id);
    public Task<List<GetAllTagResponse>> GetAll();
    Task<GetProjectResponse> Get(int id);
}


