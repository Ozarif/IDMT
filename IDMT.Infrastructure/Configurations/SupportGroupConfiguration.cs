﻿using IDMT.Domain.Shared;
using IDMT.Domain.SupportGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Infrastructure.Configurations
{
	internal sealed class SupportGroupConfiguration : IEntityTypeConfiguration<SupportGroup>
	{
		public void Configure(EntityTypeBuilder<SupportGroup> builder)
		{
			builder.ToTable("SupportGroups");
			builder.HasKey(supportGroup => supportGroup.Id);

			//builder.Property(supportGroup => supportGroup.Id).HasConversion(
			//	supportGroupId => supportGroupId.Value, 
			//	Value => new SupportGroupId(Value));

			builder.Property(supportGroup => supportGroup.Name)
						.HasMaxLength(150)
						.HasConversion(name => name.Value, value => new Name(value))
						.IsRequired(); 

			builder.Property(supportGroup => supportGroup.Email)
						.HasMaxLength(100)
						.HasConversion(email => email.Value, value => new Domain.SupportGroups.Email(value))
						.IsRequired();
			builder.Property(supportGroup => supportGroup.IsActive)
						.HasMaxLength(150)
						.HasConversion(isActive => isActive.Value, value => new IsActive(value));

			builder.HasIndex(user => user.Name).IsUnique();
			builder.HasIndex(user => user.Email).IsUnique();

		}

	}
}
