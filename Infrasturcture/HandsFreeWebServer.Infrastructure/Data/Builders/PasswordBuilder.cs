using HandsFreeWebServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HandsFreeWebServer.Infrastructure.Data.Builders;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public static class PasswordBuilder
{
  public static void AddPassword(this ModelBuilder builder)
  {
    builder.Entity<Password>(entity =>
    {
      entity.HasKey(e => new { e.UserId })
          .HasName("password_ix00");

      entity.ToTable("password");

      entity.HasComment("密码");

      // 用户ID
      entity.Property(e => e.UserId)
          .HasColumnName("user_id")
          .HasComment("用户ID")
          .IsUnicode(false)
          .HasMaxLength(50)
          .HasColumnType("varchar(50)")
          .IsRequired()
          ;
      // 手机号
      entity.Property(e => e.PhoneNumber)
          .HasColumnName("phone_number")
          .HasComment("手机号")
          .HasColumnType("varchar(20)")
          .IsRequired()
          .IsUnicode(false)
          .HasMaxLength(20)
          ;
      // 密码
      entity.Property(e => e.Passwords)
          .HasColumnName("passwords")
          .HasComment("密码")
          .IsRequired()
          .HasColumnType("varchar(500)")
          .IsUnicode(false)
          .HasMaxLength(500)
          ;
      // 创建时间
      entity.Property(e => e.CreatedDate)
          .HasColumnName("created_date")
          .HasComment("创建时间")
          .HasColumnType("date")
          .IsRequired()
          ;
      // 截止时间
      entity.Property(e => e.EndDate)
            .HasColumnName("end_date")
            .HasComment("截止时间")
            .HasColumnType("date")
            .IsRequired()
            ;
    });
  }
}
