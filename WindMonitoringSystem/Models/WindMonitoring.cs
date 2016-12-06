using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WindMonitoringSystem.Models
{
    public class WindMonitoring
    {
        public List<State> StateList { get; set; }

        public List<City> CityList { get; set; }

        public int SelectedState { get; set; }

        public int SelectedCity { get; set; }

        public string StationCode { get; set; }

        public DateTime Date { get; set; }

        public int PredictedSpeed { get; set; }

        public int ActualSpeed { get; set; }

        public int Variance { get; set; }
    }
}
