﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.SqlServer;

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SalesDBEntities : DbContext
    {
        public SalesDBEntities()
            : base("name=SalesDBEntities")
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance; 
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<FileInfo> FileInfo { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SaleInfo> SaleInfo { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
