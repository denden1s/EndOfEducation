using Computer_house.DataBase;
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
    private List<Price_list> PriceList = new List<Price_list>();
    private List<ShopRequests> ShopRequests = new List<ShopRequests>();
    private Users user;
    private string enteredPage = "График затрат и доходов";

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
      await Task.Run(() => LoadPriceInfo());
      await Task.Run(() => LoadAllInfoFromDB());
      await Task.Run(() => LoadShopRequests());
      ViewPriceInfo();
    }

    private void LoadShopRequests()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          ShopRequests = db.ShopRequests.ToList();

        foreach(ShopRequests r in ShopRequests)
          r.GetDataFromDB();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadPriceInfo()
    {
      try
      {
        PriceList.Clear();
        using(ApplicationContext db = new ApplicationContext())
          PriceList = db.Price_list.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }  
    }

    private void ViewPriceInfo()
    {
      int count = 0;
      foreach(Price_list p in PriceList)
      {
        if(p.Purchasable_price == 0 && p.Markup_percent == 0)
        {
          UnsetPrice.Items.Add(WarehouseInformationList.Single(i => i.Product_ID == p.Product_ID).ProductName);
          count++;
        }
        else
          SetingPrice.Items.Add(WarehouseInformationList.Single(i => i.Product_ID == p.Product_ID).ProductName);
      }
      if(count > 0)
      {
        MessageBox.Show("Необходимо установить цены на " + count + " товаров.");
      }
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

    private void button1_Click(object sender, EventArgs e)
    {
      if(SelectedItem.Text.Length != 0 && MarkUpPercent.Value > 0 && BuyingPrice.Value > 0)
      {
        int markUp = Convert.ToInt32(MarkUpPercent.Value);
        int id = WarehouseInformationList.Single(i => i.ProductName == SelectedItem.Text).Product_ID;
        Price_list price = new Price_list(id, (decimal)BuyingPrice.Value, markUp);
        using(ApplicationContext db = new ApplicationContext())
        {
          db.Price_list.Update(price);
          NeedToUpdate update = db.NeedToUpdate.Single(i => i.ID == 1);
          if(!update.UpdateStatusForShop)
          {
            update.UpdateStatusForShop = true;
            db.NeedToUpdate.Update(update);
          }
          db.SaveChanges();
        }
        MessageBox.Show("Сохранение прошло успешно");
        LoadPriceInfo();
        ViewPriceInfo();
        BuyingPrice.Value = 0;
        MarkUpPercent.Value = 0;
        SelectedItem.Text = "";
      }
      else
        MessageBox.Show("Не возможно сохранить данные");
    }

    private void UnsetPrice_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(UnsetPrice.SelectedIndex != -1)
      {
        SetingPrice.SelectedIndex = -1;
        SelectedItem.Text = UnsetPrice.Items[UnsetPrice.SelectedIndex].ToString();
        MarkUpPercent.Value = 0;
        BuyingPrice.Value = 0;
      }
    }

    private void SetingPrice_SelectedIndexChanged(object sender, EventArgs e)
    {
      if(SetingPrice.SelectedIndex != -1)
      {
        UnsetPrice.SelectedIndex = -1;
        SelectedItem.Text = SetingPrice.Items[SetingPrice.SelectedIndex].ToString();
        int id = WarehouseInformationList.Single(i => i.ProductName == SelectedItem.Text).Product_ID;
        MarkUpPercent.Value = (decimal)PriceList.Single(i => i.Product_ID == id).Markup_percent;
        BuyingPrice.Value = (decimal)PriceList.Single(i => i.Product_ID == id).Purchasable_price;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DemandedTable.Rows.Clear();
      DemandedChart.Series.Clear();

      EfficiencyChart.Series.Clear();
      EfficiencyTable.Rows.Clear();
      //вывод сведений о дате


      ViewGraphic();

    }

    private void CreateEfficiencyDocsWithOnlyStartTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      MessageBox.Show("Окончательный период введён неверно");
      docs = (from b in HoldingDocuments
              where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text)
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text) && b.Status == true
                  select b).ToList();
    }

    private void CreateEfficiencyDocsWithOnlyEndTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      docs = (from b in HoldingDocuments
              where b.Time.Date < Convert.ToDateTime(EndPeriod.Text)
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date < Convert.ToDateTime(EndPeriod.Text) && b.Status == true
                  select b).ToList();
    }

    private void CreateEfficiencyDocsWithAllTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      docs = (from b in HoldingDocuments
              where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
              b.Time.Date < Convert.ToDateTime(EndPeriod.Text)
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
                  b.Time.Date < Convert.ToDateTime(EndPeriod.Text) && b.Status == true
                  select b).ToList();
    }


    private void CreateDemendedDocsWithOnlyStartTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      MessageBox.Show("Окончательный период введён неверно");
      docs = (from b in HoldingDocuments
              where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text) && b.State == "Расход"
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text)
                  select b).ToList();
    }

    private void CreateDemendedDocsWithOnlyEndTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      docs = (from b in HoldingDocuments
              where b.Time.Date < Convert.ToDateTime(EndPeriod.Text) && b.State == "Расход"
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date < Convert.ToDateTime(EndPeriod.Text)
                  select b).ToList();
    }

    private void CreateDemendedDocsWithAllTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      docs = (from b in HoldingDocuments
              where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
              b.Time.Date < Convert.ToDateTime(EndPeriod.Text) && b.State == "Расход"
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date > Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
                  b.Time.Date < Convert.ToDateTime(EndPeriod.Text)
                  select b).ToList();
    }

    private void ViewGraphic()
    {
      string[] firstPeriod = StartOfPeriodTextBox.Text.Split(new char[] { '-' });
      string year = firstPeriod[0].Trim();
      string[] secondPeriod = EndPeriod.Text.Split(new char[] { '-' });
      string lastYear = secondPeriod[0].Trim();

      switch(enteredPage)
      {
        case "График затрат и доходов":
          //ситуация если сразу вводится начало периода
          if(year.Length == 4 && firstPeriod[1].Length == 2)
          {
            if(Convert.ToInt32(firstPeriod[1]) <= 12 && Convert.ToInt32(firstPeriod[1]) > 0)
            {
              if((lastYear.Length == 4 && secondPeriod[1].Length == 2) &&
                 Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                 Convert.ToInt32(secondPeriod[1]) <= 12)
              {
                //счет от начала до конца
              }
              else
              {
                //счет по одному периоду
              }
            }
            else
              MessageBox.Show("Месяц указан неверно!");
          }
          else
            MessageBox.Show("Данные введены некорректно");
          break;
        case "График востребованности":
          //нужен для составления графика востребованности
          List<Holding_document> docs = new List<Holding_document>();
          List<ShopRequests> requests = new List<ShopRequests>();
          List<Product> demandedInfo = new List<Product>();
          //ситуация если сразу вводится начало периода
          if(year.Length == 4 && firstPeriod[1].Length == 2)
          {
            if(Convert.ToInt32(firstPeriod[1]) <= 12 && Convert.ToInt32(firstPeriod[1]) > 0)
            {
              if((lastYear.Length == 4 && secondPeriod[1].Length == 2) &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 && 
                Convert.ToInt32(secondPeriod[1]) <= 12)
              {
                //ситуация когда начальные и конечные данные введены верно
                CreateDemendedDocsWithAllTime(ref docs, ref requests);
              }
              else//ситуация когда введены верно только начальные данные
                CreateDemendedDocsWithOnlyStartTime(ref docs, ref requests);

            }
            else
            {
              if(Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0)
              {
                CreateDemendedDocsWithOnlyEndTime(ref docs, ref requests);
              }
            }
          }
          else if(lastYear.Length == 4 && secondPeriod[1].Length == 2)
          {
            if(Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0)
            {
              CreateDemendedDocsWithOnlyEndTime(ref docs, ref requests);
            }
          }
          if(docs.Count > 0 || requests.Count > 0)
          {
            foreach(Holding_document d in docs)
            {
              if(demandedInfo.Where(i => i.Name == d.Product_name).Count() == 0)
              {
                demandedInfo.Add(new Product { Name = d.Product_name, ID = Convert.ToString(Math.Abs(d.Items_count_in_move)) });
              }
              else
              {
                int index = demandedInfo.IndexOf(demandedInfo.Single(i => i.Name == d.Product_name));
                demandedInfo[index].ID = Convert.ToString(Convert.ToInt32(demandedInfo[index].ID) + Math.Abs(d.Items_count_in_move));
              }
            }
            foreach(ShopRequests d in requests)
            {
              if(demandedInfo.Where(i => i.Name == d.ProductName).Count() == 0)
              {
                demandedInfo.Add(new Product { Name = d.ProductName, ID = Convert.ToString(d.Count) });
              }
              else
              {
                int index = demandedInfo.IndexOf(demandedInfo.Single(i => i.Name == d.ProductName));
                demandedInfo[index].ID = Convert.ToString(Convert.ToInt32(demandedInfo[index].ID) + d.Count);
              }
            }
            DemandedChart.ChartAreas[0].AxisX.Title = "Товары";
            DemandedChart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Malgun Gothic;", 12F);
            DemandedChart.ChartAreas[0].AxisY.Title = "Количество";
            DemandedChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Malgun Gothic;", 12F);
            foreach(Product p in demandedInfo)
            {
              var chart = DemandedChart.Series.Add(p.Name);
              chart.Points.Add(Convert.ToDouble(p.ID));
              DemandedTable.Rows.Add(p.Name, Convert.ToInt32(p.ID));
            }
            StartOfPeriodTextBox.Clear();
            EndPeriod.Clear();
          }
          else
            MessageBox.Show("Данные введены некорректно");
          break;
        case "График эффективности труда":

          List<Holding_document> docsOfEfficiency = new List<Holding_document>();
          List<ShopRequests> requestsOfEfficiency = new List<ShopRequests>();
          List<Product> efficiencyInfo = new List<Product>();

          //ситуация если сразу вводится начало периода
          if(year.Length == 4 && firstPeriod[1].Length == 2)
          {
            if(Convert.ToInt32(firstPeriod[1]) <= 12 && Convert.ToInt32(firstPeriod[1]) > 0)
            {
              if((lastYear.Length == 4 && secondPeriod[1].Length == 2) &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                Convert.ToInt32(secondPeriod[1]) <= 12)
              {
                //счет от начала до конца
                CreateEfficiencyDocsWithAllTime(ref docsOfEfficiency, ref requestsOfEfficiency);
              }
              else
              {
                //счет по одному периоду
                CreateEfficiencyDocsWithOnlyStartTime(ref docsOfEfficiency, ref requestsOfEfficiency);
              }
            }
            else
            {
              if(Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0)
              {
                CreateEfficiencyDocsWithOnlyEndTime(ref docsOfEfficiency, ref requestsOfEfficiency);
              }
            }
          }
          else if(lastYear.Length == 4 && secondPeriod[1].Length == 2)
          {
            if(Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0)
            {
              CreateEfficiencyDocsWithOnlyEndTime(ref docsOfEfficiency, ref requestsOfEfficiency);
            }
          }

          if(docsOfEfficiency.Count > 0 || requestsOfEfficiency.Count > 0)
          {
            List<Users> users = new List<Users>();
            using(ApplicationContext db = new ApplicationContext())
              users = db.Users.ToList();
            
            //выборка данных из документов движения
            foreach(Holding_document d in docsOfEfficiency)
            {
              string name = users.Single(i => i.ID == d.User_ID).Name;
              if(efficiencyInfo.Where(i => i.Name == name).Count() == 0)
              {
                efficiencyInfo.Add(new Product { Name = name, ID = Convert.ToString(Math.Abs(d.Items_count_in_move)) });
              }
              else
              {
                int index = efficiencyInfo.IndexOf(efficiencyInfo.Single(i => i.Name == name));
                efficiencyInfo[index].ID = Convert.ToString(Convert.ToInt32(efficiencyInfo[index].ID) + Math.Abs(d.Items_count_in_move));
              }
            }

            //выборка данных из запросов на склад
            foreach(ShopRequests d in requestsOfEfficiency)
            {
              string name = users.Single(i => i.ID == d.User_ID).Name;
              if(efficiencyInfo.Where(i => i.Name == name).Count() == 0)
              {
                efficiencyInfo.Add(new Product { Name = name, ID = Convert.ToString(d.Count) });
              }
              else
              {
                int index = efficiencyInfo.IndexOf(efficiencyInfo.Single(i => i.Name == name));
                efficiencyInfo[index].ID = Convert.ToString(Convert.ToInt32(efficiencyInfo[index].ID) + d.Count);
              }
            }

            //построение графика
            EfficiencyChart.ChartAreas[0].AxisX.Title = "Работники";
            EfficiencyChart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Malgun Gothic;", 12F);
            EfficiencyChart.ChartAreas[0].AxisY.Title = "Количество реализованного товара";
            EfficiencyChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Malgun Gothic;", 12F);
            foreach(Product p in efficiencyInfo)
            {
              var chart = EfficiencyChart.Series.Add(p.Name);
              chart.Points.Add(Convert.ToDouble(p.ID));
              EfficiencyTable.Rows.Add(p.Name, Convert.ToInt32(p.ID));
            }

            StartOfPeriodTextBox.Clear();
            EndPeriod.Clear();
          }
          else
            MessageBox.Show("Данные введены некорректно");
          //нужно предусмотреть что могут быть введены корректно только правые данные
          break;
        default:
          break;
      }
    }

    private void tabPage3_Enter(object sender, EventArgs e)
    {
      enteredPage = tabPage3.Text;
    }

    private void tabPage4_Enter(object sender, EventArgs e)
    {
      enteredPage = tabPage4.Text;
    }

    private void tabPage5_Enter(object sender, EventArgs e)
    {
      enteredPage = tabPage5.Text;
    }
  }
}