namespace Computer_house.DataBase.Entities.Warehouse
{
  public class Users
{
    public int ID { get; set; }

    public string Login { get; private set; }

    public string Password { get; private set; }

    public bool Authorization_status { get; set; } = false;

    public bool Authorization_status_in_shop { get; set; }


    public string Name { get; set; }

    public Users() { }
    public Users(int _id,string _login, string _password)
    {
      ID = _id;
      Login = _login;
      Password = _password;
    }
  }
}