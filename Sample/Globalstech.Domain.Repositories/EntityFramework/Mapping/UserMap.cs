using System.Data.Entity.ModelConfiguration;
using Globalstech.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Globalstech.Domain.Repositories.EntityFramework.Mapping
{
    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.UserName)
                .IsRequired()
                .HasMaxLength(20);
            Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(20);
            Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(80);

            ToTable("Users");
        }
    }
}
