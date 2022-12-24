using System.ComponentModel.DataAnnotations.Schema;

namespace Basket.API.Entities;

public class BasketCart
{
    public string UserName { get; set; }
    public List<BasketCartItem> Items { get; set; } = new List<BasketCartItem>();

    public BasketCart()
    {
    }

    public BasketCart(string userName)
    {
        UserName = userName;
    }

    [Column(TypeName = "decimal(18,2)")]
    public Decimal TotalPrice
    {
        get 
        {
            return Items.Sum(x => x.Price * x.Quantity);

            //decimal totalprice = 0;
            //foreach (var item in Items)
            //{
            //    totalprice += item.Price * item.Quantity;
            //}
            //return totalprice;
        }
    }
}
