

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using U_StudyingCommunity_Dream.Articles;

namespace U_StudyingCommunity_Dream.EntityMapper.ArticleCategorys
{
    public class ArticleCategoryCfg : IEntityTypeConfiguration<ArticleCategory>
    {
        public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        {

            builder.ToTable("ArticleCategorys", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.Name).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


