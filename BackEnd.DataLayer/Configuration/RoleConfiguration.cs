using BackEnd.DataLayer.Entities.Access;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataLayer.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        //Seed Role Data to Table
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasData(
                    new Role
                    {
                        Id = 1,
                        CreateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "Admin",
                        Title = "ادمین سایت",
                        IsDelete = false

                    },
                    new Role
                    {
                        Id = 2,
                        CreateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "SuperAdmin",
                        Title = "سوپر ادمین",
                        IsDelete = false
                    },
                    new Role
                    {
                        Id = 3,
                        CreateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "Secreter",
                        Title = "منشی",
                        IsDelete = false
                    },
                    new Role
                    {
                        Id = 4,
                        CreateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "Blogger",
                        Title = "بلاگر",
                        IsDelete = false
                    },
                    new Role
                    {
                        Id = 5,
                        CreateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "Advertiser",
                        Title = "تبلیغاتی",
                        IsDelete = false
                    },
                    new Role
                    {
                        Id = 6,
                        CreateDate = DateTime.Now,
                        LastUpdateDate = DateTime.Now,
                        Name = "User",
                        Title = "کاربر معمولی",
                        IsDelete = false
                    }
                );
        }
    }
}
