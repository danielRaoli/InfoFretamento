namespace InfoFretamento.Application.Responses
{
    public class Response<T>
    {
        public T? Data { get; private set; }
        private int _code = 200;
        public string Message { get; set; } = string.Empty;
        public bool IsSucces => _code >= 200 || _code < 300;

        public Response(T? data, int code = 200, string message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }
    }
}
