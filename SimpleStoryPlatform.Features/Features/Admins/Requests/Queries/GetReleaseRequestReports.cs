using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Requests.Queries
{
    public class GetReleaseRequestsRequest : IRequest<PageResponse<StoryReleaseRequestDetailsDto>>
    {
        public BaseRequest pageReq { get; set; } = new BaseRequest();
    }
}
