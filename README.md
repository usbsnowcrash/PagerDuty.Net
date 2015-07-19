# PagerDuty.net
----
[![Build status](https://ci.appveyor.com/api/projects/status/q94q3dvi2xi9h29l?svg=true)](https://ci.appveyor.com/project/usbsnowcrash/pagerduty-net)

A wrapper for the PagerDuty API

### Instantiate wrapper
```cs
var svc = new PagerDutyAPI("yourpagerdutysubdomain","APIToken");
```

### Get all alerts for the last 24 hours
```cs
var alerts = svc.GetAlerts(DateTime.Now.AddDay(-1),DateTime.Now,Filter.Unspecified);
```

## License
[MIT](https://github.com/usbsnowcrash/PagerDuty.Net/blob/master/MIT-LICENSE.md)

## Version History
[See Changelog](https://github.com/usbsnowcrash/PagerDuty.Net/blob/master/CHANGELOG.md)
