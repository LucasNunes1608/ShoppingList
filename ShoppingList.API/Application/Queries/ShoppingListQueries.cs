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
        public async Task<ShoppingList> GetShoppingListAsync(int id)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<dynamic>(
                    @"select sl.Title as title, sli.Description as description, sli.IsCompleted as iscompleted, sli.Quantity as quantity
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
                title = result[0].title,
                items = new List<ShoppingListItem>()
            };

            foreach (dynamic item in result)
            {
                var listItem = new ShoppingListItem()
                {
                    description = item.description,
                    iscompleted = item.iscompleted,
                    quantity = item.quantity
                };
                shoppingList.items.Add(listItem);
            }

            return shoppingList;
        }
    }
}
