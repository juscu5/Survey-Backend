using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyToolAPI.Models
{
    public class SurveyModel
    {
        public Guid Q_ID { get; set; }

        public string QUESTIONS { get; set; }

        public string TYPE { get; set; }

        public bool STATUS { get; set; }
    }
}
