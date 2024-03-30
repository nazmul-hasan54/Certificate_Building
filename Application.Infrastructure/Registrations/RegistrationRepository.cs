using Application.Contracts.Registrations;
using Application.CoreInformation.Context;
using Application.Domain.Models;
using Application.Infrastructure.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Registrations
{
    public class RegistrationRepository : BaseRepository<Users>, IRegistrationRepository
    {
        public RegistrationRepository(ProjectDbContext projectDbContext) : base(projectDbContext)
        {
        }


        public void DeleteUser(Users user)
        {
            Delete(user);
        }

        public void SaveUser(Users user)
        {
            Create(user);
        }

        public void UpdateUser(Users user)
        {
            Update(user);
        }
    }
}
