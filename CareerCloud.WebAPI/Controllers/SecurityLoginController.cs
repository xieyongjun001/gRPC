using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/SecurityLogin/v1")]
[ApiController]

public class SecurityLoginController : ControllerBase
{

    private readonly SecurityLoginLogic _logic;

    public SecurityLoginController()
    {
        var respository = new EFGenericRepository<SecurityLoginPoco>();
        _logic = new SecurityLoginLogic(respository);
    }

    [HttpGet]
    [Route("SecurityLogin/{applicantEducationId}")]
    [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetSecurityLogin(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("SecurityLogin")]
    [ProducesResponseType(typeof(List<SecurityLoginPoco>), 200)]
    public ActionResult GetAllSecurityLogin()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("SecurityLogin")]
    [ProducesResponseType(201)]
    public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetSecurityLogin), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("SecurityLogin")]
    [ProducesResponseType(200)]
    public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("SecurityLogin/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }


}