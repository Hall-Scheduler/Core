namespace HallScheduler.Server.Infrastructure
{
    public class ResponseResultObject
    {
        public ResponseResultObject(object data)
        {
            this.Success = true;
            this.Message = null;
            this.Data = data;
        }

        public ResponseResultObject(bool success, string message, object data = null)
        {
            this.Success = success;
            this.Message = message;
            this.Data = data;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
