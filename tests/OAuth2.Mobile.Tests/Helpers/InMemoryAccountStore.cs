namespace StudioDonder.OAuth2.Mobile.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using Xamarin.Auth;

    internal class InMemoryAccountStore : AccountStore
    {
        private readonly Dictionary<string, IList<Account>> servicesWithAccounts;

        public InMemoryAccountStore()
        {
            this.servicesWithAccounts = new Dictionary<string, IList<Account>>();
        }

        public override IEnumerable<Account> FindAccountsForService(string serviceId)
        {
            if (this.servicesWithAccounts.ContainsKey(serviceId))
            {
                return this.servicesWithAccounts[serviceId];
            }

            return new List<Account>();
        }

        public override void Save(Account account, string serviceId)
        {
            if (!this.servicesWithAccounts.ContainsKey(serviceId))
            {
                this.servicesWithAccounts[serviceId] = new List<Account>();
            }

            var existingAccount = this.servicesWithAccounts[serviceId].FirstOrDefault(a => a.Username == account.Username);

            if (existingAccount != null)
            {
                this.servicesWithAccounts[serviceId].Remove(existingAccount);
            }

            this.servicesWithAccounts[serviceId].Add(account);
        }
    }
}