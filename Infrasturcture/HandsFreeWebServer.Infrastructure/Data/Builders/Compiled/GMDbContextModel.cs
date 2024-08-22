using HandsFree.Infrastructure.Core.Data.Builders.Compiled;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HandsFreeWebServer.Infrastructure.Data.Builders.Compiled;

[DbContext(typeof(HandsFreeServiceDbContext))]
[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class GMDbContextModel : HandsFreeDbContextModel
{
  static GMDbContextModel()
  {
    var model = new GMDbContextModel();
    var sequences = new SortedDictionary<(string, string), ISequence>();
    model.Initialize(Schema, sequences);
    model.AddAnnotations(Schema, sequences);
    Instance = model;
  }

  private static string Schema = "app";

  public static new IModel Instance { get; private set; }

  protected override void Initialize(string schema, SortedDictionary<(string, string), ISequence> sequences)
  {
    base.Initialize(schema, sequences);

    var password = PasswordEntityType.Create(this);
    var log = LogEntityType.Create(this);
    PasswordEntityType.CreateAnnotations(password, schema);
    LogEntityType.CreateAnnotations(log, schema);


    //sequences[("TABALE_A", schema)] = new RuntimeSequence(
    //    "TABALE_A",
    //    this,
    //    typeof(long),
    //    schema: schema,
    //    cyclic: true,
    //    minValue: 1L,
    //    maxValue: 9999999999L);

    Customize(Schema, sequences);
  }

  partial void Customize(string schema, SortedDictionary<(string, string), ISequence> sequences);
}

