using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/ApplicantEducation/v1")]
[ApiController]
public class ApplicantEducationController : ControllerBase
	{

    private readonly ApplicantEducationLogic _logic;

    public ApplicantEducationController()
    {
        var respository = new EFGenericRepository<ApplicantEducationPoco>();
        _logic = new ApplicantEducationLogic(respository);
    }

    [HttpGet]
    [Route("education/{applicantEducationId}")]
    [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetApplicantEducation(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("education")]
    [ProducesResponseType(typeof(List<ApplicantEducationPoco>), 200)]
    public ActionResult GetAllApplicantEducation()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("education")]
    [ProducesResponseType(201)]
    public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
    {
        _logic.Add(poco);
                return Ok();

        //return CreatedAtAction(nameof(GetApplicantEducation), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("education")]
    [ProducesResponseType(200)]
    public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("education/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
    {


        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);

        
        return Ok();
    }
}

