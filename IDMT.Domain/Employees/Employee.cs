using IDMT.Domain.Abstractions;
using IDMT.Domain.ActiveDirectoryAccounts;
using IDMT.Domain.Employees.Events;
using IDMT.Domain.HrJobs;

namespace IDMT.Domain.Employees
{
	public sealed class Employee : Entity
	{
		private readonly HashSet<ActiveDirectoryAccount> _accounts = new();
		private Employee(Guid Id, 
						HrId hrId, 
						FirstName firstName, 
						FatherName fatherName, 
						LastName lastName, 
						SpouseName? spouseName,
						Guid hrJobId) : base (Id) { 
			HrId = hrId;
			FirstName = firstName;
			FatherName = fatherName;
			LastName = lastName;
			SpouseName = spouseName;
			HrJobId = hrJobId;
		
		}
        public HrId HrId { get; private set; }
		public FirstName FirstName { get; private set; }
		public FatherName FatherName { get; private set; }
		public LastName LastName { get; private set; }
		public SpouseName? SpouseName { get; private set; }
		public Guid HrJobId {  get; private set; }
		public Status Status { get; private set; }
		public DateTime? CreatedOn { get; private set; }
		public DateTime? UpdatedOn { get; private set; }
		public DateTime? TerminationDate { get; private set; }
		public DateTime? LastPostDate { get; internal set; }
		public FullName FullName => new($"{FirstName} {FatherName} {LastName}");

		public IReadOnlyCollection<ActiveDirectoryAccount> Accounts => _accounts.ToList();

		public static Employee Create(HrId hrId,
						FirstName firstName,
						FatherName fatherName,
						LastName lastName,
						SpouseName? spouseName,
						HrJob hrJob)
		{
			var employee =  new Employee(Guid.NewGuid(),hrId,firstName,fatherName,lastName, spouseName, hrJob.Id);
			employee.CreatedOn = employee.UpdatedOn = DateTime.UtcNow;
			employee.Status = Status.ACTIVE;
			employee.RaiseDomainEvent(new EmployeeCreatedDomainEvent(employee.Id));
			return employee;
			
		}

		public Result Update(HrId hrId,
						FirstName firstName,
						FatherName fatherName,
						LastName lastName,
						SpouseName? spouseName,
						HrJob hrJob)
		{
			HrId = hrId;
			FirstName = firstName;
			LastName = lastName;
			FatherName = fatherName;
			SpouseName = spouseName;
			HrJobId = hrJob.Id;

			return Result.Success();
		}

		public Result AddActiveDirectoryAccount(LoginName LoginName, Profile Profile, bool SetAsMainAccount)
		{
			var newAccount = ActiveDirectoryAccount.Create(Id, LoginName, Profile);
			newAccount.SetAsMain(SetAsMainAccount);


			_accounts.Add(newAccount);
			return Result.Success();
		}

		public Result Terminate()
		{
			if (Status is not Status.TERMINATED)
			{
				Status = Status.TERMINATED;
				TerminationDate = UpdatedOn = DateTime.UtcNow;
				RaiseDomainEvent(new EmployeeTerminatedDomainEvent(Id));
			}
			return Result.Success();
		}
	}
}
