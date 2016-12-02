using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestSharp
{
    public static class ExecuteExtensions
    {
        public static Task<IRestResponse> ExecuteWithTaskAsync(this IRestClient client, IRestRequest request)
        {
            var cSource = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(request, res => cSource.SetResult(res));

            return cSource.Task;
        }
    }
}
