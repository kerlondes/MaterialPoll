﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Учебаня_практика
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDEntities : DbContext
    {
        public BDEntities()
            : base("name=BDEntities")
        {
        }
        private static BDEntities m_instance;
        public static BDEntities GetInstance()
        {
            if (m_instance == null) m_instance = new BDEntities();
            return m_instance;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AuthHistory> AuthHistories { get; set; }
        public virtual DbSet<Material> Materials { get; set; }
        public virtual DbSet<MaterialsInStore> MaterialsInStores { get; set; }
        public virtual DbSet<MaterialsInSupply> MaterialsInSupplies { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PartnerType> PartnerTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsInOrder> ProductsInOrders { get; set; }
        public virtual DbSet<ProductsInSale> ProductsInSales { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<RequiredMaterial> RequiredMaterials { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SalePlace> SalePlaces { get; set; }
        public virtual DbSet<SalePlaceType> SalePlaceTypes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Supply> Supplies { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
