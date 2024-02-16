using Application.Contracts.BaseInterface;
using Application.Contracts.Employees;
using Application.CoreInformation.Context;
using Application.Domain.Models;
using Application.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Employees
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ProjectDbContext contex) : base(contex)
        {
            
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public void SaveEmployee(Employee employee)
        {
            Create(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            Update(employee);
        }
    }
}
