﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SalesApp.Data;
using SalesApp.Domain.Entity;
using SharedKernel.Data;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SalesApp.Tests.Data
{
    [TestClass]
    public class GenericRepositoryIntegrationTests
    {
        private StringBuilder _logBuilder = new StringBuilder();
        private string _log;
        private OrderSystemContext _context;
        private GenericRepository<Customer> _customerRepository;

        public GenericRepositoryIntegrationTests()
        {
            Database.SetInitializer(new NullDatabaseInitializer<OrderSystemContext>());
            _context = new OrderSystemContext();
            _customerRepository = new GenericRepository<Customer>(_context);
            SetupLogging();
        }

        //[TestMethod]
        //public void CanFindByCustomerByKeyWithDynamicLambda() {
        //  var results = _customerRepository.FindByKey(1);
        //  WriteLog();
        //  Assert.IsTrue(_log.Contains("FROM [dbo].[Customers"));
        //}

        //[TestMethod]
        //public void CanFindByProductByKeyWithDynamicLambda() {
        //  var results = new GenericRepository<Product>(_context).FindByKey(1);
        //  WriteLog();
        //  Assert.IsTrue(_log.Contains("FROM [dbo].[Products"));
        //}



        [TestMethod]
        public void NoTrackingQueriesDoNotCacheObjects()
        {
            var results = _customerRepository.All();
            Assert.AreEqual(0, _context.ChangeTracker.Entries().Count());
        }

        [TestMethod]
        public void CanQueryWithSinglePredicate()
        {
            var results = _customerRepository.FindBy(c => c.LastName.StartsWith("L"));
            WriteLog();
            Assert.IsTrue(_log.Contains("'L%'"));
        }

        [TestMethod]
        public void CanQueryWithDualPredicate()
        {
            var date = new DateTime(2001, 1, 1);
            var results = _customerRepository
              .FindBy(c => c.LastName.StartsWith("L") && c.DateOfBirth >= date);
            WriteLog();
            Assert.IsTrue(_log.Contains("'L%'") && _log.Contains("1/1/2001"));
        }

        [TestMethod]
        public void CanQueryWithComplexRelatedPredicate()
        {
            var date = new DateTime(2001, 1, 1);
            var results = _customerRepository
               .FindBy(c => c.LastName.StartsWith("L") && c.DateOfBirth >= date
                                                       && c.Orders.Any());
            WriteLog();
            Assert.IsTrue(_log.Contains("'L%'") && _log.Contains("1/1/2001") && _log.Contains("Orders"));
        }



        [TestMethod]
        public void ComposedOnAllListExecutedInMemory()
        {
            _customerRepository.All().Where(c => c.FirstName == "Julie").ToList();
            WriteLog();
            Assert.IsFalse(_log.Contains("Julie"));
        }

        private void WriteLog()
        {
            Debug.WriteLine(_log);
        }

        private void SetupLogging()
        {
            _context.Database.Log = BuildLogString;
        }

        private void BuildLogString(string message)
        {
            _logBuilder.Append(message);
            _log = _logBuilder.ToString();
        }
    }
}
