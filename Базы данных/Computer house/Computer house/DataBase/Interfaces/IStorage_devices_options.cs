
namespace Computer_house.DataBase.Interfaces
{
    interface IStorage_devices_options
    {
        int Storage_device_ID { get; set; }
        int Interface_ID { get; set; }
        int Form_factor_ID { get; set; }
        int Buffer { get; set; }
        bool Hardware_encryption { get; set; }
        int Sequential_read_speed { get; set; }
        int Sequeintial_write_speed { get; set; }
        int Random_read_speed { get; set; }
        int Random_write_speed { get; set; }

        void SetStorageOptions();
    }
}
