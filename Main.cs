using OrderInShop;

Product banana = new Product("Банан", 10);
Product pear = new Product("Груша", 5);
Product grape = new Product("Виногдар", 13); 




WareHouse wareHouse = new();
wareHouse.AddProduct("Банан", banana, 100);
wareHouse.AddProduct("Груша", pear, 50);
wareHouse.AddProduct("Виногдар", grape, 13);

wareHouse.Print();

Order check1 = new();