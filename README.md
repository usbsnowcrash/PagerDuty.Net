PagerDuty.Net
=============
[![Build status](https://ci.appveyor.com/api/projects/status/q94q3dvi2xi9h29l?svg=true)](https://ci.appveyor.com/project/usbsnowcrash/pagerduty-net)

A wrapper for the PagerDuty API

Example usage:

var svc = new PagerDutyAPI("yourpagerdutysubdomain","APIToken");

//Get all the alerts for the last 24 hours
var alerts = svc.GetAlerts(DateTime.Now.AddDay(-1),DateTime.Now,Filter.Unspecified);
