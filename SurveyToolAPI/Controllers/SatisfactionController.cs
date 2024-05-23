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
    public class SatisfactionController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CTSS_Survey;Integrated Security=True");
        //private readonly SqlConnection conn = new SqlConnection(@"Data Source=MCK-WFMSQLQA-01;Initial Catalog=CTSS_Survey;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*72021;MultipleActiveResultSets=True");

        [HttpGet]
        public IActionResult Get([FromQuery]SatisfactionModel model)
        {
            var result = conn.Query(@"
                SELECT count(IDENTITY_ID) 
                as Satisfy_Response 
                FROM [RESPOND_TABLE] 
                WHERE 
                IDENTITY_ID=@IDENTITY_ID and 
                RESPONSE=@RESPONSE", new
                {
                    IDENTITY_ID = model.IDENTITY_ID,
                    RESPONSE = model.RESPONSE,
                }
               )?.ToList();
            return Ok(result);
        }
    }
}
