namespace OrderInShop
{
    internal class Order
    {
        private Dictionary<Guid, OrderItem> _orderList = new();

        private WareHouse _wareHouse;
        private Guid _id;

        public Order(WareHouse wareHouse)
        {
            _wareHouse = wareHouse; // Принимаем ссылку на склад, чтобы использовать методы класса WareHouse.
            _id = Guid.NewGuid();
        }
        public Guid Id
        {
            get { return _id; }
        }

        public void OrderList(out Dictionary<Guid, OrderItem> list)
        {
            list = _orderList;
        }

        public bool HasProduct(Item item)
        {
            if (_orderList.ContainsKey(item.Id))
                return true;
            else
                return false; //Товара нет в корзине
        }

        public void AddProduct(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            OrderItem orderItem = new(item, count); //Объединение товара и кол-ва в одну сущность.
            _orderList.Add(item.Id, orderItem); // Добавление товара в корзину.

        }

        public void Amount()
        {
            int sum = 0;

            if (_orderList.Count > 0)
            {
                foreach (var item in _orderList)
                {
                    sum += item.Value.Item.Cost * item.Value.Count;
                }
                Console.WriteLine($"Итого: {sum}р.");
            }
            else
                Console.WriteLine("Итого: Корзина пуста");
        }

        public void AddCount(Item item) // TODO возможно не нужен уже
        {
            _orderList.TryGetValue(item.Id, out OrderItem ordItem);
            ordItem.Increase();
        }

        public void RemoveCount(Item item) // TODO возможно не нужен уже
        {
            _orderList.TryGetValue(item.Id, out OrderItem ordItem);
            ordItem.Decrease();
        }

        public void SetCount(Item item, int count) // По идее товар уже в корзине, иначе метод не доступен пользователю в UI.
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            _orderList.TryGetValue(item.Id, out OrderItem ordItem);
            ordItem.SetCount(count); 

            if (ordItem.Count == 0)
                _orderList.Remove(item.Id);
        }

        public void Print()
        {
            foreach (var product in _orderList)
            {
                Console.WriteLine($"Корзина: Товар - {product.Value.Item.Name}, кол-во - {product.Value.Count}");
            }
        }
    }
}
