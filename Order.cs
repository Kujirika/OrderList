namespace OrderInShop
{
    internal class Order
    {
        private Dictionary<Guid, OrderItem> _orderList = new();

        private Guid _id;

        private Guid _wareHouseId;
        public enum Status
        {
            Created,
            Paid,
            Cancelled
        }
        private Status _orderStatus;

        public void MarkAsCreated() => _orderStatus = Status.Created;
        public void MarkAsPaid() => _orderStatus = Status.Paid;
        public void MarkAsCancelled() => _orderStatus = Status.Cancelled;

        public Guid WareHouseId
        {
            get { return _wareHouseId; }
        }

        public Order(Guid wareHouseId)
        {
            _wareHouseId = wareHouseId;
            _id = Guid.NewGuid();

            MarkAsCreated();
        }
        public Guid Id
        {
            get { return _id; }
        }


        public void GetOrderList(out Dictionary<Guid, OrderItem> list)
        {
            list = _orderList;
        }

        public bool HasItem(Item item)
        {
            if (_orderList.ContainsKey(item.Id))
                return true;
            else
                return false; //Товара нет в корзине
        }

        public void AddItem(Item item, int count)
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

        public void SetCount(Item item, int count)
        {
            if (count < 0)
                throw new ArgumentException("Количество не может быть отрицательным");

            _orderList.TryGetValue(item.Id, out OrderItem orderItem); 
            orderItem.SetCount(count); 

            if (orderItem.Count == 0)
                _orderList.Remove(item.Id);
        }

        public void Print()
        {
            foreach (var item in _orderList)
            {
                Console.WriteLine($"Корзина:\nТовар - {item.Value.Item.Name}, кол-во - {item.Value.Count}");
            }
        }
    }
}
