﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using ClockifyCloneAPI.Database;
using ClockifyCloneAPI.Entities;
using ClockifyCloneAPI.Exceptions;
using ClockifyCloneAPI.Models.User;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace ClockifyCloneAPI.Services;
public class AuthService : IAuthService
{
    private ClockifyCloneDbContext _context;
    private readonly IMapper _mapper;

    public AuthService(ClockifyCloneDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUserResponse> GetAuthUserInfos(ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated is false || user is null)
            throw new UnauthorizedAccessException();

        var userEmail = user.FindFirst(ClaimTypes.Email)?.Value;

        var userData = await _context.Users.Where(u => u.Email == userEmail)
            .AsNoTracking()
            .Include(u => u.Role)
            .Include(u => u.Company)
            .ProjectTo<GetUserResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync() ?? throw new NotFoundException("Usuário inválido ou não encontrado!");

        return userData;
    }

    public ClaimsPrincipal GetEmailClaimsPrincipal(string email, string password)
    {
        var user = GetValidUser(email, password);
        if (user is not null)
        {
            return new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                        new Claim("Id", user.Id.ToString())
                    },
                    BearerTokenDefaults.AuthenticationScheme
                )
            );
        }
        throw new UnauthorizedAccessException();
    }

    private User? GetValidUser(string email, string password)
    {
        var user = _context.Users
            .Include(u => u.Role)
            .Where(u => u.Email == email)
            .AsEnumerable()
            .FirstOrDefault(u => BCrypt.Net.BCrypt.Verify(password, u.Password));

        return user;
    }

}

public interface IAuthService
{
    public ClaimsPrincipal GetEmailClaimsPrincipal(string email, string password);
    public Task<GetUserResponse> GetAuthUserInfos(ClaimsPrincipal user);
}

