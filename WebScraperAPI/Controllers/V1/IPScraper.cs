using Integrations.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScraperAPI.Contracts.V1;

namespace WebScraperAPI.Controllers.V1
{
    public class IPScraper : Controller
    {

        [HttpGet(ApiRoutes.IPScraper.GetInformation)]
        public async Task<IActionResult> GetAll(string IPorDomain, Nullable<int> Service)
        {
            int _service = 0;
            if(Service != null)
            {
                _service = (int)Service;
            }
            Process P = new Process();
            object ReturnInfo = await P.startProcess(IPorDomain, _service);
            return Json(ReturnInfo);
        }
    }
}
