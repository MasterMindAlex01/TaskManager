using TaskManager.Domain.Common.Contracts;
using TaskManager.Domain.Tasks;
using TaskManager.Domain.Tools;

namespace TaskManager.Domain.Identity;

public class User : BaseEntity, IAggregateRoot
{
    private readonly List<UserRole> _roles = [];
    private readonly List<Comment> _comments = [];
    private readonly List<StatusHistory> _statusHistories = [];

    internal User(
        Guid id,
        string username,
        string firstname,
        string lastname,
        string password,
        string passwordSalt,
        string email,
        bool enabled,
        bool isDeleted) : base(id)
    {
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
        Password = password;
        PasswordSalt = passwordSalt;
        Email = email;
        Enabled = enabled;
        IsDeleted = isDeleted;
    }

    public string Username { get; private set; } = null!;
    public string Firstname { get; private set; } = null!;
    public string Lastname { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public string PasswordSalt { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool Enabled { get; private set; }
    public bool IsDeleted { get; private set; }

    public virtual IReadOnlyCollection<UserRole> Roles => _roles;
    public virtual IReadOnlyCollection<Comment> Comments => _comments;
    public virtual IReadOnlyCollection<StatusHistory> StatusHistories => _statusHistories;

    public static User Create(
        Guid id,
        string username,
        string firstname,
        string lastname,
        string email,
        string password,
        bool enabled,
        bool isDeleted,
        string pepper,
        int iteration)
    {
        string passwordSalt = PasswordHasher.GenerateSalt();

        password = PasswordHasher.ComputeHash(
            password.Trim(), passwordSalt, pepper, iteration);

        return new User(
            id,
            username.Trim(),
            firstname.Trim(),
            lastname.Trim(),
            password,
            passwordSalt,
            email,
            enabled,
            isDeleted);
    }

    public void Update(
        string username,
        string firstname,
        string lastname,
        string email,
        bool enabled,
        bool isDeleted)
    {
        Username = username;
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        Enabled = enabled;
        IsDeleted = isDeleted;
    }

    public void AddRole(Guid roleId)
    {
        _roles.Add(new UserRole(Id, roleId));
    }

    public void AddRoleRange(IEnumerable<Guid> roleIdList)
    {
        foreach (var roleId in roleIdList)
        {
            _roles.Add(new UserRole(Id, roleId));
        }
    }

    public void RemoveRole(Guid roleId)
    {
        var role = Roles.FirstOrDefault(x => x.RoleId == roleId);
        if (role != null) { _roles.Remove(role); }
    }

    public void RemoveRoleRange(IEnumerable<Guid> roleIdList)
    {

        foreach (var role in roleIdList)
        {
            var currentRole = Roles.FirstOrDefault(x => x.RoleId == role);
            if (currentRole != null)
            {
                _roles.Remove(currentRole);
            }
        }
    }

    public void Delete()
    {
        IsDeleted = true;
        Enabled = false;
        Username = $"{Id}_DELETED_{Username}";
        _roles.Clear();
    }
}
