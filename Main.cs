using OrderInShop;

Item banana = new("Банан", 1);// Создание продукта
Item cherry = new("вишня", 2);// Создание продукта
Item pear = new("груша", 3);// Создание продукта
WareHouse YKT_WareHouse = new();// Создание склада
Order viktor = new(YKT_WareHouse);
OrderService vik = new(YKT_WareHouse, viktor);


YKT_WareHouse.AddItem(banana, 100);
YKT_WareHouse.AddItem(cherry, 50);
YKT_WareHouse.AddItem(pear, 150);
YKT_WareHouse.Print();

vik.OrderSetQuantity(banana, 90);
vik.OrderSetQuantity(cherry, 40);
vik.OrderSetQuantity(pear, 111);

YKT_WareHouse.Save();

vik.OrderPay();




