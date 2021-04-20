
namespace Computer_house.DataBase.Interfaces
{
    interface IEnergy_consumption
    {
        int Product_ID { get; set; }
        int Consumption { get; set; }

        void SetEnergy_consumption(ApplicationContext db);
    }
}
