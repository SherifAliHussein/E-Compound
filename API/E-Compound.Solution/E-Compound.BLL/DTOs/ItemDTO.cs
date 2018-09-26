using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class ItemDTO
    {
        public long ItemID { get; set; }
        public string ItemName { get; set; }
        public Dictionary<string, string> ItemNameDictionary { get; set; }
        public string ItemDescription { get; set; }
        public Dictionary<string, string> ItemDescriptionDictionary { get; set; }
        public long CategoryId { get; set; }
        public long MenuId { get; set; }
        public long RestaurantId { get; set; }
        //public double Price { get; set; }
        public List<SizeDto> Sizes { get; set; }
        public List<ItemSizeDto> ItemSizes { get; set; }
        public List<SideItemDTO> SideItems{ get; set; }
        public MemoryStream Image { get; set; }
        public MemoryStream Image2 { get; set; }
        public MemoryStream Image3 { get; set; }
        public bool IsImageChange { get; set; }
        public bool IsImage2Change { get; set; }
        public bool IsImage3Change { get; set; }
        public int MaxSideItemValue { get; set; }
        public bool IsActive { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}
