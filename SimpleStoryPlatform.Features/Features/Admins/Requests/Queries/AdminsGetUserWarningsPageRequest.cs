using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using SimpleStoryPlatform.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Requests.Queries
{
    public class AdminsGetUserWarningsPageRequest : IRequest<PageResponse<WarningDto>>
    {
        public WarningPageRequestDto pageReq { get; set; }
    }
}
