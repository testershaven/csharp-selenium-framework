namespace TestingFramework.ApiClient.Requests
{
    public class PostToDoRequest
    {
        public string User { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string due_on { get; set; }
    }
}
