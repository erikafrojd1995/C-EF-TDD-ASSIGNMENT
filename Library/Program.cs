using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System;
using DataAccess;
using DataInterface;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            IPathManager pathManager = new PathManager();
            pathManager.AddPath(1);
            pathManager.AddPath(2);
            pathManager.AddPath(3);

            IShelfManager shelfManager = new ShelfManager();
            var shelf1 = shelfManager.AddShelf(1, 1);
            var shelf2 = shelfManager.AddShelf(2, 1);
            var shelf3 = shelfManager.AddShelf(3, 1);
            var shelf4 = shelfManager.AddShelf(1, 2);
            var shelf5 = shelfManager.AddShelf(2, 2);
            var shelf6 = shelfManager.AddShelf(3, 2);
            var shelf7 = shelfManager.AddShelf(1, 3);
            var shelf8 = shelfManager.AddShelf(2, 3);
            var shelf9 = shelfManager.AddShelf(3, 3);

            IBookManager bookManager = new BookManager();
            bookManager.AddBook("Boken om Blå fåglar", "Bo Fink", "9789178130948", 2001, 1,995,1,false, 1 ,1 ); 
            bookManager.AddBook("Boken om Röda fåglar", "Bo Fink"," 9789113086972", 2002, 2,25,2,false, 1,1);
            bookManager.AddBook("Boken om Gröna bilar", "Bosse Bildoktorn", "9780777772348", 2003, 2,33,3,false, 2,1);
            bookManager.AddBook("Boken om Lila bilar", "Bosse Bildoktorn", "7891742479472", 2004, 2, 56,4,false, 2,1);
            bookManager.AddBook("Boken om Gråa blommor", "Ernst", "9789174247220", 2005, 2,65,5,false, 3,2);
            bookManager.AddBook("Boken om Rosa blommor", "Ernst", "9789174249613", 2006,3,70,6,false, 3,2);
            bookManager.AddBook("Boken om gröna buskar", "Laila", "9789174249958", 2007,5,88,7,false, 1,2);
            bookManager.AddBook("Boken om Turkosa buskar", "Laila", "9789129710687", 2008,3,45,8,false, 1,2);
            bookManager.AddBook("Boken om Små hundar", "Kalle", "9789127155589", 2009,1,860, 9,false, 2,3);
            bookManager.AddBook("Boken om Stora hundar", "Kalle", "9789127164345", 2010,2,56,10,false, 2,3);
            bookManager.AddBook("Boken om Tjocka katter", "Ullis", "9789127155282", 2011,3,2500,11,false, 3,4);
            bookManager.AddBook("Boken om lagom-stora katter", "Ullis", "9789174235616", 2012,4,95,12, false, 3,3);
            bookManager.AddBook("Boken om fula fiskar", "Anna", "9789172096738", 2013,2, 190,13,false, 1,3);
            bookManager.AddBook("Boken om fina fiskar", "Anna", "9789178033553", 2014,4, 220,14,false, 1,3);
            bookManager.AddBook("Boken om snälla människor", "Lotta", "9789129714036", 2015,5, 330,15,false, 2,3);
            bookManager.AddBook("Boken om dumma människor ", "Lotta", "9789129716689", 2016,2, 480,16,false, 2,3);
            bookManager.AddBook("Boken om tårtor", "Gunni", "9789129706406", 2017,1, 510,17,false, 3,3);
            bookManager.AddBook("Boken om kakor", "Gunni", "9789127154377", 2018,3,80,18,false, 3,3);

            ICustomerManager customerManager = new CustomerManager();
            customerManager.AddCustomer("Anna Karlsson", "SvansGatan 5","1995-04-17", 0,false);
            customerManager.AddCustomer("Anders Svensson", "FågelGatan 6", "1980-04-15", 20,false );
            customerManager.AddCustomer("Mimmi Andersson", "TårtVägen 7", "1970-06-22", 3,false );
            customerManager.AddCustomer("Kenny Kvist", "VolvoVägen 8", "2001-07-13", 0,false);

            IMinorCustomerManager minorCustomerManager = new MinorCustomerManager();
            minorCustomerManager.AddMinorCustomer("Klara Karlsson","SvansGatan 5", "2010-06-03",0,1 );
            minorCustomerManager.AddMinorCustomer("Max Karlsson", "SvansGatab 5", "2009-06-01", 0,1);
            minorCustomerManager.AddMinorCustomer("Håkan Bråkan","TårtVägen 7","2007-03-13",20,4);


            /*ILoanManager loanManager = new LoanManager();
            loanManager.AddLoan(1, 1,loanStart,l);
            loanManager.AddLoan(2, 2);*/

            /*IMinorCustomerLoanManager minorCustomerLoanManager = new MinorCustomerLoanManager();
            minorCustomerLoanManager.AddMinorCustomerLoan(1, 3);
            minorCustomerLoanManager.AddMinorCustomerLoan(2, 4);*/


        }
    }
}
