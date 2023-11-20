namespace Fumble.Basket.Api.ViewModels
{
    public static class ResponsePayload
    {
        public static SuccessResponsePayload<TBody> Success<TBody>(TBody value)
        {
            return new SuccessResponsePayload<TBody>()
            {
                Content = value
            };
        }

        public static ErrorResponsePayload Error(string errorCode, List<string>? notifications = null)
        {
            return new ErrorResponsePayload()
            {
                ErrorCode = errorCode,
                Notifications = notifications ?? new List<string>()
            };
        }
    }

    public class ErrorResponsePayload
    {
        public bool IsSuccess { get; } = false;
        public string ErrorCode { get; set; } = string.Empty;
        public IList<string> Notifications { get; set; } = new List<string>();
    }

    public class SuccessResponsePayload<TBody>
    {
        public bool IsSuccess { get; } = true;
        public TBody? Content { get; set; }
    }
}
