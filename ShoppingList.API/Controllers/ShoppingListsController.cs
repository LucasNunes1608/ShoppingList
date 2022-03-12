using Microsoft.AspNetCore.Mvc;
using ShoppingList.API.Application.Queries;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingList.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingListsController : ControllerBase
    {
        private readonly IShoppingListQueries _shoppingListQueries;
        public ShoppingListsController(IShoppingListQueries shoppingListQueries)
        {
            _shoppingListQueries = shoppingListQueries ?? throw new ArgumentNullException(nameof(shoppingListQueries));
        }

        [Route("{ShoppingListId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Domain.AggregatesModel.ShoppingList), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetShoppingListAsync(int ShoppingListId)
        {
            try
            {
                var shoppingList = await _shoppingListQueries.GetShoppingListAsync(ShoppingListId);
                return Ok(shoppingList);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }
    }
}
