using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamba.Infrastructure.Domain.Users
{
    internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        internal const string FollowingCreatorsList = "_followingCreators";
        internal const string CreatorTagsList = "_creatorTags";
        internal const string FavoriteTagsList = "_favoriteTags";

        public void Configure(EntityTypeBuilder<User> user)
        {
            user.ToTable("Users", "users");

            user.Ignore(u => u.FollowingCreators);
            user.Ignore(u => u.CreatorTags);
            user.Ignore(u => u.FavoriteTags);

            user.HasKey(u => u.Id);
            
            ConfigureTags(user);

            user.Property("_password").HasColumnName("Password");
        }

        private void ConfigureTags(EntityTypeBuilder<User> user)
        {
            var valueComparer = new ValueComparer<List<Tag>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());

            user.Property<List<Tag>>(CreatorTagsList)
                .HasPostgresArrayConversion(t => t.Name, name => new Tag(name))
                .Metadata
                .SetValueComparer(valueComparer);
            user.Property<List<Tag>>(FavoriteTagsList)
                .HasPostgresArrayConversion(t => t.Name, name => new Tag(name))
                .HasDefaultValue(new List<Tag>())
                .Metadata
                .SetValueComparer(valueComparer);
        }
    }
}