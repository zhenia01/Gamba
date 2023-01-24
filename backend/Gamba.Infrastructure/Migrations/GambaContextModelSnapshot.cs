﻿// <auto-generated />
using System;
using Gamba.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gamba.Infrastructure.Migrations
{
    [DbContext(typeof(GambaContext))]
    partial class GambaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Gamba.DataAccess.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCreator")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("_password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Password");

                    b.HasKey("Id");

                    b.ToTable("Users", "users");
                });

            modelBuilder.Entity("Gamba.DataAccess.Users.UserCreator", b =>
                {
                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("_followerId")
                        .HasColumnType("uuid")
                        .HasColumnName("FollowerId");

                    b.Property<DateTime>("FollowedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CreatorId", "_followerId");

                    b.HasIndex("_followerId");

                    b.ToTable("UserCreators", "users");
                });

            modelBuilder.Entity("Gamba.DataAccess.Users.UserCreator", b =>
                {
                    b.HasOne("Gamba.DataAccess.Users.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gamba.DataAccess.Users.User", null)
                        .WithMany("_followingCreators")
                        .HasForeignKey("_followerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Gamba.DataAccess.Users.User", b =>
                {
                    b.Navigation("_followingCreators");
                });
#pragma warning restore 612, 618
        }
    }
}
