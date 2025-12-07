using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Queries;
using SimpleStoryPlatform.Application.Requests;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Queries
{
    public class AdminsGetUserWarningsPageRequestHandler : IRequestHandler<AdminsGetUserWarningsPageRequest, PageResponse<WarningDto>>
    {
        private readonly IUserRepository _userRepo;
        private readonly IWarningRepository _warningRepo;
        private readonly IMapper _mapper;
        public AdminsGetUserWarningsPageRequestHandler(IUserRepository userRepository,
            IWarningRepository warningRepo,
            IMapper mapper)
        {
            _userRepo = userRepository;
            _warningRepo = warningRepo;
            _mapper = mapper;
        }
        public async Task<PageResponse<WarningDto>> Handle(AdminsGetUserWarningsPageRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<WarningDto>();

            int targetUserId = await _userRepo.GetIdByGuid(request.pageReq.UserGuid);

            //null ckeck
            if (targetUserId == 0) { response.Message = "Can'n find your desired user."; return response; }

            var req = new BaseRequest() { PageNumber = request.pageReq.Page , PageSize = 3};

            var repoRes = await _warningRepo.GetPageAsync(req);

            response = _mapper.Map<PageResponse<WarningDto>>(repoRes);
            response.Success = true;

            return response;
        }
    }
}
