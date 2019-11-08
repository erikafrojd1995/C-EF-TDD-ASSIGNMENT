using DataInterface;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
namespace UnitTest
{
    [TestClass]
    public class CustomerAndMinorCustomerAPITests
    {

        [TestMethod]
        public void TestAddCustomerAlreadyExsists()
        {
            var customerManagerMock = new Mock<ICustomerManager>();
            var successfull = AddCustomerNumberOne(customerManagerMock);
            var customer = new Customer
            {
                CustomerID = 3
            };
            Assert.AreEqual(ErrorCodesAddCustomer.CustomerAlreadyExists, successfull);
            customerManagerMock.Verify(m =>
               m.AddCustomer(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<bool>()),
                Times.Never());
        }

       [TestMethod]
        public void TestAddCustomerOk()
        {
            var customerManagerMock = new Mock<ICustomerManager>();

            customerManagerMock.Setup(c =>
               c.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns((Customer)null);

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object,null, null);
            var successfull = customerAndMinorCustomerAPI.AddCustomer("","","",1,false,3);
            Assert.AreEqual(ErrorCodesAddCustomer.Ok, successfull);
            customerManagerMock.Verify(c =>
               c.AddCustomer(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),It.IsAny<bool>()), Times.Once);

        }


        [TestMethod]
         private ErrorCodesAddCustomer AddCustomerNumberOne(Mock<ICustomerManager> customerManagerMock)
         {
            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
            .Returns(new Customer
            {

                CustomerID = 3
            });

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, null, null);
            var successfull = customerAndMinorCustomerAPI.AddCustomer("Håkan Bråkan", "TårtVägen 7", "2007-03-13", 20,false, 3);
            return successfull;
        }


        [TestMethod]
        public void TestAddMinorCustomerOK()
        {
            var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();
            var successfull = AddMinorCustomerNumberOne(minorCustomerManagerMock);
            var customer = new Customer
            {
                CustomerID = 1
            };
            Assert.AreEqual(AddMinorCustomerErrorCodes.Ok, successfull);
            minorCustomerManagerMock.Verify(m =>
               m.AddMinorCustomer(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()),
                Times.Once());

        }

        [TestMethod]
        private AddMinorCustomerErrorCodes AddMinorCustomerNumberOne(Mock<IMinorCustomerManager> minorCustomerManagerMock)
        {

            var customerManagerMock = new Mock<ICustomerManager>();
            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
            .Returns(new Customer
            {

                CustomerID = 1
            });

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, minorCustomerManagerMock.Object, null);
            var successfull = customerAndMinorCustomerAPI.AddMinorCustomer("Håkan Bråkan", "TårtVägen 7", "2007-03-13", 20, 4, 4);
            return successfull;

        }


        [TestMethod]
        public void TestAddMinorCustomerAlreadyExist()
        {
            var customerManagerMock = new Mock<ICustomerManager>();
            var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();


            minorCustomerManagerMock.Setup(m =>
            m.GetMinorCustomerByMinorCustomerID(It.IsAny<int>()))
                .Returns(new MinorCustomer
                {
                    MinorCustomerID = 2
                });

            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(new Customer
                {
                    CustomerID = 2
                });

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, minorCustomerManagerMock.Object, null);
            var result = customerAndMinorCustomerAPI.AddMinorCustomer("Håkan Bråkan", "TårtVägen 7", "20070313", 20, 4, 4);
            Assert.AreEqual(AddMinorCustomerErrorCodes.MinorCustomerAlreadyExsist, result);
            minorCustomerManagerMock.Verify(m =>
                m.AddMinorCustomer("Håkan Bråkan", "TårtVägen 7", "20070313", 20, 4), Times.Never());
        }


        [TestMethod]
        public void TestAddMinorCustomerNoGuard()
        {
            var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();
            var customerManagerMock = new Mock<ICustomerManager>();
            var minorCutomer = new MinorCustomer
            {
                MinorCustomerID = 1
            };
            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, minorCustomerManagerMock.Object, null);
            var result = customerAndMinorCustomerAPI.AddMinorCustomer("Håkan Bråkan", "TårtVägen 7", "2007-03-13", 20, 4, 4);
            Assert.AreEqual(AddMinorCustomerErrorCodes.MinorCustomerNeedsAGuard, result);
            minorCustomerManagerMock.Verify(m =>
               m.AddMinorCustomer(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()),
                Times.Never());
        }
        


       

       [TestMethod] 
        public void TestRemoveExistingCustomer()
        {

            var customerManagerMock = new Mock<ICustomerManager>();
            var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();

            customerManagerMock.Setup(m =>
               m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(new Customer
                {
                    CustomerID =4,
                    MinorCustomers= new List<MinorCustomer>()

                });

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, minorCustomerManagerMock.Object, null);
            var successfull = customerAndMinorCustomerAPI.RemoveCustomer(4);
            Assert.AreEqual(ErrorCodesRemoveCustomer.Ok, successfull);
            customerManagerMock.Verify(m =>
                m.RemoveCustomer(It.IsAny<int>()), Times.Once);
        } 

        [TestMethod]                                           
        public void TestRemoveCustomerWithMinorCustomer()
        {
              var customerManagerMock = new Mock<ICustomerManager>();
              var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();

              customerManagerMock.Setup(m =>
                 m.GetCustomerByCustomerID(It.IsAny<int>()))
                  .Returns(new Customer
                  {
                      CustomerID = 1,
                      MinorCustomers = new List<MinorCustomer>
                      {
                          new MinorCustomer()
                      }
                  });


              var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, minorCustomerManagerMock.Object,null);
              var successfull = customerAndMinorCustomerAPI.RemoveCustomer(1);
              Assert.AreEqual(ErrorCodesRemoveCustomer.CustomerIsConnectedToMinorCustomer, successfull);
              customerManagerMock.Verify(m =>
                 m.RemoveCustomer(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void TestRemoveNonexistingCustomer()
        {
            var customerManagerMock = new Mock<ICustomerManager>();
            var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();

            customerManagerMock.Setup(m =>
               m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns((Customer)null);

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(customerManagerMock.Object, minorCustomerManagerMock.Object,null);
            var successfull = customerAndMinorCustomerAPI.RemoveCustomer(1);
            Assert.AreEqual(ErrorCodesRemoveCustomer.NoSuchCustomer, successfull);
            customerManagerMock.Verify(m =>
               m.RemoveCustomer(It.IsAny<int>()), Times.Never);

        }


        [TestMethod]
        public void TestRemoveNonexistingMinorCustomer()
        {
            var minorCustomerManagerMock = new Mock<IMinorCustomerManager>();

            minorCustomerManagerMock.Setup(m =>
               m.GetMinorCustomerByMinorCustomerID(It.IsAny<int>()))
                .Returns((MinorCustomer)null);

            var customerAndMinorCustomerAPI = new CustomerAndMinorCustomerAPI(null, minorCustomerManagerMock.Object,null);
            var successfull = customerAndMinorCustomerAPI.RemoveMinorCustomer(1);
            Assert.AreEqual(ErrorCodesRemoveMinorCustomer.NoSuchMinorCustomer, successfull);
            minorCustomerManagerMock.Verify(m =>
               m.RemoveMinorCustomer(It.IsAny<int>()), Times.Never);

        }

    }
}
