using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    public class GetStoryReportsRequestHandler : IRequestHandler<GetStoryReportsRequest, PageResponse<StoryReportVM>>
    {
        private readonly IStoryReportRepository _storyReportRepo;
        private readonly IMapper _mapper;
        public GetStoryReportsRequestHandler(IStoryReportRepository storyReportRepository, IMapper mapper)
        {
            _storyReportRepo = storyReportRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<StoryReportVM>> Handle(GetStoryReportsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryReportVM>();

            IQueryable<StoryReport> query = _storyReportRepo.GetQueryable();

            query = query
                .Include(r => r.Story)
                .Include(r => r.TargetUser)
                    .ThenInclude(u => u.Warnings)
                .Where(r => r.IsComplete == false);

            //query = query
            //    .Where(r => !r.IsComplete)
            //    .Select(r => new StoryReport
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
            //            Warnings = r.TargetUser.Warnings,
            //        },
            //        Story = new Story
            //        {
            //            PublicId = r.Story.PublicId
            //        }
            //    });

            var repoRes = await _storyReportRepo.GetPageAsync(request.pageReq, query);

            response = _mapper.Map<PageResponse<StoryReportVM>>(repoRes);

            if (response.Items != null)
                foreach (var item in response.Items)
                    if (item.TargetUser.Warnings != null)
                        item.TargetUser.Warnings = item.TargetUser.Warnings.Take(3).ToList();


            response.Success = true;

            return response;
        }
    }
}
