using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop
{
    internal class Order
    {
        private Dictionary<string, (Product, int)> _orderList = new();
        private WareHouse _wareHouse;

        public Order(WareHouse wareHouse)
        {
            _wareHouse = wareHouse; // Принимаем ссылку на склад, чтобы использовать методы класса WareHouse.
        }

        public void AddProduct(Product product, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            count = count == 0 ? 1 : count; //Если при добавлении в коризу не указано кол-во, то добавляем с 1шт.

            if (_orderList.TryGetValue(product.Name, out (Product product, int count) value)) // Проверка на наличие товара в корзине.
            {
                if (_wareHouse.HasEnoughProduct(product, count, out int сountProduct)) // Проверка на наличие нужного кол-ва на складе.
                {
                    _orderList[product.Name] = (value.product, сountProduct); // Изменение кол-ва товара в корзине.
                }
            }
            else // Если товара еще нет в корзине.
            {
                if (_wareHouse.HasEnoughProduct(product, count, out int сountProduct)) // Проверка на наличие нужного кол-ва на складе.
                {
                    _orderList.Add(product.Name, (product, сountProduct)); // Добавление товара в корзину.
                }
            }
        }

        public void Amount()
        {
            int sum = 0;

            if (_orderList.Count > 0)
            {
                foreach (var item in _orderList)
                {
                    sum += item.Value.Item1.Cost * item.Value.Item2;
                }
                Console.WriteLine($"Итого: {sum}р.");
            }
            else
                Console.WriteLine("Итого: Корзина пуста");
        }

        public void AddCount(Product product) 
        {
            _orderList.TryGetValue(product.Name, out (Product product, int count) value); // По идее товар уже в корзине, иначе метод не доступны пользователю

            SetCount(product, value.count + 1);
        }

        public void RemoveCount(Product product) // По идее товар уже в корзине, иначе метод не доступны пользователю
        {
            _orderList.TryGetValue(product.Name, out (Product product, int count) value);

            SetCount(product, value.count - 1);
        }

        public void SetCount(Product product, int count) // По идее товар уже в корзине, иначе метод не доступны пользователю
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

        _orderList.TryGetValue(product.Name, out (Product product, int count) value); 

        if (count == 0) // Удаление товара из списка корзины, если кол-во товара = 0.
        {
            _orderList.Remove(product.Name);
        }
        else
        {
            if (_wareHouse.HasEnoughProduct(product, count, out int сountProduct)) // Проверка на наличие нужного кол-ва на складе
            {
                _orderList[product.Name] = (value.product, сountProduct); // Изменение кол-ва товара в корзине.
            }
            // else недостаточно товара на складе UI уведомление.
        }
        }

        public void Print()
        {
            foreach (var product in _orderList)
            {
                Console.WriteLine($"Корзина: Товар - {product.Key}, кол-во - {product.Value.Item2}");
            }
        }
    }
}
