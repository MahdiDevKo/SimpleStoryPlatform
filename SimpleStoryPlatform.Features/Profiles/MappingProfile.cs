using AutoMapper;
using SimpleStoryPlatform.Application.DTOs;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.ReportDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Features.Writers.Requests.Commands;
using SimpleStoryPlatform.Application.Responses;
using SimpleStoryPlatform.Application.Services;
using SimpleStoryPlatform.Application.ViewModels.Reports;
using SimpleStoryPlatform.Domain.Entites;
using SimpleStoryPlatform.Domain.Entites.Report;
using System;
using System.Collections.Generic;
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
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.WritedStories, opt => opt.Ignore());

            CreateMap<User, UserWithWarningsDto>()
                .ForMember(dest => dest.TotalWarnings,
                opt => opt.MapFrom(src => src.Warnings != null ? src.Warnings.Count : 0))
                .ReverseMap();

            //story profiles
            CreateMap<StoryReview, StoryReviewDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Reviewer))
                .ForMember(dest => dest.TargetStoryGuid, opt => opt.MapFrom(src => src.TargetStory.PublicId))
                .ReverseMap();

            CreateMap<StoryReview, StoryReviewCreateDto>();

            CreateMap<Story, StoryDetailsDto>()
                .ForMember(s => s.PlayListGuid,
                    opt => opt.MapFrom(src => src.PlayList != null ? src.PlayList.PublicId : (Guid?)null))
                .ForMember(s => s.Data, opt => opt.MapFrom(src => src.Data))
                .ReverseMap();

            CreateMap<StorySection, StorySectionDto>().ReverseMap();

            CreateMap<StorySection, StorySectionUpdateDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => HashIdHelper.Encode(src.Id)))
            .ReverseMap()
            .ForMember(dest => dest.Id,
                opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrEmpty(src.Id)); // checking for ==> string Id != null
                    opt.MapFrom(src => HashIdHelper.Decode(src.Id));
                });

            //CreateMap<StorySection, StorySectionDto>()
            //    .ForMember(dest => dest.Id,
            //        opt => opt.MapFrom(src => HashId.Encode(src.Id))) // مستقیم
            //    .ReverseMap()
            //    .ForMember(dest => dest.Id,
            //        opt => opt.MapFrom(src => HashId.Decode(src.Id)));

            CreateMap<Story, StoryPreviewDto>()
                    .ForMember(dest => dest.SectionsCount,
                        opt => opt.MapFrom(src => src.Data != null ? src.Data.Count : 0))
                    .ForMember(dest => dest.ReviewsCount,
                        opt => opt.MapFrom(src => src.Reviews != null ? src.Reviews.Count : 0))
                    .ForMember(dest => dest.InLibraryOf,
                        opt => opt.MapFrom(src => src.InLibraryOf.Count != null ? src.Reviews.Count : 0))
                    .ForMember(dest => dest.PlayListGuid,
                        opt => opt.MapFrom(src => src.PlayList != null ? src.PlayList.PublicId : (Guid?)null))
                    .ReverseMap()
                    .ForMember(dest => dest.Data, opt => opt.Ignore())
                    .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                    .ForMember(dest => dest.PlayList, opt => opt.Ignore());

            CreateMap<Story, StoryCreateDto>().ReverseMap();

            CreateMap<Story, StoryUpdateDto>()
                    .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => HashIdHelper.Encode(src.Id)))
                .ReverseMap()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => HashIdHelper.Decode(src.Id))); ;

            //report section
            CreateMap<Warning, WarningDto>().ReverseMap();

            //CreateMap<StoryReport, StoryReportDto>()
            //    .ForMember(dest => dest.StoryGuid, opt => opt.MapFrom(src => src.Object.PublicId))
            //    .ReverseMap();

            //CreateMap<StoryReviewReport, ReviewReportDto>()
            //    .ReverseMap();

            //CreateMap<StoryReleaseRequest, StoryReleaseRequestDetailsDto>()
            //    .ForMember(dest => dest.Report, opt => opt.MapFrom(src => src.Object))
            //    .ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();


            //CreateMap<PageResponse<,>, PageResponse<,>>
            CreateMap(typeof(PageResponse<>), typeof(PageResponse<>));


            //View Models maps
            CreateMap<BaseReportEntity, BaseReportVM>()
                    .ForMember(dest => dest.TargetUser,
                        opt => opt.MapFrom(src => src.TargetUser))
                    .ForMember(dest => dest.ReportReason,
                        opt => opt.MapFrom(src => src.ReportText));

            CreateMap<StoryReport, StoryReportVM>()
                .ForMember(dest => dest.StoryGuid,
                    opt => opt.MapFrom(src => src.Story.PublicId));

            CreateMap<StoryReviewReport, ReviewReportVM>()
                .ForMember(dest => dest.ReviewGuid,
                    opt => opt.MapFrom(src => src.Review.PublicId))
                .ForMember(dest => dest.ReviewData,
                    opt => opt.MapFrom(src => src.Review.Data));

            CreateMap<StoryReleaseRequest, ReleaseRequestVM>()
                .ForMember(dest => dest.StoryGuid,
                    opt => opt.MapFrom(src => src.Story.PublicId));




        }
    }
}
