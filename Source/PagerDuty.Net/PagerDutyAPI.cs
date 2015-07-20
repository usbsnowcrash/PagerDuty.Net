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
        /// Acknowledge an incident using the integration API.
        /// </summary>
        /// <param name="integrationUrl">Integration API Url</param>
        /// <param name="aRequest">Acknowledge details</param>
        /// <returns></returns>
        public IntegrationResponse Acknowledge(AcknowledgeRequest aRequest) {
            return this.PostIntegrationRequest(aRequest);
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

        /// <summary>
        /// Get high level statistics about the number of alerts (SMSes, phone calls and emails) sent for the desired time period, summed daily, weekly or monthly
        /// </summary>
        /// <param name="since">The start of the date range over which you want to search. The time element is optional.</param>
        /// <param name="until">The end of the date range over which you want to search.</param>
        /// <param name="rollup">Specifies the bucket duration for each summation</param>
        /// <returns></returns>
        public AlertReport GetAlertsReport(DateTime? since, DateTime? until, Rollup rollup) {
            var client = this.GetClient("/v1/reports/alerts_per_time");
            var req = this.GetRequest();

            if(since != null){
                req.AddParameter("since", since.Value.ToString("s"));
            }
            if (until != null) {
                req.AddParameter("until", until.Value.ToString("s"));
            }
            req.AddParameter("rollup", rollup.ToString());
            var resp = client.Execute<AlertReport>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Retrieve an incident
        /// </summary>
        /// <param name="id">ID of the incident</param>
        /// <returns></returns>
        public Incident GetIncident(string id) {
            var client = this.GetClient("/v1/incidents/" + id);
            var req = this.GetRequest();
            
            var resp = client.Execute<Incident>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Get incident log entries
        /// </summary>
        /// <param name="id">Incident id</param>
        /// <param name="filters">Log entry filters</param>
        /// <returns></returns>
        public LogEntriesResponse GetIncidentLogEntries(string id, LogEntriesFilter filters) {
            return this.GetLogEntriesRequest("/v1/incidents/" + id + "/log_entries", filters);
        }

        /// <summary>
        ///  Used to query current and historical PagerDuty incidents over a date range
        /// </summary>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="fields">Specify which fields return, all fields return by default</param>
        /// <param name="status">Returns only the incidents currently in the passed status(es). Valid status options are triggered, acknowledged, and resolved</param>
        /// <param name="incident_key">Returns only the incidents with the passed de-duplication key</param>
        /// <param name="service">Returns only the incidents associated with the passed service(s). This expects one or more service IDs.  Separate multiple ids by a comma</param>
        /// <param name="assigned_to_user">Returns only the incidents currently assigned to the passed user(s). This expects one or more user IDs. </param>
        /// <param name="sort_by">Which field to sort by</param>
        /// <param name="sort_direction">Sort direction (default is asc)</param>
        /// <param name="offSet">The offset of the first incident record returned</param>
        /// <param name="limit">The number of incidents returned.</param>
        /// <returns></returns>
        public IncidentsResponse GetIncidents(IncidentFilter filters, IncidentSortBy sort_by, SortDirection sort_direction, int offSet, int limit) {
            var client = this.GetClient("/v1/incidents");
            var req = this.GetRequest();

            if (filters.ReturnAll) {
                req.AddParameter("date_range", "all");
            } 
            else {
                req.AddParameter("since", filters.since.ToString("s"));
                req.AddParameter("until", filters.until.ToString("s"));
            }
            
            if (!String.IsNullOrEmpty(filters.fields)) {
                req.AddParameter("fields", filters.fields);
            }

            if (!String.IsNullOrEmpty(filters.status)) {
                req.AddParameter("status", filters.fields);
            }

            if (!String.IsNullOrEmpty(filters.incident_key)) {
                req.AddParameter("incident_key", filters.incident_key);
            }

            if (!String.IsNullOrEmpty(filters.service)) {
                req.AddParameter("service", filters.service);
            }

            if (!String.IsNullOrEmpty(filters.assigned_to_user)) {
                req.AddParameter("assigned_to_user", filters.assigned_to_user);
            }

            if (sort_by != IncidentSortBy.unspecified) {
                req.AddParameter("sort_by", sort_by.ToString() + ":" + sort_direction.ToString());
            }
            
            req.AddParameter("offset", offSet);
            req.AddParameter("limit", limit);
            var resp = client.Execute<IncidentsResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// List all incident log entries across the entire account
        /// </summary>
        /// <param name="filters">Log entry filters</param>
        /// <returns></returns>
        public LogEntriesResponse GetLogEntries(LogEntriesFilter filters) {
            return this.GetLogEntriesRequest("/v1/log_entries", filters);
        }

        /// <summary>
        /// Get details for a specific incident log entry
        /// </summary>
        /// <param name="id">Id of the log entry</param>
        /// <returns></returns>
        public LogEntryResponse GetLogEntry(string id, LogEntriesFilter filters) {
            var client = this.GetClient("/v1/log_entries/" + id);
            var req = this.GetRequest();

            if (!String.IsNullOrEmpty(filters.time_zone)) {
                req.AddParameter("time_zone", filters.time_zone);
            }

            if (filters.include != null && filters.include.Count > 0) {
                req.AddParameter("include", filters.include);
            }

            var resp = client.Execute<LogEntryResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Retrieve notes for an incident
        /// </summary>
        /// <param name="id">ID of the incident</param>
        /// <returns></returns>
        public List<Note> GetNotesForIncident(string id) {
            var client = this.GetClient("/v1/incidents/"+id+"/notes");
            var req = this.GetRequest();

            var resp = client.Execute<List<Note>>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Makes a call to https://<subdomain>.pagerduty.com/api/v1/schedules
        /// </summary>
        /// <returns></returns>
        public SchedulesResponse GetSchedules()
        {
            var client = this.GetClient("/v1/schedules");
            var req = this.GetRequest();

            var resp = client.Execute<SchedulesResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Query schedule entries over a date range
        /// </summary>
        /// <param name="id">The schedule id you are querying</param>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="overflow">Overflow the entries beyond the specified date range</param>
        /// <param name="time_zone">Time zone used for the results to be rendered</param>
        /// <param name="user_id">Filter the results by a specific User</param>
        /// <returns></returns>
        public ScheduleEntriesResponse GetScheduleEntries(string id, ScheduleEntriesFilter filters)
        {
            var client = this.GetClient("/v1/schedules/" + id + "/entries");
            var req = this.GetRequest();

            if (filters.since != DateTime.MinValue) {
                req.AddParameter("since", filters.since.ToString("s"));
            }

            if (filters.until != DateTime.MinValue) {
                req.AddParameter("until", filters.until.ToString("s"));
            }

            if (filters.overflow) {
                req.AddParameter("overflow", "true");
            }

            if (!String.IsNullOrEmpty(filters.time_zone)) {
                req.AddParameter("time_zone", filters.time_zone);
            }

            if (!String.IsNullOrEmpty(filters.user_id)) {
                req.AddParameter("user_id", filters.user_id);
            }

            var resp = client.Execute<ScheduleEntriesResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Add a new note to an incident
        /// </summary>
        /// <param name="note">Content of the note</param>
        /// <param name="incident_id">The incident you are attaching to</param>
        /// <param name="requestor_id">User you are posting the note on behalf of</param>
        /// <returns></returns>
        public Note PostNoteForIncident(string note, string incident_id, string requestor_id)
        {
            var client = this.GetClient("/v1/incidents/" + incident_id + "/notes");
            var req = this.GetRequest();
            req.Method = Method.POST;

            req.AddParameter("application/json; charset=utf-8", "{\"requester_id\":\"" + requestor_id + "\",\"note\":{\"content\":\"" + note + "\"}}", ParameterType.RequestBody);
            req.RequestFormat = DataFormat.Json;

            var resp = client.Execute<Note>(req);

            if (resp.Data == null)
            {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Trigger an incident using the integration API.
        /// </summary>
        /// <param name="integrationUrl">Integration API Url</param>
        /// <param name="aRequest">Trigger details</param>
        /// <returns></returns>
        public IntegrationResponse Trigger(TriggerRequest tRequest) {
            return this.PostIntegrationRequest(tRequest);
        }

        /// <summary>
        /// Post a message to the integration API.
        /// </summary>
        /// <param name="integrationUrl">Integration API Url</param>
        /// <param name="request">Message to post</param>
        /// <returns></returns>
        private IntegrationResponse PostIntegrationRequest(object request) {
            var integrationUrl = "https://events.pagerduty.com/generic/2010-04-15/create_event.json";
            var client = new RestClient() { Timeout = this.Timeout, BaseUrl = integrationUrl };
            var req = this.GetRequest();
            req.Method = Method.POST;

            req.RequestFormat = DataFormat.Json;
            req.AddBody(request);

            var resp = client.Execute<IntegrationResponse>(req);

            if (resp.Data == null)
            {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Get a list of log entries
        /// </summary>
        /// <param name="url">Url to use for the call</param>
        /// <param name="filters">Log entry filters</param>
        /// <returns></returns>
        private LogEntriesResponse GetLogEntriesRequest(string url, LogEntriesFilter filters) {
            var client = this.GetClient(url);
            var req = this.GetRequest();

            if (filters.since != DateTime.MinValue) {
                req.AddParameter("since", filters.since.ToString("s"));
            }

            if (filters.until != DateTime.MinValue) {
                req.AddParameter("until", filters.until.ToString("s"));
            }

            if (!String.IsNullOrEmpty(filters.time_zone)) {
                req.AddParameter("time_zone", filters.time_zone);
            }

            if (filters.is_overview) {
                req.AddParameter("is_overview", "true");
            }

            if (filters.include != null && filters.include.Count > 0) {
                req.AddParameter("include", filters.include);
            }

            var resp = client.Execute<LogEntriesResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }
    }
}
