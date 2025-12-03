namespace SimpleStoryPlatform.Web.Services.Interfaces
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "14.6.3.0 (NJsonSchema v11.5.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface IClient
    {
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AllReportsDtoBaseResponseWithData> AvailableReportsAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<AllReportsDtoBaseResponseWithData> AvailableReportsAsync(System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryReportDtoPageResponse> AvailableStoryReportsAsync(BaseRequest body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryReportDtoPageResponse> AvailableStoryReportsAsync(BaseRequest body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ReviewReportDtoPageResponse> AvailableReviewReportsAsync(BaseRequest body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ReviewReportDtoPageResponse> AvailableReviewReportsAsync(BaseRequest body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryReleaseRequestDetailsDtoPageResponse> AvailableReleaseRequestsAsync(BaseRequest body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryReleaseRequestDetailsDtoPageResponse> AvailableReleaseRequestsAsync(BaseRequest body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> CompleteReportStoryAsync(ReportCompleteDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> CompleteReportStoryAsync(ReportCompleteDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> CompleteReportReviewAsync(ReportCompleteDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> CompleteReportReviewAsync(ReportCompleteDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> CompleteReportReleaseRequestAsync(ReportCompleteDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> CompleteReportReleaseRequestAsync(ReportCompleteDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryPreviewDtoPageResponse> LastStoriesAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryPreviewDtoPageResponse> LastStoriesAsync(System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryDetailsDtoBaseResponseWithData> ReadStoryAsync(System.Guid? body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryDetailsDtoBaseResponseWithData> ReadStoryAsync(System.Guid? body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StringBaseResponseWithData> LoginAsync(UserLoginDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StringBaseResponseWithData> LoginAsync(UserLoginDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StringBaseResponseWithData> SignupAsync(UserCreateDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StringBaseResponseWithData> SignupAsync(UserCreateDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryPreviewDtoPageResponse> SearchResultAsync(StorySearchOptionsDtoSearchRequest body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryPreviewDtoPageResponse> SearchResultAsync(StorySearchOptionsDtoSearchRequest body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<UserDetailsDtoBaseResponseWithData> ProfileAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<UserDetailsDtoBaseResponseWithData> ProfileAsync(System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> AddReviewAsync(StoryReviewCreateDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> AddReviewAsync(StoryReviewCreateDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> ReportReviewAsync(UserReportDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> ReportReviewAsync(UserReportDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> ReportStoryAsync(UserReportDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> ReportStoryAsync(UserReportDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<GuidNullableBaseResponseWithData> CreateNewStoryAsync(StoryCreateDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<GuidNullableBaseResponseWithData> CreateNewStoryAsync(StoryCreateDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryPreviewDtoListBaseResponseWithData> MyStoriesAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryPreviewDtoListBaseResponseWithData> MyStoriesAsync(System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryDetailsDtoBaseResponseWithData> MyStoryDetailsAsync(System.Guid? body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryDetailsDtoBaseResponseWithData> MyStoryDetailsAsync(System.Guid? body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryDetailsDtoBaseResponseWithData> UpdateStoryAsync(StoryDetailsDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<StoryDetailsDtoBaseResponseWithData> UpdateStoryAsync(StoryDetailsDto body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> UnpublishStoryAsync(System.Guid? body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> UnpublishStoryAsync(System.Guid? body, System.Threading.CancellationToken cancellationToken);

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> ReleaseRequestAsync(StoryReleaseRequestCreateDto body);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<BaseResponse> ReleaseRequestAsync(StoryReleaseRequestCreateDto body, System.Threading.CancellationToken cancellationToken);

    }

}