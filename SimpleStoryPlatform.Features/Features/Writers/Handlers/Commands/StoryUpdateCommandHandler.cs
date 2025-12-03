using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryUpdateCommandHandler : IRequestHandler<StoryUpdateCommand, BaseResponse>
    {
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public StoryUpdateCommandHandler(IMapper mapper, IStoryRepository storyRepository, IUserRepository userRepo)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
        }
        public async Task<BaseResponse> Handle(StoryUpdateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse();

            var story = await _storyRepo.GetStoryDetails(request.storyDto.PublicId);

            if (story == null)
                response.Message = "داستان موردنظر یافت نشد.";

            else if (story.CreatedBy != request.userGuid)
                response.Message = "شما اجازه ی تغییر این داستان را ندارید!";

            else if (story.IsVisible)
                response.Message = "شما نمیتوانید داستانی که در معرض عموم قرار دارد را ویرایش کنید!";

            else
            {
                story.Name = request.storyDto.Name;
                story.Preview = request.storyDto.Preview;
                story.Data = _mapper.Map<List<StorySection>>(request.storyDto.Data);
                story.IsPublished = request.Publish;

                story = await _storyRepo.UpdateEntityAsync(story);

                response.Success = true;
                response.Message = "داستان شما با موفقیت بروزرسانی شد!";
            }

            return response;
        }
    }
}
