using IDMT.Application.Features.HrJobs.Shared;
using IDMT.Domain.Employees;
using IDMT.Domain.HrJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Application.Features.Employees.Shared;

internal static class EmployeeMapping
{
	public static EmployeeResponse ToResponse(this Employee source)
	{
		return new()
		{
			Id = source.Id,
			HrId = source.HrId.Value,
			FirstName = source.FirstName.Value,
			FatherName = source.FatherName.Value,
			LastName = source.LastName.Value,
			SpouseName = source.SpouseName.Value,
			HrJobId = source.HrJobId
		};
	}

	public static IQueryable<EmployeeResponse> ToIQueryableResponse(this IQueryable<Employee> source)
	{
		return source.Select(hj => new EmployeeResponse
		{
			Id = hj.Id,
			HrId = hj.HrId.Value,
			FirstName = hj.FirstName.Value,
			FatherName = hj.FatherName.Value,
			LastName = hj.LastName.Value,
			SpouseName = hj.SpouseName.Value,
			HrJobId = hj.HrJobId
		});
	}
}


