using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/ApplicantResume/v1")]
[ApiController]
public class ApplicantResumeController : ControllerBase
{
    private readonly ApplicantResumeLogic _logic;

    public ApplicantResumeController()
    {
        var respository = new EFGenericRepository<ApplicantResumePoco>();
        _logic = new ApplicantResumeLogic(respository);
    }

    [HttpGet]
    [Route("ApplicantResume/{applicantEducationId}")]
    [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetApplicantResume(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("ApplicantResume")]
    [ProducesResponseType(typeof(List<ApplicantResumePoco>), 200)]
    public ActionResult GetAllApplicantResume()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("ApplicantResume")]
    [ProducesResponseType(201)]
    public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetApplicantResume), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("ApplicantResume")]
    [ProducesResponseType(200)]
    public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("ApplicantResume/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }


}
