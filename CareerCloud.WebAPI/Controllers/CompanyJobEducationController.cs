using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/CompanyJobEducation/v1")]
[ApiController]
public class CompanyJobEducationController : ControllerBase
{
    private readonly CompanyJobEducationLogic _logic;

    public CompanyJobEducationController()
    {
        var respository = new EFGenericRepository<CompanyJobEducationPoco>();
        _logic = new CompanyJobEducationLogic(respository);
    }

    [HttpGet]
    [Route("CompanyJobEducation/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyJobEducation(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyJobEducation")]
    [ProducesResponseType(typeof(List<CompanyJobEducationPoco>), 200)]
    public ActionResult GetAllCompanyJobEducation()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyJobEducation")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyJobEducation), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyJobEducation")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyJobEducation/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
