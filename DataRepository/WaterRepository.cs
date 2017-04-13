using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DataModel.Water;
using Newtonsoft.Json;

namespace DataRepository
{
    public class WaterRepository: IRepository<WaterDayReport>
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

        public List<WaterDayReport> GetAll()
        {
            throw new NotImplementedException();
        }

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

        public void AddWaterOperation(OnceDrink newOperation)
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

        public void DeleteWaterOperation(OnceDrink delWaterOperation)
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
    }
}
