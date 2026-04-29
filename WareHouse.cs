using System.Net.WebSockets;
using System.Runtime.Serialization.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace OrderInShop
{
    internal class WareHouse
    {

        private Dictionary<Guid, OrderItem> _wareHouseDict = new();
        private string _wareHouseName;
        private Guid _id;

        public Guid Id
        {
            get { return _id; }
        }

        static string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName; // поднимаемся из bin/Debug/net8.0/

        public WareHouse(string wareHouseName) 
        {
            if (string.IsNullOrEmpty(wareHouseName) || wareHouseName.Length < 3)
                throw new Exception("Неверное имя склада");

            _wareHouseName = wareHouseName;
            _id = Guid.NewGuid();
        }

        public void AddItem(Item item, int count)
        {
            if (_wareHouseDict.TryGetValue(item.Id, out OrderItem value))
            {
                value.SetCount(value.Count + count); // Увеличение кол-ва  товара на складе.
            }
            else
            {
                OrderItem orderItem = new(item, count);
                _wareHouseDict.Add(item.Id, orderItem); // Добавление товара на склад
            }
        }

        public void RemoveItem(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!_wareHouseDict.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");
            if (value.Count - count < 0)
                throw new ArgumentException("На складе нет такого количества продуктов");

            value.SetCount(value.Count - count);

            if (value.Count == 0)
                _wareHouseDict.Remove(item.Id);
        }

        public void SetCount(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!_wareHouseDict.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");

            value.SetCount(count);

            if (value.Count == 0)
                _wareHouseDict.Remove(item.Id);
        }

        public bool HasItem(Item item)
        {
            if (_wareHouseDict.ContainsKey(item.Id))
                return true;
            else
                return false; //Товара нет на складе
        }

        public bool HasEnoughItem(Item item, int count)
        {
            if (!_wareHouseDict.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");

            if (value.Count >= count)
                return true;
            else
            {
                Console.WriteLine($"На складе {value.Count}шт");
                return false;
            }
        }

        public bool CanReserveItem(OrderItem ordItem)
        {
            _wareHouseDict.TryGetValue(ordItem.Item.Id, out OrderItem wareHouse);

            if (ordItem.Count <= wareHouse.Count)
                return true;
            else
                return false;
        }

        public void ReserveItem(OrderItem ordItem)
        {
            _wareHouseDict.TryGetValue(ordItem.Item.Id, out OrderItem wareHouse);

            wareHouse.SetCount(wareHouse.Count - ordItem.Count);
        }

        public void Save()
        {

            string fileName = string.Concat(_wareHouseName + "WareHouse.json");
            string path = Path.Combine(projectPath, fileName);

            Console.WriteLine($"File name = {fileName}, path = {path}");

            string json = JsonSerializer.Serialize(_wareHouseDict, new JsonSerializerOptions
            {
                WriteIndented = true, // чтобы JSON был красивым, с отступами
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            });

            File.WriteAllText(path, json);
        }
        public void Load()
        {
            string path = Path.Combine(projectPath, "wareHouse.json");

            string json = File.ReadAllText(path);
            var jsonRead = JsonSerializer.Deserialize<Dictionary<Guid, OrderItem>>(json);
            if (jsonRead != null)
            {
                _wareHouseDict = jsonRead;
            }
        }

        public void Print()
        {
            Console.WriteLine("Склад: ");
            foreach (var item in _wareHouseDict)
            {
                Console.WriteLine($"Товар: {item.Value.Item.Name}, количество: {item.Value.Count}, цена: {item.Value.Item.Cost}");
            }
        }
    }
}
