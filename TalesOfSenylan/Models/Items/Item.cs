namespace TalesOfSenylan.Models.Items
{
    public abstract class Item
    {
        public string name { get; }

        protected Item(string name)
        {
            this.name = name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            var item = obj as Item;

            if (item == null)
            {
                return false;
            }
            
            return name.Equals(item.name);
        }
    }
}