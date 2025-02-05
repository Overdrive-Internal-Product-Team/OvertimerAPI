﻿using ClockifyCloneAPI.Entities;

namespace ClockifyCloneAPI.Models.User;
public class GetUserResponse : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public Role Role { get; set; }
    public Entities.Company Company { get; set; }
}
