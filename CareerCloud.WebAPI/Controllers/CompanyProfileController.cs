using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/CompanyProfile/v1")]
[ApiController]
 public class CompanyProfileController : ControllerBase
{
    private readonly CompanyProfileLogic _logic;

    public CompanyProfileController()
    {
        var respository = new EFGenericRepository<CompanyProfilePoco>();
        _logic = new CompanyProfileLogic(respository);
    }

    [HttpGet]
    [Route("CompanyProfile/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyProfile(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyProfile")]
    [ProducesResponseType(typeof(List<CompanyProfilePoco>), 200)]
    public ActionResult GetAllCompanyProfile()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyProfile")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyProfile), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyProfile")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyProfile/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }
}
