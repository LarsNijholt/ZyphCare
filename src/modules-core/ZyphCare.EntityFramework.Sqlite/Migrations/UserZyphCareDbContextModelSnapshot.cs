﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZyphCare.EntityFramework.Units.Users;

#nullable disable

namespace ZyphCare.EntityFramework.Sqlite.Migrations
{
    [DbContext(typeof(UserZyphCareDbContext))]
    partial class UserZyphCareDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("ZyphCare.Users.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Auth0Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Auth0Id")
                        .IsUnique()
                        .HasDatabaseName("IX_User_Auth0Id");

                    b.HasIndex("Role")
                        .HasDatabaseName("IX_User_Role");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
