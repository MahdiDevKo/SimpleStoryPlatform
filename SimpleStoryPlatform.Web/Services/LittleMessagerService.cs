namespace SimpleStoryPlatform.Web.Services
{
    public class LittleMessagerService
    {
        public event Action<string, string, int>? OnShow;

        public void ShowMessage(string message = "404", string type = "info", int durration = 3000)
        {
            if (message == "404")
            {
                type = "error";
                message = "this service is not available (yet)";
            }

            OnShow?.Invoke(message, type, durration);
        }
    }
}
