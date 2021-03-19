
namespace Computer_house.DataBase.Interfaces
{
    interface ISizes_of_components
    {
        int Product_ID { get; set; }
        int Length { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        int Diameter { get; set; }
        float Thickness { get; set; }
        int Depth { get; set; }

        void SetSezesOptions(ApplicationContext db);
    }
}
