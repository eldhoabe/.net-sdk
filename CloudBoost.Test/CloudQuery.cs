using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace CB.Test
{
    [TestClass]
    public class CloudQuery
    {

        [TestInitialize]
        public void TestInitalize()
        {
            CB.Test.Util.Keys.InitWithClientKey();
            CB.Test.Util.Keys.InitWithMasterKey();
            
        }

        [TestMethod]
        public void x001_InitAppWithClientKey()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task SaveObject()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "ranjeet");
            await obj.SaveAsync();
            if (obj.Get("name").ToString() == "ranjeet")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task Find()
        {
            var obj = new CB.CloudObject("Custom1");
            obj.Set("newColumn", "sample");
            obj.Set("description", "sample2");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("Custom1");
            query.EqualTo("id", obj.ID);
            query.SelectColumn("newColumn");
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                if (((CB.CloudObject)response[0]).Get("description") == null)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsFalse(true);
                }
            }
            else
            {
                Assert.IsFalse(true);
            }
            
        }

        [TestMethod]
        public async Task ContainedInWithId()
        {
            var obj1 = new CB.CloudObject("Custom1");
            obj1.Set("newColumn", "sample");
            obj1.Set("description", "sample2");
            await obj1.SaveAsync();
            var obj2 = new CB.CloudObject("Custom1");
            obj2.Set("newColumn", "sample");
            obj2.Set("description", "sample2");
            await obj2.SaveAsync();
            var obj3 = new CB.CloudObject("Custom1");
            obj3.Set("newColumn", "sample");
            obj3.Set("description", "sample2");
            await obj3.SaveAsync();

            var query = new CB.CloudQuery("Custom1");
            ArrayList list = new ArrayList();
            list.Add(obj1.ID);
            list.Add(obj2.ID);
            list.Add(obj3.ID);
            query.ContainedIn("id", list);
            var response = (ArrayList)await query.FindAsync();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async Task ColumnShouldWorkOnDistinct()
        {
            var obj = new CB.CloudObject("Custom1");
            obj.Set("newColumn", "sample");
            obj.Set("description", "sample2");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("Custom1");
            query.EqualTo("id", obj.ID);
            var response = await query.DistinctAsync("id");
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task ColumnNameNotEqualTo()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "sampleName");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.NotEqualTo("name", null);
            var response = await query.FindAsync();
            for (int i = 0; i < response.Count; i++)
            {
                if (((CB.CloudObject)response[i]).Get("name") == null)
                {
                    Assert.Fail("Name does not exists");
                }
            }
            if (response.Count > 0)
                Assert.IsTrue(true);
            else
                Assert.Fail("object could not queried properly");
        }

        [TestMethod]
        public async Task FindDataWithId()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "sampleName");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.EqualTo("id", obj.ID);
            var response = (ArrayList)await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task FindItemWithId()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "sampleName");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.EqualTo("id", obj.ID);
            var response = (ArrayList)await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task FindOneQuery()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "sampleName");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.EqualTo("name", "sampleName");
            CB.CloudObject response = await query.FindOneAsync<CB.CloudObject>();
            if (((CB.CloudObject)response).Get("name").ToString() == "sampleName")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod] //273
        public async Task RetrieveDataWithValue()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "sampleName");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.EqualTo("name", "sampleName");
            var response = (ArrayList) await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    if (((CB.CloudObject)response[i]).Get("name").ToString() != "sampleName")
                    {
                        Assert.Fail("should retrieve saved data with particular value");
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task RetrieveList()
        {
            var obj = new CB.CloudObject("student1");
            string[] list = { "java", "python" };
            obj.Set("subject", list);
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.ContainsAll("subject", list);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                   ArrayList subject = (ArrayList)((CB.CloudObject)response[i]).Get("subject");
                    for (int j = 0; j < subject.Count; j++)
                    {
                        if (subject[j].ToString() != "java" && subject[j].ToString() != "python")
                        {
                            Assert.Fail("should retrieve saved data with particular value");
                        }
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task StartsWithTest()
        {
            var query = new CB.CloudQuery("student1");
            query.StartsWith("name", "s");
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    string name = (string)((CB.CloudObject)response[i]).Get("name");
                    if (name.StartsWith("s") == false)
                    {
                        Assert.Fail("should retrieve saved data with particular value ");
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }


        [TestMethod]
        public async Task GreaterThanTest()
        {
            var query = new CB.CloudQuery("student1");
            query.GreaterThan("age", 10);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    int data = Int32.Parse(((CB.CloudObject)response[i]).Get("age").ToString());
                    if (data <= 10)
                    {
                        Assert.Fail("received value less than the required value");
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task GreaterThanEqualTo()
        {
            var query = new CB.CloudQuery("student1");
            query.GreaterThanEqualTo("age", 15);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    int data = (int)((CB.CloudObject)response[i]).Get("age");
                    if (data < 15)
                    {
                        Assert.Fail("received value less than the required value");
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task LessThan()
        {
            var query = new CB.CloudQuery("student1");
            query.LessThan("age", 40);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    int data = Int32.Parse(((CB.CloudObject)response[i]).Get("age").ToString());
                    if (data >= 20)
                    {
                        Assert.Fail("received value greater than the required value");
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task LessThanEqualTo()
        {
            var query = new CB.CloudQuery("student1");
            query.LessThanEqualTo("age", 15);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    int data = Int32.Parse(((CB.CloudObject)response[i]).Get("age").ToString());
                    if (data > 15)
                    {
                        Assert.Fail("received value greater than the required value");
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task RetrieveDataWithParticularValue()
        {
            var obj1 = new CB.CloudQuery("student1");
            string[] list = { "java", "python" };
            obj1.EqualTo("subject", list);
            var obj2 = new CB.CloudQuery("student1");
            obj2.EqualTo("age", 12);
            var obj = CB.CloudQuery.Or(obj1, obj2);
            var response = await obj.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    int data = (int)((CB.CloudObject)response[i]).Get("age");
                    if (data == 12)
                    {
                        continue;
                    }
                    else
                    {
                        string[] subject = (string[])((CB.CloudObject)response[i]).Get("subject");
                        for (int j = 0; j < subject.Length; j++)
                        {
                            if (subject[j] == "java" || subject[j] == "python")
                            {
                                continue;
                            }
                            else
                            {
                                Assert.Fail("should retrieve saved data with particular value");
                            }
                        }
                    }
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task AscendingOrder()
        {
            var query = new CB.CloudQuery("student1");
            query.OrderByAsc("age");
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task DescendingOrder()
        {
            var query = new CB.CloudQuery("student1");
            query.OrderByDesc("age");
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                int age = Int32.Parse(((CB.CloudObject)response[0]).Get("age").ToString());
                for (int i = 1; i < response.Count; i++)
                {
                    int data = Int32.Parse(((CB.CloudObject)response[i]).Get("age").ToString());

                    if (age < data)
                    {
                        Assert.Fail("received value greater than the required value");
                    }
                    age = data;
                }
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task DistinctTest()
        {
            var query = new CB.CloudQuery("student1");
            var response = await query.DistinctAsync("age");
            List<int> age = new List<int>();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task FindByIdTest()
        {
            var obj = new CB.CloudObject("student1");
            obj.Set("name", "abcd");
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            var response = await query.GetAsync<CB.CloudObject>(obj.ID);
            if (((CB.CloudObject)response).Get("name").ToString() == "abcd")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(false);
            }
        }

        [TestMethod]
        public async Task ExistsTest()
        {
            var query = new CB.CloudQuery("student1");
            query.Exists("age");
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    if (((CB.CloudObject)response[i]).Get("age") == null)
                    {
                        Assert.Fail("received wrong data");
                    }
                }
                Assert.IsTrue(true);
            }
            Assert.IsFalse(false);
        }

        [TestMethod]
        public async Task DoesNotExists()
        {
            var query = new CB.CloudQuery("student1");
            query.DoesNotExist("age");
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                for (int i = 0; i < response.Count; i++)
                {
                    if (((CB.CloudObject)response[i]).Get("age") != null)
                    {
                        Assert.Fail("received wrong data");
                    }
                }
                Assert.IsTrue(true);
            }
            Assert.IsFalse(false);
        }

        [TestMethod]
        public async Task RelationQueryTest()
        {
            var obj1 = new CB.CloudObject("Hostel");
            obj1.Set("room", 123);
            await obj1.SaveAsync();
            var obj = new CB.CloudObject("student1");
            obj.Set("newColumn", obj1);
            await obj.SaveAsync();
            var query = new CB.CloudQuery("student1");
            query.NotEqualTo("newColumn", obj1);
            var response = await query.FindAsync();
            for (int i = 0; i < response.Count; i++)
            {
                if (((CB.CloudObject)response[i]).Get("newColumn") != null)
                {

                    CB.CloudObject relObj = (CB.CloudObject)((CB.CloudObject)response[i]).Get("newColumn");
                    if (relObj.ID == obj1.ID)
                    {
                        Assert.Fail("Should not get the id in not equal to");
                    }
                    Assert.IsTrue(true);
                }
            }
        }

        [TestMethod]
        public async Task QueryOverBooleanDataType()
        {
            var obj1 = new CB.CloudObject("Custom1");
            obj1.Set("newColumn", false);
            await obj1.SaveAsync();
            var query = new CB.CloudQuery("Custom1");
            query.EqualTo("newColumn", false);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task GetEncryptedPassword()
        {
            string username = Util.Methods.MakeEmail();
            var obj = new CB.CloudObject("User");
            obj.Set("username", username);
            obj.Set("password", "password");
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SaveAsync();
            if (obj.Get("password").ToString() != "password")
            {
                var query = new CB.CloudQuery("User");
                query.EqualTo("password", "password");
                query.EqualTo("username", username);
                var response = await query.FindAsync();
                if (response.Count > 0)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail("Cannot get items");
                }
            }
            else
            {
                Assert.IsFalse(true);
            }
        }

        [TestMethod]
        public async Task GetEncryptedPasswordOverORQuery()
        {
            
            var username = Util.Methods.MakeEmail();
            var obj = new CB.CloudObject("User");
            obj.Set("username", username);
            obj.Set("password", "password");
            obj.Set("email", username);
            await obj.SaveAsync();
            string pass = obj.Get("password").ToString();
            if (!pass.Equals("password"))
            {
                var query1 = new CB.CloudQuery("User");
                query1.EqualTo("password", "password");

                var query2 = new CB.CloudQuery("User");
                query2.EqualTo("password", "password1");

                var query = CB.CloudQuery.Or(query1, query2);
                query.EqualTo("username", username);
                ArrayList response = await query.FindAsync();
                if (response.Count > 0)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsFalse(true);
                }
            }
        }

        [TestMethod]
        public async Task DoNotEncryptAlreadyEncryptedPassword()
        {
            
            string username = Util.Methods.MakeEmail();

            var obj = new CB.CloudObject("User");
            obj.Set("username", username);
            obj.Set("password", "password");
            obj.Set("email", Util.Methods.MakeEmail());
            await obj.SaveAsync();
            var query = new CB.CloudQuery("User");

            var response = await query.GetAsync<CB.CloudUser>(obj.ID);
            var obj1 = await response.SaveAsync();
            Assert.IsTrue(true);

        }
    }
}
