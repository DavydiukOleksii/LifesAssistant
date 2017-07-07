using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using DataModel.Calendar;
using Newtonsoft.Json;

namespace DataRepository
{
    public class CalendarRepository: ARepository, IRepository<DayTaskReport, OneTask>
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
        protected string HBFileName = "HB.json";
        //protected string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + "\\Config\\Resource\\";

        #endregion

        #region Task Methods
        public DayTaskReport GetByDay(DateTime day)
        {
            try
            {
                DayTaskReport result;

                CheckFileExists(resourceFolderPath + fileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + fileName))
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
                        today = new List<DayTaskReport>
                        {
                            new DayTaskReport()
                            {
                                Date = DateTime.Today,
                                DailyTasks = new ObservableCollection<OneTask>()
                            }
                        };
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
                DateTime date = DateTime.Parse(newTask.Time.ToLongDateString());

                CheckFileExists(resourceFolderPath + fileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<DayTaskReport> today = JsonConvert.DeserializeObject<List<DayTaskReport>>(json);
                    if (today != null && today.Where(x => x.Date == date)
                            .FirstOrDefault() != null)
                    {
                        today.Where(x => x.Date == date)
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
                            Date = date,
                            DailyTasks = new ObservableCollection<OneTask>() { newTask }
                        });
                    }
                    newJson = JsonConvert.SerializeObject(today);
                }

                File.WriteAllText(resourceFolderPath + fileName, newJson);
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
                DateTime date = DateTime.Parse(dellTask.Time.ToLongDateString());

                CheckFileExists(resourceFolderPath + fileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<DayTaskReport> today = JsonConvert.DeserializeObject<List<DayTaskReport>>(json);
                    if (today == null)
                    {
                        return;
                    }
                    else
                    {
                        today.Where(x => x.Date == date).FirstOrDefault().DailyTasks.Remove(dellTask);
                    }
                    newJson = JsonConvert.SerializeObject(today);
                }
                File.WriteAllText(resourceFolderPath + fileName, newJson);
            }
            catch{}
        }

        public List<DateTime> GetListDaysWithTask()
        {
            try
            {
                List<DateTime> result = new List<DateTime>();

                CheckFileExists(resourceFolderPath + fileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + fileName))
                {
                    string json = r.ReadToEnd();
                    List<DayTaskReport> today = JsonConvert.DeserializeObject<List<DayTaskReport>>(json);
                    if (today != null)
                    {
                        result = today.Where(x => x.DailyTasks.Count > 0).Select(x => x.Date).ToList();
                    }
                    else
                    {
                        result.Add(DateTime.Today);
                    }

                }
                return result;
            }
            catch
            {
                return new List<DateTime>();

            }
        }
        #endregion

        #region HB Methods

        public List<OneHB> GetHBByDay(DateTime day)
        {
            try
            {
                List<OneHB> result = new List<OneHB>();

                CheckFileExists(resourceFolderPath + HBFileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + HBFileName))
                {
                    string json = r.ReadToEnd();
                    List<OneHB> today = JsonConvert.DeserializeObject<List<OneHB>>(json);
                    if (today != null)
                    {
                        result = today.Where(x => x.Date.Day == day.Day && x.Date.Month == day.Month).Select(x => x).ToList();
                    }
                }
                return result;
            }
            catch
            {
                return new List<OneHB>();
            }
        }

        public void AddHBOperation(OneHB newHB)
        {
            try
            {
                string newJson = "";

                CheckFileExists(resourceFolderPath + HBFileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + HBFileName))
                {
                    string json = r.ReadToEnd();
                    List<OneHB> today = JsonConvert.DeserializeObject<List<OneHB>>(json);
                    if (today != null )
                    {
                        today.Add(newHB);
                    }
                    else
                    {
                        if (today == null)
                        {
                            today = new List<OneHB>();
                        }
                        today.Add(new OneHB()
                        {
                            Date = newHB.Date,
                            FullName = newHB.FullName
                        });
                    }
                    newJson = JsonConvert.SerializeObject(today);
                }

                File.WriteAllText(resourceFolderPath + HBFileName, newJson);
            }
            catch {}
        }

        public void DeleteHBOperation(OneHB delHB)
        {
            try
            {
                string newJson = "";

                CheckFileExists(resourceFolderPath + HBFileName);

                using (StreamReader r = new StreamReader(resourceFolderPath + HBFileName))
                {
                    string json = r.ReadToEnd();
                    List<OneHB> today = JsonConvert.DeserializeObject<List<OneHB>>(json);
                    if (today == null)
                    {
                        return;
                    }
                    else
                    {
                        bool tryDell = today.Remove(today.Where(x => (x.Date.Year == delHB.Date.Year && x.Date.Year == delHB.Date.Year && x.Date.Year == delHB.Date.Year && x.FullName == delHB.FullName)).FirstOrDefault());
                    }
                    newJson = JsonConvert.SerializeObject(today);
                }

                File.WriteAllText(resourceFolderPath + HBFileName, newJson);
            }
            catch { }
        }

        #endregion
    }
}
