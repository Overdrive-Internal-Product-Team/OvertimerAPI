using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.Work;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClockifyCloneAPI.Services
{
    public class WorkService : IWorkService
    {
        private readonly IMapper _mapper;
        private readonly ClockifyCloneDbContext _context;

        public WorkService(IMapper mapper, ClockifyCloneDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> Create(PostWorkRequest request)
        {
            Work work = _mapper.Map<Work>(request);

            var tagIds = request.TagIds.Distinct();

            var tags = await _context.Tags
                .Where(x => tagIds.Contains(x.Id))
                .ToListAsync();

            work.Tags.AddRange(tags);

            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();

            return "Registro de trabalho criado com sucesso!";
        }

        public async Task<string> Delete(int id)
        {
            var work = await _context.Works
                .FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new NotFoundException("Registro de trabalho não encontrado!");

            _context.Works.Remove(work);
            await _context.SaveChangesAsync();

            return "Registro de trabalho removido com sucesso!";
        }

        public async Task<GetWorkResponse> Get(int id)
        {
            var work = await _context.Works
                .ProjectTo<GetWorkResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new NotFoundException("Registro de trabalho não encontrado!");

            return work;
        }

        public async Task<List<GetAllWorkResponse>> GetAll(ClaimsPrincipal user)
        {
            String? userIdClaim = user.FindFirst("Id")?.Value 
                ?? throw new Exception("Claim inválida ou vazia!");

            int userId = Convert.ToInt32(userIdClaim);

            var works = await _context.Works
                .AsNoTracking()
                .Where(u => u.UserId == userId)
                .ProjectTo<GetAllWorkResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return works;
        }

        public async Task<string> Update(int id, UpdateWorkRequest request)
        {
            var work = await _context.Works
                .Include(w => w.Tags)
                .FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new NotFoundException("Registro de trabalho não encontrado!");

            _mapper.Map(request, work);

            work.Tags.Clear();

            var tagIds = request.TagIds?.Distinct();
            if (tagIds is not null)
            {
                var tags = await _context.Tags
                        .Where(x => tagIds.Contains(x.Id))
                        .ToListAsync();

                work.Tags.AddRange(tags);
            }


            await _context.SaveChangesAsync();

            return "Registro de trabalho atualizado com sucesso!";
        }

    }

    public interface IWorkService
    {
        Task<string> Update(int id, UpdateWorkRequest request);
        Task<string> Create(PostWorkRequest request);
        Task<string> Delete(int id);
        Task<List<GetAllWorkResponse>> GetAll(ClaimsPrincipal user);
        Task<GetWorkResponse> Get(int id);
    }
}
