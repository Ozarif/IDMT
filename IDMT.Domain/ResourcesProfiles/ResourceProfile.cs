using IDMT.Domain.Abstractions;
using IDMT.Domain.Resources;
using IDMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Domain.ResourcesProfiles
{
    public sealed class ResourceProfile : Entity
    {
        private ResourceProfile(Guid id, Name name, Guid resourceId, IsActive isActive)
            : base(id)
        {
            Name = name;
            ResourceId = resourceId;
            IsActive = isActive;
        }
        private ResourceProfile()
        {
            
        }
        public Name Name { get; private set; }
        public Guid ResourceId { get; private set; }
        public IsActive IsActive { get; private set; }
        public void Deactivate()
        {
            IsActive = new IsActive(false);
        }
        public static ResourceProfile Create(Guid id, Name name, Resource resource)
        {
            return new ResourceProfile(Guid.NewGuid(), name, resource.Id, new IsActive(true));
        }
    }
}
