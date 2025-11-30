using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Queries
{
    public class GetReleaseRequestsRequestHandler : IRequestHandler<GetReleaseRequestsRequest, PageResponse<StoryReleaseRequestDetailsDto>>
    {
        private readonly IStoryReleaseRepository _releaseRepo;
        private readonly IMapper _mapper;
        public GetReleaseRequestsRequestHandler(IStoryReleaseRepository storyReleaseRepository, IMapper mapper)
        {
            _releaseRepo = storyReleaseRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<StoryReleaseRequestDetailsDto>> Handle(GetReleaseRequestsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryReleaseRequestDetailsDto>();

            IQueryable<StoryReleaseRequest> query = _releaseRepo.GetQueryable();

            query = query
                .Include(r => r.Object)             //Story report
                    .ThenInclude(r => r.Object)     //Story
                .Include(r => r.TargetUser)         //user
                    .ThenInclude(u => u.Warnings);  //warnings

            var repoRes = await _releaseRepo.GetPageAsync(request.pageReq, query);

            response = _mapper.Map<PageResponse<StoryReleaseRequestDetailsDto>>(repoRes);

            response.Success = true;

            return response;
        }
    }
}
