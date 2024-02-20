using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedDTO
{
    public class CertificateDTO
    {
        public int CertificateId { get; set; }
        public string? CertificateName { get; set; }
        public DateTime CertificateDate { get; set; }
        public int EmployeeId { get; set; }
    }
}
