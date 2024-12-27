namespace Mango.Service.ShoppingCartAPI.Models.Dto
{
    public class CartDto
    {
        public CartHeaderDto CartHeader{ get; set; }
        public virtual IEnumerable<CartDetailsDto>? CartDetails { get; set; }
    }
}
