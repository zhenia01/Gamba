using Gamba.DataAccess.BuildingBlocks;

namespace Gamba.DataAccess.Users.Rules
{
    public class UserNameMustBeUniqueRule : IBusinessRule
    {
        private readonly IUserUniquenessChecker _userUniquenessChecker;

        private readonly string _name;

        public UserNameMustBeUniqueRule(
            IUserUniquenessChecker userUniquenessChecker, 
            string name)
        {
            _userUniquenessChecker = userUniquenessChecker;
            _name = name;
        }

        public bool IsBroken => !_userUniquenessChecker.IsUnique(_name);

        public string Message => $"User {_name} already exists";
    }
}