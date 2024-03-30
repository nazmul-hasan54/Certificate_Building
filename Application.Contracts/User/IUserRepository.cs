using Application.Contracts.BaseInterface;
using Application.SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.User
{
    public interface IUserRepository: IBaseRepository<UserRegistrationRequestDTO>
    {
        void SaveUser(UserRegistrationRequestDTO user);
    }
}
