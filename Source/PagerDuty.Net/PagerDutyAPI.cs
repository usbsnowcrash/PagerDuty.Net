using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {
    [Serializable()]
    public class PagerDutyAPI {
        public string AccessToken { get; set; }
        public string Subdomain { get; set; }
        public int Timeout { get; set; }

        public PagerDutyAPI(string domain, string token) {
            if (String.IsNullOrEmpty(domain)) {
                throw new ArgumentNullException("domain");
            }

            if (String.IsNullOrEmpty(token)) {
                throw new ArgumentNullException("token");
            }
            Subdomain = domain;
            AccessToken = token;
            Timeout = 10000;
        }
        
        /// <summary>
        /// Returns a client object with the default values, virtual for testability
        /// </summary>
        /// <param name="url">The API endpoint you are hitting</param>
        /// <returns></returns>
        protected virtual IRestClient GetClient(string url) {
            var client = new RestClient() { Timeout = this.Timeout, BaseUrl = "https://" + Subdomain + ".pagerduty.com/api" + url };
            client.AddDefaultParameter(new Parameter { Name = "Authorization", Value = "Token token=" + this.AccessToken, Type = ParameterType.HttpHeader });
            return client;
        }

        /// <summary>
        /// Returns a request object with the default values, virtual for testability
        /// </summary>
        /// <returns></returns>
        protected virtual IRestRequest GetRequest() {
            var request = new RestRequest();
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        /// <summary>
        /// Makes a call to https://<subdomain>.pagerduty.com/api/v1/alerts
        /// </summary>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="filter">Filter by SMS, Email, Phone or Push</param>
        /// <returns></returns>
        public AlertsResponse GetAlerts(DateTime since, DateTime until, Filter filter) {
            return GetAlerts(since, until, filter, 0, 100);
        }

        /// <summary>
        /// Makes a call to https://<subdomain>.pagerduty.com/api/v1/alerts
        /// </summary>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="filter">Filter by SMS, Email, Phone or Push</param>
        /// <param name="offSet">The offset of the first incident record returned. Default is 0.</param>
        /// <param name="limit">The number of incidents returned. Default (and max limit) is 100</param>
        /// <returns></returns>
        public AlertsResponse GetAlerts(DateTime since, DateTime until, Filter filter, int offSet, int limit) {
            var client = this.GetClient("/v1/alerts");
            var req = this.GetRequest();

            req.AddParameter("since", since.ToString("s"));
            req.AddParameter("until", until.ToString("s"));
            if (filter != Filter.Unspecified) {
                req.AddParameter("filter", "{:type=>\"" + filter.ToString() + "\"}");
            }
            req.AddParameter("offset", offSet);
            req.AddParameter("limit", limit);
            var resp = client.Execute<AlertsResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

    }
}
