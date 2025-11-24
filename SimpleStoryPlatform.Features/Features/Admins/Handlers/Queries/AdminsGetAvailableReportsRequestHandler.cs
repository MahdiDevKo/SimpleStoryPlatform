using AutoMapper;
using MediatR;
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
    public class AdminsGetAvailableReportsRequestHandler : IRequestHandler<AdminsGetAvailableReportsRequest, BaseResponseWithData<AllReportsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IReviewReportRepository _reviewReportRepo;
        private readonly IStoryReportRepository _storyReportRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICurrentUserToken _currentUserToken;
        public AdminsGetAvailableReportsRequestHandler(
            IMapper mapper,
            IReviewReportRepository reviewReportRepo,
            IStoryReportRepository storyReportRepo,
            IUserRepository userRepository,
            ICurrentUserToken currentUserToken)
        {
            _mapper = mapper;
            _reviewReportRepo = reviewReportRepo;
            _storyReportRepo = storyReportRepo;
            _userRepo = userRepository;
            _currentUserToken = currentUserToken;
        }
        public async Task<BaseResponseWithData<AllReportsDto>> Handle(AdminsGetAvailableReportsRequest request, CancellationToken cancellationToken)
        {
            var releaseReueqsts = await _userRepo.GetStoryReleaseRequests((Guid)_currentUserToken.UserGuid);

            var releaseReueqstsDtos = _mapper.Map<List<StoryReleaseRequestDetailsDto>>(releaseReueqsts);

            var reviewReports = _mapper.Map<List<ReviewReportDto>>(await _reviewReportRepo.GetAllWithDetail());

            var storyReports = _mapper.Map<List<StoryReportDto>>(await _storyReportRepo.GetAllWithDetail());


            var response = new BaseResponseWithData<AllReportsDto>()
            {
                Success = true,

                data = new AllReportsDto()
                {
                    ReviewReports = reviewReports,
                    StoryReports = storyReports,
                    StoryReleaseRequests = releaseReueqstsDtos
                }
            };

            return response;
        }
    }
}
