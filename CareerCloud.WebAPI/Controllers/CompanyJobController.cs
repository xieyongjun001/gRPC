using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/CompanyJob/v1")]
[ApiController]
public class CompanyJobController : ControllerBase
{
    private readonly CompanyJobLogic _logic;

    public CompanyJobController()
    {
        var respository = new EFGenericRepository<CompanyJobPoco>();
        _logic = new CompanyJobLogic(respository);
    }

    [HttpGet]
    [Route("CompanyJob/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyJobPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyJob(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyJob")]
    [ProducesResponseType(typeof(List<CompanyJobPoco>), 200)]
    public ActionResult GetAllCompanyJob()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyJob")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyJob), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyJob")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyJob/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }
}
