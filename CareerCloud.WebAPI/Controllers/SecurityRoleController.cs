using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/SecurityRole/v1")]
[ApiController]
public class SecurityRoleController : ControllerBase
{
    private readonly SecurityRoleLogic _logic;

    public SecurityRoleController()
    {
        var respository = new EFGenericRepository<SecurityRolePoco>();
        _logic = new SecurityRoleLogic(respository);
    }

    [HttpGet]
    [Route("SecurityRole/{applicantEducationId}")]
    [ProducesResponseType(typeof(SecurityRolePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetSecurityRole(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("SecurityRole")]
    [ProducesResponseType(typeof(List<SecurityRolePoco>), 200)]
    public ActionResult GetAllSecurityRole()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("SecurityRole")]
    [ProducesResponseType(201)]
    public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetSecurityRole), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("SecurityRole")]
    [ProducesResponseType(200)]
    public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("SecurityRole/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
