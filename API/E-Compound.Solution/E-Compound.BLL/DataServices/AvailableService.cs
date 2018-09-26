using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class AvailableService:Service<Available>,IAvailableService
    {
        public AvailableService(IRepositoryAsync<Available> repository):base(repository)
        {
            _repository = repository;
        }
        public void InsertOrUpdateRange(List<Available> availables)
        {
            foreach (var available in availables)
            {
                if(_repository.Find(available.AvailableId)!=null)
                _repository.Update(available);
                else
                {
                    _repository.Insert(available);
                }
            }
        }
    }
}
