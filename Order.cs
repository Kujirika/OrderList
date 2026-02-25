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
            _wareHouse = wareHouse; // Принимаем ссылку на склад, чтобы использовать методы класса WareHouse
        }

        public void AddProduct(string name, Product product, int count)
        {
            if (String.IsNullOrEmpty(name) || name.Length < 2)
                throw new ArgumentException("Некорректное название");
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            if (_orderList.ContainsKey(name))
            {
                _orderList[name] = (product, count);
            }
            else
                _orderList.Add(name, (product, count));
        }

        public void Amount()
        {
            int sum = 0;

            foreach (var item in _orderList)
            {
                Product product = item.Value.Item1;
                int count = item.Value.Item2;
                sum += product.Cost * count;
            }
            Console.WriteLine($"С вас: {sum}р.");
        }

        public void AddCount(Product product) 
        {
            _orderList.TryGetValue(product.Name, out (Product product, int count) value); // По идее товар уже в корзине, иначе методы не доступны пользователю

            if (_wareHouse.HasEnoughProduct(product, value.count + 1, out int сountProduct)) // Проверка на наличие нужного кол-ва на складе
            {
                _orderList[product.Name] = (value.product, сountProduct); // Изменение кол-ва товара в корзине.
            }
            // else недостаточно товара на складе
        }

        public void RemoveCount(Product product)
        {
            _orderList.TryGetValue(product.Name, out (Product product, int count) value); // По идее товар уже в корзине, иначе методы не доступны пользователю

            if (value.count - 1 == 0)// Удаление товара из списка корзины, если кол-во товара = 0.
            {
                _orderList.Remove(product.Name);
            }
            else
                _orderList[product.Name] = (value.product, value.count - 1); // Изменение кол-ва товара в корзине.   
        }

        public void SetCount(Product product, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (_orderList.TryGetValue(product.Name, out (Product product, int count) value)) // Проверка на наличие товара в корзине.
            {
                if (count == 0)// Удаление товара из списка корзины, если кол-во товара = 0.
                {
                    _orderList.Remove(product.Name); 
                }
                else
                {
                    if (_wareHouse.HasEnoughProduct(product, count, out int сountProduct)) // Проверка на наличие нужного кол-ва на складе
                    {
                        _orderList[product.Name] = (value.product, сountProduct); // Изменение кол-ва товара в корзине.
                    }
                    // else недостаточно товара на складе
                }
            }
            else
                throw new ArgumentException("Товара не существует в корзине");
        }

        public void Print()
        {
            foreach (var product in _orderList)
            {
                Console.WriteLine(product);
            }
        }
    }
}
