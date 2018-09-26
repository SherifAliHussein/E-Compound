using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface ITemplateFacade
    {
        List<TemplateDTO> GetAllTemplates();
        void AddTemplateForCategory(long categoryId,List<PageDTO> pageDtos);
    }
}
