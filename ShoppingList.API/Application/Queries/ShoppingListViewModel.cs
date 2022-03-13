namespace ShoppingList.API.Application.Queries
{
    public record ShoppingList
    {
        public int Id { get; init; }
        public string title { get; init; }
        public List<ShoppingListItem> items { get; set; }
    }

    public record ShoppingListItem
    {
        public int id { get; init; }
        public string description { get; init; }
        public int quantity { get; init; }
        public bool iscompleted { get; init; }
    }
}
