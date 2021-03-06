
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly IInMemItemsRepository repository;
        public ItemsController(IInMemItemsRepository repository)
        {
            this.repository = repository;

        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repository.GetItems().Select(item => item.asDto());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return item.asDto();
        }

        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            var item = new Item { Id = Guid.NewGuid(), Name = itemDto.Name, Price = itemDto.Price, CreatedDate = DateTimeOffset.UtcNow };
            repository.CreateItem(item);
            // return item.asDto();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.asDto());

        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdatetemDto itemDto)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();

            }

            Item updateItem = item with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repository.UpdateItem(updateItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            repository.DeleteItem(id);
            return NoContent();
        }
    }

}