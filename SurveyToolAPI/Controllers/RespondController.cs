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
    public class RespondController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CTSS_Survey;Integrated Security=True");
        //private readonly SqlConnection conn = new SqlConnection(@"Data Source=MCK-WFMSQLQA-01;Initial Catalog=CTSS_Survey;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*72021;MultipleActiveResultSets=True");

        [HttpGet]
        public IActionResult Get()
        {
            var result = conn.Query<RespondModel>(@"
                SELECT * FROM [RESPOND_TABLE]
            ")?.ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] RespondModel model)
        {
            var result = conn.Query<RespondModel>(@"
                INSERT INTO RESPOND_TABLE ([RESPOND_ID]
                  ,[IDENTITY_ID]
                  ,[Q_ID]
                  ,[RESPONSE])
                VALUES
                (@RESPOND_ID
                 ,@IDENTITY_ID
                 ,@Q_ID
                 ,@RESPONSE)
            ", new
            {
                RESPOND_ID = Guid.NewGuid(),
                IDENTITY_ID = model.IDENTITY_ID,
                Q_ID = model.Q_ID,
                RESPONSE = model.RESPONSE,
            });
            return Ok();
        }
    }
}
