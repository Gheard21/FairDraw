using AutoMapper;
using FairDraw.App.Api.Controllers.Admin;
using FairDraw.App.Core.Entities;
using FairDraw.App.Core.Requests;
using FairDraw.App.Core.Responses;
using FairDraw.App.Infrastructure.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FairDraw.Tests.Api.Controllers.Admin
{
    public class CompetitionsControllerTests
    {
        private readonly Mock<ICompetitionsRepository> _competitionsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IValidator<NewCompetitionRequest>> _validatorMock;
        private readonly CompetitionsController _target;

        public CompetitionsControllerTests()
        {
            _competitionsRepositoryMock = new Mock<ICompetitionsRepository>();
            _mapperMock = new Mock<IMapper>();
            _validatorMock = new Mock<IValidator<NewCompetitionRequest>>();
            _target = new CompetitionsController(
                _competitionsRepositoryMock.Object, 
                _mapperMock.Object, 
                _validatorMock.Object);
        }

        #region GetCompetition
        [Fact]
        public async Task GetCompetition_ReturnsOk_WhenCompetitionExists()
        {
            // Arrange
            var competitionId = Guid.NewGuid();
            var entity = new CompetitionEntity { Id = competitionId, Title = "Test" };
            var response = new GetCompetitionResponse { Id = competitionId, Title = "Test" };
            _competitionsRepositoryMock.Setup(r => r.FindAsync(competitionId)).ReturnsAsync(entity);
            _mapperMock.Setup(m => m.Map<GetCompetitionResponse>(entity)).Returns(response);

            // Act
            var result = await _target.GetCompetition(competitionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async Task GetCompetition_ReturnsNotFound_WhenCompetitionDoesNotExist()
        {
            // Arrange
            var competitionId = Guid.NewGuid();
            _competitionsRepositoryMock.Setup(r => r.FindAsync(competitionId)).ReturnsAsync((CompetitionEntity?)null);

            // Act
            var result = await _target.GetCompetition(competitionId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region PostCompetition
        [Fact]
        public async Task PostCompetition_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var request = new NewCompetitionRequest { Title = "" };
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Title", "Required") });
            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);

            // Act
            var result = await _target.PostCompetition(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(validationResult.Errors, badRequest.Value);
        }

        [Fact]
        public async Task PostCompetition_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var request = new NewCompetitionRequest { Title = "Test" };
            var entity = new CompetitionEntity { Id = Guid.NewGuid(), Title = "Test" };
            var response = new GetCompetitionResponse { Id = entity.Id, Title = entity.Title };
            var validationResult = new ValidationResult();
            _validatorMock.Setup(v => v.ValidateAsync(request, default)).ReturnsAsync(validationResult);
            _mapperMock.Setup(m => m.Map<CompetitionEntity>(request)).Returns(entity);
            _mapperMock.Setup(m => m.Map<GetCompetitionResponse>(entity)).Returns(response);

            // Act
            var result = await _target.PostCompetition(request);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_target.GetCompetition), createdResult.ActionName);
            Assert.NotNull(createdResult.RouteValues);
            Assert.True(createdResult.RouteValues!.ContainsKey("competitionId"));
            Assert.Equal(entity.Id, createdResult.RouteValues["competitionId"]);
            Assert.Equal(response, createdResult.Value);
            _competitionsRepositoryMock.Verify(r => r.AddAsync(entity), Times.Once);
            _competitionsRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        #endregion
    }
}
