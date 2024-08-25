using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using WindowsAPI.Tools;

namespace WindowsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerChangeController: ControllerBase
    {
        [HttpPost("Shutdown")]
        public IActionResult Shutdown()
        {
            try
            {
                PowerChanger.Shutdown();
                return Ok("计算机正在关机");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Shutdown/{tSecond}")]
        public IActionResult ShutdownWithDelay(int tSecond)
        {
            if (tSecond < 0)
            {
                return BadRequest("tSecond 参数不能为负数");
            }

            try
            {
                PowerChanger.Shutdown(tSecond);
                return Ok($"计算机将在{tSecond}秒后关机");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Reboot")]
        public IActionResult Reboot()
        {
            try
            {
                PowerChanger.Reboot();
                return Ok("计算机正在重启");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        

        [HttpPost("Reboot/{tSecond}")]
        public IActionResult RebootWithDelay(int tSecond)
        {
            if (tSecond < 0)
            {
                return BadRequest("tSecond 参数不能为负数");
            }

            try
            {
                PowerChanger.Reboot(tSecond);
                return Ok($"计算机将在{tSecond}秒后重启");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Sleep")]
        public IActionResult Sleep()
        {
            try
            {
                PowerChanger.Sleep();
                return Ok("计算机正在进入睡眠状态");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Hibernate")]
        public IActionResult Hibernate()
        {
            try
            {
                PowerChanger.Hibernate();
                return Ok("计算机正在进入休眠状态");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Hibernate/{tSecond}")]
        public IActionResult Hibernate(int tSecond)
        {
            if (tSecond < 0)
            {
                return BadRequest("tSecond 参数不能为负数");
            }

            try
            {
                PowerChanger.Hibernate(tSecond);
                return Ok($"计算机将在{tSecond}秒后进入休眠状态");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("CancelHibernate")]
        public IActionResult CancelHibernate()
        {
            try
            {
                PowerChanger.CancelHibernate();
                return Ok("已取消休眠");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("CancelPowerChange")]
        public IActionResult CancelPowerChange()
        {
            try
            {
                PowerChanger.CancelShutdown();
                return Ok("已取消计算机电源状态更改计划");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}