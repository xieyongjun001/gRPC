using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/CompanyLocation/v1")]
[ApiController]
public class CompanyLocationController : ControllerBase
{
    private readonly CompanyLocationLogic _logic;

    public CompanyLocationController()
    {
        var respository = new EFGenericRepository<CompanyLocationPoco>();
        _logic = new CompanyLocationLogic(respository);
    }

    [HttpGet]
    [Route("CompanyLocation/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyLocation(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyLocation")]
    [ProducesResponseType(typeof(List<CompanyLocationPoco>), 200)]
    public ActionResult GetAllCompanyLocation()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyLocation")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyLocation), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyLocation")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyLocation/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
