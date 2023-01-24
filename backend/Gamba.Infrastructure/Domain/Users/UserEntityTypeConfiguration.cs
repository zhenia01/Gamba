using Gamba.DataAccess.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gamba.Infrastructure.Domain.Users
{
    internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        internal const string FollowingCreatorsList = "_followingCreators";

        public void Configure(EntityTypeBuilder<User> user)
        {
            user.ToTable("Users", "users");

            // user.Navigation(FollowingCreatorsList);

            user.Ignore(u => u.FollowingCreators);
            
            user.HasKey(b => b.Id);

            user.Property("_password").HasColumnName("Password");

            // user.HasMany<UserCreator>(FollowingCreatorsList)
            //     .WithOne(uc => uc.Follower)
            //     .HasForeignKey("Follower");
        }
    }
}