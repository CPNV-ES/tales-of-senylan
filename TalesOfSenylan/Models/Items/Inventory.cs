using System;
using System.Collections.Generic;

namespace TalesOfSenylan.Models.Items
{
    public class Inventory : Dictionary<Item, int>
    {
        public void AddItem(Item item)
        {
            try
            {
                var quantity = ContainsKey(item) ? this[item] + 1 : 1;
                this[item] = quantity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}