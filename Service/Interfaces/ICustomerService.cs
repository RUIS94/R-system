using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;
using Model.DTO;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerWithAccountAsync(CreateCustomerDto customer);
    }
}
