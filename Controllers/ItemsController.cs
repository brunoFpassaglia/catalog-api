
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
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item is null)
            {
                return NotFound();
            }
            return item;
        }
    }
}