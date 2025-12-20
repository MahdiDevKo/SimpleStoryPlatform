using MediatR;
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
    public class UserReadWarningRequestHandler : IRequestHandler<UserReadWarningRequest, BaseResponse>
    {
        private readonly IWarningRepository _warningRepo;
        public UserReadWarningRequestHandler(IWarningRepository warningRepository)
        {
            _warningRepo = warningRepository;
        }
        public async Task<BaseResponse> Handle(UserReadWarningRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();
            var warning = await _warningRepo.GetByGuidAsync(request.warningGuid);

            if (warning == null) { response.Message = "cant find warning! (for set as READED warning)"; return response; }

            warning.IsReaded = true;

            await _warningRepo.UpdateStatesAsync(warning);

            response.Success = true;
            return response;
        }
    }
}
