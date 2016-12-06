using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WindMonitoringSystem.Models;

namespace WindMonitoringSystem.Controllers
{
    public class WindMonitoringController : Controller
    {
        public static List<State> StateList = new List<State> { 
            new State { StateName = "Karnataka", StateId = 1 },
            new State { StateName = "TamilNadu", StateId = 2 },
            new State { StateName = "Andhra Pradesh", StateId = 3 },
            new State { StateName = "Kerala", StateId = 4 }
        };

        public static List<City> CityList = new List<City> { 
            new City { CityName = "Bengaluru", CityId = 1, StateId=1,StationCode="KA-BA-01" },
            new City { CityName = "Hubli-Dharwad", CityId = 2, StateId=1,StationCode="KA-HU-01" },
            new City { CityName = "Belagavi", CityId = 3, StateId=1,StationCode="KA-BE-01" },
            new City { CityName = "Mangaluru", CityId = 4, StateId=1,StationCode="KA-MA-01" },

            new City { CityName = "Chennai", CityId = 5, StateId=2,StationCode="TN-CH-01" },
            new City { CityName = "Coimbatore", CityId = 6, StateId=2,StationCode="TN-CO-01" },
            new City { CityName = "Madurai", CityId = 7, StateId=2,StationCode="TN-MD-01" },
            new City { CityName = "Tiruchirappalli", CityId = 8, StateId=2,StationCode="TN-TP-01" },
            new City { CityName = "Salem", CityId = 9, StateId=2,StationCode="TN-SA-01" },
            new City { CityName = "Tirunelveli", CityId = 10, StateId=2,StationCode="TN-TI-01" },

            new City { CityName = "Visakhapatnam", CityId = 11, StateId=3,StationCode="AP-VP-01" },
            new City { CityName = "Vijayawada", CityId = 12, StateId=3,StationCode="AP-VW-01" },
            new City { CityName = "Guntur", CityId = 13, StateId=3,StationCode="AP-GN-01" },
            new City { CityName = "Nellore", CityId = 14, StateId=3,StationCode="AP-NE-01" },

            new City { CityName = "Thiruvananthapuram", CityId = 15, StateId=4,StationCode="KL-TH-01" },
            new City { CityName = "Kochi", CityId = 16, StateId=4,StationCode="KL-KC-01" },
            new City { CityName = "Kozhikode", CityId = 17, StateId=4,StationCode="KL-KO-01" },
            new City { CityName = "Thrissur", CityId = 18, StateId=4,StationCode="KL-TR-01" }
        };

        private List<PredictedSpeed> PredictedSpeed = new List<PredictedSpeed> { 
        new PredictedSpeed{StationCode="KA-BA-01",PredSpeed=1},
        new PredictedSpeed{StationCode="KA-HU-01",PredSpeed=2},
        new PredictedSpeed{StationCode="KA-BE-01",PredSpeed=3},
        new PredictedSpeed{StationCode="KA-MA-01",PredSpeed=4},

        new PredictedSpeed{StationCode="TN-CH-01",PredSpeed=5},
        new PredictedSpeed{StationCode="TN-CO-01",PredSpeed=6},
        new PredictedSpeed{StationCode="TN-MD-01",PredSpeed=7},
        new PredictedSpeed{StationCode="TN-TP-01",PredSpeed=8},
        new PredictedSpeed{StationCode="TN-SA-01",PredSpeed=9},
        new PredictedSpeed{StationCode="TN-TI-01",PredSpeed=10},
        new PredictedSpeed{StationCode="AP-VP-01",PredSpeed=11},
        new PredictedSpeed{StationCode="AP-VW-01",PredSpeed=12},
        new PredictedSpeed{StationCode="AP-GN-01",PredSpeed=13},
        new PredictedSpeed{StationCode="AP-NE-01",PredSpeed=5},
        new PredictedSpeed{StationCode="KL-TH-01",PredSpeed=6},
        new PredictedSpeed{StationCode="KL-KC-01",PredSpeed=4},
        new PredictedSpeed{StationCode="KL-KO-01",PredSpeed=8},
        new PredictedSpeed{StationCode="KL-TR-01",PredSpeed=9}
        };

        // GET: WindMonitoring
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetInitialData()
        {
            var windMonitor = new WindMonitoring { StateList = StateList, CityList = CityList };
            return Json(windMonitor, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPredictedSpeed(string stationCode)
        {
            var predictedSpeed = PredictedSpeed.Where(c => c.StationCode.Equals(stationCode)).ToList();
            int preSpeed = 0;
            if (predictedSpeed != null && predictedSpeed.Any())
            {
                preSpeed = predictedSpeed.FirstOrDefault().PredSpeed;
            }
            return Json(preSpeed, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveWindMonitoring([FromBody] WindMonitoring windMonitoring)
        {
            if (Session["windMonitoring"] == null)
            {
                Session["windMonitoring"] = new List<WindMonitoring>();
            }
            var windMonitoringList = Session["windMonitoring"] as List<WindMonitoring>;
            if (windMonitoringList != null)
            {
                windMonitoringList.Add(windMonitoring);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}