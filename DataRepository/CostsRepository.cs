using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Credit;
using Newtonsoft.Json;

namespace DataRepository
{
    public class CostsRepository: IRepository<CostsDayReport, OneCashTransaction>
    {
        #region Singleton
        protected static CostsRepository instance = null;
        public static CostsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CostsRepository();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected CostsRepository() { }
        #endregion

        #region Data

        protected string fileName = "costs.json";
        protected string filePath = "Resource\\";

        #endregion

        public CostsDayReport GetByDay(DateTime day)
        {
            try
            {
                CostsDayReport result;

                if (!File.Exists(filePath + fileName))
                {
                    File.Create(filePath + fileName);
                }

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<CostsDayReport> today = JsonConvert.DeserializeObject<List<CostsDayReport>>(json);
                    if (today != null && today.Where(x => x.Date == day)
                            .FirstOrDefault() != null)
                    {
                        result = today.Where(x => x.Date == day)
                            .FirstOrDefault();
                    }
                    else
                    {
                        today = new List<CostsDayReport>();
                        today.Add(new CostsDayReport()
                        {
                            Date = DateTime.Today,
                            TotalCosts = 0,
                            DailyCosts = new ObservableCollection<OneCashTransaction>()});
                        result = today.First();
                    }
                    
                }
                return result;
            }
            catch
            {
                return new CostsDayReport(){
                            Date = DateTime.Today,
                            TotalCosts = 0,
                            DailyCosts = new ObservableCollection<OneCashTransaction>()
                };
                    
            }
        }

        public void AddOperation(OneCashTransaction newTransaction)
        {
            try
            {
                string newJson = "";

                if (!File.Exists(filePath + fileName))
                {
                    File.Create(filePath + fileName);
                }

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<CostsDayReport> today = JsonConvert.DeserializeObject<List<CostsDayReport>>(json);
                    if (today != null && today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault() != null)
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailyCosts.Add(newTransaction);
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().TotalCosts += newTransaction.Money;
                    }
                    else
                    {
                        if (today == null)
                        {
                            today = new List<CostsDayReport>();
                        }
                        today.Add(new CostsDayReport()
                        {
                            Date = DateTime.Today, 
                            TotalCosts = newTransaction.Money,
                            DailyCosts = new ObservableCollection<OneCashTransaction>() { newTransaction}
                        });
                    }
                    newJson = JsonConvert.SerializeObject(today);
                }

                File.WriteAllText(filePath + fileName, newJson);
            }
            catch
            {
                
            }
        }

        public void DeleteOperation(OneCashTransaction delCashTransaction)
        {
            try
            {
                string newJson = "";

                if (!File.Exists(filePath + fileName))
                {
                    File.Create(filePath + fileName);
                }

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<CostsDayReport> today = JsonConvert.DeserializeObject<List<CostsDayReport>>(json);
                    if (today == null)
                    {
                        return;
                    }
                    else
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailyCosts.Remove(delCashTransaction);
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().TotalCosts -= delCashTransaction.Money;
                    }
                    newJson = JsonConvert.SerializeObject(today);
                }

                File.WriteAllText(filePath + fileName, newJson);
            }
            catch
            {

            }
        }
    }
}
