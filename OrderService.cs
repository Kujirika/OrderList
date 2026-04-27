using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace OrderInShop
{
    internal class OrderService
    {
        private Dictionary<Guid, Order> _orders = new();
        private Dictionary<Guid, WareHouse> _wareHouses = new();

        public WareHouse CreateNewWareHouse(string wareHouseName)
        {
            var wareH = new WareHouse(wareHouseName);
            _wareHouses.Add(wareH.Id, wareH);
            return wareH;
        }
        public Order CreateNewOrder(Guid wareHouseID)
        {
            var order = new Order(wareHouseID);
            _orders.Add(order.Id, order);
            return order;
        }
        
        //public void OrderIncreaseQuantity(Item item) => OrderSetQuantity(item, 1); // В UI это "+"

        //public void OrderDecreaseQuantity(Item item) => OrderSetQuantity(item, -1); // В UI это "-" //TODO исключение "-1", мб OrderItem использовать

        public void SetItemQuantity(Order order, Item item, int count) // В UI это поле для ввода между "+" и "-" или вызов при выборе товара в каталоге.(сделать два разным метода? в будущем)
        {
            var wareHouse = GetWareHouseByOrder(order);

            if (!order.HasItem(item)) // Проверка на наличие товара в корзине
            {
                if (!wareHouse.HasEnoughItem(item, count)) // Проверка на наличие достаточного кол-ва на складе. 
                    throw new ArgumentException("Недостаточно товара на складе"); //TODO выдача кол-ва которое есть на складе, вместо Exception

                order.AddItem(item, count);
            }
            else  // Товар есть в корзине
            {
                if (!wareHouse.HasEnoughItem(item, count)) // Проверка на наличие достаточного кол-ва на складе. 
                    throw new ArgumentException("Недостаточно товара на складе"); //TODO выдача кол-ва которое есть на складе, вместо Exception

                order.SetCount(item, count);
            }
        }

        // Оплата.
        public void OrderPay(Order order)
        {
            var wareHouse = GetWareHouseByOrder(order);

            order.GetOrderList(out var OrderList); // Делаем копию списка корзины.

            foreach (var item in OrderList) // Проверяем, что товара хватает на складе
            {
                if (!wareHouse.CanReserveItem(item.Value))
                    throw new ArgumentException("Недостаточно продуктов на складе");
            }
            
            foreach (var item in OrderList) // Резервирование после проверки, что каждого вида товара хватает на складе. 
            {
                wareHouse.ReserveItem(item.Value);
                // Надо реализовать резервацию на 10 минут.
            }
        }
        private WareHouse GetWareHouseByOrder(Order order)
        {
            if (!_wareHouses.TryGetValue(order.WareHouseId, out WareHouse wareHouse))
                throw new Exception("Склада не существует");
            return wareHouse;
        }
    }
}
