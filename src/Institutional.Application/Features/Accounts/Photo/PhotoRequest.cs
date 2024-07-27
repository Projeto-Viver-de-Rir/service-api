using Ardalis.Result;
using Institutional.Application.Common.Requests;
using MediatR;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace Institutional.Application.Features.Accounts.Photo;

public record PhotoRequest : IRequest<Result<string>>
{
    public string FileName { get; init; }
    public string FileExtension { get; init; }
    public MemoryStream Input { get; set; }
    
    [JsonIgnore]
    public AuditData? AuditFields { get; init; }
}