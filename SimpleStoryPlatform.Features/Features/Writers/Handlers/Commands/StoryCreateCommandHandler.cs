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
    public class StoryCreateCommandHandler : IRequestHandler<StoryCreateCommand, BaseResponseWithData<Guid?>>
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserToken _currentUser;
        private readonly IMapper _mapper;
        public StoryCreateCommandHandler(IMapper mapper, IStoryRepository storyRepository, IUserRepository userRepo, ICurrentUserToken currentUser)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _userRepo = userRepo;
            _currentUser = currentUser;
        }
        public async Task<BaseResponseWithData<Guid?>> Handle(StoryCreateCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseWithData<Guid?>();

            var userId = await _userRepo.GetIdByGuid(_currentUser.UserGuid);

            if (userId == 0) { response.Message = "authorization error - user not found"; return response; }

            var story = _mapper.Map<Story>(request.createDto);

            story.WriterId = userId;
            story.Data = new List<StorySection>() { new StorySection() { Narration = "Let's make an awsome story :D" } };

            story = await _storyRepo.AddAsync(story);

            if (story.Id == 0) { response.Message = "there was a problem in creating a new story..."; return response; }

            response.data = story.PublicId;
            response.Success = true;

            return response;
        }
    }
}
