﻿using BackEnd.DataLayer.Entities.Access;
using BackEnd.DataLayer.Entities.Account;
using BackEnd.DataLayer.Entities.Gallery;
using BackEnd.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BackEnd.DataLayer.Context
{
    public class BackEndDbContext: DbContext
    {

        #region Construcor
        public BackEndDbContext(DbContextOptions<BackEndDbContext> options): base(options)
        {

        }
        #endregion


        #region disable cascading delete in data base
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var item in cascades)
            {
                item.DeleteBehavior = DeleteBehavior.Restrict;

            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<ContactAddress> ContactAddresses { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<ImageGallery> imageGalleries { get; set; }
        public DbSet<ImageComment> ImageComments { get; set; }









        #endregion
    }
}
