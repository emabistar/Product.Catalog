using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Product.Catalog.Service.Dtos;
namespace Product.Catalog.Service.Controllers
{
   [ApiController]
   [Route("items" )]

   public class ItemsController:ControllerBase
   {
       private static readonly List<ItemDto> items = new ()
       {
        new  ItemDto(Guid.NewGuid(),"Lenovo Ts","Lenovo laptop with 300G Disk and 8G Ram",600, DateTime.UtcNow),
        new  ItemDto(Guid.NewGuid(),"Samsung ","Sumsung laptop with 200G Disk and 8G Ram",500, DateTime.UtcNow),
        new  ItemDto(Guid.NewGuid(),"MacBook Pro","Macbook Pro with 156G DDS and 4G Ram",200, DateTime.UtcNow),
        new  ItemDto(Guid.NewGuid(),"MacBook Air Ts","MackBook Air 300G DDS and 8G Ram",400, DateTime.UtcNow),

       };
        [HttpGet]
        public IEnumerable<ItemDto>Get()
        {
            return items;
        }

       
        //Get/item/1234
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetById(Guid id){

             var item = items.Where(item => item.Id == id).SingleOrDefault();
             if(item == null){
                 return NotFound();
             }
             return item;
        }
        [HttpPost]
        public  ActionResult<ItemDto>Post(CreateItemDto createItemDto){
         var item  = new  ItemDto (Guid.NewGuid(),
         createItemDto.Name, createItemDto.Description,createItemDto.Price, DateTimeOffset.UtcNow
         );
         items.Add(item);
         return  CreatedAtAction(nameof(GetById), new{Id = item.Id}, item);


        }
        // Put/item/{id}
        [HttpPut("{id}")]
        public IActionResult put( Guid id, UpdateItemDto updateItemDto){

         var existingItem  = items.Where(item  => item.Id == id ).SingleOrDefault();
         if(existingItem == null){
                 return NotFound();
             }
         
         var updateItem = existingItem with {
              Name = updateItemDto.Name, 
              Description = updateItemDto.Description,
              Price = updateItemDto.Price,
           
         };
          var index = items.FindIndex(existingItem => existingItem.Id ==id);
          
         if(index == null){
             return NotFound();
         }

          items[index] = updateItem;
          return NoContent();

        }
        // DELETE/items/1234
    [HttpDelete("{id}")]
     public IActionResult  DeleteItem( Guid id){

     var item  = items.FindIndex(existingItem => existingItem.Id == id);
      items.RemoveAt(item);
      return NoContent();
     }

   }





}