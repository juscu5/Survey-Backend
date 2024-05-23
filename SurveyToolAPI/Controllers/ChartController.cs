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
    public class ChartController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CTSS_Survey;Integrated Security=True");
        //private readonly SqlConnection conn = new SqlConnection(@"Data Source=MCK-WFMSQLQA-01;Initial Catalog=CTSS_Survey;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*72021;MultipleActiveResultSets=True");

        [HttpGet("Total_Respondent")]
        public IActionResult GetTotalRespondent()
        {

            var result = conn.Query(@"
                SELECT count(CCMS_ID) as Total_Respondent FROM [IDENTITY_TABLE]
            ")?.ToList();

            return Ok(result);

        }

        [HttpGet("Total_Male")]
        public IActionResult GetTotalMale()
        {

            var result = conn.Query(@"
                SELECT count(CCMS_ID) as Total_Male FROM [IDENTITY_TABLE] where Gender = 'Male'
            ")?.ToList();

            return Ok(result);

        }

        [HttpGet("Total_Female")]
        public IActionResult GetTotalFemale()
        {

            var result = conn.Query(@"
                SELECT count(CCMS_ID) as Total_Female FROM [IDENTITY_TABLE] where GENDER = 'Female'
            ")?.ToList();

            return Ok(result);

        }

        [HttpGet("Satisfy_Response")]
        public IActionResult GetTotalSatisfyResponse()
        {

            var result = conn.Query(@"
                SELECT count(CCMS_ID) as Satisfy_Response FROM [RESPOND_TABLE] where RESPONSE >= 3
            ")?.ToList();

            return Ok(result);

        }

        [HttpGet("Total_Response")]
        public IActionResult GetTotalResponse()
        {

            var result = conn.Query(@"
                SELECT count(CCMS_ID) as Total_Response FROM [RESPOND_TABLE]
            ")?.ToList();

            return Ok(result);

        }
    }

}
