using Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IService
{
    public interface IAuthManager
    {
        Task<bool> Validate(LoginUserDTO userDTO);
        Task<string> CreateToken();
    }
}
