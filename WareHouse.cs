using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop
{
    internal class WareHouse
    {
        Dictionary<Guid, OrderItem> WareHouseD = new();
        public void AddItem(Item item, int count)
        {
            if (WareHouseD.TryGetValue(item.Id, out OrderItem value))
            {
                value.SetCount(value.Count + count); // Увеличение кол-ва  товара на складе.
            }
            else
            {
                OrderItem orderItem = new(item, count);
                WareHouseD.Add(item.Id, orderItem); // Добавление товара на склад
            }
        }

        public void RemoveProduct(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!WareHouseD.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");
            if (value.Count - count < 0)
                throw new ArgumentException("На складе нет такого количества продуктов");

            value.SetCount(value.Count - count);

            if (value.Count == 0)
                WareHouseD.Remove(item.Id);
        }

        public void SetCount(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!WareHouseD.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");

            value.SetCount(count);

            if(value.Count == 0)
                WareHouseD.Remove(item.Id);
        }

        public bool HasProduct(Item item)
        {
            if (WareHouseD.ContainsKey(item.Id))
                return true;
            else
                return false; //Товара нет на складе
        }

        public bool HasEnoughProduct(Item item, int count)
        {
            if (!WareHouseD.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");

                if (value.Count >= count)
                    return true;
                else
                    return false;
        }

        public void Print()
        {
            Console.Write("Склад: ");
            foreach (var item in WareHouseD)
            {
                Console.WriteLine($"Товар: {item.Value.Item.Name}, количество: {item.Value.Count}, цена: {item.Value.Item.Cost}");
            }
        }  
    }
}
