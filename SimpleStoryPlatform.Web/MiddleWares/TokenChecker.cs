namespace SimpleStoryPlatform.Web.MiddleWares
{
    public class TokenChecker : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenChecker(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null && context.Request.Cookies.TryGetValue("Token", out var token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
