namespace DSCITY.Services;

public sealed class AccountDirectory
{
    private readonly List<PortalUser> _users =
    [
        new()
        {
            FullName = "Lê Thu Hà",
            Phone = "0123456789",
            Password = "123456",
            Role = "Quản trị viên cấp cao"
        }
    ];

    public bool ValidateCredentials(string phone, string password, out PortalUser? user)
    {
        user = _users.FirstOrDefault(x => x.Phone == phone && x.Password == password);
        return user is not null;
    }

    public bool Register(string fullName, string phone, string password, out string? error)
    {
        if (_users.Any(x => x.Phone == phone))
        {
            error = "Số điện thoại đã tồn tại.";
            return false;
        }

        _users.Add(new PortalUser
        {
            FullName = fullName,
            Phone = phone,
            Password = password,
            Role = "Quản trị viên đối tác"
        });

        error = null;
        return true;
    }
}

public sealed class AuthSession
{
    public event Action? OnChange;

    public PortalUser? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;

    public void SignIn(PortalUser user)
    {
        CurrentUser = user;
        OnChange?.Invoke();
    }

    public void SignOut()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}

public sealed class PortalUser
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
