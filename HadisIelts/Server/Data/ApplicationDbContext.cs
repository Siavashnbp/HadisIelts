using Duende.IdentityServer.EntityFramework.Options;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HadisIelts.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<WritingCorrectionServicePrice> WritingCorrectionServicePrices { get; set; }
        public DbSet<ApplicationWritingType> WritingTypes { get; set; }
        public DbSet<WritingCorrectionFile> WritingCorrectionFiles { get; set; }
        public DbSet<WritingCorrectionSubmissionGroup> WritingCorrectionSubmissionGroups { get; set; }
        public DbSet<PaymentPicture> PaymentPictures { get; set; }
        public DbSet<PaymentGroup> PaymentGroups { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<CorrectedWritingFile> CorrectedWritingFiles { get; set; }
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //WritingCorrectionPrice
            builder.Entity<WritingCorrectionServicePrice>().HasKey(x => x.ID);
            builder.Entity<WritingCorrectionServicePrice>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<WritingCorrectionServicePrice>().Property(x => x.Price).IsRequired();
            builder.Entity<WritingCorrectionServicePrice>().Property(x => x.WordCount).IsRequired().HasDefaultValue(0);
            //WritingCorrectionPrices relation with WritingType
            builder.Entity<WritingCorrectionServicePrice>().HasOne(x => x.WritingType)
                .WithMany(y => y.WritingCorrectionServicePrices).HasForeignKey(x => x.WritingTypeID); ;
            //WritingType
            builder.Entity<ApplicationWritingType>().HasKey(x => x.ID);
            builder.Entity<ApplicationWritingType>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<ApplicationWritingType>().Property(x => x.Name).IsRequired();
            //WritingCorrectionFile
            builder.Entity<WritingCorrectionFile>().HasKey(x => x.ID);
            builder.Entity<WritingCorrectionFile>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<WritingCorrectionFile>().HasOne(x => x.ApplicationWritingType)
                .WithMany(x => x.WritingCorrectionFiles).HasForeignKey(x => x.ApplicationWritingTypeID);
            //SubmittedWritingCorrectionFiles
            builder.Entity<WritingCorrectionSubmissionGroup>().HasKey(x => x.ID);
            builder.Entity<WritingCorrectionSubmissionGroup>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<WritingCorrectionSubmissionGroup>().HasMany(x => x.WritingCorrectionFiles)
                .WithOne(x => x.WritingCorrectionSubmissionGroup).HasForeignKey(x => x.WritingCorrectionSubmissionGroupID);
            builder.Entity<WritingCorrectionSubmissionGroup>().Property(x => x.PaymentGroupID).IsRequired(false);
            //PaymentPicture
            builder.Entity<PaymentPicture>().HasKey(x => x.ID);
            builder.Entity<PaymentPicture>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<PaymentPicture>().HasOne(x => x.PaymentGroup)
                .WithMany(x => x.PaymentPictures).HasForeignKey(x => x.PaymentGroupID);
            //PaymentGroup
            builder.Entity<PaymentGroup>().HasKey(x => x.ID);
            builder.Entity<PaymentGroup>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<PaymentGroup>().HasOne(x => x.Service)
                .WithMany(x => x.PaymentGroups).HasForeignKey(x => x.ServiceID);
            //Service
            builder.Entity<Service>().HasKey(x => x.ID);
            builder.Entity<Service>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<Service>().Property(x => x.Name).IsRequired();
            builder.Entity<Service>().Property(x => x.Description).IsRequired(false);
            //CorrectedWritingFile
            builder.Entity<CorrectedWritingFile>().HasKey(x => x.ID);
            builder.Entity<CorrectedWritingFile>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<CorrectedWritingFile>().HasOne(x => x.WritingCorrectionSubmissionGroup)
                .WithMany(x => x.CorrectedWritingFiles).HasForeignKey(x => x.WritingCorrectionSubmissionGroupID);
            base.OnModelCreating(builder);
        }
    }
}