using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/SecurityLoginLog/v1")]
[ApiController]
public class SecurityLoginsLogController : ControllerBase
{
    private readonly SecurityLoginsLogLogic _logic;

    public SecurityLoginsLogController()
    {
        var respository = new EFGenericRepository<SecurityLoginsLogPoco>();
        _logic = new SecurityLoginsLogLogic(respository);
    }

    [HttpGet]
    [Route("SecurityLoginLog/{applicantEducationId}")]
    [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetSecurityLoginLog(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("SecurityLoginLog")]
    [ProducesResponseType(typeof(List<SecurityLoginsLogPoco>), 200)]
    public ActionResult GetAllSecurityLoginLog()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("SecurityLoginLog")]
    [ProducesResponseType(201)]
    public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();
        //return CreatedAtAction(nameof(GetSecurityLoginLog), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("SecurityLoginLog")]
    [ProducesResponseType(200)]
    public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("SecurityLoginLog/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }
}

