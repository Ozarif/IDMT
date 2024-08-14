using IDMT.Domain.Abstractions;
using IDMT.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDMT.Domain.ActiveDirectoryAccounts
{
    public sealed class ActiveDirectoryAccount : AuditableEntity
    {
        private ActiveDirectoryAccount()
        {

        }
        internal ActiveDirectoryAccount(Guid id,
                        Guid employeeId,
                        LoginName loginName,
                        Profile profile) : base(id)
        {

            EmployeeId = employeeId;
            LoginName = loginName;
            Profile = profile;
        }

        public Guid EmployeeId { get; private set; }
        public LoginName LoginName { get; private set; }
        public Profile Profile { get; private set; }
        public IsMain IsMain { get; private set; }
        public IsActive IsActive { get; private set; }

        public static ActiveDirectoryAccount Create(Guid EmployeeId, LoginName LoginName, Profile Profile)
        {
            var account = new ActiveDirectoryAccount(Guid.NewGuid(), EmployeeId, LoginName, Profile);
            account.IsActive = new(true);
            account.SetAsMain(false);

            return account;

        }
        public void SetAsMain(bool isMain)
        {
            IsMain = new(isMain);
        }
    }
}
