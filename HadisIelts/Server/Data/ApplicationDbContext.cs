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
        public DbSet<SubmittedWritingCorrectionFiles> SubmittedWritingCorrectionFiles { get; set; }
        public DbSet<WritingPaymentPicture> WritingPaymentPictures { get; set; }
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
            builder.Entity<SubmittedWritingCorrectionFiles>().HasKey(x => x.ID);
            builder.Entity<SubmittedWritingCorrectionFiles>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<SubmittedWritingCorrectionFiles>().HasMany(x => x.WritingCorrectionFiles)
                .WithOne(x => x.SubmittedWritingCorrectionFiles).HasForeignKey(x => x.SubmittedWritingCorecionFilesID);
            //WritingPaymentPicture
            builder.Entity<WritingPaymentPicture>().HasKey(x => x.ID);
            builder.Entity<WritingPaymentPicture>().Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Entity<WritingPaymentPicture>().HasOne(x => x.SubmittedWritingCorrectionFiles)
                .WithMany(y => y.PaymentPictures).HasForeignKey(x => x.SubmitedWritingCorrectionFilesID);
            base.OnModelCreating(builder);
        }
    }
}