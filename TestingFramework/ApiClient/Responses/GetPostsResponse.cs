using System.Collections.Generic;

namespace TestingFramework.ApiClient.Responses
{
    public class GetPostsResponse
    {
        public GetMeta meta { get; set; }

        public IList<PostData> data { get; set; }
    }
}
