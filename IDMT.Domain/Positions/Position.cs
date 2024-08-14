using IDMT.Domain.Abstractions;
using IDMT.Domain.PositionsResources;
using IDMT.Domain.Resources;
using IDMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IDMT.Domain.Positions
{
    public sealed class Position : Entity
	{
		private readonly HashSet<PositionResource> _positionResources = new();
        private Position(Guid id, Name name) : base(id)
		{
			Name = name;
		}
		private Position() { }

		public Name Name { get; private set; }
		public IsActive IsActive { get; private set; }
		public IReadOnlyCollection<PositionResource> PositionResources => _positionResources.ToList();

		public static Position Create(Name name)
		{
			var position = new Position(Guid.NewGuid(), name);
			position.IsActive = new IsActive(true);
			return position;
		}
		public Result AddResource(Resource resource)
		{
			if (_positionResources.Any(pa => pa.ResourceId == resource.Id))
			{
				return Result.Failure(PositionErrors.AlreadyAssigned);
			}

			var positionResource = new PositionResource(Id,resource.Id);
			_positionResources.Add(positionResource);

			return Result.Success();
		}
		public void RemoveApplication(Resource resource)
		{
			var positionResource = _positionResources.FirstOrDefault(pa => pa.PositionId == Id && pa.ResourceId == resource.Id);
			if (positionResource != null)
			{
				_positionResources.Remove(positionResource);
			}
		}
	}
}
