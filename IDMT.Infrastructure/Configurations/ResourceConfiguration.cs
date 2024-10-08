﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDMT.Domain.SupportGroups;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IDMT.Domain.Positions;
using IDMT.Domain.Resources;
using IDMT.Domain.PositionsResources;
using IDMT.Domain.Shared;
using IDMT.Domain.Posts;

namespace IDMT.Infrastructure.Configurations
{
	internal sealed class ResourceConfiguration : IEntityTypeConfiguration<Resource>
	{
		public void Configure(EntityTypeBuilder<Resource> builder)
		{
			builder.ToTable("Resources");

			builder.HasKey(resource => resource.Id);

			builder.Property(resource => resource.Name)
						.HasMaxLength(150)
						.HasConversion(name => name.Value, value => new Name(value));

			builder.HasOne<SupportGroup>()
							.WithMany()
							.HasForeignKey(resource => resource.SupportGroupId);


			builder.HasMany(a => a.ResourcePositions)
					.WithOne()
					.HasForeignKey(pr => pr.ResourceId);



		}
	}
}
