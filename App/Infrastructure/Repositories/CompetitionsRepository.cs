using FairDraw.App.Core.Entities;
using FairDraw.App.Infrastructure.Interfaces;

namespace FairDraw.App.Infrastructure.Repositories
{
    public class CompetitionsRepository(DataContext dataContext) : BaseRepository<CompetitionEntity>(dataContext), ICompetitionsRepository
    {
    }
}
