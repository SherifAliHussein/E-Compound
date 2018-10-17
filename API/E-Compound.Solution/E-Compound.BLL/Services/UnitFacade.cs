using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class UnitFacade : BaseFacade, IUnitFacade
    {
        private IUnitService _unitService;
        private IRoomService _roomService;
        private IAdminService _adminService;
        private IPackageService _packageService;

        public UnitFacade(IUnitService unitService, IPackageService packageService, IAdminService adminService, IRoomService roomService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _unitService = unitService;
            _roomService = roomService;
            _adminService = adminService;
            _packageService = packageService;
        }

        public PagedResultsDto GetAllPagingUnits(long userId, int page, int pageSize)
        {
            var units = _unitService.GetAllPagingUnits(userId, page, pageSize);
            return units;
        }

        public void AddUnit(UnitDto unitDto, long userId)
        {
            var unit = Mapper.Map<Unit>(unitDto);

            unit.CreateTime = DateTime.Now;
            unit.AdminId = userId;
            unit.Name = unitDto.Name;
            unit.IsActive = true;
            unit.UnitTypeId = unitDto.UnitTypeId;

            Package package;

            var packages = _packageService.Query(x => x.AdminId == unit.AdminId).Include(x => x.Units).Select().ToList();
            package = packages.OrderBy(x => x.Start).FirstOrDefault();
            int count = 1;
            while (true)
            {
                if (package.Limit > package.Units.Count(x => !x.IsDeleted))
                {
                    break;
                }
                //else
                //{
                //    consumedWaiters = consumedWaiters - package.MaxNumberOfWaiters;
                //}

                package = packages.OrderBy(x => x.Start).Skip(count).FirstOrDefault();
                count++;
            }
            unit.PackageId = package.PackageId;

            _unitService.Insert(unit);
            SaveChanges();
            var pack = _unitService.GetConsumedUnits(userId);

            //UpdateSubscription(userId, package.PackageGuid, package.Units.Count(x => !x.IsDeleted));
            UpdateSubscription(userId, package.PackageGuid, _unitService.GetConsumedUnits(userId));
        }

        public void UpdateUnit(long userId, UnitDto unitDto)
        {
            var unit = _unitService.Find(unitDto.UnitId);
            if (unit == null) throw new NotFoundException(ErrorCodes.UnitNotFound);

            unit.ModifyTime = DateTime.Now;
            unit.ModifiedBy = userId;
            unit.Name = unitDto.Name;
            unit.UnitTypeId = unitDto.UnitTypeId;
            _unitService.Update(unit);

            SaveChanges();
        }

        public void DeleteUnit(long userId, long unitId)
        {
            var unit = _unitService.Find(unitId);
            if (unit == null) throw new NotFoundException(ErrorCodes.UnitNotFound);

            var hasRelation = _roomService.RelationValidation(userId, unitId);
            if (hasRelation != null) throw new NotFoundException(ErrorCodes.UnitHasRelation);
            unit.IsDeleted = true;
            _unitService.Update(unit);
            SaveChanges();
        }

        public void UpdateSubscription(long adminId, Guid packageGuid, int consumed)
        {
            var admin = _adminService.Find(adminId);
            string url = ConfigurationManager.AppSettings["subscriptionURL"];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/Users/EditUserConsumer");
            //request.Headers.Add("X-Auth-Token:" + token);
            request.ContentType = "application/json";
            request.Method = "POST";
            var serializer = JsonConvert.SerializeObject(new
            {
                userConsumer = consumed,
                userAccountId = admin.UserAccountId,
                backageGuid = packageGuid,
                //productId = admin.ProductId
            });
            //request.ContentLength = serializer.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = serializer;

                streamWriter.Write(json);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var infoResponse = readStream.ReadToEnd();

                response.Close();
                receiveStream.Close();
                readStream.Close();
            }
        }
    }
}
