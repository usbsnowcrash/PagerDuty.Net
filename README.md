PagerDuty.Net
=============

A wrapper for the PagerDuty API

Example usage:

var svc = new PagerDutyAPI("yourpagerdutysubdomain","APIToken");

//Get all the alerts for the last 24 hours
var alerts = svc.GetAlerts(DateTime.Now.AddDay(-1),DateTime.Now,Filter.Unspecified);
