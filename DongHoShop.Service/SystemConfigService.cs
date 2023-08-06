using DongHoShop.Data.Infrastructure;
using DongHoShop.Data.Repositories;
using DongHoShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHoShop.Service
{
    public interface ISystemConfigService
    {
        SystemConfig GetSystemConfig(string code);
    }
    public class SystemConfigService : ISystemConfigService
    {
        ISystemConfigRepository _systemConfigRepository;
        IUnitOfWork _unitOfWork;
        public SystemConfigService(ISystemConfigRepository systemConfigRepository, IUnitOfWork unitOfWork)
        {
            this._systemConfigRepository = systemConfigRepository;
            this._unitOfWork = unitOfWork;
        }

        public SystemConfig GetSystemConfig(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }
    }
}
