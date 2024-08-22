using HandsFreeWebServer.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace HandsFreeWebServer.Infrastructure.Data.Builders.Compiled;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public static partial class PasswordEntityType
{
  public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
  {
    var runtimeEntityType = model.AddEntityType(
        "HandsFree.Domain.AppService.Entities.Password",
        typeof(Password),
        baseEntityType);

    var userId = runtimeEntityType.AddProperty(
        "UserId",
        typeof(string),
        maxLength: 50,
        propertyInfo: typeof(Password).GetProperty("UserId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(Password).GetField("_userId", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
    userId.AddAnnotation("Relational:ColumnName", "user_id");
    userId.AddAnnotation("Relational:ColumnType", "varchar(50)");

    var phoneNumber = runtimeEntityType.AddProperty(
       "PhoneNumber",
       typeof(string),
         maxLength: 20,
       propertyInfo: typeof(Password).GetProperty("PhoneNumber", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
       fieldInfo: typeof(Password).GetField("_phoneNumber", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
    phoneNumber.AddAnnotation("Relational:ColumnName", "phone_number");
    phoneNumber.AddAnnotation("Relational:ColumnType", "varchar(20)");

    var passwords = runtimeEntityType.AddProperty(
      "Passwords",
      typeof(string),
        maxLength: 500,
      propertyInfo: typeof(Password).GetProperty("Passwords", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
      fieldInfo: typeof(Password).GetField("_passwords", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
    passwords.AddAnnotation("Relational:ColumnName", "passwords");
    passwords.AddAnnotation("Relational:ColumnType", "varchar(500)");

    var endDate = runtimeEntityType.AddProperty(
        "EndDate",
        typeof(DateOnly),
        propertyInfo: typeof(Password).GetProperty("EndDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(Password).GetField("_endDate", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
    endDate.AddAnnotation("Relational:ColumnName", "end_date");
    endDate.AddAnnotation("Relational:ColumnType", "date");

    var lastloginTime = runtimeEntityType.AddProperty(
        "CreatedDate",
        typeof(DateOnly),
        propertyInfo: typeof(Password).GetProperty("CreatedDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(Password).GetField("_createdDate", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
    lastloginTime.AddAnnotation("Relational:ColumnName", "created_date");
    lastloginTime.AddAnnotation("Relational:ColumnType", "date");

    var ix00 = runtimeEntityType.AddKey(
              new[] { userId });
    runtimeEntityType.SetPrimaryKey(ix00);
    ix00.AddAnnotation("Relational:Name", "password_ix00");

    return runtimeEntityType;
  }
  public static void CreateAnnotations(RuntimeEntityType runtimeEntityType, string schema)
  {
    string? schemaName = null;

    runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
    runtimeEntityType.AddAnnotation("Relational:Schema", schemaName ?? schema);
    runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
    runtimeEntityType.AddAnnotation("Relational:TableName", "password");
    runtimeEntityType.AddAnnotation("Relational:ViewName", null);
    runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

    Customize(runtimeEntityType, schema);
  }

  static partial void Customize(RuntimeEntityType runtimeEntityType, string schema);
}
