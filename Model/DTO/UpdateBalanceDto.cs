using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Model.DTO
{
    public class UpdateBalanceDto
    {
        public int CustomerID { get; set; }
        public decimal Balance { get; set; }
    }
}
