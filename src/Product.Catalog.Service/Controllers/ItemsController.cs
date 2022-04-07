using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Product.Catalog.Service.Dtos;
using Product.Catalog.Service.Repositories;

using Product.Catalog.Service.Extensions;
using System.Threading.Tasks;
using Product.Catalog.Service.Entity;

namespace Product.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]

    public class ItemsController : ControllerBase
    {
        /*  private static readonly List<ItemDto> items = new()
         {
             new ItemDto(Guid.NewGuid(), "Lenovo Ts", "Lenovo laptop with 300G Disk and 8G Ram", 600, DateTime.UtcNow),
             new ItemDto(Guid.NewGuid(), "Samsung ", "Sumsung laptop with 200G Disk and 8G Ram", 500, DateTime.UtcNow),
             new ItemDto(Guid.NewGuid(), "MacBook Pro", "Macbook Pro with 156G DDS and 4G Ram", 200, DateTime.UtcNow),
             new ItemDto(Guid.NewGuid(), "MacBook Air Ts", "MackBook Air 300G DDS and 8G Ram", 400, DateTime.UtcNow),

         }; */

        private readonly ItemsRepository _itemsRepository;
        public ItemsController(ItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await _itemsRepository.GetAllAsync())
            .Select(item => item.AsDto());
            return items;
        }



        //Get/item/1234
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {

            var item = await _itemsRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.AsDto();
        }
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            await _itemsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { Id = item.Id }, item);


        }
        // Put/item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateItemDto updateItemDto)
        {

            var existingItem = await _itemsRepository.GetAsync(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;

            await _itemsRepository.UpdateAsync(existingItem);
            return NoContent();

        }
        // DELETE/items/1234
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            await _itemsRepository.RemoveAsync(item.Id);
            return NoContent();
        }


    }





}