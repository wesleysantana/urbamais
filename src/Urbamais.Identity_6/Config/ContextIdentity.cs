using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Identity.Config;

public class ContextIdentity : IdentityDbContext
{
    public ContextIdentity(DbContextOptions<ContextIdentity> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}