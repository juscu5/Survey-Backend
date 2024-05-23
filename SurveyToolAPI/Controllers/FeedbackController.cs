using Dapper;
using SurveyToolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace SurveyToolAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CTSS_Survey;Integrated Security=True");
        //private readonly SqlConnection conn = new SqlConnection(@"Data Source=MCK-WFMSQLQA-01;Initial Catalog=CTSS_Survey;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*72021;MultipleActiveResultSets=True");

        [HttpPost]
        public IActionResult Post([FromBody] FeedbackModel model)
        {
            var result = conn.Query<FeedbackModel>(@"
                INSERT INTO FEEDBACK_TABLE ([FB_ID]
                  ,[IDENTITY_ID]
                  ,[FB_RATINGS]
                  ,[FB_COMMENTS])
                VALUES
                (@FB_ID
                 ,@IDENTITY_ID
                 ,@FB_RATINGS
                 ,@FB_COMMENTS)
            ", new
            {
                FB_ID = Guid.NewGuid(),
                IDENTITY_ID = model.IDENTITY_ID,
                FB_RATINGS = model.FB_RATINGS,
                FB_COMMENTS = model.FB_COMMENTS,

            });
            return Ok();
        }
    }
}
