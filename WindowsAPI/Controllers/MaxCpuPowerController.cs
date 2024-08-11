using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WindowsAPI.Tools;

namespace WindowsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaxCpuPowerController : ControllerBase
    {
        [HttpPost]
        public IActionResult SetPowerValue(int PowerValue)
        {
            if (PowerValue < 0 || PowerValue > 100)
            {
                return BadRequest("数值必须在0到100之间");
            }
            PowerChanger.SetMaxCpuValue(PowerValue);
            return Ok("设置成功");
        }
        [HttpGet]
        public IActionResult GetPowerValue()
        {
            int powerValue = PowerChanger.GetCurrentMaxCpuValue(); 
            return Ok(powerValue);
        }
    }
}
