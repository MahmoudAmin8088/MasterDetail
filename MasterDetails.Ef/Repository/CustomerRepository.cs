using MasterDetails.Core.IRepository;
using MasterDetails.Core.Models;
using MasterDetails.Ef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Ef.Repository
{
    public class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
