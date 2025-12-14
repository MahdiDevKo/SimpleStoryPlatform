using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserGetWarningsRequestHandler : IRequestHandler<UserGetWarningsRequest, PageResponse<WarningDto>>
    {
        private readonly ICurrentUserToken _currentUser;
        private readonly IWarningRepository _warningRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserGetWarningsRequestHandler(
            ICurrentUserToken currentUserToken,
            IWarningRepository warningRepo,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _currentUser = currentUserToken;
            _warningRepo = warningRepo;
            _userRepo = userRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<WarningDto>> Handle(UserGetWarningsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<WarningDto>();
            int userId = await _userRepo.GetIdByGuid(_currentUser.UserGuid);

            if (userId == 0) { response.Message = "there was a problem with your identity"; return response; }

            var query = _warningRepo.GetQueryable()
                .Where(w => w.UserId == userId);

            var repoRes = await _warningRepo.GetPageAsync(request.req, query);

            response = _mapper.Map<PageResponse<WarningDto>>(repoRes);
            response.Success = true;

            return response;
        }
    }
}
