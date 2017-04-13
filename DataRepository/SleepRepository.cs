using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DataModel.Dream;
using Newtonsoft.Json;

namespace DataRepository
{
    public class SleepRepository: IRepository<DaySleepReport, OneSleep>
    {
        #region Singleton
        protected static SleepRepository instance = null;
        public static SleepRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new SleepRepository();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected SleepRepository() { }
        #endregion

        #region Data

        protected string fileName = "sleep.json";
        protected string filePath = "Resource\\";

        #endregion

        public DaySleepReport GetByDay(DateTime day)
        {
            try
            {
                DaySleepReport resoult;

                if (!File.Exists(filePath + fileName))
                {
                    File.Create(filePath + fileName);
                }

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<DaySleepReport> today = JsonConvert.DeserializeObject<List<DaySleepReport>>(json);
                    if (today != null && today.Where(x => x.Date == day)
                            .FirstOrDefault() != null)
                    {
                        resoult = today.Where(x => x.Date == day)
                            .FirstOrDefault();
                    }
                    else
                    {
                        today = new List<DaySleepReport>();
                        today.Add(new DaySleepReport()
                        {
                            Date = DateTime.Today,
                            TotalSleepTimeInSecond = 0,
                            DailySleepTimes = new ObservableCollection<OneSleep>()
                        });
                        resoult = today.First();
                    }

                }
                return resoult;
            }
            catch
            {
                return new DaySleepReport()
                {
                    Date = DateTime.Today,
                    TotalSleepTimeInSecond = 0,
                    DailySleepTimes = new ObservableCollection<OneSleep>()
                };

            }
        }

        public void AddOperation(OneSleep newOperation)
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
                    List<DaySleepReport> today = JsonConvert.DeserializeObject<List<DaySleepReport>>(json);
                    if (today != null && today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault() != null)
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailySleepTimes.Add(newOperation);
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().TotalSleepTimeInSecond += newOperation.GetDurationInSecond();
                    }
                    else
                    {
                        if (today == null)
                        {
                            today = new List<DaySleepReport>();
                        }
                        today.Add(new DaySleepReport()
                        {
                            Date = DateTime.Today,
                            TotalSleepTimeInSecond = newOperation.GetDurationInSecond(),
                            DailySleepTimes = new ObservableCollection<OneSleep>() { newOperation }
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

        public void DeleteOperation(OneSleep dellOperation)
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
                    List<DaySleepReport> today = JsonConvert.DeserializeObject<List<DaySleepReport>>(json);
                    if (today == null)
                    {
                        return;
                    }
                    else
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailySleepTimes.Remove(dellOperation);
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().TotalSleepTimeInSecond -= dellOperation.GetDurationInSecond();
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
