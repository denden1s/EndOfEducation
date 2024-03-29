﻿using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house.OtherClasses
{
  class SQLRequests
  {
    public static string[] FindProductID(int _product_ID)
    {
      using (ApplicationContext db = new ApplicationContext())
      {
        var findProductName = (from b in db.Mediator
                               where b.ID == _product_ID
                               select b).FirstOrDefault();
        string productID = "";
        switch (findProductName.Components_type)
        {
          case "CPU":
            productID = findProductName.CPU_ID;
            break;
          case "Case":
            productID = findProductName.Case_ID;
            break;
          case "Cooling system":
            productID = findProductName.Cooling_system_ID;
            break;
          case "GPU":
            productID = findProductName.GPU_ID;
            break;
          case "SD":
            productID = findProductName.SD_ID;
            break;
          case "Motherboard":
            productID = findProductName.Motherboard_ID;
            break;
          case "PSU":
            productID = findProductName.PSU_ID;
            break;
          case "RAM":
            productID = findProductName.RAM_ID;
            break;
          default:
              break;
        }
        string[] result = { productID, findProductName.Components_type };
        return result;
      }
    }

    

    //Добавление данных 
    public static void AddCPUSeries(CPU_series _series, bool _option)
    {
      try
      {
        if (Warning(_option, _series.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.CPU_series.Add(_series);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }  
    }
    public static void AddCPUCodeName(CPU_codename _codeName, bool _option)
    {
      try
      {
        if(Warning(_option, _codeName.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.CPU_codename.Add(_codeName);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddSocket(Sockets _socket, bool _options)
    {
      try
      {
        if(Warning(_options, _socket.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Sockets.Add(_socket);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddChipset(Chipset _chipset, bool _option)
    {
      try
      {
        if(Warning(_option, _chipset.Name))
        {
          using(ApplicationContext db = new ApplicationContext())
          {
            db.Chipset.Add(_chipset);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddChanel(RAM_chanels _chanel, bool _option)
    {
      try
      {
        if (Warning(_option, _chanel.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.RAM_chanels.Add(_chanel);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddFrequency(RAM_frequency _frequency, bool _option)
    {
      try
      { 
        if (Warning(_option, Convert.ToString(_frequency.Frequency)))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.RAM_frequency.Add(_frequency);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void AddFormFactor(Form_factors _formFactor, bool _option)
    {
      try
      {
        if (Warning(_option, _formFactor.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Form_factors.Add(_formFactor);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void AddMemoryType(Memory_types _type, bool _option)
    {
      try
      { 
        if (Warning(_option, _type.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Memory_types.Add(_type);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void AddConnectionInterface(Connection_interfaces _interface, bool _option)
    {
      try
      { 
        if (Warning(_option, _interface.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Connection_interfaces.Add(_interface);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void AddPowerConnector(Power_connectors _connector, bool _option)
    {
      try
      { 
        if (Warning(_option, _connector.Connectors))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Power_connectors.Add(_connector);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }


    //Изменение данных
    public static void ChangeSeries(CPU_series _series, bool _option)
    {
      try
      {
        if (Warning(_option, _series.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.CPU_series.Update(_series);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeCodeName(CPU_codename _codeName, bool _option)
    {
      try
      { 
        if (Warning(_option, _codeName.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.CPU_codename.Update(_codeName);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeSocketInfo(Sockets _socket, bool _option)
    {
      try
      { 
        if(Warning(_option, _socket.Name))
        {
          using(ApplicationContext db = new ApplicationContext())
          {
            db.Sockets.Update(_socket);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
          MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeChipsetInfo(Chipset _chipset, bool _option)
    {
      try
      {
        if(Warning(_option, _chipset.Name))
        {
          using(ApplicationContext db = new ApplicationContext())
          {
            db.Chipset.Update(_chipset);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeChanelInfo(RAM_chanels _chanel, bool _option)
    {
      try
      { 
        if (Warning(_option, _chanel.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.RAM_chanels.Update(_chanel);
            MessageBox.Show("Изменение прошло успешно!");
            db.SaveChanges();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeFrequencyInfo(RAM_frequency _frequency, bool _option)
    {
      try
      { 
        if (Warning(_option, Convert.ToString(_frequency.Frequency)))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.RAM_frequency.Update(_frequency);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void ChangeFormFactors(Form_factors _formFactor, bool _option)
    {
      try
      { 
        if (Warning(_option, _formFactor.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Form_factors.Update(_formFactor);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void ChangeMemoryType(Memory_types _type, bool _option)
    {
      try
      { 
        if (Warning(_option, _type.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Memory_types.Update(_type);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void ChangeConnectionInterface(Connection_interfaces _interface, bool _option)
    {
      try
      { 
        if (Warning(_option, _interface.Name))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Connection_interfaces.Update(_interface);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void ChangePowerConnector(Power_connectors _connector, bool _option)
    {
      try
      {
        if (Warning(_option, _connector.Connectors))
        {
          using (ApplicationContext db = new ApplicationContext())
          {
            db.Power_connectors.Update(_connector);
            db.SaveChanges();
            MessageBox.Show("Изменение прошло успешно!");
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static bool Warning(bool _act, string _name)
    {
      string[] words;
      if (_act)
        words = new string[]{ "добавить", "Добавление"};
      else
        words = new string[2] { "изменить", "Изменение" };

      DialogResult questionResult = MessageBox.Show($"Вы действительно хотите {words[0]} " +
                          $"элемент {_name}",
                          $"{words[1]} элемента!",
                          MessageBoxButtons.YesNo,
                          MessageBoxIcon.Information,
                          MessageBoxDefaultButton.Button2,
                          MessageBoxOptions.DefaultDesktopOnly);
      if (questionResult == DialogResult.Yes)
      {      
        return true;
      }
      else
      {
        MessageBox.Show(words[1] + " не прошло!");
        return false;   
      }
    }

    //Добавление в бд через объект класса !!!

    public static void EditCPUInMediator(CPU _cpu, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();

          mediator.Components_type = "CPU";
          mediator.CPU_ID = _cpu.ID;
          if(_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "CPU" && b.CPU_ID == _cpu.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          {
            db.Price_list.Add(new Price_list(tempMediatorID,0,0));
            db.Warehouse_info.Add(info);
          }
                        
          Energy_consumption energy = new Energy_consumption(tempMediatorID, _cpu.Consumption);
          if (_method == "Add")
            db.Energy_consumption.Add(energy);
          else if (_method == "Edit")
            db.Energy_consumption.Update(energy);

          Base_and_max_options baseAndMaxOptions = new Base_and_max_options(tempMediatorID, _cpu.Base_state, _cpu.Max_state); ;
          if (_method == "Add")
            db.Base_and_max_options.Add(baseAndMaxOptions);
          else if (_method == "Edit")
            db.Base_and_max_options.Update(baseAndMaxOptions);
          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void AddCPU(CPU _cpu)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.CPU.Add(_cpu);
            db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeCPU(CPU _cpu)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.CPU.Update(_cpu);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddGPU(GPU _gpu)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.GPU.Add(_gpu);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeGPU(GPU _gpu)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.GPU.Update(_gpu);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void EditGPUInMediator(GPU _gpu, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();
          mediator.Components_type = "GPU";
          mediator.GPU_ID = _gpu.ID;
          if (_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "GPU" && b.GPU_ID == _gpu.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          {
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }         
          //настроить возможные варианты если происходит добавление или изменение
          Energy_consumption energy_Consumption = new Energy_consumption(info.Product_ID, _gpu.Consumption);
          if(_method == "Add")
            db.Energy_consumption.Add(energy_Consumption);
          else if(_method == "Edit")
            db.Energy_consumption.Update(energy_Consumption);

          Memory_capacity capacity = new Memory_capacity(info.Product_ID, _gpu.Capacity);
          if (_method == "Add")
            db.Memory_capacity.Add(capacity);
          else if (_method == "Edit")
            db.Memory_capacity.Update(capacity);

          Sizes_of_components sizes = new Sizes_of_components();
          sizes.Product_ID = info.Product_ID;
          sizes.Length = _gpu.Length;
          sizes.Height = _gpu.Height;
          if (_method == "Add")
            db.Sizes_of_components.Add(sizes);
          else if (_method == "Edit")
            db.Sizes_of_components.Update(sizes);

          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddMotherboard(Motherboard _motherboard)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.Motherboard.Add(_motherboard);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeMotherboard(Motherboard _motherboard)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.Motherboard.Update(_motherboard);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void EditMotherboardMediator(Motherboard _motherboard, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();
          mediator.Components_type = "Motherboard";
          mediator.Motherboard_ID = _motherboard.ID;
          if (_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "Motherboard" && b.Motherboard_ID == _motherboard.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          {
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }      
          //настроить возможные варианты если происходит добавление или изменение
          Sizes_of_components sizes = new Sizes_of_components();
          sizes.Product_ID = info.Product_ID;
          sizes.Width = _motherboard.Width;
          sizes.Length = _motherboard.Length;
          if (_method == "Add")
            db.Sizes_of_components.Add(sizes);
          else if (_method == "Edit")
            db.Sizes_of_components.Update(sizes);

          Base_and_max_options maxOptions = new Base_and_max_options();
          maxOptions.Product_ID = info.Product_ID;
          maxOptions.Base_state = 0;
          maxOptions.Max_state = _motherboard.RAM_frequency;
          if (_method == "Add")
            db.Base_and_max_options.Add(maxOptions);
          else if (_method == "Edit")
            db.Base_and_max_options.Update(maxOptions);
          Memory_capacity capacity = new Memory_capacity(info.Product_ID, _motherboard.Capacity);
          if (_method == "Add")
            db.Memory_capacity.Add(capacity);
          else if (_method == "Edit")
            db.Memory_capacity.Update(capacity);

          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddCase(Case _case)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.Case.Add(_case);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeCase(Case _case)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.Case.Update(_case);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void EditCaseMediator(Case _case, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();
          mediator.Components_type = "Case";
          mediator.Case_ID = _case.ID;
          if (_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "Case" && b.Case_ID == _case.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          { 
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }        
          //настроить возможные варианты если происходит добавление или изменение
          Sizes_of_components sizesOfCase = new Sizes_of_components();
          sizesOfCase.Product_ID = info.Product_ID;
          sizesOfCase.Height = _case.Height;
          sizesOfCase.Width = _case.Width;
          sizesOfCase.Depth = _case.Depth;
          if (_method == "Add")
            db.Sizes_of_components.Add(sizesOfCase);
          else if (_method == "Edit")
            db.Sizes_of_components.Update(sizesOfCase);
          Base_and_max_options options = new Base_and_max_options(info.Product_ID, 
                                        _case.Coolers_count, _case.Coolers_slots);
          if (_method == "Add")
            db.Base_and_max_options.Add(options);
          else if (_method == "Edit")
            db.Base_and_max_options.Update(options);
          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddRAM(RAM _ram)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.RAM.Add(_ram);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangeRAM(RAM _ram)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.RAM.Update(_ram);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void EditRAMMediator(RAM _ram, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();
          mediator.Components_type = "RAM";
          mediator.RAM_ID = _ram.ID;
          if (_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "RAM" && b.RAM_ID == _ram.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          {
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }        
          //настроить возможные варианты если происходит добавление или изменение
          Memory_capacity capacity = new Memory_capacity();
          capacity.Product_ID = info.Product_ID;
          capacity.Capacity = _ram.Capacity;
          if (_method == "Add")
            db.Memory_capacity.Add(capacity);
          else if (_method == "Edit")
            db.Memory_capacity.Update(capacity);
          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        MessageBox.Show(ex.InnerException.Message);
      }
    }

    public static void AddCoolingSystem(Cooling_system _coolingSys)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.Cooling_system.Add(_coolingSys);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void ChangeCoolingSystem(Cooling_system _coolingSys)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.Cooling_system.Update(_coolingSys);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void EditCoolingSystemMediator(Cooling_system _coolingSys, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();

          mediator.Components_type = "Cooling system";
          mediator.Cooling_system_ID = _coolingSys.ID;
          if (_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "Cooling system" && b.Cooling_system_ID == _coolingSys.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          {
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }            
          //настроить возможные варианты если происходит добавление или изменение
          Base_and_max_options options = new Base_and_max_options(info.Product_ID, 0, _coolingSys.Max_state);
          if (_method == "Add")
            db.Base_and_max_options.Add(options);
          else if (_method == "Edit")
            db.Base_and_max_options.Update(options);
          Energy_consumption consumption = new Energy_consumption(info.Product_ID, _coolingSys.Consumption);
          if (_method == "Add")
            db.Energy_consumption.Add(consumption);
          else if (_method == "Edit")
            db.Energy_consumption.Update(consumption);

          Sizes_of_components sizes = new Sizes_of_components();
          sizes.Product_ID = info.Product_ID;
          sizes.Diameter = _coolingSys.Diameter;
          if (_method == "Add")
            db.Sizes_of_components.Add(sizes);
          else if (_method == "Edit")
            db.Sizes_of_components.Update(sizes);
          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddPSU(PSU _psu)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.PSU.Add(_psu);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void ChangePSU(PSU _psu)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          db.PSU.Update(_psu);
          db.SaveChanges();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void EditPSUMediator(PSU _psu, string _method)
    {
      try
      {
        using (ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();

          mediator.Components_type = "PSU";
          mediator.PSU_ID = _psu.ID;
          if (_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "PSU" && b.PSU_ID == _psu.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if (_method == "Add")
          {
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }
                        
          //настроить возможные варианты если происходит добавление или изменение
          Energy_consumption consumption = new Energy_consumption(info.Product_ID, _psu.Consumption);
          if (_method == "Add")
            db.Energy_consumption.Add(consumption);
          else if (_method == "Edit")
            db.Energy_consumption.Update(consumption);

          Sizes_of_components sizes = new Sizes_of_components();
          sizes.Product_ID = info.Product_ID;
          sizes.Length = _psu.Length;
          if (_method == "Add")
            db.Sizes_of_components.Add(sizes);
          else if (_method == "Edit")
            db.Sizes_of_components.Update(sizes);
          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddSD(Storage_devices _storageDevice)
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          db.Storage_devices.Add(_storageDevice);
          db.SaveChanges();
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void ChangeSD(Storage_devices _storageDevice)
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          db.Storage_devices.Update(_storageDevice);
          db.SaveChanges();
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void EditSDMediator(Storage_devices _sd, string _method)
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          Mediator mediator = new Mediator();
          mediator.Components_type = "SD";
          mediator.SD_ID = _sd.ID;
          if(_method == "Add")
          {
            db.Mediator.Add(mediator);
            db.SaveChanges();
          }
          int tempMediatorID = (from b in db.Mediator
                                where b.Components_type == "SD" && b.SD_ID == _sd.ID
                                select b.ID).SingleOrDefault();
          Warehouse_info info = new Warehouse_info(tempMediatorID, 0);
          if(_method == "Add")
          {
            db.Warehouse_info.Add(info);
            db.Price_list.Add(new Price_list(tempMediatorID, 0, 0));
          }
          //настроить возможные варианты если происходит добавление или изменение
          Memory_capacity capacity = new Memory_capacity(info.Product_ID, _sd.Capacity);
          if(_method == "Add")
            db.Memory_capacity.Add(capacity);
          else if(_method == "Edit")
            db.Memory_capacity.Update(capacity);

          Energy_consumption consumption = new Energy_consumption(info.Product_ID, _sd.Consumption);
          if(_method == "Add")
            db.Energy_consumption.Add(consumption);
          else if(_method == "Edit")
            db.Energy_consumption.Update(consumption);

          Sizes_of_components thickness = new Sizes_of_components(info.Product_ID);
          thickness.Thickness = _sd.Thickness;
          if(_method == "Add")
            db.Sizes_of_components.Add(thickness);
          else if(_method == "Edit")
            db.Sizes_of_components.Update(thickness);

          db.SaveChanges();
        }
        //После добавления в медиатор вытянуть этот же объект 
        //и добавить по int-товому id в warehouseinfo с количеством 0
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void RemoveShopRequestFromRequestList(ShopRequests _request)
    {
      try
      {
        _request.Status = true;
        using(ApplicationContext db = new ApplicationContext())
        {
          db.ShopRequests.Update(_request);
          db.SaveChanges();
        }
          
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    public static void AddLocation(Locations_in_warehouse _locations)
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          db.Locations_in_warehouse.Add(_locations);
          db.SaveChanges();
        }
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    public static void CreateHoldingDocument(Warehouse_info _infoAboutProduct, int _itemsCount, Users _user, string _deviceType)
    {
      if (_itemsCount < 0)
      {
        string message = $"Со склада снято {_infoAboutProduct.ProductName} в объёме:\n";
        //расход 
        using (ApplicationContext db = new ApplicationContext())
        {
          List<Products_location> locations = (from b in db.Products_location
                                              where b.Product_ID == _infoAboutProduct.Product_ID
                                              select b).ToList();
          if (locations.Count != 0)
          {
            if(_infoAboutProduct.Current_items_count >= Math.Abs(_itemsCount))
            {
              foreach(Products_location p in locations)
              {
                Locations_in_warehouse locationInWarehouse = new Locations_in_warehouse();
                locationInWarehouse = db.Locations_in_warehouse.Single(i => i.ID == p.Location_ID);
                if(p.Items_count < Math.Abs(_itemsCount))
                { 
                  message += Convert.ToString(p.Items_count) + " шт. с места " + locationInWarehouse.Location_label +";\n";
                  _infoAboutProduct.Current_items_count += p.Items_count * -1;
                  
                  locationInWarehouse.Current_item_count += p.Items_count * -1;
                  Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Расход",
                    p.Items_count *-1, _user.ID, p.Location_ID);
                  db.Holding_document.Add(holding_Document);
                  _itemsCount += p.Items_count;
                  p.Items_count = 0;
                }
                else
                {
                  message += Convert.ToString(Math.Abs(_itemsCount)) + " шт. с места " + locationInWarehouse.Location_label + ";\n";
                  p.Items_count += _itemsCount;
                  _infoAboutProduct.Current_items_count += _itemsCount;
                  locationInWarehouse.Current_item_count += _itemsCount;
                  Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Расход",
                    _itemsCount, _user.ID, p.Location_ID);
                  db.Holding_document.Add(holding_Document);
                }
                _infoAboutProduct.Items_in_shop += Math.Abs(_itemsCount);
                db.Warehouse_info.Update(_infoAboutProduct);
                db.Locations_in_warehouse.Update(locationInWarehouse);
                db.Products_location.Update(p);
                db.SaveChanges();
              }
              MessageBox.Show(message);
              return;
            }
            else
            {
              MessageBox.Show("На складе недостаточно материалов!");
              return;
            } 
          }
          else
          {
            MessageBox.Show("Товар отсутствует на складе");
            return;
          }
        }
      }
      else if (_itemsCount > 0)
      {
        int location_ID = -1;
        //приход
        using (ApplicationContext db = new ApplicationContext())
        {
          List<Locations_in_warehouse> locations = (from b in db.Locations_in_warehouse
                                                    where b.Location_label.Contains(_deviceType) &&
                                                    b.Max_item_count >= b.Current_item_count + _itemsCount
                                                    select b).ToList();

          if (locations.Count != 0)
          {
            List<Products_location> LocationsWithThisItem = new List<Products_location>();
            LocationsWithThisItem = db.Products_location.Where(i => i.Product_ID == _infoAboutProduct.Product_ID).ToList();
            foreach (var i in locations)
            {
              foreach (var a in LocationsWithThisItem)
              {
                if ((i.ID == a.Location_ID) && (a.Product_ID == _infoAboutProduct.Product_ID))
                  location_ID = a.Location_ID;
              }
            }
            //Если такого нет то добавить в первый блок
            if (location_ID == -1)
            {
              location_ID = locations[0].ID;
              Products_location productsLocation1 = new Products_location(_infoAboutProduct.Product_ID, location_ID, _itemsCount);
              db.Products_location.Add(productsLocation1);
            }
            else
            {
              Products_location productsLocation = (from b in db.Products_location
                                                    where b.Location_ID == location_ID && 
                                                    b.Product_ID == _infoAboutProduct.Product_ID
                                                    select b).Single();
              productsLocation.Items_count += _itemsCount; 
              db.Products_location.Update(productsLocation);
            }
            Holding_document holding_Document = new Holding_document(_infoAboutProduct.Product_ID, "Приход",
                _itemsCount, _user.ID, location_ID);

            Locations_in_warehouse locations_InWarehouse = db.Locations_in_warehouse.Single(i => i.ID == location_ID);
            locations_InWarehouse.Current_item_count += _itemsCount;
            db.Locations_in_warehouse.Update(locations_InWarehouse);

            db.Holding_document.Add(holding_Document);
            _infoAboutProduct.Current_items_count += _itemsCount;
            db.Warehouse_info.Update(_infoAboutProduct);
            db.SaveChanges();
            MessageBox.Show("Добавление прошло успешно в количестве:" + _itemsCount + "шт. в место: " + 
              locations_InWarehouse.Location_label);
            return;
          }
          else
          {
            string message = "На склад можно добавить следующее кол-во элементов по отдельности:\n";
            List<Locations_in_warehouse> locationsCanSet = (from b in db.Locations_in_warehouse
                                                            where b.Location_label.Contains(_deviceType)
                                                            select b).ToList();
            int maxItems = 0;
            foreach(Locations_in_warehouse item in locationsCanSet)
              maxItems += item.Max_item_count - item.Current_item_count;

            foreach(Locations_in_warehouse item in locationsCanSet)
              message += $"Месторасположение: {item.Location_label}, кол-во товара: " +
                $"{item.Max_item_count - item.Current_item_count};\n";

            MessageBox.Show(message);
          }
        }
      }
    }

    public static void CreateAuthentificationLog(Authentification_logs log)
    {
      using(ApplicationContext db = new ApplicationContext())
      {
        db.Authentification_logs.Add(log);
        db.SaveChanges();
      }
    }
  }
}