namespace ShoppingList.API.Application.Queries
{
    public record ShoppingList
    {
        public string title { get; init; }
        public List<ShoppingListItem> items { get; set; }
    }

    public record ShoppingListItem
    {
        public string description { get; init; }
        public int quantity { get; init; }
        public bool iscompleted { get; init; }
    }
}
