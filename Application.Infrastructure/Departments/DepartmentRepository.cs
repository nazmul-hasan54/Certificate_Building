using Application.Contracts.Departments;
using Application.CoreInformation.Context;
using Application.Domain.Models;
using Application.Infrastructure.BaseRepository;
using Application.SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Departments
{
    public class DepartmentRepository: BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ProjectDbContext context): base(context)
        {
            
        }

        public void DeleteDepartment(Department department) => Delete(department);

        public void SaveDepartment(Department department) => Create(department);

        public void UpdateDepartment(Department department) => Update(department);
    }
}
