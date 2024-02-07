using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/pplicantJobApplication/v1")]
[ApiController]
public class ApplicantJobApplicationController : ControllerBase
{
    private readonly ApplicantJobApplicationLogic _logic;

    public ApplicantJobApplicationController()
    {
        var respository = new EFGenericRepository<ApplicantJobApplicationPoco>();
        _logic = new ApplicantJobApplicationLogic(respository);
    }

    [HttpGet]
    [Route("ApplicantJobApplication/{applicantEducationId}")]
    [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetApplicantJobApplication(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("ApplicantJobApplication")]
    [ProducesResponseType(typeof(List<ApplicantJobApplicationPoco>), 200)]
    public ActionResult GetAllApplicantJobApplication()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("ApplicantJobApplication")]
    [ProducesResponseType(201)]
    public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetApplicantJobApplication), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("ApplicantJobApplication")]
    [ProducesResponseType(200)]
    public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("ApplicantJobApplication/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
