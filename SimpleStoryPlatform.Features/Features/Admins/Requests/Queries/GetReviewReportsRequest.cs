using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.ViewModels.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Requests.Queries
{
    public class GetReviewReportsRequest : IRequest<PageResponse<ReviewReportVM>>
    {
        public BaseRequest pageReq { get; set; } = new BaseRequest();
    }
}
