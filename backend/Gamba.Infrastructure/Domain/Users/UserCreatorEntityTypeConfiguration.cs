using Gamba.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamba.Infrastructure.Domain.Users;

internal class UserCreatorEntityTypeConfiguration : IEntityTypeConfiguration<UserCreator>
{
    public void Configure(EntityTypeBuilder<UserCreator> userCreator)
    {
        userCreator.ToTable("UserCreators", "users");
        
        userCreator.Property<UserId>("_followerId").HasColumnName("FollowerId");
        userCreator.Property<UserId>("CreatorId");
        userCreator.Property(uc => uc.FollowedAt);

        userCreator.HasKey("CreatorId", "_followerId");
        
        userCreator.HasOne<User>()
            .WithMany(UserEntityTypeConfiguration.FollowingCreatorsList)
            .HasForeignKey("_followerId");
        
        userCreator.HasOne<User>(uc => uc.Creator)
            .WithMany()
            .HasForeignKey("CreatorId");
        
        userCreator.Navigation(uc => uc.Creator).AutoInclude();
    }
}