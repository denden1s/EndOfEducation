namespace Computer_house.DataBase.Entities.PC_Options
{
public class Sizes_of_components
  {
    public int Product_ID { get; set; }
    public int Length { get; set; } = 0;
    public int Height { get; set; } = 0;
    public int Width { get; set; } = 0;
    public int Diameter { get; set; } = 0;
    public float Thickness { get; set; } = 0;
    public int Depth { get; set; } = 0;

    public Sizes_of_components() { }

    public Sizes_of_components(int _productID)
    {
      Product_ID = _productID;
    }
  }
}
