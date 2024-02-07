using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/SystemLanguageCode/v1")]
[ApiController]
public class SystemLanguageCodeController : ControllerBase
{
    private readonly SystemLanguageCodeLogic _logic;

    public SystemLanguageCodeController()
    {
        var respository = new EFGenericRepository<SystemLanguageCodePoco>();
        _logic = new SystemLanguageCodeLogic(respository);
    }

    [HttpGet]
    [Route("SystemLanguageCode/{SystemLanguageCodeId}")]
    [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetSystemLanguageCode(String id)
    {
        /*var poco = _logic.Get(Guid.Parse(id));*/
        var poco = _logic.Get(id);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("SystemLanguageCode")]
    [ProducesResponseType(typeof(List<SystemLanguageCodePoco>), 200)]
    public ActionResult GetAllSystemLanguageCode()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("SystemLanguageCode")]
    [ProducesResponseType(201)]
    public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
    {
        _logic.Add(poco);
        return Ok();
        //return CreatedAtAction(nameof(GetSystemLanguageCode), new { SystemLanguageCodeId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("SystemLanguageCode")]
    [ProducesResponseType(200)]
    public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("SystemLanguageCode/{SystemLanguageCodeId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }
}
