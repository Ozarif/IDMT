using IDMT.Domain.Abstractions.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Domain.SupportGroups.Specification
{
    public class SupportGroupsListSpecification : BaseSpecification<SupportGroup>
	{
		public SupportGroupsListSpecification(SupportGroupPaginationParam supportGroupSpecificationParams):base() {
			
			IsSplitQuery = false;
			
			ApplyPaging(supportGroupSpecificationParams.PageSize * (supportGroupSpecificationParams.PageNumber - 1), supportGroupSpecificationParams.PageSize);
		}
	}
}
