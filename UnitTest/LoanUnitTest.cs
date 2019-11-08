using DataInterface;
using Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    [TestClass]
    public class LoanUnitTests
    {
        [TestMethod]
        public void AddToLoanNoSuchBook()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var loanAPI = new LoanAPI(bookManagerMock.Object, null, null);
            var result = loanAPI.AddLoan(0, 0);
            Assert.AreEqual(ErrorCodesLoan.NoSuchBook, result);
        }

        [TestMethod]
        public void AddToLoanNoSuchCustomer()
        {
            var bookManagerMock = new Mock<IBookManager>();
            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(
                new Book()
                );
            var customerManager = new Mock<ICustomerManager>();

            var loanAPI = new LoanAPI(bookManagerMock.Object, null, customerManager.Object);
            var result = loanAPI.AddLoan(0, 0);
            Assert.AreEqual(ErrorCodesLoan.NoSuchCustomer, result);
        }

        [TestMethod] //inte löst
        public void AddToLoanBookBelongsToActiveLoan()
        {
            var loanManagerMock = new Mock<ILoanManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var customerManagerMock = new Mock<ICustomerManager>();
            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(
                new Customer
                {
                    CustomerID = 4,
                    
                }
                ) ;
            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(
                new Book
                {
                    BookNumber = 1,
                    BookActiveLoan=true
                    
                }
                ); ;

            var loanAPI = new LoanAPI(bookManagerMock.Object, null, customerManagerMock.Object);
            var successfull = loanAPI.AddLoan(1, 4);
            Assert.AreEqual(ErrorCodesLoan.BookBelongsToActiveLoan, successfull);
            loanManagerMock.Verify(m =>
                m.AddLoan(3, 2, It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never());
        }

        [TestMethod]
        public void addLoanOk()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var customerManagerMock = new Mock<ICustomerManager>();
            var loanManagerMock = new Mock<ILoanManager>();
            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(
                new Book
                {
                    BookNumber = 2
                }
            );

            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(
                new Customer
                {
                    CustomerID = 4
                }
                );

            loanManagerMock.Setup(m =>
            m.GetLoanByCustomerAndBook(It.Is<int>(i => i == 2), (It.Is<int>(i => i == 4))))
                .Returns(
                    new Loan
                    {
                        CustomerID = 4,
                        BookID = 1
                    }
                );
            loanManagerMock.Setup(m =>
            m.GetLoanByCustomers(It.IsAny<int>()))
                .Returns(new List<Loan>
                { 
                    new Loan
                    {
                        CustomerID = 1,
                        BookID = 5
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID = 3
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID=2
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID =6
                    },

                }
            );
            loanManagerMock.Setup(m =>
            m.AddLoan(It.Is<int>(i => i == 3), It.Is<int>(i => i == 2), It.IsAny<DateTime>(), It.IsAny<DateTime>()));
            var loanAPI = new LoanAPI(bookManagerMock.Object, loanManagerMock.Object, customerManagerMock.Object);
            var successfull = loanAPI.AddLoan(4, 2);
            Assert.AreEqual(ErrorCodesLoan.Ok, successfull);
            loanManagerMock.Verify(m =>
                m.AddLoan(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }


        [TestMethod]
        public void addLoanCustomerHasToManyActiveLoans()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var customerManagerMock = new Mock<ICustomerManager>();
            var loanManagerMock = new Mock<ILoanManager>();
            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(
                new Book
                {
                    BookNumber = 2
                }
            );

            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(
                new Customer
                {
                    CustomerID = 4
                }
                );

            loanManagerMock.Setup(m =>
            m.GetLoanByCustomerAndBook(It.Is<int>(i => i == 2), (It.Is<int>(i => i == 4))))
                .Returns(
                    new Loan
                    {
                        CustomerID = 4,
                        BookID = 1
                    }
                );
            loanManagerMock.Setup(m =>
            m.GetLoanByCustomers(It.IsAny<int>()))
                .Returns(new List<Loan>
                {
                    new Loan
                    {
                        CustomerID = 1,
                        BookID = 5
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID = 3
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID=2
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID =6
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID =7
                    },
                    new Loan
                    {
                        CustomerID = 1,
                        BookID =9
                    },

                }
            );
            loanManagerMock.Setup(m =>
            m.AddLoan(It.Is<int>(i => i == 3), It.Is<int>(i => i == 2), It.IsAny<DateTime>(), It.IsAny<DateTime>()));
            var loanAPI = new LoanAPI(bookManagerMock.Object, loanManagerMock.Object, customerManagerMock.Object);
            var successfull = loanAPI.AddLoan(4, 2);
            Assert.AreEqual(ErrorCodesLoan.CustomerHasToManyBooks, successfull);
            loanManagerMock.Verify(m =>
                m.AddLoan(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Never);
        }
        
        





        [TestMethod]
        public void ReturnLoanNoOpenLoan()
        {
            var bookManagerMock = new Mock<IBookManager>();
            var loanManagerMock = new Mock<ILoanManager>();
            var customerManagerMock = new Mock<ICustomerManager>();
            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(
                new Book
                {
                    BookNumber = 2
                }
            );

            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(
                new Customer
                {
                    CustomerID = 4
                }
                );

            loanManagerMock.Setup(m =>
            m.GetLoanByCustomerAndBook(It.Is<int>(i => i == 2), (It.Is<int>(i => i == 4))))
                .Returns(
                new Loan
                {
                    CustomerID = 4,
                    BookID = 3
                });
            loanManagerMock.Setup(m =>
            m.ReturnLoan(It.Is<int>(i => i == 3), It.Is<int>(i => i == 2)));
            var loanAPI = new LoanAPI(bookManagerMock.Object, loanManagerMock.Object, customerManagerMock.Object);
            var successfull = loanAPI.ReturnLoan(2, 4);
            Assert.AreEqual(ErrorCodesReturnBook.NoOpenLoan, successfull);
            loanManagerMock.Verify(m =>
                m.ReturnLoan(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void ReturnLoanLoanNoSuchCustomer()
        {
            var loanManagerMock = new Mock<ILoanManager>();
            var customerManagerMock = new Mock<ICustomerManager>();
            var bookManagerMock = new Mock<IBookManager>();


            loanManagerMock.Setup(m =>
            m.GetLoanByCustomerAndBook(It.IsAny<int>(), (It.IsAny<int>())))
                .Returns(new Loan
                {
                    CustomerID = 2,
                    BookID = 2
                });

            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(new Book
                {
                    BookNumber = 2
                });

            var loanAPI = new LoanAPI(bookManagerMock.Object, loanManagerMock.Object, customerManagerMock.Object);
            var result = loanAPI.ReturnLoan(2, 2);
            Assert.AreEqual(ErrorCodesReturnBook.NoSuchCustomer, result);
            loanManagerMock.Verify(m =>
                m.ReturnLoan(2, 2), Times.Never());
        }

        [TestMethod] //inte löst
        public void ReturnLoanLoanOk()
        {

            var loanManagerMock = new Mock<ILoanManager>();
            var bookManagerMock = new Mock<IBookManager>();
            var customerManagerMock = new Mock<ICustomerManager>();


            loanManagerMock.Setup(m =>
            m.GetLoanByCustomerAndBook(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new Loan
                {
                    LoanID = 2,
                    Items = new Book()
                });


            customerManagerMock.Setup(m =>
            m.GetCustomerByCustomerID(It.IsAny<int>()))
                .Returns(new Customer
                {
                    CustomerID = 2
                });
            bookManagerMock.Setup(m =>
            m.GetBookByBookNumber(It.IsAny<int>()))
                .Returns(
                new Book
                {
                    BookNumber = 1,
                    BookActiveLoan = true

                }
                ); 
            var loanAPI = new LoanAPI(bookManagerMock.Object, loanManagerMock.Object, customerManagerMock.Object);
            var result = loanAPI.ReturnLoan(2, 2);
            Assert.AreEqual(ErrorCodesReturnBook.Ok, result);
            loanManagerMock.Verify(m =>
                m.ReturnLoan(It.IsAny<int>(),(It.IsAny<int>())), Times.Once());

        }

        [TestMethod]
        private Mock<ILoanManager> SetupMock(Loan loan)
        {
            var loanManagerMock = new Mock<ILoanManager>();

            loanManagerMock.Setup(m =>
                    m.GetLoanByCustomerAndBook(It.IsAny<int>(),(It.IsAny<int>())))
                .Returns(loan);

            loanManagerMock.Setup(m =>
                m.AddLoan(It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>()

                ));
            return loanManagerMock;
        }


    }
}

