using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
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
    public class StoryPlayListGetDetailRequestHandler : IRequestHandler<StoryPlayListGetDetailRequest, BaseResponseWithData<StoryPlayListDto>>
    {
        private readonly IStoryPlayListRepository _playListRepo;
        private readonly IMapper _mapper;
        public StoryPlayListGetDetailRequestHandler(IMapper mapper, IStoryPlayListRepository storyPlayListRepository)
        {
            _mapper = mapper;
            _playListRepo = storyPlayListRepository;
        }
        public async Task<BaseResponseWithData<StoryPlayListDto>> Handle(StoryPlayListGetDetailRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<StoryPlayListDto>();

            var playlist = await _playListRepo.GetByGuidAsync(request.reqDto.PlayListGuid);

            if (playlist == null) { response.Message = "play list not found."; return response; }

            var pageRes = await _playListRepo.GetPageAsync(playlist.Id, request.reqDto);

            //setting information
            response.data = _mapper.Map<StoryPlayListDto>(playlist);

            response.data.StoriesInPage = _mapper.Map<PageResponse<StoryPreviewDto>>(pageRes);

            response.Success = true;

            return response;
}
    }
}
