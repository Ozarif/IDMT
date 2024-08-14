using IDMT.Domain.Abstractions;
using IDMT.Domain.Shared;
using IDMT.Domain.SupportGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Domain.HrJobs
{
	public class HrJob : Entity
	{
		private HrJob(Guid id, Name name) : base(id)
		{
			Name = name;
		}
		private HrJob()
		{
		}
		public Name Name { get; private set; }

		public void Update(Name newName)
		{
			Name = newName;
		}
		public static HrJob Create(Name name)
		{
			return new HrJob(Guid.NewGuid(), name);
		}
	}
}
