using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Services;
public class UserService : IUserService
{
    readonly ClockifyCloneDbContext _context;
    private IMapper _mapper;
    public UserService(ClockifyCloneDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Create(PostUserRequest userModel)
    {
        var user = _mapper.Map<User>(userModel);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.Active = true;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return "User successfully created!";
    }

    public async Task<GetUserResponse> Get(int id)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .Include(x => x.Company)
            .ProjectTo<GetUserResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(u => u.Id == id) ?? throw new NotFoundException("Usuário não encontrado!");

        return user;
    }

    public async Task<List<GetUserResponse>> GetAll()
    {
        var users = await _context.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .ProjectTo<GetUserResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return users;
    }

    public async Task<string> Update(int id, UpdateUserRequest request)
    {
        var user = await _context.Users
            .Include(u => u.Company)
            .Include(u => u.Role)
           .FirstOrDefaultAsync(c => c.Id == id)
           ?? throw new NotFoundException("Usuário não encontrado!");

        _mapper.Map(request, user);

        await _context.SaveChangesAsync();

        return "Dados da empresa alterados com sucesso!";
    }

}

public interface IUserService
{
    public Task<string> Create(PostUserRequest userModel);
    public Task<string> Update(int id, UpdateUserRequest request);
    public Task<List<GetUserResponse>> GetAll();
    public Task<GetUserResponse> Get(int id);
}
