using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop
{
    internal class Order
    {
        private Dictionary<Guid, OrderItem> _orderList = new();

        private WareHouse _wareHouse;
        private Guid _id;

        public Order(WareHouse wareHouse)
        {
            _wareHouse = wareHouse; // Принимаем ссылку на склад, чтобы использовать методы класса WareHouse.
            _id = Guid.NewGuid();
        }
        public Guid Id
        {
            get { return _id; }
        }

        public void AddProduct(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!_wareHouse.HasEnoughProduct(item, count)) // Проверка на наличие достаточного кол-ва на складе.
                throw new ArgumentException("Недостаточно товара на складе");

            if (_orderList.TryGetValue(item.Id, out OrderItem value)) // Проверка на наличие товара в корзине.
            {
                value.SetCount(count); // Изменение кол-ва товара в корзине.
            }
            else // Если товара еще нет в корзине.
            {
                OrderItem orderItem = new(item, count);
                _orderList.Add(item.Id, orderItem); // Добавление товара в корзину.
            }
        }

        public void Amount()
        {
            int sum = 0;

            if (_orderList.Count > 0)
            {
                foreach (var item in _orderList)
                {
                    sum += item.Value.Item.Cost * item.Value.Count;
                }
                Console.WriteLine($"Итого: {sum}р.");
            }
            else
                Console.WriteLine("Итого: Корзина пуста");
        }

        public void AddCount(Item item) 
        {
            _orderList.TryGetValue(item.Id, out OrderItem value);
            value.Increase();
        }

        public void RemoveCount(Item item)
        {
            _orderList.TryGetValue(item.Id, out OrderItem value);
            value.Decrease();
        }

        public void SetCount(Item item, int count) // По идее товар уже в корзине, иначе метод не доступны пользователю
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!_orderList.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара нет в корзине");

            if (_wareHouse.HasEnoughProduct(item, count)) // Проверка на наличие нужного кол-ва на складе
                value.SetCount(count);

            if (value.Count == 0)
                _orderList.Remove(item.Id);
        }

        public void Print()
        {
            foreach (var product in _orderList)
            {
                Console.WriteLine($"Корзина: Товар - {product.Value.Item.Name}, кол-во - {product.Value.Count}");
            }
        }
    }
}
