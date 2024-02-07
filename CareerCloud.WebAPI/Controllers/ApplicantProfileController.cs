using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/ApplicantProfile/v1")]
[ApiController]
public class ApplicantProfileController : ControllerBase
{
    private readonly ApplicantProfileLogic _logic;

    public ApplicantProfileController()
    {
        var respository = new EFGenericRepository<ApplicantProfilePoco>();
        _logic = new ApplicantProfileLogic(respository);
    }

    [HttpGet]
    [Route("ApplicantProfile/{applicantEducationId}")]
    [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetApplicantProfile(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("ApplicantProfile")]
    [ProducesResponseType(typeof(List<ApplicantProfilePoco>), 200)]
    public ActionResult GetAllApplicantProfile()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("ApplicantProfile")]
    [ProducesResponseType(201)]
    public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetApplicantProfile), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("ApplicantProfile")]
    [ProducesResponseType(200)]
    public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("ApplicantProfile/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }


}
