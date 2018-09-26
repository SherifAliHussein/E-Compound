using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.Services.ManageStorage
{
    public interface IManageStorage
    {
        void UploadImage(string path, MemoryStream image, string id);
    }
}
