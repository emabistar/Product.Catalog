using Product.Catalog.Service.Dtos;
using Product.Catalog.Service.Entity;

namespace Product.Catalog.Service.Extensions
{

    /***
Translate Item entity to ItemDto for the purpose of returning to 
 CRURD operation for the Rest Api
*/

    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}