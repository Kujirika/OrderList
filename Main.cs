using OrderInShop;

Product banana = new("Банан", 1);// Создание продукта
WareHouse YKT_WareHouse = new();// Создание склада
Order Viktor = new(YKT_WareHouse);// Создание заказа на имя пользователя и привязка к складу

YKT_WareHouse.AddProduct(banana, 100);
YKT_WareHouse.Print();

Viktor.AddProduct(banana, 110);
Viktor.Print();
Viktor.AddCount(banana);
Viktor.Print();
Viktor.RemoveCount(banana);
Viktor.Print();
Viktor.SetCount(banana, 0);
Viktor.Print();