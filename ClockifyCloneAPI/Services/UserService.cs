using AutoMapper;
using BCrypt.Net;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Models.User;

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
        var user = _mapper.Map<UserEntity>(userModel);
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        user.active = true;

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return "User successfully created!";
    }
}

public interface IUserService
{
    public Task<string> Create(PostUserRequest userModel);
}
