using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop
{
    internal class Order
    {
        Dictionary<string, (Product, int)> OrderList = new();
        public void AddProduct(string name, Product product, int count)
        {
            if (String.IsNullOrEmpty(name) || name.Length < 2)
                throw new ArgumentException("Некорректное название");
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            if (OrderList.ContainsKey(name))
            {
                OrderList[name] = (product, count);
            }
            else
                OrderList.Add(name, (product, count));
        }
        public void Amount()
        {
            int sum = 0;

            foreach (var item in OrderList)
            {
                Product product = item.Value.Item1;
                int count = item.Value.Item2;
                sum += product.Cost * count;
            }

            Console.WriteLine($"С вас: {sum}р.");
        }
        public void AddCount(string name)
        {
            if (OrderList.TryGetValue(name, out (Product product, int count) value))
            {
                OrderList[name] = (value.product, value.count + 1);
            }
            else
                throw new ArgumentException("Товара не существует");
        }
        public void RemoveCount(string name)
        {
            if (OrderList.TryGetValue(name, out (Product product, int count) value))
            {
                if (value.count - 1 <= 0)
                {
                    OrderList.Remove(name);
                    Console.WriteLine($"Удаление {name} из корзины");
                }
                else
                    OrderList[name] = (value.product, value.count - 1);
            }
            else
                throw new ArgumentException("Товара не существует");
        }
        public void SetCount(string name, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество должно быть больше 0");
            if (OrderList.TryGetValue(name, out (Product product, int count) value))
            {
                if (count == 0)
                {
                    OrderList.Remove(name);
                    Console.WriteLine($"Удаление товара {name}");
                }
                OrderList[name] = (value.product, count);
            }
            else
                throw new ArgumentException("Товара не существует");
        }

        public void Print()
        {
            foreach (var product in OrderList)
            {
                Console.WriteLine(product);
            }
        }
    }
}
