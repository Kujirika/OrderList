using System.Collections.Specialized;

namespace OrderInShop
{
    internal class OrderService
    {
        private WareHouse _orderWareHouse;
        private Order _order;

        // Получается нужно тут делать свой список заказов. Уник ID для заказов. Так как пользователь не всегда регается для заказа.
        private Guid _orderId;
        private Dictionary<Guid, Order> OrderDictionary = new();

        public OrderService(WareHouse wareHouse, Order order)
        {
            _orderWareHouse = wareHouse;
            _order = order;
        }

        public void OrderIncreaseQuantity(Item item) => OrderSetQuantity(item, 1); // В UI это "+"

        public void OrderDecreaseQuantity(Item item) => OrderSetQuantity(item, -1); // В UI это "-" //TODO исключение "-1", мб OrderItem использовать

        public void OrderSetQuantity(Item item, int count) // В UI это поле для ввода между "+" и "-" или вызов при выборе товара в каталоге.(сделать два разным метода? в будущем)
        {
            if (!_order.HasProduct(item)) // Проверка на наличие товара в корзине
            {
                if (!_orderWareHouse.HasEnoughProduct(item, count)) // Проверка на наличие достаточного кол-ва на складе. 
                    throw new ArgumentException("Недостаточно товара на складе"); //TODO выдача кол-ва которое есть на складе, вместо Exception

                _order.AddProduct(item, count);
            }
            else  // Товар есть в корзине
            {
                if (!_orderWareHouse.HasEnoughProduct(item, count)) // Проверка на наличие достаточного кол-ва на складе. 
                    throw new ArgumentException("Недостаточно товара на складе"); //TODO выдача кол-ва которое есть на складе, вместо Exception

                _order.SetCount(item, count);
            }
        }

        // Оплата.
        public void OrderPay()
        {
            _order.OrderList(out var OrderList); // Делаем копию списка корзины.

            foreach (var item in OrderList) // Проверяем, что товара хватает на складе
            {
                if (!_orderWareHouse.CanReserveProduct(item.Value))
                    throw new ArgumentException("Недостаточно продуктов на складе");
            }
            
            foreach (var item in OrderList) // Резервирование после проверки, что каждого вида товара хватает на складе. 
            {
                _orderWareHouse.ReserveProduct(item.Value);
                // Надо реализовать резервацию на 10 минут.
            }

            _order.Amount();
        }
    }
}
