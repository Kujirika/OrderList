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

            if (WareHouseD.ContainsKey(name))
            {
                throw new ArgumentException("Данный продукт уже есть на складе");
            }
            else
                WareHouseD.Add(name, (product, count));
        }

        public void RemoveProduct(string name)
        {
            if (WareHouseD.ContainsKey(name))
            {
                WareHouseD.Remove(name);
                Console.WriteLine($"Товар \"{name}\" удалён.");
            }
            else
                throw new ArgumentException("Данного товара нет на складе");
        }

        Dictionary<string, (Product, int)> WareHouseD = new();
    }
}
