using AutoMapper;
using SimpleStoryPlatform.Application.DTOs;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //common
            CreateMap<BaseDomainEntity, BaseDtoInfo>().ReverseMap();

            //user profiles
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserDetailsDto>().ReverseMap();
            CreateMap<User, UserPreviewDto>().ReverseMap();
            CreateMap<User, UserWithWarningsDto>().ReverseMap();

            //story profiles
            CreateMap<StoryReview, StoryReviewDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Reviewer))
                .ForMember(dest => dest.TargetStoryGuid, opt => opt.MapFrom(src => src.TargetStory.PublicId))
                .ReverseMap();

            CreateMap<StoryReview, StoryReviewCreateDto>();

            CreateMap<StorySection, StorySectionDto>().ReverseMap();
            CreateMap<Story, StoryDetailsDto>()
                .ForMember(s => s.PlayListGuid,
                    opt => opt.MapFrom(src => src.PlayList != null ? src.PlayList.PublicId : (Guid?)null))
                .ForMember(s => s.Reviews,
                    opt => opt.MapFrom(src => src.Reviews))
                .ReverseMap();

            CreateMap<Story, StoryPreviewDto>()
                .ForMember(dest => dest.SectionsCount,
                    opt => opt.MapFrom(src => src.Data != null ? src.Data.Count : 0))
                .ForMember(dest => dest.ReviewsCount,
                    opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0))
                .ForMember(dest => dest.PlayListGuid,
                    opt => opt.MapFrom(src => src.PlayList != null ? src.PlayList.PublicId : (Guid?)null))
                .ReverseMap()
                .ForMember(dest => dest.Data, opt => opt.Ignore())
                .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                .ForMember(dest => dest.PlayList, opt => opt.Ignore());

            CreateMap<Story, StoryCreateDto>().ReverseMap();


            //report section
            CreateMap<Warning, WarningDto>().ReverseMap();

            CreateMap<StoryReport, StoryReportDto>()
                .ForMember(dest => dest.StoryGuid, opt => opt.MapFrom(src => src.Object.PublicId))
                .ReverseMap();

            CreateMap<StoryReviewReport, ReviewReportDto>()
                .ReverseMap();

            CreateMap<StoryReleaseRequest, StoryReleaseRequestDetailsDto>()
                .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Object))
                .ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();

        }
    }
}
