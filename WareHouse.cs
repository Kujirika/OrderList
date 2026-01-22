using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop
{
    internal class WareHouse
    {
        public void AddProduct(string name ,Product product, int count)
        {
            if (String.IsNullOrEmpty(name) || name.Length < 2) 
                throw new ArgumentException("Некорректное название");
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            if (WareHouseD.TryGetValue(name, out(Product product, int count) value))
            {
                WareHouseD[name] = (value.product, value.count + count);
            }
            else
                WareHouseD.Add(name, (product, count));
        }

        public void RemoveProduct(string name, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            if (WareHouseD.TryGetValue(name, out (Product product, int count) value))
            {
                if (value.count - count < 0)
                    throw new ArgumentException("На складе нет такого количества продуктов");
                if (value.count - count == 0)
                {
                    WareHouseD.Remove(name);
                    Console.WriteLine($"Продукт \"{name}\" закончился");
                }
                else
                    WareHouseD[name] = (value.product, value.count - count);
            }
            else
                throw new ArgumentException("Товара не существует");
        }

        Dictionary<string, (Product, int)> WareHouseD = new();
    }
}
