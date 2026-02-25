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
        public void AddProduct(Product product, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            if (WareHouseD.TryGetValue(product.Name, out(Product product, int count) value))
            {
                WareHouseD[product.Name] = (value.product, value.count + count);
            }
            else
                WareHouseD.Add(product.Name, (product, count));
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
                }
                else
                    WareHouseD[name] = (value.product, value.count - count);
            }
            else
                throw new ArgumentException("Товара не существует");
        }

        public bool HasProduct(Product product)
        {
            if (WareHouseD.ContainsKey(product.Name))
                return true;
            else
                return false; //Товара нет на складе
        }

        public bool HasEnoughProduct(Product product, int count, out int сountProduct)
        {
            if (WareHouseD.TryGetValue(product.Name, out (Product product, int count) value))
            {
                if (value.count >= count)
                {
                    сountProduct = count;
                    return true;
                }
                else
                {
                    //Вывод сообщения, что продуктов недостаточно и выдаем максимально доступное число.
                    сountProduct = value.count;
                    return false;
                }
            }
            else
                throw new ArgumentException("Товара не существует");
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
