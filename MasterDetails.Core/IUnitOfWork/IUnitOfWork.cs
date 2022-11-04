using AutoMapper.Configuration.Conventions;
using MasterDetails.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.IUnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IitemRepository Items { get; }
        IOrderItemRepository OrderItems { get; }

        int Complete();
    }
}
