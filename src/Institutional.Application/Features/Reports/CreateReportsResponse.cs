namespace Institutional.Application.Features.Reports;

public record CreateReportsResponse
{
    public int GeneratedItems { get; init; }
}