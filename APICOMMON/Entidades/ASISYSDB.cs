using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Threading.Tasks;
using APICOMMON.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace APICOMMON.Entidades
{
    public partial class ASISYSDB : DbContext
    {
        private readonly IConfiguration _configuration;
        public ASISYSDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        #region Tablas

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }

        #endregion Tablas




        #region Vistas

        #endregion Vistas
        
        #region Procedimientos
        
        //public virtual DbSet<management_API_TraerDatosFacturasResult> management_API_TraerDatosFacturas { get; set; }

        #endregion Procedimientos

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string DefaultConnection = _configuration.GetConnectionString("StringConnection");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DefaultConnection, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //modelBuilder.Entity<management_API_TraerDatosFacturasResult>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
