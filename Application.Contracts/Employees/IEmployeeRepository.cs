using Application.Contracts.BaseInterface;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Employees
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        void SaveEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
