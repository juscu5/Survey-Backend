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
    public class SurveyController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CTSS_Survey;Integrated Security=True");
        //private readonly SqlConnection conn = new SqlConnection(@"Data Source=MCK-WFMSQLQA-01;Initial Catalog=CTSS_Survey;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*72021;MultipleActiveResultSets=True");

        [HttpGet]
        public IActionResult Get()
        {
            var result = conn.Query<SurveyModel>(@"
                SELECT * FROM [SURVEY_TABLE]
            ")?.ToList();
            return Ok(result);
        }

        [HttpGet("{TYPE}")]
        public IActionResult Get(string TYPE)
        {
            var result = conn.Query<SurveyModel>(@"
                SELECT * FROM [SURVEY_TABLE] WHERE TYPE=@TYPE
            ", new
            {
                TYPE = TYPE
            }
            )?.ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SurveyModel model)
        {
            var result = conn.Query<SurveyModel>(@"
                INSERT INTO SURVEY_TABLE ([Q_ID]
                  ,[QUESTIONS]
                  ,[TYPE]
                  ,[STATUS])
                VALUES
                (@Q_ID
                 ,@QUESTIONS
                 ,@TYPE
                 ,@STATUS)
            ", new
            {
                Q_ID = Guid.NewGuid(),
                QUESTIONS = model.QUESTIONS,
                TYPE = model.TYPE,
                STATUS = model.STATUS
            });
            return Ok();
        }

        [HttpDelete("{Q_ID}")]
        public IActionResult Delete(string Q_ID)
        {
            conn.Execute(@"DELETE FROM SURVEY_TABLE WHERE Q_ID=@Q_ID", new
            {
                Q_ID = Guid.Parse(Q_ID)
            });
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(SurveyModel survey)
        {
            conn.Execute(@"
            UPDATE SURVEY_TABLE SET
            QUESTIONS=@QUESTIONS, STATUS=@STATUS, TYPE=@TYPE
            WHERE Q_ID=@Q_ID", new
            {
                Q_ID = survey.Q_ID,
                QUESTIONS = survey.QUESTIONS,
                TYPE = survey.TYPE,
                STATUS = survey.STATUS
            });
            return Ok();
        }
    }
}
