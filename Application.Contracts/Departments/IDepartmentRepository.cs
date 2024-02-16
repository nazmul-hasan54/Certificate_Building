using Application.Contracts.BaseInterface;
using Application.Domain.Models;
using Application.SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Departments
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        void SaveDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);
    }
}
