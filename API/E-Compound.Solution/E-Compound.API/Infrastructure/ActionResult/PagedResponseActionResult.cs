using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace E_Compound.API.Infrastructure.ActionResult
{
    public class PagedResponseActionResult: IHttpActionResult
    {
        private HttpRequestMessage _request;
        private long _totalCount { get; set; }
        private string _nextPageURL { get; set; }
        private string _prevPageURL { get; set; }
        private dynamic _results { get; set; }
        private UrlHelper _url;
        public PagedResponseActionResult(HttpRequestMessage request, string routeName, int currentPage, int pageSize, long totalCount, dynamic results)
        {
            _request = request;
            _results = results;

            _url = new UrlHelper(request);
            var routeValues = request.GetQueryNameValuePairs().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);

            if (!routeValues.Keys.Contains("page"))
            {
                routeValues.Add("page", "");
            }
            if ((currentPage * pageSize) < totalCount)
            {
                routeValues["page"] = (currentPage + 1).ToString();
                _nextPageURL = _url.Link(routeName, routeValues);

            }

            if (currentPage > 1)
            {
                routeValues["page"] = (currentPage - 1).ToString();
                _prevPageURL = _url.Link(routeName, routeValues);
            }

            _totalCount = totalCount;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {

            var response = new
            {
                TotalCount = _totalCount,
                NextPageURL = _nextPageURL,
                PrevPageURL = _prevPageURL,
                Results = _results,
            };
            return Task.FromResult(_request.CreateResponse(System.Net.HttpStatusCode.OK, response));
        }
    }
}