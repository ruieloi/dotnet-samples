using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nmbrs.Infrastructure.Data.Repositories.Firebase.Test.Integration.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Firebase.Tests.Integration
{
    [TestCategory("Firebase_Repository")]
    [TestClass]
    public class FirebaseRepositoryTests
    {
        private readonly string FIREBASEAPPSECRET; //Firebase APP Secret
        private readonly string FIREBASESERVER; //Firebase Server url ex: https://xxxxxxxxxxxxxxxx.firebaseio.com

        private string COLLECTIONNAMEKEY = "FirebaseRepositoryTest_";
        private FirebaseRepositoryBase _firebaseRepository;

        [TestInitialize]
        public void Init()
        {
            COLLECTIONNAMEKEY += Guid.NewGuid();
            _firebaseRepository = new FirebaseRepositoryBase(FIREBASESERVER, FIREBASEAPPSECRET, COLLECTIONNAMEKEY);
        }

        [TestMethod]
        public async Task BaseRepository_Add()
        {
            //Reset Data
            await _firebaseRepository.ClearCollection();

            var entityAdd = DomainTest.Create(1, "Test1", "test1@nmbrs.nl");
            await _firebaseRepository.Insert<DomainTest>(entityAdd);

            var result = await _firebaseRepository.GetAll<DomainTest>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            var returnedDomain = result.FirstOrDefault();

            Assert.AreEqual(entityAdd.Name, returnedDomain.Name);
            Assert.AreEqual(entityAdd.Email, returnedDomain.Email);
            Assert.AreEqual(entityAdd.ID, returnedDomain.ID);

            //Reset Data
            await _firebaseRepository.ClearCollection();
        }

        [TestMethod]
        public async Task BaseRepository_Update()
        {
            //Reset Data
            await _firebaseRepository.ClearCollection();

            var entity = DomainTest.Create(1, "Test", "test@nmbrs.nl");

            await _firebaseRepository.Insert<DomainTest>(entity);

            var result = await _firebaseRepository.GetAll<DomainTest>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            var updatedEntity = result.FirstOrDefault();

            updatedEntity.Update("Test 2", "test@gmail.com");

            await _firebaseRepository.Update<DomainTest>(updatedEntity);

            result = await _firebaseRepository.GetAll<DomainTest>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            var returnedEntity = result.FirstOrDefault();

            Assert.AreEqual(updatedEntity.Name, returnedEntity.Name);
            Assert.AreEqual(updatedEntity.Email, returnedEntity.Email);

            //Reset Data
            await _firebaseRepository.ClearCollection();
        }

        [TestMethod]
        public async Task BaseRepository_Delete()
        {
            //Reset Data
            await _firebaseRepository.ClearCollection();

            var entity = DomainTest.Create(1, "Test", "test@nmbrs.nl");

            await _firebaseRepository.Insert<DomainTest>(entity);
            var result = await _firebaseRepository.GetAll<DomainTest>();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());

            await _firebaseRepository.Delete<DomainTest>(result.FirstOrDefault());
            result = await _firebaseRepository.GetAll<DomainTest>();

            Assert.IsNull(result);

            //Reset Data
            await _firebaseRepository.ClearCollection();
        }
    }
}
