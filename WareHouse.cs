using System.Runtime.Serialization.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace OrderInShop
{
    internal class WareHouse
    {
        private Dictionary<Guid, OrderItem> _wareHouseD = new();
        public void AddItem(Item item, int count)
        {
            if (_wareHouseD.TryGetValue(item.Id, out OrderItem value))
            {
                value.SetCount(value.Count + count); // Увеличение кол-ва  товара на складе.
            }
            else
            {
                OrderItem orderItem = new(item, count);
                _wareHouseD.Add(item.Id, orderItem); // Добавление товара на склад
            }
        }

        public void RemoveProduct(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!_wareHouseD.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");
            if (value.Count - count < 0)
                throw new ArgumentException("На складе нет такого количества продуктов");

            value.SetCount(value.Count - count);

            if (value.Count == 0)
                _wareHouseD.Remove(item.Id);
        }

        public void SetCount(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");
            if (!_wareHouseD.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");

            value.SetCount(count);

            if (value.Count == 0)
                _wareHouseD.Remove(item.Id);
        }

        public bool HasProduct(Item item)
        {
            if (_wareHouseD.ContainsKey(item.Id))
                return true;
            else
                return false; //Товара нет на складе
        }

        public bool HasEnoughProduct(Item item, int count)
        {
            if (!_wareHouseD.TryGetValue(item.Id, out OrderItem value))
                throw new ArgumentException("Товара не существует на складе");

            if (value.Count >= count)
                return true;
            else
            {
                Console.WriteLine($"На складе {value.Count}шт");
                return false;
            }
        }

        public bool CanReserveProduct(OrderItem ordItem)
        {
            _wareHouseD.TryGetValue(ordItem.Item.Id, out OrderItem wareHouse);

            if (ordItem.Count <= wareHouse.Count)
                return true;
            else
                return false;
        }

        public void ReserveProduct(OrderItem ordItem)
        {
            _wareHouseD.TryGetValue(ordItem.Item.Id, out OrderItem wareHouse);

            wareHouse.SetCount(_wareHouseD.Count - ordItem.Count);
        }

        public void Save()
        {
            string projectPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)
                        .Parent.Parent.Parent.FullName; // поднимаемся из bin/Debug/net8.0/

            // Создаём полный путь к файлу
            string path = Path.Combine(projectPath, "wareHouse.json");

            var jsonDict = new Dictionary<Guid, OrderItem>(_wareHouseD); // создаем переменную в которую копируем словарь товаров на складе

            string json = JsonSerializer.Serialize(jsonDict, new JsonSerializerOptions
            {
                WriteIndented = true, // чтобы JSON был красивым, с отступами
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            });

            File.WriteAllText(path, json);
        }
        public void Load()
        { 
            
        }

        public void Print()
        {
            Console.Write("Склад: ");
            foreach (var item in _wareHouseD)
            {
                Console.WriteLine($"Товар: {item.Value.Item.Name}, количество: {item.Value.Count}, цена: {item.Value.Item.Cost}");
            }
        }
    }
}
