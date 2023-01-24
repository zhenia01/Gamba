namespace Gamba.DataAccess.Users
{
    public interface IUserUniquenessChecker
    {
        bool IsUnique(string userName);
    }
}