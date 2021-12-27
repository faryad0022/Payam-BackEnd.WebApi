using BackEnd.DataLayer.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataLayer.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasData
            (
                new UserRole
                {
                    Id = 1,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    RoleId = 1,
                    UserId = 1
                },
                new UserRole
                {
                    Id = 2,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    RoleId = 2,
                    UserId = 1
                },
                new UserRole
                {
                    Id = 3,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    RoleId = 3,
                    UserId = 1
                },
                new UserRole
                {
                    Id = 4,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    RoleId = 4,
                    UserId = 1
                },
                new UserRole
                {
                    Id = 5,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    RoleId = 5,
                    UserId = 1
                },
                new UserRole
                {
                    Id = 6,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    RoleId = 6,
                    UserId = 1
                }
            );
        }
    }
}
