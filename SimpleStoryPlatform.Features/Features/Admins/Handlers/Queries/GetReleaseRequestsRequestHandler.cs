using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Application.ViewModels.Reports;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Queries
{
    public class GetReleaseRequestsRequestHandler : IRequestHandler<GetReleaseRequestsRequest, PageResponse<ReleaseRequestVM>>
    {
        private readonly IStoryReleaseRepository _releaseRepo;
        private readonly IMapper _mapper;
        public GetReleaseRequestsRequestHandler(IStoryReleaseRepository storyReleaseRepository, IMapper mapper)
        {
            _releaseRepo = storyReleaseRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<ReleaseRequestVM>> Handle(GetReleaseRequestsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<ReleaseRequestVM>();

            IQueryable<StoryReleaseRequest> query = _releaseRepo.GetQueryable();

            query = query
                .Include(r => r.Story)
                .Where(r => r.IsComplete == false);

            //query = query
            //    .Where(r => !r.IsComplete)
            //    .Select(r => new StoryReleaseRequest
            //    {
            //        PublicId = r.PublicId,
            //        CreatedBy = r.CreatedBy,
            //        CreatedAt = r.CreatedAt,
            //        RequestMessage = r.RequestMessage,

            //        Story = new Story
            //        {
            //            PublicId = r.Story.PublicId,
            //        },
            //    });

            var repoRes = await _releaseRepo.GetPageAsync(request.pageReq, query);

            response = _mapper.Map<PageResponse<ReleaseRequestVM>>(repoRes);



            //response.Items = _mapper.Map<List<StoryReleaseRequestDetailsDto>>(repoRes.Items);

            response.Success = true;

            return response;
        }
    }
}
