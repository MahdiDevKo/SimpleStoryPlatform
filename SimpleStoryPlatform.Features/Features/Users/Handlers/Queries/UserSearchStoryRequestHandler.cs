using AutoMapper;
using MediatR;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Users.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Users.Handlers.Queries
{
    public class UserSearchStoryRequestHandler : IRequestHandler<UserSearchStoryRequest, PageResponse<StoryPreviewDto>>
    {
        private readonly ICurrentUserToken _currentUser;
        IStoryRepository _storyRepo;
        IMapper _mapper;
        public UserSearchStoryRequestHandler(IMapper mapper, IStoryRepository storyRepository, ICurrentUserToken currentUser)
        {
            _mapper = mapper;
            _storyRepo = storyRepository;
            _currentUser = currentUser;
        }
        public async Task<PageResponse<StoryPreviewDto>> Handle(UserSearchStoryRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryPreviewDto>();

            IQueryable<Story> query = _storyRepo.GetQueryable();

            query = query.Include(s => s.Writer);

            if (!string.IsNullOrEmpty(request.info.Options.StoryName))
                query = query
                    .Where(s => s.Name
                    .Contains(request.info.Options.StoryName));

            if (!string.IsNullOrEmpty(request.info.Options.WriterUsername))
                query = query
                    .Where(s => s.Writer.Username
                    .Contains(request.info.Options.WriterUsername));

            string role = _currentUser.UserRole;

            if (role != "admin" && role != "owner")
                query.Where(s => s.IsDeleted == false) ;
            

            var pageReponse = await _storyRepo.GetPageAsync(request.info, query);

            response = _mapper.Map<PageResponse<StoryPreviewDto>?>(pageReponse);

            response.Success = true;

            return response;
        }
    }
}
