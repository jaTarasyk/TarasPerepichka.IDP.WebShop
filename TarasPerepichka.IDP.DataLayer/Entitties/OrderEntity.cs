namespace TarasPerepichka.IDP.DataLayer.Entitties
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string UserRef { get; set; }
        public int Quantity { get; set; }

        public int ArticleId { get; set; }
        public ArticleEntity Article { get; set; }
    }
}
