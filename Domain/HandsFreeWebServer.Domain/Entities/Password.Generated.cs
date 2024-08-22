#nullable enable
using HandsFree.Domain.Core.Utils;

namespace HandsFreeWebServer.Domain.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
[System.Runtime.CompilerServices.CompilerGenerated]
public partial class Password
{
  private string? _userId { get; set; }

  private string? _phoneNumber { get; set; }

  private string? _passwords { get; set; }

  private DateOnly _endDate { get; set; }

  private DateOnly _createdDate { get; set; }

  public string UserId { get => Guard.GetNotNull(_userId, nameof(UserId)); set => _userId = value; }

  public string PhoneNumber { get => Guard.GetNotNull(_phoneNumber, nameof(PhoneNumber)); set => _phoneNumber = value; }

  public string Passwords { get => Guard.GetNotNull(_passwords, nameof(Password)); set => _passwords = value; }

  public DateOnly EndDate { get => this._endDate; set => this._endDate = value; }

  public DateOnly CreatedDate { get => this._createdDate; set => this._createdDate = value; }
}
