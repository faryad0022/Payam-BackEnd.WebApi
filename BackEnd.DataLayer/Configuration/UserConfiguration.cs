using BackEnd.DataLayer.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.DataLayer.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public static string EncodePasswordMd5(string password) //Encrypt using MD5    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasData(
                     new User
                     {
                         Id = 1,
                         CreateDate = DateTime.Now,
                         LastUpdateDate = DateTime.Now,
                         FirstName = "فریاد",
                         LastName = "ابوالحسنی",
                         Address = "کوی فراز - خیابان مینا - پلاک 5 - واحد 17",
                         Email = "mahancomputer49@gmail.com",
                         Password = EncodePasswordMd5("F@ryad1788"),
                         IsActivated = true,
                         IsDelete = false
                     }

                );
        }
    }
}
