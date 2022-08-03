using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ipc_hanam.sensor.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            HubConnectionBuilder
            return new JsonResult("hahaha");
        }
    }
}
