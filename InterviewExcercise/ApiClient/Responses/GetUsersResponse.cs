using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewExcercise.ApiClient.Responses
{
    public class GetUsersResponse
    {
        public GetMeta meta { get; set; }
        public IList<UserData> data { get; set; }
    }
}
