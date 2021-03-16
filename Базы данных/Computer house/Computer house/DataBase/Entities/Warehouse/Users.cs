
namespace Computer_house.DataBase.Entities.Warehouse
{
    class Users
    {
        public int ID { get; set; }

        public string Login { get; private set; }

        public string Password { get; private set; }

        public bool Authorization_status { get; set; } = false;

        public Users()
        {

        }
    }
}
