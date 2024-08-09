using Ardalis.Result;
using Institutional.Domain.Entities.Common;
using MediatR;

namespace Institutional.Application.Features.Reports.Debts.DeleteItemDebtReport;

public record DeleteItemDebtReportRequest(ReportDebtId Id) : IRequest<Result>;