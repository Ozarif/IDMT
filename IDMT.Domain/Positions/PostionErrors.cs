using IDMT.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Domain.Positions
{
	public class PostionErrors
	{
		public static readonly Error AlreadyAssigned = new(
		"Position.AlreadyAssigned",
		"This application is already assigned to the position.");
	}
}
