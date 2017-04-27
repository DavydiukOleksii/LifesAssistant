using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DataModel.Charts;
using DataModel.Water;
using Newtonsoft.Json;

namespace DataRepository
{
    public class WaterRepository: IRepository<WaterDayReport, OnceDrink>
    {
        #region Singleton
        protected static WaterRepository instance = null;
        public static WaterRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new WaterRepository();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected WaterRepository() { }
        #endregion

        #region Data

        protected string fileName = "water.json";
        protected string filePath = "Resource\\";

        #endregion
        
        public WaterDayReport GetByDay(DateTime day)
        {
            try
            {
                WaterDayReport resoult;

                if (!File.Exists(filePath + fileName))
                {
                    File.Create(filePath + fileName);
                }

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<WaterDayReport> today = JsonConvert.DeserializeObject<List<WaterDayReport>>(json);
                    if (today != null && today.Where(x => x.Date == day)
                            .FirstOrDefault() != null)
                    {
                        resoult = today.Where(x => x.Date == day)
                            .FirstOrDefault();
                    }
                    else
                    {
                        today = new List<WaterDayReport>();
                        today.Add(new WaterDayReport()
                        {
                            Date = DateTime.Today,
                            TotalCapacity = 0,
                            DailyWaterOperations = new ObservableCollection<OnceDrink>()});
                        resoult = today.First();
                    }
                    
                }
                return resoult;
            }
            catch
            {
                return new WaterDayReport()
                {
                            Date = DateTime.Today,
                            TotalCapacity = 0,
                            DailyWaterOperations = new ObservableCollection<OnceDrink>()
                };
                    
            }
        }

        public void AddOperation(OnceDrink newOperation)
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
                    List<WaterDayReport> today = JsonConvert.DeserializeObject<List<WaterDayReport>>(json);
                    if (today != null && today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault() != null)
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailyWaterOperations.Add(newOperation);
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().TotalCapacity += newOperation.Capasity;
                    }
                    else
                    {
                        if (today == null)
                        {
                            today = new List<WaterDayReport>();
                        }
                        today.Add(new WaterDayReport()
                        {
                            Date = DateTime.Today,
                            TotalCapacity = newOperation.Capasity,
                            DailyWaterOperations = new ObservableCollection<OnceDrink>() { newOperation }
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

        public void DeleteOperation(OnceDrink delWaterOperation)
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
                    List<WaterDayReport> today = JsonConvert.DeserializeObject<List<WaterDayReport>>(json);
                    if (today == null)
                    {
                        return;
                    }
                    else
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailyWaterOperations.Remove(delWaterOperation);
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().TotalCapacity -= delWaterOperation.Capasity;
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
                    List<WaterDayReport> today = JsonConvert.DeserializeObject<List<WaterDayReport>>(json);
                    if (today != null)
                    {
                        result = today
                                    .AsEnumerable()
                                    .Where(y => y.Date.Year == DateTime.Now.Year && y.Date.Month == DateTime.Now.Month)
                                    .GroupBy(o => o.Date.Day)
                                    .Select(g => new ChartsElement() { Name = g.First().Date.Day.ToString(), Value = g.Sum(c => c.TotalCapacity) })
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
                    List<WaterDayReport> today = JsonConvert.DeserializeObject<List<WaterDayReport>>(json);
                    if (today != null)
                    {
                        result = today
                                    .AsEnumerable()
                                    .Where(y => y.Date.Year == DateTime.Now.Year)
                                    .GroupBy(o => o.Date.Month)
                                    .Select(g => new ChartsElement() { Name = g.First().Date.Month.ToString(), Value = g.Sum(c => c.TotalCapacity) })
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
                    List<WaterDayReport> today = JsonConvert.DeserializeObject<List<WaterDayReport>>(json);
                    if (today != null)
                    {
                        result = today
                                    .AsEnumerable()
                                    .GroupBy(o => o.Date.Year)
                                    .Select(g => new ChartsElement()
                                    {
                                        Name = g.First().Date.Year.ToString(),
                                        Value = g.Sum(c => c.TotalCapacity)
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
