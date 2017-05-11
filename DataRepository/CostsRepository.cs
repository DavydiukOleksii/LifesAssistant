using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Charts;
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
        protected string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Config\\Resource\\";

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

        public ObservableCollection<ChartsElement> GetTotalByDay()
        {
            try
            {
                List<ChartsElement> result;

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<CostsDayReport> today = JsonConvert.DeserializeObject<List<CostsDayReport>>(json);
                    if (today != null )
                    {
                        result = today
                                    .AsEnumerable()
                                    .Where(y => y.Date.Year == DateTime.Now.Year && y.Date.Month == DateTime.Now.Month)
                                    .GroupBy(o => o.Date.Day)
                                    .Select(g => new ChartsElement(){Name = g.First().Date.Day.ToString(), Value = g.Sum(c => c.TotalCosts)})
                                    .ToList();
                    }
                    else
                    {
                        result = new List<ChartsElement>();
                    }
                }
                return new ObservableCollection<ChartsElement>(result);
            }
            catch
            {
                return null;
            }
        }

        public ObservableCollection<ChartsElement> GetTotalByMonth()
        {
            try
            {
                List<ChartsElement> result;

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<CostsDayReport> today = JsonConvert.DeserializeObject<List<CostsDayReport>>(json);
                    if (today != null)
                    {
                        result = today
                                    .AsEnumerable()
                                    .Where(y => y.Date.Year == DateTime.Now.Year)
                                    .GroupBy(o => o.Date.Month)
                                    .Select(g => new ChartsElement() { Name = g.First().Date.Month.ToString(), Value = g.Sum(c => c.TotalCosts) })
                                    .ToList();
                    }
                    else
                    {
                        result = new List<ChartsElement>();
                    }

                }
                return new ObservableCollection<ChartsElement>(result);
            }
            catch
            {
                return null;
            }
        }

        public ObservableCollection<ChartsElement> GetTotalByYear()
        {
            try
            {
                List<ChartsElement> result;

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<CostsDayReport> today = JsonConvert.DeserializeObject<List<CostsDayReport>>(json);
                    if (today != null)
                    {
                        result = today
                                    .AsEnumerable()
                                    .GroupBy(o => o.Date.Year)
                                    .Select(g => new ChartsElement()
                                    {
                                        Name = g.First().Date.Year.ToString(), 
                                        Value = g.Sum(c => c.TotalCosts)
                                    })
                                    .ToList();
                    }
                    else
                    {
                        result = new List<ChartsElement>();
                    }

                }
                return new ObservableCollection<ChartsElement>(result);
            }
            catch
            {
                return null;
            }
        }
    }
}
