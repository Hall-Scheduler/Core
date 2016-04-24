namespace HallScheduler.Desktop.Models
{
    public class ResponseResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
