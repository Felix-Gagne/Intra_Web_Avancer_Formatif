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

            Bill[] bills = new Bill[] 
            { 
                new Bill
                {
                    Id = 1,
                    Name = "Test",
                    Price = 1,
                    Items = items
                },
                new Bill
                {
                    Id = 2,
                    Name = "Test1",
                    Price = 2,
                    Items = items
                },
                new Bill
                {
                    Id = 3,
                    Name = "Test2",
                    Price = 3,
                    Items = items
                },
            };
            db.AddRange(items);
            db.AddRange(bills);
            db.SaveChanges();
        }

        [TestCleanup]
        public void Dispose()
        {
            using ApplicationDbContext db = new ApplicationDbContext(options);
        }

        [TestMethod()]
        public void CreateBillTest()
        {
            Assert.Fail();
        }
    }
}