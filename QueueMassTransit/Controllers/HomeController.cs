﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedMessage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueMassTransit.Sender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly ILogger<HomeController> logger;

        public HomeController(IPublishEndpoint publishEndpoint, ILogger<HomeController> logger)
        {
            this.publishEndpoint = publishEndpoint;
            this.logger = logger;
        }

        [HttpGet]
        public async void Get()
        {
           await publishEndpoint.Publish(new Message{ Text = "some Text" });
            logger.LogInformation("Message sent");
        }

    }
}
