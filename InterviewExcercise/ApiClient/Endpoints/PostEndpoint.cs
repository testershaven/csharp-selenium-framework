using InterviewExcercise.ApiClient.Requests;
using InterviewExcercise.Reporter;
using RestSharp;
using System.Threading.Tasks;

namespace InterviewExcercise.ApiClient.Endpoints
{
    public class PostEndpoint
    {
        public static async Task<IRestResponse> CreatePost(CreatePostRequest requestBody, int userId)
        {
            ExtentTestManager.SetStepStatusPass("Posting Post");
            var request = new RestRequest($"/public/v1/users/{userId}/posts")
            {
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer()
            };
            request.AddJsonBody(requestBody);
            request.Method = Method.POST;

            Task<IRestResponse> t = RestClientFixture.Instance.ExecuteAsync(request);
            t.Wait();
            return await t;
        }

        public static async Task<IRestResponse> GetPosts()
        {
            ExtentTestManager.SetStepStatusPass("Getting list of posts");
            var request = new RestRequest($"/public/v1/posts")
            {
                JsonSerializer = new RestSharp.Serializers.NewtonsoftJson.JsonNetSerializer(),
                Method = Method.GET
            };

            Task<IRestResponse> t = RestClientFixture.Instance.ExecuteAsync(request);
            t.Wait();
            return await t;
        }
    }
}
