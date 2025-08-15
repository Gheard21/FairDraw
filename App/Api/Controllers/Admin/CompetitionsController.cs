using AutoMapper;
using FairDraw.App.Core.Entities;
using FairDraw.App.Core.Requests;
using FairDraw.App.Core.Responses;
using FairDraw.App.Infrastructure.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FairDraw.App.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class CompetitionsController(ICompetitionsRepository competitionRepository, IMapper mapper, IValidator<NewCompetitionRequest> validator) : ControllerBase
    {
        [HttpGet("{competitionId:guid}")]
        public async Task<IActionResult> GetCompetition(Guid competitionId)
        {
            var competitionEntity = await competitionRepository.FindAsync(competitionId);

            if (competitionEntity == null)
                return NotFound();

            var getCompetitionResponse = mapper.Map<GetCompetitionResponse>(competitionEntity);

            return Ok(getCompetitionResponse);
        }

        [HttpPost]
        public async Task<IActionResult> PostCompetition([FromBody] NewCompetitionRequest request)
        {
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var competitionEntity = mapper.Map<CompetitionEntity>(request);

            await competitionRepository.AddAsync(competitionEntity);
            await competitionRepository.SaveChangesAsync();

            var getCompetitionResponse = mapper.Map<GetCompetitionResponse>(competitionEntity);

            return CreatedAtAction(nameof(GetCompetition), new { competitionId = competitionEntity.Id }, getCompetitionResponse);
        }
    }
}
