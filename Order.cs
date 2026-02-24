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
            SetCount(product, 1);
        }
        public void RemoveCount(Product product)
        {
            SetCount(product, 1/*-1*/); // -1 Вызывает исключение.
        }
        public void SetCount(Product product, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (_orderList.TryGetValue(product.Name, out (Product product, int count) value)) //Проверка на наличие товара в корзине.
            {
                if (count == 0)
                {
                    _orderList.Remove(product.Name); //Удаление товара из списка корзины, если кол-во товара = 0.
                }
                else
                {
                    if (_wareHouse.ReserveProduct(product, count, out int reservedCount)) //Проверка на возможность зарезервировать товар на складе.
                    {
                        _orderList[product.Name] = (value.product, count); //Изменение кол-ва товара в корзине.
                    }
                    // else недостаточное кол-во на складе
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
