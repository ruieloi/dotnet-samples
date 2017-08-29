using Data.Firebase.Interfaces;

namespace Nmbrs.Infrastructure.Data.Repositories.Firebase.Test.Integration.Models
{
    public class DomainTest: IFirebaseEntity
    {
        public string Key { get; set; }
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public static DomainTest Create(int id, string name, string email)
        {
            return new DomainTest
            {
                ID = id,
                Name = name,
                Email = email
            };
        }

        public void Update(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }
    }
}
