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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Queries
{
    public class GetReviewReportsRequestHandler : IRequestHandler<GetReviewReportsRequest, PageResponse<ReviewReportDto>>
    {
        private readonly IReviewReportRepository _reivewRepo;
        private readonly IMapper _mapper;
        public GetReviewReportsRequestHandler(IReviewReportRepository reviewReportRepository, IMapper mapper)
        {
            _reivewRepo = reviewReportRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<ReviewReportDto>> Handle(GetReviewReportsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<ReviewReportDto>();

            IQueryable<StoryReviewReport> query = _reivewRepo.GetQueryable();

            query = query
                .Include(r => r.Object)
                    .ThenInclude(s => s.TargetStory)
                .Include(r => r.Object)
                    .ThenInclude(s => s.Reviewer)
                .Include(r => r.TargetUser)
                    .ThenInclude(u => u.Warnings)
                .Where(r => r.IsComplete == false);

            var repoRes = await _reivewRepo.GetPageAsync(request.pageReq, query);

            response = _mapper.Map<PageResponse<ReviewReportDto>>(repoRes);

            response.Success = true;

            return response;
        }
    }
}
