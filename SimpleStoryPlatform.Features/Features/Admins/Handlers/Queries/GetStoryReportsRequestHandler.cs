using MediatR;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.Features.Admins.Requests.Queries;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleStoryPlatform.Domain.Entites.Report;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleStoryPlatform.Application.Features.Admins.Handlers.Queries
{
    public class GetStoryReportsRequestHandler : IRequestHandler<GetStoryReportsRequest, PageResponse<StoryReportDto>>
    {
        private readonly IStoryReportRepository _storyReportRepo;
        private readonly IMapper _mapper;
        public GetStoryReportsRequestHandler(IStoryReportRepository storyReportRepository, IMapper mapper)
        {
            _storyReportRepo = storyReportRepository;
            _mapper = mapper;
        }
        public async Task<PageResponse<StoryReportDto>> Handle(GetStoryReportsRequest request, CancellationToken cancellationToken)
        {
            var response = new PageResponse<StoryReportDto>();

            IQueryable<StoryReport> query = _storyReportRepo.GetQueryable();

            query = query
                .Include(r => r.Object)
                .Include(r => r.TargetUser)
                    .ThenInclude(u => u.Warnings)
                .Where(r => r.IsComplete == false);

            var repoRes = await _storyReportRepo.GetPageAsync(request.pageReq, query);

            response = _mapper.Map<PageResponse<StoryReportDto>>(repoRes);

            response.Success = true;

            return response;
        }
    }
}
