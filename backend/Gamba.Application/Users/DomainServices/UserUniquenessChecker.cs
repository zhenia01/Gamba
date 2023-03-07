using Gamba.Domain.Users;
using Gamba.Infrastructure.Database;

namespace Gamba.Application.Users.DomainServices;

public class UserUniquenessChecker : IUserUniquenessChecker
{
    private readonly GambaContext _context;

    public UserUniquenessChecker(GambaContext context)
    {
        _context = context;
    }
    
    public bool IsUnique(string userName)
    {
        return !_context.Users.Any(u => u.Name == userName);
    }
}