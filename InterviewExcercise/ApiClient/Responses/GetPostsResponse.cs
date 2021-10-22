using System.Collections.Generic;

namespace InterviewExcercise.ApiClient.Responses
{
    public class GetPostsResponse
    {
        public GetMeta meta { get; set; }

        public IList<PostData> data { get; set; }
    }
}
