namespace Ecommerce.Api.Search.Models
{
    public class Order
    {
        public int Id { get; set; }

        //Removed Customer ID. It is present in Order service
        //Include Customer Name Instead
        public string  CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public Decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
