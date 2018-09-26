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
    public class PageService:Service<Page>,IPageService
    {
        public PageService(IRepositoryAsync<Page> repository):base(repository)
        {
        }

        public int GetLastPageNumberForCategory(long categoryId)
        {
            var lastRecord = _repository.Query(x => x.CategoryId == categoryId).Select().OrderBy(x => x.PageNumber).LastOrDefault();
            return lastRecord != null ? lastRecord.PageNumber : 0;
        }
    }
}
