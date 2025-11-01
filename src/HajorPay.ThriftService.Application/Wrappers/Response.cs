namespace HajorPay.ThriftService.Application.Wrappers
{
    public class Response<T>
    {
        public Response(string message, bool succeeded)
        {
            Message = message;
            Succeeded = succeeded;
        }

        public Response(T data, string? message = null, bool succeeded = true)
        {
            Succeeded = succeeded;
            Data = data;
            Message = message;
        }

        public bool Succeeded { get; set; } = true;
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static Response<T> Success(T data, string message) => new Response<T>(data, message, true);
        public static Response<T> Fail(string message) => new Response<T>(message, false);

    }
    //public class Response<T>
    //{
    //    public Response(T data, string? message = null, bool succeeded = true)
    //    {
    //        Succeeded = succeeded;
    //        Data = data;
    //        if (!string.IsNullOrEmpty(message))
    //            Messages.Add(message);
    //    }

    //    public Response(string message)
    //    {
    //        Succeeded = false;
    //        Messages = new List<string> { message };
    //    }
    //    public bool Succeeded { get; set; } = true;
    //    public List<string> Messages { get; set; } = new List<string>();
    //    public T? Data { get; set; }

    //}
}
