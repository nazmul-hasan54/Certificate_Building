using Application.Contracts.Users;
using Application.CoreInformation.Context;
using Application.Infrastructure.BaseRepository;
using Application.SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Infrastructure.Users
{
    public class UserRepository : BaseRepository<UserRegistrationRequestDTO>, IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context)
        {
            
        }

        public void SaveUser(UserRegistrationRequestDTO user)
        {
            Create(user);
        }
    }
}
