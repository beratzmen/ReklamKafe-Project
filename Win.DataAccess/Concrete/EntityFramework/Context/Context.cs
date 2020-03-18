namespace Win.Entities.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Neighborhood> Neighborhood { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<UserAdressInformation> UserAdressInformation { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserProduct> UserProduct { get; set; }
        public virtual DbSet<Bulletin> Bulletin { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<ProductFavorite> ProductFavorite { get; set; }
        public virtual DbSet<AdsManagement> AdsManagement { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAdressInformation>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
