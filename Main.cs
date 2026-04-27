using OrderInShop;

// Создание продуктов
Item banana = new("Банан", 1);
Item cherry = new("вишня", 2);
Item pear = new("груша", 3);



//Заполнение склада


OrderService orderService = new OrderService();// Создаём сервис, чтобы можно было использовать его функционал

var wareHouse = orderService.CreateNewWareHouse("Ekb");

wareHouse.AddItem(pear, 110);
wareHouse.AddItem(cherry, 11);
wareHouse.AddItem(banana, 1);

var order = orderService.CreateNewOrder(wareHouse.Id);// Создаем заказ через OrderService

wareHouse.Print();

orderService.SetItemQuantity(order,pear, 100);

order.Print();