

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using U_StudyingCommunity_Dream.UserDetails;

namespace U_StudyingCommunity_Dream.EntityMapper.UserDetail_Projects
{
    public class UserDetail_ProjectCfg : IEntityTypeConfiguration<UserDetail_Project>
    {
        public void Configure(EntityTypeBuilder<UserDetail_Project> builder)
        {

            builder.ToTable("UserDetail_Projects", YoYoAbpefCoreConsts.SchemaNames.CMS);

            
			builder.Property(a => a.UserId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.ProjectId).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.TagName).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.IsPublic).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.CreationTime).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);
			builder.Property(a => a.Praise).HasMaxLength(YoYoAbpefCoreConsts.EntityLengthNames.Length64);


        }
    }
}


