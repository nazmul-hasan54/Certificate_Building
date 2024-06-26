﻿using Application.Contracts.Certificates;
using Application.Contracts.Departments;
using Application.Contracts.Employees;
using Application.Contracts.Registrations;
using Application.Contracts.User;
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
        IUserRepository User { get; }
        IRegistrationRepository Registration { get; }
        
        void Save();
    }
}
