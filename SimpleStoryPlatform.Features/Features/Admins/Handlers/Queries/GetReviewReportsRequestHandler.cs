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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Queries
{
    public class GetReviewReportsRequestHandler : IRequestHandler<GetReviewReportsRequest, PageResponse<ReviewReportVM>>
    {
        private readonly IReviewReportRepository _reivewRepo;
        private readonly IMapper _mapper;
        public GetReviewReportsRequestHandler(IReviewReportRepository reviewReportRepository, IMapper mapper)
        {
            _reivewRepo = reviewReportRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<ReviewReportVM>> Handle(GetReviewReportsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<ReviewReportVM>();

            IQueryable<StoryReviewReport> query = _reivewRepo.GetQueryable();

            query = query
                .Include(r => r.Review)
                .Include(r => r.TargetUser)
                    .ThenInclude(u => u.Warnings)
                .Where(r => r.IsComplete == false);

            //query = query
            //    .Where(r => !r.IsComplete)
            //    .Include(r => r.TargetUser)
            //        .ThenInclude(u => u.Warnings)
            //    .Select(r => new StoryReviewReport
            //    {
            //        PublicId = r.PublicId,
            //        CreatedBy = r.CreatedBy,
            //        CreatedAt = r.CreatedAt,
            //        ReportText = r.ReportText,
            //        TargetUser = new User
            //        {
            //            PublicId = r.TargetUser.PublicId,
            //            FirstName = r.TargetUser.FirstName,
            //            LastName = r.TargetUser.LastName,
            //            Username = r.TargetUser.Username,
            //            Warnings = r.TargetUser.Warnings.ToList(),
            //        },
            //        Review = new StoryReview
            //        {
            //            PublicId = r.Review.PublicId,
            //            Data = r.Review.Data,
            //        }
            //    });

            var repoRes = await _reivewRepo.GetPageAsync(request.pageReq, query);

            response = _mapper.Map<PageResponse<ReviewReportVM>>(repoRes);

            if (response.Items != null)
                foreach (var item in response.Items)
                    if (item.TargetUser.Warnings != null)
                        item.TargetUser.Warnings = item.TargetUser.Warnings.Take(3).ToList();

            response.Success = true;

            return response;
        }
    }
}
