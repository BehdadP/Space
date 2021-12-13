using HangfireApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using System;
using HangfireApplication.Settings;
using Microsoft.Extensions.Options;

namespace HangfireApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListJobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly JobSettings _jobSettings;

        public WatchListJobController(IJobService jobService, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager, IOptions<JobSettings> jobSettings)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
            _jobSettings = jobSettings.Value;

        }



        [HttpGet("/WatchListJob")]
        public ActionResult CreateReccuringJob()
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.WatchListJobAsync(), _jobSettings.SchedulingCronExpression);
            return Ok();
        }



    }
}
