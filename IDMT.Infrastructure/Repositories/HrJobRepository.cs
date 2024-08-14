using IDMT.Domain.Abstractions;
using IDMT.Domain.Abstractions.Specification;
using IDMT.Domain.HrJobs;
using IDMT.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace IDMT.Infrastructure.Repositories
{
	internal sealed class HrJobRepository : Repository<HrJob>, IHrJobRepository
	{
        public HrJobRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task<PagedList<HrJob>> GetHrJobsAsync(HrJobPaginationParam hrJobParams, CancellationToken cancellationToken = default)
		{
			var query = await FindAllAsync();

			if (!string.IsNullOrEmpty(hrJobParams.Search))
			{
				query = query.Where(x =>
				(string.IsNullOrEmpty(hrJobParams.Search) || EF.Functions.Like(x.Name.Value.ToLower(), $"%{hrJobParams.Search}%")));
			}

			if (!string.IsNullOrEmpty(hrJobParams.SortColumn) && !string.IsNullOrEmpty(hrJobParams.SortDirection))
			{
				var sortExpression = await HelperFunctions.GetSortExpressionAsync<HrJob>(hrJobParams.SortColumn);

				if (hrJobParams.SortDirection.ToLower() == OrderByDirection.Ascending.Code)
				{
					query = query.OrderBy(sortExpression);
				}
				else
				{
					query = query.OrderByDescending(sortExpression);
				}
			}


			return PagedList<HrJob>.ToPagedList(query, hrJobParams.PageNumber, hrJobParams.PageSize);
		}
	}
}
