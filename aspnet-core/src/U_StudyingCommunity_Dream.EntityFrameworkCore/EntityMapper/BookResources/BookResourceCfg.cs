

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using U_StudyingCommunity_Dream.Books;

namespace U_StudyingCommunity_Dream.EntityMapper.BookResources
{
    public class BookResourceCfg : IEntityTypeConfiguration<BookResource>
    {
        public void Configure(EntityTypeBuilder<BookResource> builder)
        {

            builder.ToTable("BookResources", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.BookId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Url).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Name).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Uploader).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Auditor).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Status).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


