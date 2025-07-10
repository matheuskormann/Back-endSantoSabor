using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantoSabor.Application.DTOS
{
    public class ClientUpdateDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Cpf { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }
    }
}