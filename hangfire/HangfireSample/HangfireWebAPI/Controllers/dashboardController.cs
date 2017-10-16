using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Hangfire.Storage;

namespace HangfireWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var monitoringApi = JobStorage.Current.GetMonitoringApi();

           

            var isSucceed = monitoringApi.SucceededJobs(0, System.Convert.ToInt32((int) monitoringApi.SucceededListCount()));

            var details = isSucceed.FirstOrDefault().Value.Job;

           

            return new string[] { "value1", "value2" };
        }
    }
}
