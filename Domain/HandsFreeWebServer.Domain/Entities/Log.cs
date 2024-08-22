using HandsFree.Domain.Core.Utils;

namespace HandsFreeWebServer.Domain.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public class Log
{
  private string? _macAddress { get; set; }
  private string? _ipv6Address { get; set; }
  private DateTime _updatedTime { get; set; }
  private string? _level { get; set; }
  private string? _logger { get; set; }
  private string? _message { get; set; }


  public string MacAddress { get => Guard.GetNotNull(this._macAddress, nameof(this.MacAddress)); set => this._macAddress = value; }
  public string Ipv6Address { get => Guard.GetNotNull(this._ipv6Address, nameof(this.Ipv6Address)); set => this._ipv6Address = value; }
  public DateTime UpdatedTime { get => this._updatedTime; set => this._updatedTime = value; }
  public string? Level { get => this._level; set => this._level = value; }
  public string? Logger { get => this._logger; set => this._logger = value; }
  public string? Message { get => this._message; set => this._message = value; }
}
