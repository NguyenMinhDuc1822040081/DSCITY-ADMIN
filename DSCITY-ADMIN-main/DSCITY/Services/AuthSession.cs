using DSCITY.Data;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSCITY.Services;

public sealed class AccountDirectory
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private readonly PasswordHasher<PortalUser> _hasher = new();

    public AccountDirectory(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public bool ValidateCredentials(string phone, string password, out PortalUser? user)
    {
        using var db = _factory.CreateDbContext();
        var stored = db.Users.AsNoTracking().FirstOrDefault(x => x.Phone == phone);
        if (stored is null)
        {
            user = null;
            return false;
        }

        var result = _hasher.VerifyHashedPassword(stored, stored.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
        {
            user = null;
            return false;
        }

        user = stored;
        return true;
    }

    public PortalUser? FindByPhone(string phone)
    {
        using var db = _factory.CreateDbContext();
        return db.Users.AsNoTracking().FirstOrDefault(x => x.Phone == phone);
    }
}

public sealed class AuthSession
{
    private const string StorageKey = "dscity.auth.phone";

    private readonly ProtectedLocalStorage _storage;
    private readonly AccountDirectory _accounts;

    public AuthSession(ProtectedLocalStorage storage, AccountDirectory accounts)
    {
        _storage = storage;
        _accounts = accounts;
    }

    public event Action? OnChange;

    public PortalUser? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser is not null;
    public bool IsInitialized { get; private set; }

    public async Task InitializeAsync()
    {
        if (IsInitialized) return;

        try
        {
            var result = await _storage.GetAsync<string>(StorageKey);
            if (result.Success && !string.IsNullOrWhiteSpace(result.Value))
            {
                var user = _accounts.FindByPhone(result.Value);
                if (user is not null)
                {
                    CurrentUser = user;
                }
            }
        }
        catch
        {
            // ProtectedLocalStorage is unavailable during prerender; ignore and resolve later.
        }

        IsInitialized = true;
        OnChange?.Invoke();
    }

    public void SignIn(PortalUser user)
    {
        CurrentUser = user;
        _ = PersistAsync(user.Phone);
        OnChange?.Invoke();
    }

    public void SignOut()
    {
        CurrentUser = null;
        _ = ClearAsync();
        OnChange?.Invoke();
    }

    private async Task PersistAsync(string phone)
    {
        try { await _storage.SetAsync(StorageKey, phone); } catch { }
    }

    private async Task ClearAsync()
    {
        try { await _storage.DeleteAsync(StorageKey); } catch { }
    }
}

public sealed class PortalUser
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
