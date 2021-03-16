
namespace Computer_house.DataBase.Interfaces
{
    interface IMemory_capacity
    {
        int Product_ID { get; set; }
        int Capacity { get; set; }

        void SetMemoryCapacity();
    }
}
