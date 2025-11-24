using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Queries
{
    public class GetWritedStoriesRequestHandler : IRequestHandler<GetWritedStoriesRequest, BaseResponseWithData<List<StoryPreviewDto>?>>
    {
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public GetWritedStoriesRequestHandler(IMapper mapper, IStoryRepository storyRepository)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponseWithData<List<StoryPreviewDto>?>> Handle(GetWritedStoriesRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<List<StoryPreviewDto>?>();

            if (request.UserGuid != null)
            {
                var result = await _storyRepo.GetWritedStories(request.UserGuid);

                if(result.Any())
                    response.data = _mapper.Map<List<StoryPreviewDto>>(result);

                response.Success = true;
            }
            else
                response.Message = "مشکلی در اعتبار سنجی حساب کاربری شما پیش اومده.";

            return response;
        }
    }
}
