using OrderInShop;

Product banana = new Product("Банан", 10);
Product pear = new Product("Груша", 5);
Product grape = new Product("Виногдар", 13); 

Order check1 = new();
check1.AddProduct("Банан", banana, 10);
check1.AddProduct("Груша", pear, 30);
check1.Amount();

check1.AddCount("Банан");
check1.AddCount("Банан");
check1.AddCount("Банан");
check1.AddCount("Банан");
check1.RemoveCount("Груша");
check1.Amount();

check1.RemoveCount("Груша");
check1.SetCount("Банан", 1);
check1.Amount();