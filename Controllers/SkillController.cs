using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.DTO;
using MyApi.Entities;
using System.Net;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly juliaContext DBContext;

        public SkillController(juliaContext dBContext)
        {
            DBContext = dBContext;
        }

        [HttpGet("GetSkills")]
        public async Task<ActionResult<List<SkillDTO>>> Get()
        {
            var List = await DBContext.Skills.Select(
                s => new SkillDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetSkillById")]
        public async Task<ActionResult<SkillDTO>> GetSkillById(Guid Id)
        {
            SkillDTO Skill = await DBContext.Skills.Select(s => new SkillDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description= s.Description
            }).FirstOrDefaultAsync(s => s.Id == Id);

            if (Skill == null)
            {
                return NotFound();
            } else
            {
                return Skill;
            }
        }

        [HttpPost("InsertSkill")]
        public async Task<HttpStatusCode> InsertSkill(SkillDTO Skill)
        {
            var entity = new Skill()
            {
                Name = Skill.Name,
                Description = Skill.Description
            };
            DBContext.Skills.Add(entity);

            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpDelete("DeleteSkill/{Id}")]
        public async Task <HttpStatusCode> DeleteSkill(Guid Id)
        {
            var entity = new Skill()
            {
                Id = Id
            };
            DBContext.Skills.Attach(entity);
            DBContext.Skills.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}
