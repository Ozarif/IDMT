using IDMT.Domain.Abstractions;
using IDMT.Domain.Employees.Events;
using IDMT.Domain.Shared;
using IDMT.Domain.SupportGroups.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Domain.SupportGroups
{
    public sealed class SupportGroup : Entity
	{

		private SupportGroup(Guid id, Name name, Email email) : base(id)
		{
			Name = name;
			Email = email;
		}
        private SupportGroup()
        {          
        }
	
        public Name Name { get; private set; }
		public Email Email { get; private set; }

		public void Update(Name newName,Email newEmail)
		{
			Name = newName;
			Email = newEmail;
			RaiseDomainEvent(new SupportGroupUpdatedDomainEvent(Id));
		}

		public void Delete()
		{
			IsActive = new IsActive(false);
			RaiseDomainEvent(new SupportGroupDeletedDomainEvent(Id));
		}
		public IsActive IsActive { get; private set; }

		public static SupportGroup Create(Name name, Email email)
		{
			var supportGroup = new SupportGroup(Guid.NewGuid(), name, email);
			supportGroup.IsActive = new IsActive(true);

			supportGroup.RaiseDomainEvent(new SupportGroupCreatedDomainEvent(supportGroup.Id));
			return supportGroup;
		}
	}
}
