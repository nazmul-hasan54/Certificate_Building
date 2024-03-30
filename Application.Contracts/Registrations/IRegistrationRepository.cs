using Application.Contracts.BaseInterface;
using Application.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Registrations
{
    public interface IRegistrationRepository: IBaseRepository<Users>
    {
        void SaveUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(Users user);
    }
}
