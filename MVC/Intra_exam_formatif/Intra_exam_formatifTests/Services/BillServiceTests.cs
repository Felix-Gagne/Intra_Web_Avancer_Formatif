using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intra_exam_formatif.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Intra_exam_formatif.Data;
using Intra_exam_formatif.Models;
using Intra_exam_formatif.Models.Exception;

namespace Intra_exam_formatif.Services.Tests
{
    [TestClass()]
    public class BillServiceTests
    {
        DbContextOptions<ApplicationDbContext> options;
        public BillServiceTests()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BillService")
                .Options;
        }

        [TestInitialize]
        public void Init()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);

            Item[] items = new Item[]
            {
                new Item
                {
                    Id = 1,
                    Name = "Foo",
                    Price = 1
                },
                new Item
                {
                    Id = 2,
                    Name = "Faa",
                    Price = 2
                },
                new Item
                {
                    Id = 3,
                    Name = "Fop",
                    Price = 3
                }
            };
            db.AddRange(items);
            db.SaveChanges();
        }

        [TestCleanup]
        public void Dispose()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);
            db.Bills.RemoveRange(db.Bills);
            db.Items.RemoveRange(db.Items);
            db.SaveChanges();
        }

        [TestMethod()]
        public void EmptyBillName()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);

            BillService service = new BillService(db);
            Item[] i = new Item[]
            {
                new Item
                {
                    Id = 4,
                    Name = "Test",
                    Price = 1
                },
                new Item
                {
                    Id= 5,
                    Name = "Test4",
                    Price = 4
                }
            };

            Assert.ThrowsException<Exception>(() => service.CreateBill("", i), "You need a name to create a bill");
        }

        [TestMethod()]
        public void NullBillName()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);

            BillService service = new BillService(db);
            Item[] i = new Item[]
            {
                new Item
                {
                    Id = 4,
                    Name = "Test",
                    Price = 1
                },
                new Item
                {
                    Id= 5,
                    Name = "Test4",
                    Price = 4
                }
            };

            Assert.ThrowsException<Exception>(() => service.CreateBill(null, i), "You need a name to create a bill");
        }

        [TestMethod()]
        public void ItemListIsEmpty()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);

            BillService service = new BillService(db);
            List<Item> list = null;

            Assert.ThrowsException<ArgumentNullException>(() => service.CreateBill("test", list));
        }

        [TestMethod()]
        public void ItemIsFree()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);

            BillService service = new BillService(db);
            Item[] i = new Item[]
            {
                new Item
                {
                    Id = 4,
                    Name = "Test",
                    Price = 0
                },
                new Item
                {
                    Id= 5,
                    Name = "Test4",
                    Price = 4
                }
            };

            Assert.ThrowsException<AreYouInsaneException>(() => service.CreateBill("test", i), "Not giving free stuff either!!");
        }

        [TestMethod()]
        public void HaveToPay()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);

            BillService service = new BillService(db);
            Item[] i = new Item[]
            {
                new Item
                {
                    Id = 4,
                    Name = "Test",
                    Price = -5
                },
                new Item
                {
                    Id= 5,
                    Name = "Test4",
                    Price = 4
                }
            };

            Assert.ThrowsException<AreYouInsaneException>(() => service.CreateBill("test", i), "Not paying for you to take something!");
        }
    }
}