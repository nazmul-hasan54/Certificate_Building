using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SharedDTO
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        //public IList<CertificateDTO>? Certificates { get; set; }
    }
}
