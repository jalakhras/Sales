using SalesApp.Domain.Entity;
using SharedKernel.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SalesApp.Data
{
    public class CustomerData
    {
        public List<Customer> GetAllCustomers()
        {
            using (var context = new OrderSystemContext())
            {
                return context.Customers.AsNoTracking().ToList();
            }
        }

        public Customer FindCustomer(int? id)
        {
            using (var context = new OrderSystemContext())
            {
                return context.Customers
                  .AsNoTracking()
                  .SingleOrDefault(c => c.CustomerId == id);
            }
        }

        public void AddCustomer(Customer customer)
        {
            using (var context = new OrderSystemContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (var context = new OrderSystemContext())
            {
                context.Entry(customer).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void RemoveCustomer(int id)
        {
            using (var context = new OrderSystemContext())
            {
                context.Customers.Remove(context.Customers.Find(id));
                context.SaveChanges();
            }
        }
    }
    public class UOW
    {
        OrderSystemContext _context;
        public UOW()
        {
            _context = new OrderSystemContext();
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
    public class UOWWrappingGenericRepos
    {
        private GenericRepository<Customer> _custRepo;
        private GenericRepository<Order> _orderRepo;
        OrderSystemContext _context;
        public UOWWrappingGenericRepos()
        {
            _context = new OrderSystemContext();
        }
        public GenericRepository<Customer> CustomerRepository
        {
            get
            {

                if (_custRepo == null)
                {
                    _custRepo = new GenericRepository<Customer>(_context);
                }
                return _custRepo;
            }
        }

        public GenericRepository<Order> OrderRepository
        {
            get
            {

                if (this._orderRepo == null)
                {
                    this._orderRepo = new GenericRepository<Order>(_context);
                }
                return _orderRepo;
            }
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
