using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/SecurityLoginsRole/v1")]
[ApiController]
public class SecurityLoginsRoleController : ControllerBase
{
    private readonly SecurityLoginsRoleLogic _logic;

    public SecurityLoginsRoleController()
    {
        var respository = new EFGenericRepository<SecurityLoginsRolePoco>();
        _logic = new SecurityLoginsRoleLogic(respository);
    }

    [HttpGet]
    [Route("SecurityLoginsRole/{applicantEducationId}")]
    [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetSecurityLoginsRole(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("SecurityLoginsRole")]
    [ProducesResponseType(typeof(List<SecurityLoginsRolePoco>), 200)]
    public ActionResult GetAllSecurityLoginsRole()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("SecurityLoginsRole")]
    [ProducesResponseType(201)]
    public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetSecurityLoginsRole), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("SecurityLoginsRole")]
    [ProducesResponseType(200)]
    public ActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("SecurityLoginsRole/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
