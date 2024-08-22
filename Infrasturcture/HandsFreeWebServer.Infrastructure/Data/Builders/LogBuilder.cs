using HandsFreeWebServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HandsFreeWebServer.Infrastructure.Data.Builders
{
  public static class LogBuilder
  {
    public static void AddLog(this ModelBuilder builder)
    {
      builder.Entity<Log>(entity =>
      {
        entity.HasNoKey();

        entity.ToTable("log");

        entity.HasComment("日志");
        // Mac地址
        entity.Property(e => e.MacAddress)
            .HasColumnName("mac_address")
            .HasComment("Mac地址")
            .IsUnicode(false)
            .HasMaxLength(50)
            .HasColumnType("varchar(50)")
            ;
        // Ipv6地址
        entity.Property(e => e.Ipv6Address)
            .HasColumnName("ipv6_address")
            .HasComment("Ipv6地址")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .HasMaxLength(50)
            ;
        // 更新时间
        entity.Property(e => e.UpdatedTime)
            .HasColumnName("updated_time")
            .HasComment("更新时间")
            .HasColumnType("timestamp")
            ;

        // 日志等级
        entity.Property(e => e.Level)
            .HasColumnName("level")
            .HasComment("日志等级")
            .HasColumnType("varchar(10)")
            .IsUnicode(false)
            .HasMaxLength(10);

        // 日志器
        entity.Property(e => e.Logger)
           .HasColumnName("logger")
           .HasComment("日志器")
           .HasColumnType("varchar(100)")
           .IsUnicode(false)
           .HasMaxLength(100);

        // 日志内容
        entity.Property(e => e.Message)
           .HasColumnName("message")
           .HasComment("日志内容")
           .HasColumnType("varchar(1000)")
           .IsUnicode(false)
           .HasMaxLength(1000);
      });
    }
  }
}
