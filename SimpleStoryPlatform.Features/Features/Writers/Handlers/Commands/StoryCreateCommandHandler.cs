using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Writers.Handlers.Commands
{
    public class StoryCreateCommandHandler : IRequestHandler<StoryCreateCommand, BaseResponseWithData<Guid>>
    {
        IStoryRepository _storyRepo;
        IUserRepository _userRepo;
        IMapper _mapper;
        public StoryCreateCommandHandler(IMapper mapper, IStoryRepository storyRepository, IUserRepository userRepo)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _userRepo = userRepo;
        }
        public async Task<BaseResponseWithData<Guid>> Handle(StoryCreateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<Guid>();

            var user = await _userRepo.GetByGuidAsync(request.createDto.WriterGuid);

            if (user != null)
            {
                Story story = _mapper.Map<Story>(request.createDto);
                //story.CreatedBy = request.createDto.CreatedBy;
                story.WriterId = user.Id;
                story.Data = new List<StorySection>() { new StorySection() { Narration = "داستانت رو شروع کن!" } };

                story = await _storyRepo.AddAsync(story);

                if (story.Id != 0)
                {
                    response.data = story.PublicId;
                    response.Success = true;
                }
                else
                    response.Message = "در ساخت داستان مشکلی پیش آمده.";

            }
            else
                response.Message = "در اعتبار سنجی حساب کاربری شما، مشکلی پیش آمده.";

            return response;
        }
    }
}
