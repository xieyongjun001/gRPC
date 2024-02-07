using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;


[Route("api/CareerCloud/ApplicantSkill/v1")]
[ApiController]
public class ApplicantSkillController : ControllerBase
{
    private readonly ApplicantSkillLogic _logic;

    public ApplicantSkillController()
    {
        var respository = new EFGenericRepository<ApplicantSkillPoco>();
        _logic = new ApplicantSkillLogic(respository);
    }

    [HttpGet]
    [Route("ApplicantSkill/{applicantEducationId}")]
    [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetApplicantSkill(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("ApplicantSkill")]
    [ProducesResponseType(typeof(List<ApplicantSkillPoco>), 200)]
    public ActionResult GetAllApplicantSkill()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("ApplicantSkill")]
    [ProducesResponseType(201)]
    public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetApplicantSkill), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("ApplicantSkill")]
    [ProducesResponseType(200)]
    public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("ApplicantSkill/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }
}
