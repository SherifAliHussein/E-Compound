using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class RefreshTokenFacade:BaseFacade,IRefreshTokenFacade
    {
        private IRefreshTokenService _refreshTokenService;

        public RefreshTokenFacade(IRefreshTokenService refreshTokenService, IUnitOfWorkAsync unitOFWork)
            : base(unitOFWork)
        {
            _refreshTokenService = refreshTokenService;

        }
        public RefreshTokenDto FindRefreshTokenNotExpired(string id)
        {
            RefreshToken token =
                _refreshTokenService.Queryable()
                    .SingleOrDefault(
                        r => r.Id == id && r.ExpiresUtc > DateTime.UtcNow);
            if (token != null)
                return Mapper.Map<RefreshTokenDto>(token);
            else
                return null;
        }
        public bool AddRefreshToken(RefreshTokenDto token)
        {
            RefreshToken tokenObj = Mapper.Map<RefreshToken>(token);
            var existingToken = _refreshTokenService.Queryable().Where(r => r.UserName == token.UserName);
            if (existingToken.Any())
            {
                RemoveRefreshToken(existingToken);
            }
            _refreshTokenService.Insert(tokenObj);
            return _unitOfWork.SaveChanges() > 0;
        }
        private void RemoveRefreshToken(IQueryable<RefreshToken> refreshTokens)
        {
            foreach (RefreshToken refreshToken in refreshTokens)
            {
                _refreshTokenService.Delete(refreshToken);
            }
        }
    }
}
