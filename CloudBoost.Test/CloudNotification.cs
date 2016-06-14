using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CB.Test
{
    [TestClass]
    public class CloudNotification
    {

        [TestInitialize]
        public void TestInitalize()
        {
            CB.Test.Util.Keys.InitWithMasterKey();
        }

        [TestMethod]
        public void subscribeToChannel()
        {
            
            CB.CloudNotification.On("sample", new Callback(action));
            Assert.IsTrue(true);
        }

        void action(Object result)
        {
           //do nithign. 
        }

        [TestMethod]
        public void publishDataToChannel()
        {
            
            CB.CloudNotification.On("sample", new Callback(anotherAction));
            CB.CloudNotification.Publish("sample", "data");
        }

        void anotherAction(Object result)
        {
            
            if (result.ToString() == "data")
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Error wrong data received");
            }
        }

        [TestMethod]
        public void shouldStopListeningChannel()
        {
            
            CB.CloudNotification.Off("sample");
            Assert.IsTrue(true);
        }
    }
}
