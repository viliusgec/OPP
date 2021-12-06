namespace Client
{
    class ShopItems
    {
        string Name { get; set; }
        int ID { get; set; }
        public ShopItems(string Name, int ID)
        {
            this.Name = Name;
            this.ID = ID;
        }
    }
}
