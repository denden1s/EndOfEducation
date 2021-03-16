
namespace Computer_house.DataBase.Interfaces
{
    interface IBase_and_max_options
    {
        int Product_ID { get; set; }
        int Base_state { get; set; }
        int Max_state { get; set; }

        void SetBaseAndMaxOptions();


    }
}
