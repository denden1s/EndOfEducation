using Computer_house.DataBase.Entities;
using Computer_house.DataBase.Entities.PC_Components;
using Computer_house.DataBase.Entities.PC_Options;
using Computer_house.DataBase.Entities.Warehouse;
using Computer_house.OtherClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationContext = Computer_house.DataBase.ApplicationContext;

namespace Computer_house
{
  public partial class AuthorizedForm : Form
  {
    private List<Warehouse_info> WarehouseInformationList = new List<Warehouse_info>();
    private List<Locations_in_warehouse> LocationInWarehouseList = new List<Locations_in_warehouse>();
    private List<Products_location> ProductLocationsList = new List<Products_location>();
    private List<Mediator> Mediators = new List<Mediator>();
    private List<Holding_document> HoldingDocuments = new List<Holding_document>();
    private Users user;

    //организация блокирования функции перетаскивания формы
    const int SC_CLOSE = 0xF010;
    const int MF_BYCOMMAND = 0;
    [DllImport("User32.dll")]
    static extern int SendMessage(IntPtr hWnd,
    int Msg, IntPtr wParam, IntPtr lParam);

    [DllImport("User32.dll")]
    static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

    [DllImport("User32.dll")]
    static extern bool RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      IntPtr hMenu = GetSystemMenu(Handle, false);
      RemoveMenu(hMenu, SC_CLOSE, MF_BYCOMMAND);
    }

    public AuthorizedForm()
    {
      InitializeComponent();
    }

    public AuthorizedForm(Users _user)
    {
      user = _user;
      InitializeComponent();  
    }

    private async void AuthorizedForm_Load(object sender, EventArgs e)
    {
      Width = Convert.ToInt32(DesktopScreen.Width / DesktopScreen.GetScalingFactor());
      Height = Convert.ToInt32(DesktopScreen.Height / DesktopScreen.GetScalingFactor());
      await Task.Run(() => LoadHoldingDocsFromDB());
      ViewDocsInDataGrid();
      Task.Run(() => UpdateInfo());
    }

    private void AuthorizedForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Application.Exit();
    }
    private void LoadInfoAboutMediatorFromDB()
    {
      try
      {
        Mediators.Clear();
        using(ApplicationContext db = new ApplicationContext())
          Mediators = db.Mediator.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadProductLocationFromDB()
    {
      try
      {
        ProductLocationsList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          ProductLocationsList = db.Products_location.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }
    private void LoadLocationInWarehouseFromDB()
    {
      try
      {
        LocationInWarehouseList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          LocationInWarehouseList = db.Locations_in_warehouse.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }      
    private void LoadAllInfoFromDB()
    {
      try
      {
        WarehouseInformationList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          if(db.Warehouse_info.Count() > 0)
            WarehouseInformationList = db.Warehouse_info.ToList();

        foreach(Warehouse_info w in WarehouseInformationList)
          w.SetName();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      } 
    }


    //Нужен для того, чтобы после добавления данных обновить список в таблице
    private void AuthorizedForm_Enter(object sender, EventArgs e)
    {
      AuthorizedForm_Load(sender, e);
    }

    private void настроитьIPToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SystemFunctions.SetNewDataBaseAdress();
    }

    private void перейтиВРазделРедактированияToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //ComponentsOptionsForm addComponentsOptionsForm = new ComponentsOptionsForm(user, Cpus, Gpus, 
      //  Motherboards, Cases, Rams, CoolingSystems, Psus, StorageDevices);
      //this.Hide();
      //addComponentsOptionsForm.Show();
    }

    private void выйтиИзУчётнойЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AuthentificationForm authentificationForm = new AuthentificationForm();
      authentificationForm.Show();
      this.Hide();
    }

    private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //Открыть PDF
      string fbPath = Application.StartupPath;
      string fname = "Справка.pdf";
      string filename = fbPath + @"\" + fname;
      Help.ShowHelp(this, filename, HelpNavigator.Find, "");
    }

    private void ViewDocsInDataGrid()
    {
      HoldingDocsDatagridView.Rows.Clear();
      foreach(var i in HoldingDocuments)
      {
        i.GetDataFromDB();
        string name;
        using(ApplicationContext db = new ApplicationContext())
          name = db.Users.Single(b => b.ID == i.User_ID).Name;

        HoldingDocsDatagridView.Rows.Add(i.ID, i.Product_name, i.Time, i.State, i.Items_count_in_move, name, i.Location_name);
      }
    }

    private void LoadHoldingDocsFromDB()
    {
      HoldingDocuments.Clear();
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          HoldingDocuments = db.Holding_document.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private async void UpdateInfo()
    {
      while(true)
      {
        using(ApplicationContext db = new ApplicationContext())
        {
          NeedToUpdate needToUpdate = db.NeedToUpdate.Single(i => i.ID == 1);
          if(needToUpdate.UpdateStatus)
          {
            await Task.Run(() => LoadHoldingDocsFromDB());
            await Task.Run(() => ViewDocsInDataGrid());
            needToUpdate.UpdateStatus = false;
            db.NeedToUpdate.Update(needToUpdate);
            db.SaveChanges();
          }
        }
      }
    }
  }
}