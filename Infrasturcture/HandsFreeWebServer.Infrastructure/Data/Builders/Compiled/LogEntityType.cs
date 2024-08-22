using HandsFreeWebServer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace HandsFreeWebServer.Infrastructure.Data.Builders.Compiled
{
  public static partial class LogEntityType
  {
    public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
    {
      var runtimeEntityType = model.AddEntityType(
          "HandsFree.Domain.AppService.Entities.Log",
          typeof(Log),
          baseEntityType);

      var macAddress = runtimeEntityType.AddProperty(
          "MacAddress",
          typeof(string),
          maxLength: 50,
          propertyInfo: typeof(Log).GetProperty("MacAddress", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
          fieldInfo: typeof(Log).GetField("_macAddress", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
      macAddress.AddAnnotation("Relational:ColumnName", "mac_address");
      macAddress.AddAnnotation("Relational:ColumnType", "varchar(50)");

      var ipv6Address = runtimeEntityType.AddProperty(
         "Ipv6Address",
         typeof(string),
           maxLength: 50,
         propertyInfo: typeof(Log).GetProperty("Ipv6Address", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
         fieldInfo: typeof(Log).GetField("_ipv6Address", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
      ipv6Address.AddAnnotation("Relational:ColumnName", "ipv6_address");
      ipv6Address.AddAnnotation("Relational:ColumnType", "varchar(50)");

      var updatedTime = runtimeEntityType.AddProperty(
        "UpdatedTime",
        typeof(DateTime),
        propertyInfo: typeof(Log).GetProperty("UpdatedTime", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(Log).GetField("_updatedTime", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
      updatedTime.AddAnnotation("Relational:ColumnName", "updated_time");
      updatedTime.AddAnnotation("Relational:ColumnType", "timestamp");

      var level = runtimeEntityType.AddProperty(
          "Level",
           typeof(string),
           maxLength: 10,
          propertyInfo: typeof(Log).GetProperty("Level", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
          fieldInfo: typeof(Log).GetField("_level", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
      level.AddAnnotation("Relational:ColumnName", "level");
      level.AddAnnotation("Relational:ColumnType", "varchar(10)");

      var logger = runtimeEntityType.AddProperty(
         "Logger",
          typeof(string),
          maxLength: 100,
         propertyInfo: typeof(Log).GetProperty("Logger", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
         fieldInfo: typeof(Log).GetField("_logger", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
      logger.AddAnnotation("Relational:ColumnName", "logger");
      logger.AddAnnotation("Relational:ColumnType", "varchar(100)");

      var message = runtimeEntityType.AddProperty(
        "Message",
         typeof(string),
         maxLength: 1000,
        propertyInfo: typeof(Log).GetProperty("Message", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(Log).GetField("_message", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
      message.AddAnnotation("Relational:ColumnName", "message");
      message.AddAnnotation("Relational:ColumnType", "varchar(1000)");

      return runtimeEntityType;
    }
    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType, string schema)
    {
      string? schemaName = null;

      runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
      runtimeEntityType.AddAnnotation("Relational:Schema", schemaName ?? schema);
      runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
      runtimeEntityType.AddAnnotation("Relational:TableName", "log");
      runtimeEntityType.AddAnnotation("Relational:ViewName", null);
      runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

      Customize(runtimeEntityType, schema);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType, string schema);
  }
}
