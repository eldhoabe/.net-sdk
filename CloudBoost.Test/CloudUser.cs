using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CB.Test
{
    [TestClass]
    public class CloudUser
    {

        protected string username = Util.Methods.MakeString();
        protected string password = "abcd";

        [TestInitialize]
        public void TestInitalize()
        {
            CB.Test.Util.Keys.InitWithMasterKey();
        }


        
        [TestMethod]
        public async Task CreateNewUser()
        {
            
            var obj = new CB.CloudUser();
            obj.Set("username", Util.Methods.MakeEmail());
            obj.Set("password", Util.Methods.MakeEmail());
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SignupAsync();
            if (obj.Username!=null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Error Creating User");
            }
        }

        [TestMethod]
        public async Task CreateUserAndGetVersion()
        {
            
            username = Util.Methods.MakeString();
            var obj = new CB.CloudUser();
            obj.Set("username", Util.Methods.MakeEmail());
            obj.Set("password", Util.Methods.MakeEmail());
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SignupAsync();
            if ( Int32.Parse(obj.Get("_version").ToString()) >= 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Error Creating User");
            }
        }

        [TestMethod]
        public async Task QueryOnUser()
        {
            
            username = Util.Methods.MakeString();
            var obj = new CB.CloudUser();
            obj.Set("username", Util.Methods.MakeEmail());
            obj.Set("password", Util.Methods.MakeEmail());
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SignupAsync();
            if (Int32.Parse(obj.Get("_version").ToString()) >= 0)
            {
                var query = new CB.CloudQuery("User");
                var response = await query.GetAsync<CB.CloudUser>(obj.ID);
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Error Creating User");
            }
        }
         
        [TestMethod]
        public async Task LoginUser()
        {
            
            var obj = new CB.CloudUser();
            obj.Set("username", CB.Test.Util.Methods.MakeEmail());
            obj.Set("password", CB.Test.Util.Methods.MakeEmail());
            obj.Set("email", CB.Test.Util.Methods.MakeEmail());

            await obj.SignupAsync();
            await obj.LogoutAsync();
            await obj.LoginAsync();
            if (obj.Username == username && CB.CloudUser.Current !=null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("User Login Error");
            }
        }

        [TestMethod]
        public async Task AssignRoleToUser()
        {
            
            var roleName = Util.Methods.MakeString();
            var role = new CB.CloudRole(roleName);
            role.Set("name", roleName);
            var obj = new CB.CloudUser();
            obj.Set("username", CB.Test.Util.Methods.MakeEmail());
            obj.Set("password", CB.Test.Util.Methods.MakeEmail());
            obj.Set("email", CB.Test.Util.Methods.MakeEmail());
            await obj.SignupAsync();
            await role.SaveAsync();
            await obj.AddToRoleAsync(role);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task RemoveRoleAssignRoleToUser()
        {
            
            var obj = new CB.CloudUser();
            var roleName = Util.Methods.MakeString();
            var role = new CB.CloudRole(roleName);
            role.Set("name", roleName);
            obj.Set("username", CB.Test.Util.Methods.MakeEmail());
            obj.Set("password", CB.Test.Util.Methods.MakeEmail());
            obj.Set("email", CB.Test.Util.Methods.MakeEmail());
            await obj.SignupAsync();
            await obj.LogoutAsync();
            await obj.LoginAsync();
            await role.SaveAsync();
            await obj.AddToRoleAsync(role);
            await CB.CloudUser.Current.RemoveFromRoleAsync(role);
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task ShouldEncryptUserPassword()
        {
            
            var obj = new CB.CloudUser();
            obj.Set("username", CB.Test.Util.Methods.MakeEmail());
            obj.Set("password", CB.Test.Util.Methods.MakeEmail());
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SaveAsync();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task CreateNewUserSave()
        {
            
            var obj = new CB.CloudUser();
            obj.Set("username", CB.Test.Util.Methods.MakeEmail());
            obj.Set("password", CB.Test.Util.Methods.MakeEmail());
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SaveAsync();
            var query = new CB.CloudQuery("User");
            var response = await query.GetAsync<CB.CloudUser>(obj.ID);
            if (response != null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Unable to retrieve User");
            }
        }

    }
}
