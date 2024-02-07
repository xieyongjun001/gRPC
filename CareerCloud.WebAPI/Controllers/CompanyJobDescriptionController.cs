using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/CompanyJobsDescription/v1")]
[ApiController]
public class CompanyJobsDescriptionController : ControllerBase
{

    private readonly CompanyJobDescriptionLogic _logic;

    public CompanyJobsDescriptionController()
    {
        var respository = new EFGenericRepository<CompanyJobDescriptionPoco>();
        _logic = new CompanyJobDescriptionLogic(respository);
    }

    [HttpGet]
    [Route("CompanyJobsDescription/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyJobsDescription(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyJobsDescription")]
    [ProducesResponseType(typeof(List<CompanyJobDescriptionPoco>), 200)]
    public ActionResult GetAllCompanyJobsDescription()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyJobsDescription")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyJobsDescription), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyJobsDescription")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyJobsDescription/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
