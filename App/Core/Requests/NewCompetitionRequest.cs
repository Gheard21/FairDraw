using System.ComponentModel.DataAnnotations;

namespace FairDraw.App.Core.Requests
{
    public record NewCompetitionRequest
    {
        public required string Title { get; set; }
    };
}
