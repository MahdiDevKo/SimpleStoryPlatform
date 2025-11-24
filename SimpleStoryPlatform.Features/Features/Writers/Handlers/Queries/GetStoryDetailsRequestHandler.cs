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
    public class GetStoryDetailsRequestHandler : IRequestHandler<GetStoryDetailsRequest, BaseResponseWithData<StoryDetailsDto>>
    {
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public GetStoryDetailsRequestHandler(IMapper mapper, IStoryRepository storyRepository)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
        }

        public async Task<BaseResponseWithData<StoryDetailsDto>> Handle(GetStoryDetailsRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryDetailsDto>();

            var story = _mapper.Map<StoryDetailsDto>(await _storyRepo.GetStoryDetails(request.storyGuid));
            if (story == null)
                response.Message = "داستان موردنظر یافت نشد.";
            else if (story.CreatedBy == request.userGuid)
            {
                if (!story.IsVisible)
                {
                    response.data = story;
                    response.Success = true;
                }
                else
                    response.Message = "داستان شما منتشر شده. برای ویرایش داستان، لطفا آن را از حالت انتشار خارج کنید.";
            }
            else
                response.Message = "این داستان متعلق به شما نیست!";

            return response;
        }
    }
}
