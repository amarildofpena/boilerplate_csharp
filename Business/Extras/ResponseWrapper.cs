namespace Business.Extras
{
    public class ResponseWrapper<T>
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public ResponseWrapper(int statusCode, T data = default, string errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}
