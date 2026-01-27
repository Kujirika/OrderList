using OrderInShop;

Product banana = new Product("Банан", 10);
Product pear = new Product("Груша", 5);
Product grape = new Product("Виногдар", 13); 




WareHouse wareHouse = new();
wareHouse.AddProduct( banana, 100);
wareHouse.AddProduct( pear, 50);
wareHouse.AddProduct( grape, 13);

wareHouse.Print();

Order check1 = new();