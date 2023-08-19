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
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //WritingCorrectionPrice
            builder.Entity<WritingCorrectionServicePrice>().HasKey(x => x.ID);
            builder.Entity<WritingCorrectionServicePrice>().Property(x => x.Price).IsRequired();
            builder.Entity<WritingCorrectionServicePrice>().Property(x => x.WordCount).IsRequired().HasDefaultValue(0);
            //WritingCorrectionPrices relation with WritingType
            builder.Entity<WritingCorrectionServicePrice>().HasOne(x => x.WritingType)
                .WithMany(y => y.WritingCorrectionServicePrices).HasForeignKey(x => x.WritingTypeID); ;
            //WritingType
            builder.Entity<ApplicationWritingType>().HasKey(x => x.ID);
            builder.Entity<ApplicationWritingType>().Property(x => x.Name).IsRequired();
            base.OnModelCreating(builder);
        }
    }
}