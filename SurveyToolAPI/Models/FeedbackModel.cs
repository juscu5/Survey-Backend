using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyToolAPI.Models
{
    public class FeedbackModel
    {
        public Guid FB_ID { get; set; }

        public Guid IDENTITY_ID { get; set; }

        public float FB_RATINGS { get; set; }

        public string FB_COMMENTS { get; set; }
    }
}
