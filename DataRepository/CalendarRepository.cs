using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DataModel.Calendar;
using Newtonsoft.Json;

namespace DataRepository
{
    public class CalendarRepository: IRepository<DayTaskReport, OneTask>
    {
        #region Singleton
        protected static CalendarRepository instance = null;
        public static CalendarRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new CalendarRepository();
                return instance;
            }
        }
        #endregion

        #region Constructor
        protected CalendarRepository() { }
        #endregion

        #region Data

        protected string fileName = "calendar.json";
        protected string filePath = "Resource\\";

        #endregion

        public DayTaskReport GetByDay(DateTime day)
        {
            try
            {
                DayTaskReport result;

                if (!File.Exists(filePath + fileName))
                {
                    File.Create(filePath + fileName);
                }

                using (StreamReader r = new StreamReader(filePath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<DayTaskReport> today = JsonConvert.DeserializeObject<List<DayTaskReport>>(json);
                    if (today != null && today.Where(x => x.Date == day)
                            .FirstOrDefault() != null)
                    {
                        result = today.Where(x => x.Date == day)
                            .FirstOrDefault();
                    }
                    else
                    {
                        today = new List<DayTaskReport>();
                        today.Add(new DayTaskReport()
                        {
                            Date = DateTime.Today,
                            DailyTasks = new ObservableCollection<OneTask>()
                        });
                        result = today.First();
                    }

                }
                return result;
            }
            catch
            {
                return new DayTaskReport()
                {
                    Date = DateTime.Today,
                    DailyTasks = new ObservableCollection<OneTask>()
                };

            }
        }

        public void AddOperation(OneTask newTask)
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
                    List<DayTaskReport> today = JsonConvert.DeserializeObject<List<DayTaskReport>>(json);
                    if (today != null && today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault() != null)
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailyTasks.Add(newTask);
                    }
                    else
                    {
                        if (today == null)
                        {
                            today = new List<DayTaskReport>();
                        }
                        today.Add(new DayTaskReport()
                        {
                            Date = DateTime.Today,
                            DailyTasks = new ObservableCollection<OneTask>() { newTask }
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

        public void DeleteOperation(OneTask dellTask)
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
                    List<DayTaskReport> today = JsonConvert.DeserializeObject<List<DayTaskReport>>(json);
                    if (today == null)
                    {
                        return;
                    }
                    else
                    {
                        today.Where(x => x.Date == DateTime.Today)
                            .FirstOrDefault().DailyTasks.Remove(dellTask);
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
