using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class CreateAccountDto
    {
        public int CustomerID { get; set; }
        public decimal Balance { get; set; }
    }
}
