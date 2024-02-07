using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using System.Data.SqlTypes;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/SystemCountryCode/v1")]
[ApiController]
public class SystemCountryCodeController : ControllerBase
{
    private readonly SystemCountryCodeLogic _logic;

    public SystemCountryCodeController()
    {
        var respository = new EFGenericRepository<SystemCountryCodePoco>();
        _logic = new SystemCountryCodeLogic(respository);
    }

    [HttpGet]
    [Route("SystemCountryCode/{applicantEducationId}")]
    [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetSystemCountryCode(String Code)
    {
        var poco = _logic.Get(Code);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("SystemCountryCode")]
    [ProducesResponseType(typeof(List<SystemCountryCodePoco>), 200)]
    public ActionResult GetAllSystemCountryCode()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("SystemCountryCode")]
    [ProducesResponseType(201)]
    public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] poco) { 
    
        _logic.Add(poco);
        return Ok();
        //return CreatedAtAction(nameof(GetSystemCountryCode), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("SystemCountryCode")]
    [ProducesResponseType(200)]
    public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("SystemCountryCode/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }


}
