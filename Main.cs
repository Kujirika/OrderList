using OrderInShop;

Item banana = new("Банан", 1);// Создание продукта
Item cherry = new("вишня", 2);// Создание продукта
Item pear = new("груша", 3);// Создание продукта
WareHouse wareHouse = new("Ekb");// Создание склада
Order viktor = new(wareHouse);
OrderService vik = new(wareHouse, viktor);


wareHouse.AddItem(banana, 100);
wareHouse.AddItem(cherry, 50);
wareHouse.AddItem(pear, 150);
wareHouse.Print();

wareHouse.Save();

vik.OrderSetQuantity(banana, 90);
vik.OrderSetQuantity(cherry, 40);
vik.OrderSetQuantity(pear, 111);

wareHouse.Save();

vik.OrderPay();




