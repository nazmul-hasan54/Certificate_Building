using Application.Contracts.Departments;
using Application.Contracts.Employees;
using Application.Contracts.UnitOfWork;
using Application.CoreInformation.Context;
using Application.Infrastructure.Departments;
using Application.Infrastructure.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.UnitOfWorkRepo
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private ProjectDbContext _projectDbContext;
        private IDepartmentRepository _department;
        private IEmployeeRepository _employee;
        public IDepartmentRepository Department 
        {
            get 
            {
                if (_department == null) 
                {
                    _department = new DepartmentRepository(_projectDbContext);
                }
                return _department; 
            }
        }
        public IEmployeeRepository Employee 
        {
            get 
            {
                if (_employee == null) 
                {
                    _employee = new EmployeeRepository(_projectDbContext);
                }
                return _employee; 
            }
        }
        public UnitOfWorkRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public void Save()
        {
            _projectDbContext.SaveChanges();
        }
    }
}
