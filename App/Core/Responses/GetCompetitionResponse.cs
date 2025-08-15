namespace FairDraw.App.Core.Responses
{
    public record GetCompetitionResponse
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
    }
}
