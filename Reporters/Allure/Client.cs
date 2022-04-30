using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestingFramework.Reporters.Allure.Responses;

namespace TestingFramework.Reporters.Allure
{
    public class Client
    {
        RestClient allureEndpoint;
        public Client(string host)
        {
            allureEndpoint = new RestClient(host);
            allureEndpoint.AddDefaultHeader("Content-Type", "application/json");
            allureEndpoint.AddDefaultHeader("accept", "*/*");
        }

        internal void CleanResults(string project)
        {
            var request = new RestRequest($"/clean-results?project_id={project}")
            {
                Method = Method.Get
            };

            var t = allureEndpoint.ExecuteAsync(request);
            t.Wait();
        }

        internal void SendResults(string results, string project)
        {
            var request = new RestRequest($"/send-results?project_id={project}")
            {
               Method = Method.Post
            };
            request.AddJsonBody(results);

            Task<RestResponse> t = allureEndpoint.ExecuteAsync(request);
            t.Wait();
        }

        internal GenerateReportResponse GenerateReport(string project)
        {
            var request = new RestRequest($"/generate-report?project_id={project}")
            {
                Method = Method.Get
            };

            var t = allureEndpoint.ExecuteAsync<GenerateReportResponse>(request);
            t.Wait();

            return t.Result.Data;
        }

        internal void Login(string username, string password)
        {
            var requestBody = $"{{ username: {username}, password: {password} }}";

            var request = new RestRequest($"/login")
            {
                Method = Method.Post
            };
            request.AddJsonBody(requestBody);

            Task<RestResponse> t = allureEndpoint.ExecuteAsync(request);
            t.Wait();

            var result = t.Result;
            allureEndpoint.CookieContainer.Add(result.Cookies);
            allureEndpoint.AddDefaultHeader("X-CSRF-Token", result.Cookies.Single( cookie => cookie.Name.Contains("csrf_access_token")).Value);
        }
    }
}
