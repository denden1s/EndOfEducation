namespace Computer_house.DataBase.Entities.PC_Options
{
  public class Base_and_max_options
  {
    public int Product_ID { get; set; }
    public int Base_state { get; set; } = 0;
    public int Max_state { get; set; } = 0;

    public Base_and_max_options() { }
    public Base_and_max_options(int _id, int _bState, int _mState)
    {
      Product_ID = _id;
      Base_state = _bState;
      Max_state = _mState;
    }
  }
}