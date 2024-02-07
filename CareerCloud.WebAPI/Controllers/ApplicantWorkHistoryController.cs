using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/ApplicantWorkHistory/v1")]
[ApiController]
public class ApplicantWorkHistoryController : ControllerBase
{
    private readonly ApplicantWorkHistoryLogic _logic;

    public ApplicantWorkHistoryController()
    {
        var respository = new EFGenericRepository<ApplicantWorkHistoryPoco>();
        _logic = new ApplicantWorkHistoryLogic(respository);
    }

    [HttpGet]
    [Route("ApplicantWorkHistory/{applicantEducationId}")]
    [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetApplicantWorkHistory(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("ApplicantWorkHistory")]
    [ProducesResponseType(typeof(List<ApplicantWorkHistoryPoco>), 200)]
    public ActionResult GetAllApplicantWorkHistory()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("ApplicantWorkHistory")]
    [ProducesResponseType(201)]
    public ActionResult PostApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetApplicantWorkHistory), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("ApplicantWorkHistory")]
    [ProducesResponseType(200)]
    public ActionResult PutApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("ApplicantWorkHistory/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
