using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OrderInShop
{
    internal class OrderItem
    {
        private Item _item;
        private int _count;

        public Item Item
        {
            get { return _item; }
        }
        public int Count
        { 
            get { return _count; }
        }

        public OrderItem(Item item, int count)
        {
            _item = item;
            _count = count;
        }

        public void SetCount(int count) => _count = count;

        public void Increase() => _count += 1;
        public void Decrease() => _count -= 1;
    }
}
