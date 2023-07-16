using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Urbamais.Identity.Config;

namespace Urbamais.Identity.Services;

public class RoleService //: IRoleAppService//, IRoleStore<IdentityRole>
{
    private readonly ContextIdentity _dbContext;

    public RoleService(ContextIdentity dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Add(role);
        await Commit(cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        _dbContext.Entry(role).State = EntityState.Modified;
        await Commit(cancellationToken);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Remove(role);
        await Commit(cancellationToken);
        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Id);
    }

    public Task<string?> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.Name)!;
    }

    public Task SetRoleNameAsync(IdentityRole role, string? roleName, CancellationToken cancellationToken)
    {
        role.Name = roleName;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(IdentityRole role, string? normalizedName, CancellationToken cancellationToken)
    {
        role.NormalizedName = normalizedName;
        return Task.CompletedTask;
    }

    public async Task<IdentityRole?> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        return (await _dbContext.Roles.FindAsync(new object[] { roleId }, cancellationToken))!;
    }

    public async Task<IdentityRole?> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return (await _dbContext.Roles.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken))!;
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await Commit(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}