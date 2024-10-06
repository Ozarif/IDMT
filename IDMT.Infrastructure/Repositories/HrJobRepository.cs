using IDMT.Application.Models;
using IDMT.Domain.Abstractions;
using IDMT.Domain.Abstractions.Specification;
using IDMT.Domain.HrJobs;
using IDMT.Domain.Shared;
using IDMT.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace IDMT.Infrastructure.Repositories
{
	internal sealed class HrJobRepository : Repository<HrJob>, IHrJobRepository
	{
        public HrJobRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task<PagedList<HrJob>> GetHrJobsAsync(HrJobPaginationParam hrJobParams, CancellationToken cancellationToken = default)
		{
			IQueryable<HrJob> query = DbContext.Set<HrJob>();
			IEnumerable<HrJob> hrJobs;
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
			if (!string.IsNullOrEmpty(hrJobParams.Search))
			{

				query = query.AsEnumerable().Where(x => x.Name.Value.ToLower().Contains(hrJobParams.Search.ToLower())).AsQueryable();
				//hrJobs = query.ToList().Where(c => c.Name.Value.Contains(hrJobParams.Search));
			}
			else{
				hrJobs =query.ToList();
			}
//return PagedData<HrJob>.Create(hrJobs,hrJobParams.PageNumber, hrJobParams.PageSize);
			return PagedList<HrJob>.ToPagedList(query, hrJobParams.PageNumber, hrJobParams.PageSize);
		}
	}
}
