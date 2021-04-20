namespace Computer_house.DataBase.Entities.PC_Options
{
    public class RAM_chanels : ProductWithOnlyName
    {
        public int ID { get; set; }
        //public string Name { get; set; }

        public RAM_chanels() { }
        public RAM_chanels(string _name)
        {
            Name = _name;
        }
        public RAM_chanels(int _id, string _name)
        {
            ID = _id;
            Name = _name;
        }
    }
}
