using Dapper;
using Microsoft.Data.SqlClient;

namespace ShoppingList.API.Application.Queries
{
    public class ShoppingListQueries : IShoppingListQueries
    {
        private readonly string _connectionString = string.Empty;

        public ShoppingListQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<ShoppingList>> GetAllShoppingListsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                    @"select sl.Id as Id, sl.Title as title, sli.Id as itemid, sli.Description as description, sli.IsCompleted as iscompleted, sli.Quantity as quantity
                        FROM ShoppingLists sl
                        LEFT JOIN ShoppingListItem sli ON sl.Id = sli.ShoppingListId
                        ORDER BY sl.Id"
                    );
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapItemsFromMultipleLists(result);
            }
        }

        public async Task<ShoppingList> GetShoppingListAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                    @"select sl.Id as Id, sl.Title as title, sli.Id as itemid, sli.Description as description, sli.IsCompleted as iscompleted, sli.Quantity as quantity
                        FROM ShoppingLists sl
                        LEFT JOIN ShoppingListItem sli ON sl.Id = sli.ShoppingListId
                        WHERE sl.Id=@id",
                    new { id }
                    );
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return MapItems(result);
            }
        }

        private ShoppingList MapItems(dynamic result)
        {
            var shoppingList = new ShoppingList()
            {
                Id = result[0].Id,
                title = result[0].title,
                items = new List<ShoppingListItem>()
            };

            foreach (dynamic item in result)
            {
                var listItem = new ShoppingListItem()
                {
                    id = item.itemid,
                    description = item.description,
                    iscompleted = item.iscompleted,
                    quantity = item.quantity
                };
                shoppingList.items.Add(listItem);
            }

            return shoppingList;
        }

        private IEnumerable<ShoppingList> MapItemsFromMultipleLists(dynamic result)
        {
            var shoppingLists = new List<ShoppingList>();
            foreach (dynamic item in result)
            {
                var shoppingList = new ShoppingList()
                {
                    Id = item.Id,
                    title = item.title,
                    items = new List<ShoppingListItem>()
                };

                if (!shoppingLists.Any(sls => sls.Id == shoppingList.Id))
                {
                    shoppingLists.Add(shoppingList);
                }
                if (item.itemid != null)
                {
                    var listItem = new ShoppingListItem()
                    {
                        id = item.itemid,
                        description = item.description,
                        iscompleted = item.iscompleted,
                        quantity = item.quantity
                    };
                    shoppingList.items.Add(listItem);
                }
            }
            return shoppingLists;
        }
    }
}
