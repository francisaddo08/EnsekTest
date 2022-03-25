using Microsoft.EntityFrameworkCore;
namespace BackEndApiEF.data
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Domain.entities.Account> Accounts { get; set; }
        public DbSet<Domain.entities.MeterRead> MeterReads { get; set; }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
        public ProjectDbContext() { }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //---------------------------------------Account------------------------
            modelBuilder.Entity<Domain.entities.Account>().ToTable("Test_Accounts");
            modelBuilder.Entity<Domain.entities.Account>().HasKey(p => p.AccountId);
            //------------------ID-----------------------
            modelBuilder.Entity<Domain.entities.Account>().Property(p => p.AccountId)
                .ValueGeneratedNever()
                .IsRequired();
    //        modelBuilder.Entity<Domain.entities.Account>()
    //.HasOne<Domain.entities.MeterRead>(s => s.)
    //.WithMany(g => g.Students)
    //.HasForeignKey(s => s.CurrentGradeId);


            //------------------------FIRST NAME-------------------------
            modelBuilder.Entity<Domain.entities.Account>().Property(p => p.FirstName)
                 .IsRequired();

            //------------------------Last NAME-------------------------
            modelBuilder.Entity<Domain.entities.Account>().Property(p => p.LastName)
                 .IsRequired();
                
            //************************************************************************************************
            //---------------------------------------METER READ------------------------
            modelBuilder.Entity<Domain.entities.MeterRead>().ToTable("Meter_Reading");
            modelBuilder.Entity<Domain.entities.MeterRead>().HasKey(m => m.ID);
            modelBuilder.Entity<Domain.entities.MeterRead>()
              .HasOne<Domain.entities.Account>()
              .WithMany()
              .HasForeignKey(p => p.AccountId);


            //------------------ID-----------------------
            modelBuilder.Entity<Domain.entities.MeterRead>().Property(i => i.ID)
                .ValueGeneratedOnAdd()
                .IsRequired();
            //------------------------Read date-------------------------
            modelBuilder.Entity<Domain.entities.MeterRead>().Property(p => p.MeterReadingDateTime)
                 .IsRequired();
            //------------------------meter read value-------------------------
            modelBuilder.Entity<Domain.entities.MeterRead>().Property(p => p.MeterReadValue);
                
           
               

        }
    }
}
