using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyToolAPI.Models
{
    public class IdentityModel
    {
        public Guid IDENTITY_ID { get; set; }

        public string CCMS_ID { get; set; }

        public string MANAGER_IDENT { get; set; }

        public int E_ID { get; set; }

        public string GENDER { get; set; }

        public string GEO { get; set; }

        public string WORKTYPE { get; set; }

        public string DIRECTOR { get; set; }

        public bool STATUS { get; set; }

        public DateTime DATE { get; set; }
    }
}
