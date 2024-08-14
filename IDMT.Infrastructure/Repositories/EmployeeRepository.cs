using IDMT.Domain.Abstractions;
using IDMT.Domain.Abstractions.Specification;
using IDMT.Domain.Employees;
using IDMT.Domain.HrJobs;
using IDMT.Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Infrastructure.Repositories
{
	internal sealed class EmployeeRepository : Repository<Employee>, IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

		public async Task<PagedList<Employee>> GetEmployeesAsync(EmployeePaginationParam employeeParams, CancellationToken cancellationToken = default)
		{
			var query = await FindAllAsync();

			if (!string.IsNullOrEmpty(employeeParams.Search))
			{
				query = query.Where(x =>
				(string.IsNullOrEmpty(employeeParams.Search) || x.HrId.Value.ToLower().Contains(employeeParams.Search)
				|| x.FirstName.Value.ToLower().Contains(employeeParams.Search)
				|| x.FatherName.Value.ToLower().Contains(employeeParams.Search)
				|| x.LastName.Value.ToLower().Contains(employeeParams.Search)
				|| x.SpouseName.Value.ToLower().Contains(employeeParams.Search)));
			}

			if (!string.IsNullOrEmpty(employeeParams.SortColumn) && !string.IsNullOrEmpty(employeeParams.SortDirection))
			{
				var sortExpression = await HelperFunctions.GetSortExpressionAsync<Employee>(employeeParams.SortColumn);

				if (employeeParams.SortDirection.ToLower() == OrderByDirection.Ascending.Code)
				{
					query = query.OrderBy(sortExpression);
				}
				else
				{
					query = query.OrderByDescending(sortExpression);
				}
			}


			return PagedList<Employee>.ToPagedList(query, employeeParams.PageNumber, employeeParams.PageSize);
		}

		public Task<IReadOnlyList<Employee>> GetEmployeesAsync(Expression<Func<Employee, bool>> filter, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
