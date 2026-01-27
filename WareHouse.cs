using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
                    Console.WriteLine($"Продукт \"{name}\" закончился, удаление со склада");
                }
                else
                    WareHouseD[name] = (value.product, value.count - count);
            }
            else
                throw new ArgumentException("Товара не существует");
        }

        public bool HasProduct(string name, int count)
        {
            if (String.IsNullOrEmpty(name) || name.Length < 2)
                throw new ArgumentException("Некорректное название");
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");


            if (WareHouseD.TryGetValue(name, out (Product product, int count) value))
            {
                if (WareHouseD.ContainsKey(name) && value.count - count >= 0)
                    return true;
                else
                    return false;
            }
            else
                return false; //Товара нет на складе
        }

        public void Print()
        {
            foreach (var product in WareHouseD)
            {
                Console.WriteLine($"Товар: {product.Key}, количество: {product.Value.Item2}, цена: {product.Value.Item1.Cost}");
            }
        }

        Dictionary<string, (Product, int)> WareHouseD = new();  
    }
}
