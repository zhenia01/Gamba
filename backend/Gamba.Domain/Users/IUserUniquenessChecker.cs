namespace Gamba.Domain.Users
{
    public interface IUserUniquenessChecker
    {
        bool IsUnique(string userName);
    }
}