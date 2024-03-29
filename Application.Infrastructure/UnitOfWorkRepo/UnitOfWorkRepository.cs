﻿using Application.Contracts.Certificates;
using Application.Contracts.Departments;
using Application.Contracts.Employees;
using Application.Contracts.UnitOfWork;
using Application.Contracts.Users;
using Application.CoreInformation.Context;
using Application.Infrastructure.Certificates;
using Application.Infrastructure.Departments;
using Application.Infrastructure.Employees;
using Application.Infrastructure.Users;
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
        private ICertificateRepository _certificate;
        private IUserRepository _user;
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

        public ICertificateRepository Certificate
        {
            get
            {
                if (_certificate == null)
                {
                    _certificate = new CertificateRepository(_projectDbContext);
                }
                return _certificate;
            }
        }

        public IUserRepository User 
        {
            get 
            {
                if (_user == null) 
                {
                    _user = new UserRepository(_projectDbContext);
                }
                return _user; 
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
