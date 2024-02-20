using Application.Contracts.Certificates;
using Application.Contracts.Departments;
using Application.Contracts.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        IDepartmentRepository Department { get; }
        IEmployeeRepository Employee { get; }
        ICertificateRepository Certificate { get; }
        
        void Save();
    }
}
