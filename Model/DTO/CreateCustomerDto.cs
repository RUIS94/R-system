using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Model.DTO
{
    public class CreateCustomerDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? VIPLevel { get; set; }
        public string? Notes { get; set; }
    }
}
