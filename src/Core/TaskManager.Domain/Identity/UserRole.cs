﻿namespace TaskManager.Domain.Identity;

public class UserRole
{
    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public virtual User User { get; private set; } = null!;
    public virtual Role Role { get; private set; } = null!;
}
