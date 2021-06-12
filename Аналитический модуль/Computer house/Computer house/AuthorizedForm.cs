using Computer_house.DataBase;
using Computer_house.DataBase.Entities;
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
    private List<Holding_document> HoldingDocuments = new List<Holding_document>();
    private List<Price_list> PriceList = new List<Price_list>();
    private List<ShopRequests> ShopRequests = new List<ShopRequests>();
    private List<Purchases> Purchases = new List<Purchases>();
    private List<Sellings> Sellings = new List<Sellings>();
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
      await Task.Run(() => LoadPurchases());
      await Task.Run(() => LoadSellings());
      ViewPriceInfo();
    }


    private void LoadSellings()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          Sellings = db.Sellings.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
    }

    private void LoadPurchases()
    {
      try
      {
        using(ApplicationContext db = new ApplicationContext())
          Purchases = db.Purchases.ToList();
      }
      catch(Exception ex)
      {
        MessageBox.Show(ex.Message);
      }
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

      PurchasingChart.Series.Clear();
      PurchasingTable.Rows.Clear();

      DemandedChart.Titles.Clear();
      EfficiencyChart.Titles.Clear();
      PurchasingChart.Titles.Clear();
      //вывод сведений о дате

      ViewGraphic();
    }

    private void CreatePurchaseDocsWithOnlyStartTime(ref List<Purchases> purchases, ref List<Sellings> sellings)
    {
      MessageBox.Show("Окончательный период введён неверно");
      purchases = (from b in Purchases
                   where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text)
                   select b).ToList();
      sellings = (from b in Sellings
                  where b.Selling_date.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text)
                  select b).ToList();
    }

    private void CreatePurchaseDocsWithAllTime(ref List<Purchases> purchases, ref List<Sellings> sellings)
    {
      purchases = (from b in Purchases
                   where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
                   b.Time.Date <= Convert.ToDateTime(StartOfPeriodTextBox.Text)
                   select b).ToList();
      sellings = (from b in Sellings
                  where b.Selling_date.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
                  b.Selling_date.Date <= Convert.ToDateTime(EndPeriod.Text)
                  select b).ToList();
    }


    private void CreatePurchaseDocsWithOnlyEndTime(ref List<Purchases> purchases, ref List<Sellings> sellings)
    {
      MessageBox.Show("Начальный период введён неверно");
      purchases = (from b in Purchases
                   where b.Time.Date <= Convert.ToDateTime(EndPeriod.Text)
                   select b).ToList();
      sellings = (from b in Sellings
                  where b.Selling_date.Date <= Convert.ToDateTime(EndPeriod.Text)
                  select b).ToList();
    }


    private void CreateEfficiencyDocsWithOnlyStartTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      MessageBox.Show("Окончательный период введён неверно");
      docs = (from b in HoldingDocuments
              where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text)
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) && b.Status == true
                  select b).ToList();
    }

    private void CreateEfficiencyDocsWithOnlyEndTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      MessageBox.Show("Начальный период введён неверно");
      docs = (from b in HoldingDocuments
              where b.Time.Date <= Convert.ToDateTime(EndPeriod.Text)
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date <= Convert.ToDateTime(EndPeriod.Text) && b.Status == true
                  select b).ToList();
    }

    private void CreateEfficiencyDocsWithAllTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      docs = (from b in HoldingDocuments
              where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
              b.Time.Date <= Convert.ToDateTime(EndPeriod.Text)
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
                  b.Time.Date < Convert.ToDateTime(EndPeriod.Text) && b.Status == true
                  select b).ToList();
    }


    private void CreateDemendedDocsWithOnlyStartTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      MessageBox.Show("Окончательный период введён неверно");
      docs = (from b in HoldingDocuments
              where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) && b.State == "Расход"
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text)
                  select b).ToList();
    }

    private void CreateDemendedDocsWithOnlyEndTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      MessageBox.Show("Начальный период введён неверно");
      docs = (from b in HoldingDocuments
              where b.Time.Date <= Convert.ToDateTime(EndPeriod.Text) && b.State == "Расход"
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date <= Convert.ToDateTime(EndPeriod.Text)
                  select b).ToList();
    }

    private void CreateDemendedDocsWithAllTime(ref List<Holding_document> docs, ref List<ShopRequests> requests)
    {
      docs = (from b in HoldingDocuments
              where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
              b.Time.Date < Convert.ToDateTime(EndPeriod.Text) && b.State == "Расход"
              select b).ToList();
      requests = (from b in ShopRequests
                  where b.Time.Date >= Convert.ToDateTime(StartOfPeriodTextBox.Text) &&
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
          List<PurchaseStatistic> purchasingStatistic = new List<PurchaseStatistic>();
          List<Purchases> purchases = new List<Purchases>();
          List<Sellings> sellings = new List<Sellings>();
          //ситуация если сразу вводится начало периода
          if(year.Length == 4 && firstPeriod[1].Length == 2 && firstPeriod[2].Length == 2)
          {
            if(Convert.ToInt32(firstPeriod[1]) <= 12 && Convert.ToInt32(firstPeriod[1]) > 0 &&
              Convert.ToInt32(firstPeriod[2]) <= 31 && Convert.ToInt32(firstPeriod[2]) > 0)
            {
              if((lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2) &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) >= 1 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) >= 1)
              {
                //ситуация когда начальные и конечные данные введены верно
                CreatePurchaseDocsWithAllTime(ref purchases, ref sellings);
              }
              else//ситуация когда введены верно только начальные данные
                CreatePurchaseDocsWithOnlyStartTime(ref purchases, ref sellings);

            }
            else
            {
              if(lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2 &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) >= 1 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) >= 1)
              {
                CreatePurchaseDocsWithOnlyEndTime(ref purchases, ref sellings);
              }
            }
          }
          else if(lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2)
          {
            if((Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0) &&
                (Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) > 0))
            {
              CreatePurchaseDocsWithOnlyEndTime(ref purchases, ref sellings);
            }
          }
          if(purchases.Count > 0 || sellings.Count > 0)
          {
            foreach(Purchases p in purchases)
            {
              if(purchasingStatistic.Where(i => i.Time.ToString("d") == p.Time.ToString("d")).Count() == 0)
              {
                purchasingStatistic.Add(new PurchaseStatistic(p.Time, p.Price, (decimal)0.0));
              }
              else
              {
                int index = purchasingStatistic.IndexOf(purchasingStatistic.Single(i => i.Time.ToString("d") == p.Time.ToString("d")));
                purchasingStatistic[index].Purchase += + p.Price;
              }
            }
            foreach(Sellings s in sellings)
            {
              if(purchasingStatistic.Where(i => i.Time.ToString("d") == s.Selling_date.ToString("d")).Count() == 0)
              {
                purchasingStatistic.Add(new PurchaseStatistic(s.Selling_date, (decimal)0.0, s.Price));
              }
              else
              {
                int index = purchasingStatistic.IndexOf(purchasingStatistic.Single(i => i.Time.ToString("d") == s.Selling_date.ToString("d")));
                purchasingStatistic[index].Sales += s.Price;
              }
            }
            foreach(PurchaseStatistic st in purchasingStatistic)
            {
              st.SetIncome();
            }
            PurchasingChart.ChartAreas[0].AxisX.Title = "Время";
            PurchasingChart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Malgun Gothic;", 12F);
            PurchasingChart.ChartAreas[0].AxisY.Title = "Прибыль";
            PurchasingChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Malgun Gothic;", 12F);
            PurchasingChart.Series.Add("Прибыль");
            PurchasingChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            PurchasingChart.Series[0].BorderWidth = 3;
            PurchasingChart.Titles.Add("График затрат и доходов");
            PurchasingChart.Titles[0].Font = new System.Drawing.Font("Malgun Gothic;", 12F);
            purchasingStatistic = (from b in purchasingStatistic
                                   orderby b.Time ascending
                                   select b).ToList();
            foreach(PurchaseStatistic p in purchasingStatistic)
            {
              PurchasingChart.Series[0].Points.AddXY(p.Time.ToString("d"),Convert.ToDouble(p.Income));
              PurchasingTable.Rows.Add(p.Time.ToString("d"), Math.Round(p.Purchase,2), Math.Round(p.Sales, 2), Math.Round(p.Income, 2));
            }
            DialogResult questionResult = MessageBox.Show("Распечатать график затрат?",
                                          "Печать документов",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Information,
                                          MessageBoxDefaultButton.Button2,
                                          MessageBoxOptions.DefaultDesktopOnly);
            if(questionResult == DialogResult.Yes)
            {
              PurchasingChart.Printing.PageSetup();
              PurchasingChart.Printing.PrintPreview();
            }
          }
          else
            MessageBox.Show("Данные введены некорректно, либо они отсутствуют");
          break;
        case "График востребованности":
          //нужен для составления графика востребованности
          List<Holding_document> docs = new List<Holding_document>();
          List<ShopRequests> requests = new List<ShopRequests>();
          List<Product> demandedInfo = new List<Product>();
          //ситуация если сразу вводится начало периода
          if(year.Length == 4 && firstPeriod[1].Length == 2 && firstPeriod[2].Length == 2)
          {
            if(Convert.ToInt32(firstPeriod[1]) <= 12 && Convert.ToInt32(firstPeriod[1]) > 0 && 
              Convert.ToInt32(firstPeriod[2]) <= 31 && Convert.ToInt32(firstPeriod[2]) > 0)
            {
              if((lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2) &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 && 
                Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) >= 1 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) >= 1) 
              {
                //ситуация когда начальные и конечные данные введены верно
                CreateDemendedDocsWithAllTime(ref docs, ref requests);
              }
              else//ситуация когда введены верно только начальные данные
                CreateDemendedDocsWithOnlyStartTime(ref docs, ref requests);

            }
            else
            {
              if(lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2 &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) >= 1 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) >= 1)
              {
                CreateDemendedDocsWithOnlyEndTime(ref docs, ref requests);
              }
            }
          }
          else if(lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2)
          {
            if((Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0) &&
                (Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) > 0))
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
            DemandedChart.Titles.Add("График востребованности");
            DemandedChart.Titles[0].Font = new System.Drawing.Font("Malgun Gothic;", 12F);
            foreach(Product p in demandedInfo)
            {
              var chart = DemandedChart.Series.Add(p.Name);
              chart.Points.Add(Convert.ToDouble(p.ID));
              DemandedTable.Rows.Add(p.Name, Convert.ToInt32(p.ID));
            }
            DialogResult questionResult = MessageBox.Show("Распечатать график востребованности?",
                                          "Печать документов",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Information,
                                          MessageBoxDefaultButton.Button2,
                                          MessageBoxOptions.DefaultDesktopOnly);
            if(questionResult == DialogResult.Yes)
            {
              DemandedChart.Printing.PageSetup();
              DemandedChart.Printing.PrintPreview();
            }
          }
          else
            MessageBox.Show("Данные введены некорректно, либо они отсутствуют");
          break;
        case "График эффективности труда":

          List<Holding_document> docsOfEfficiency = new List<Holding_document>();
          List<ShopRequests> requestsOfEfficiency = new List<ShopRequests>();
          List<Product> efficiencyInfo = new List<Product>();

          //ситуация если сразу вводится начало периода
          if(year.Length == 4 && firstPeriod[1].Length == 2 && firstPeriod[2].Length == 2)
          {
            if(Convert.ToInt32(firstPeriod[1]) <= 12 && Convert.ToInt32(firstPeriod[1]) > 0 &&
              Convert.ToInt32(firstPeriod[2]) <= 31 && Convert.ToInt32(firstPeriod[2]) > 0)
            {
              if((lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2) &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) >= 1 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) >= 1)
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
              if(lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2 &&
                Convert.ToInt32(lastYear) - Convert.ToInt32(year) >= 0 &&
                Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) >= 1 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) >= 1)
              {
                CreateEfficiencyDocsWithOnlyEndTime(ref docsOfEfficiency, ref requestsOfEfficiency);
              }
            }
          }
          else if(lastYear.Length == 4 && secondPeriod[1].Length == 2 && secondPeriod[2].Length == 2)
          {
            if(Convert.ToInt32(secondPeriod[1]) <= 12 && Convert.ToInt32(secondPeriod[1]) > 0 &&
                Convert.ToInt32(secondPeriod[2]) <= 31 && Convert.ToInt32(secondPeriod[2]) > 0)
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
            EfficiencyChart.Titles.Add("График эффективности");
            EfficiencyChart.Titles[0].Font = new System.Drawing.Font("Malgun Gothic;", 12F);
            foreach(Product p in efficiencyInfo)
            {
              var chart = EfficiencyChart.Series.Add(p.Name);
              chart.Points.Add(Convert.ToDouble(p.ID));
              EfficiencyTable.Rows.Add(p.Name, Convert.ToInt32(p.ID));
            }
            DialogResult questionResult = MessageBox.Show("Распечатать график эффективности?",
                                          "Печать документов",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Information,
                                          MessageBoxDefaultButton.Button2,
                                          MessageBoxOptions.DefaultDesktopOnly);
            if(questionResult == DialogResult.Yes)
            {
              EfficiencyChart.Printing.PageSetup();
              EfficiencyChart.Printing.PrintPreview();
            }
          }
          else
            MessageBox.Show("Данные введены некорректно, либо они отсутствуют");
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