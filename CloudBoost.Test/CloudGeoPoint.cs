using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CB.Test
{
    [TestClass]
    public class CloudGeoPoint
    {

        [TestInitialize]
        public void TestInitalize()
        {
            CB.Test.Util.Keys.InitWithMasterKey();
        }


        [TestMethod]
        public async Task SaveGeoPoint()
        {
            
            var obj = new CB.CloudObject("Custom5");
            var loc = new CB.CloudGeoPoint((decimal)17.7, (decimal)78.9);
            obj.Set("geopoint", loc);
            await obj.SaveAsync();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void CreateGeoPointWithZero()
        {
            var loc = new CB.CloudGeoPoint(0, 0);
        }

        [TestMethod]
        public async Task NearTest()
        {
            
            var query = new CB.CloudQuery("Custom5");
            var loc = new CB.CloudGeoPoint((decimal)17.7, (decimal)78.9);
            query.Near("geopoint", loc, 400000);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("should retrieve saved data with particular value");
            }
        }

        [TestMethod]
        public async Task GeoWithinTest()
        {
            
            var query = new CB.CloudQuery("Custom5");
            var loc1 = new CB.CloudGeoPoint((decimal)18.4, (decimal)78.9);
            var loc2 = new CB.CloudGeoPoint((decimal)17.4, (decimal)78.4);
            var loc3 = new CB.CloudGeoPoint((decimal)17.7, (decimal)80.4);
            CB.CloudGeoPoint[] loc = { loc1, loc2, loc3 };

            query.GeoWithin("location", loc);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("should retrieve saved data with particular value");
            }
        }

        [TestMethod]
        public async Task GeoWithinTestLimit()
        {
            
            var query = new CB.CloudQuery("Custom5");
            var loc1 = new CB.CloudGeoPoint((decimal)18.4, (decimal)78.9);
            var loc2 = new CB.CloudGeoPoint((decimal)17.4, (decimal)78.4);
            var loc3 = new CB.CloudGeoPoint((decimal)17.7, (decimal)80.4);
            CB.CloudGeoPoint[] loc = { loc1, loc2, loc3 };
            query.Limit = 4;
            query.GeoWithin("location", loc);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("should retrieve saved data with particular value");
            }
        }

        [TestMethod]
        public async Task GeoWithinTestCircle()
        {
            
            var query = new CB.CloudQuery("Custom5");
            var loc = new CB.CloudGeoPoint((decimal)17.3, (decimal)78.3);
            query.Limit = 4;
            query.GeoWithin("location", loc, 1000);
            var response = await query.FindAsync();
            if (response.Count > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("should retrieve saved data with particular value");
            }
        }

        [TestMethod]
        public async Task UpdateGeoPoint()
        {
            
            var obj = new CB.CloudObject("Custom5");
            var loc = new CB.CloudGeoPoint((decimal)17.7, (decimal)78.9);
            obj.Set("geopoint", loc);
            await obj.SaveAsync();
            Assert.IsTrue(true);
        }
    }
}
