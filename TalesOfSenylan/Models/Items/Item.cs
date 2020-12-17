namespace TalesOfSenylan.Models.Items
{
    public abstract class Item
    {
        public string name { get; }

        protected Item(string name)
        {
            this.name = name;
        }
    }
}