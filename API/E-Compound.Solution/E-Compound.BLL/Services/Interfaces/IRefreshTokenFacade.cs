using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IRefreshTokenFacade
    {
        RefreshTokenDto FindRefreshTokenNotExpired(string userName);
        bool AddRefreshToken(RefreshTokenDto token);
    }
}
