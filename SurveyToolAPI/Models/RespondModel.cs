using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyToolAPI.Models
{
    public class RespondModel
    {
        public Guid RESPOND_ID { get; set; }

        public Guid IDENTITY_ID { get; set; }

        public Guid Q_ID { get; set; }

        public float RESPONSE { get; set; }
    }
}
