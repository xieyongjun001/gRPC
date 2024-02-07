using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers;

[Route("api/CareerCloud/ompanyJobSkill/v1")]
[ApiController]
public class CompanyJobSkillController : ControllerBase
{
    private readonly CompanyJobSkillLogic _logic;

    public CompanyJobSkillController()
    {
        var respository = new EFGenericRepository<CompanyJobSkillPoco>();
        _logic = new CompanyJobSkillLogic(respository);
    }

    [HttpGet]
    [Route("CompanyJobSkill/{applicantEducationId}")]
    [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
    [ProducesResponseType(404)]
    public ActionResult GetCompanyJobSkill(Guid applicantEducationId)
    {
        var poco = _logic.Get(applicantEducationId);

        if (poco == null)
        {
            return NotFound();
        }

        return Ok(poco);
    }

    [HttpGet]
    [Route("CompanyJobSkill")]
    [ProducesResponseType(typeof(List<CompanyJobSkillPoco>), 200)]
    public ActionResult GetAllCompanyJobSkill()
    {
        var list = _logic.GetAll();
        return Ok(list);
    }

    [HttpPost]
    [Route("CompanyJobSkill")]
    [ProducesResponseType(201)]
    public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
    {
        _logic.Add(poco);
        return Ok();

        //return CreatedAtAction(nameof(GetCompanyJobSkill), new { applicantEducationId = poco.Id }, poco);
    }

    [HttpPut]
    [Route("CompanyJobSkill")]
    [ProducesResponseType(200)]
    public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
    {
        _logic.Update(poco);
        return Ok();
    }

    [HttpDelete]
    [Route("CompanyJobSkill/{applicantEducationId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
    {
        if (poco == null || poco.Length == 0)
        {
            return BadRequest("Delete poco cannot be null or empty.");
        }


        _logic.Delete(poco);


        return Ok();
    }

}
