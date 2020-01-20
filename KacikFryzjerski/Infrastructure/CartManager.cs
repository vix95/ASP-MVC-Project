using KacikFryzjerski.DAL;
using KacikFryzjerski.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Infrastructure
{
    public class CartManager
    {
        private ProjectDbContext db;
        private ISessionManager session;
        public CartManager(ISessionManager session, ProjectDbContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<CartPosition> GetCart()
        {
            List<CartPosition> cart;

            if (session.GetT<List<CartPosition>>(Consts.CartSessionKey) == null)
            {
                cart = new List<CartPosition>();
            }
            else
            {
                cart = session.GetT<List<CartPosition>>(Consts.CartSessionKey) as List<CartPosition>;
            }

            return cart;
        }

        public void AddToCart(int product_id)
        {
            var cart = GetCart();
            var cartPosition = cart.Find(x => x.Product.Id == product_id);

            if (cartPosition != null)
            {
                cartPosition.quantity++;
            }
            else
            {
                var productToAdd = db.Products.Where(x => x.Id == product_id).SingleOrDefault();

                if (productToAdd != null)
                {
                    var newCartPosition = new CartPosition()
                    {
                        Product = productToAdd,
                        quantity = 1,
                        price = productToAdd.Product_price
                    };

                    cart.Add(newCartPosition);
                }
            }

            session.Set(Consts.CartSessionKey, cart);
        }

        public int DeleteFromCart(int product_id)
        {
            var cart = GetCart();
            var cartPosition = cart.Find(x => x.Product.Id == product_id);

            if (cartPosition != null)
            {
                if (cartPosition.quantity > 1)
                {
                    cartPosition.quantity--;
                    return cartPosition.quantity;
                }
                else
                {
                    cart.Remove(cartPosition);
                }
            }

            return 0;
        }

        public double GetCartValue()
        {
            var cart = GetCart();
            return cart.Sum(x => (x.quantity * x.Product.Product_price));
        }

        public int GetQuantityCartPosition()
        {
            var cart = GetCart();
            int quantity = cart.Sum(x => x.quantity);

            return quantity;
        }

        public OrderModels CreateOrder(OrderModels newOrder, string user_id)
        {
            var cart = GetCart();
            newOrder.Order_ordered_at = DateTime.Now;
            newOrder.Order_User_id = user_id;

            db.Orders.Add(newOrder);

            if (newOrder.OrderPosition == null)
                newOrder.OrderPosition = new List<OrderPositionModels>();

            double cartValue = 0;
            foreach (var cartElem in cart)
            {
                var newOrderPosition = new OrderPositionModels()
                {
                    OrderPosition_product_id = cartElem.Product.Id,
                    OrderPosition_quantity = cartElem.quantity,
                    OrderPosition_price = cartElem.Product.Product_price
                };

                cartValue += (cartElem.quantity * cartElem.Product.Product_price);
                newOrder.OrderPosition.Add(newOrderPosition);
            }

            newOrder.Order_total_order_price = cartValue;
            db.SaveChanges();

            return newOrder;
        }

        public void EmptyCart()
        {
            session.Set<List<CartPosition>>(Consts.CartSessionKey, null);
        }
    }
}