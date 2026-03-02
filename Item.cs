using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderInShop
{
    internal class Item
    {
        private string _name;
        private int _cost;
        private Guid _id;
        public string Name
        {
            get { return _name; }
        }
        public int Cost
        {
            get { return _cost; }
        }
        public Guid Id
        {
            get { return _id; }
        }
        public Item(string name, int cost)
        {
            if (cost < 0) // Проверка цены на не отрицательное значение
                throw new ArgumentException("Цена не может быть отрицательной");
            if (String.IsNullOrEmpty(name) || name.Length < 2) // Проверка имени продукта на корректное название
                throw new ArgumentException("Некорректное название");

            _name = name;
            _cost = cost;
            _id = Guid.NewGuid();// Назначение ID созданному продукту.
        }
    }
}
