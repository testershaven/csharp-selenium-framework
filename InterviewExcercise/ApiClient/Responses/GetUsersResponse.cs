using System.Collections.Generic;

namespace InterviewExcercise.ApiClient.Responses
{
    public class GetUsersResponse
    {
        public GetMeta meta { get; set; }
        public IList<UserData> data { get; set; }
    }
}
