using Integrations.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScraperAPI.Contracts.V1;
using WebScraperAPI.Domain;

namespace WebScraperAPI.Controllers.V1
{
    public class ServicesController : Controller
    {

        private List<Service> _Services;

        public ServicesController()
        {
            _Services = new List<Service>();
            _Services.Add(new Service { ID = 0, Value = "All" });
            _Services.Add(new Service { ID = 1, Value = "Geo API" });
            _Services.Add(new Service { ID = 2, Value = "Ping" });
            _Services.Add(new Service { ID = 3, Value = "Reverse DNS" });
            _Services.Add(new Service { ID = 4, Value = "Domain Availabily" });
            _Services.Add(new Service { ID = 5, Value = "Port API" });

        }


        [HttpGet(ApiRoutes.Services.GetServices)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_Services);
        }

    }
}
