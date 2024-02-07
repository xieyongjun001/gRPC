using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/CompanyDescription/v1")]
[ApiController]
public class CompanyDescriptionController : ControllerBase
{
    private readonly CompanyDescriptionLogic _logic;

    public CompanyDescriptionController()
    {
        var respository = new EFGenericRepository<CompanyDescriptionPoco>();
        _logic = new CompanyDescriptionLogic(respository);
    }

    [HttpGet]
    [Route("CompanyDescription/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyDescription(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyDescription")]
    [ProducesResponseType(typeof(List<CompanyDescriptionPoco>), 200)]
    public ActionResult GetAllCompanyDescription()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyDescription")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyDescription), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyDescription")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyDescription/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
