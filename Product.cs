using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop //Поменять, создание автоматически при добавлении продукта на склад.
{
    internal class Product
    {
        private string _name;
        private int _cost;

        public string Name
        {
            get { return _name; }
        }
        public int Cost
        {
            get { return _cost; }
        }
        public Product(string name, int cost)
        {
            if (cost < 0) //проверка цены на не отрицательное значение
                throw new ArgumentException("Цена не может быть отрицательной");
            if (String.IsNullOrEmpty(name) || name.Length < 2) //проверка имени продукта на корректное название
                throw new ArgumentException("Некорректное название");

            _name = name;
            _cost = cost;
        }
    }
}
