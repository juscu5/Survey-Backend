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
    public class IdentityController : ControllerBase
    {
        private readonly SqlConnection conn = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CTSS_Survey;Integrated Security=True");
        private readonly SqlConnection conn1 = new SqlConnection(@"Data Source=JUNELPC\SQLEXPRESS;Initial Catalog=CCMS;Integrated Security=True");
        //private readonly SqlConnection conn = new SqlConnection(@"Data Source=MCK-WFMSQLQA-01;Initial Catalog=CTSS_Survey;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*72021;MultipleActiveResultSets=True");
        //private readonly SqlConnection conn1 = new SqlConnection(@"Data Source=MCKWFMDB01;Initial Catalog=TPSWeb;Persist Security Info=True;User Id=TPPH_WFM_AppUser;Password=M8jbYuWU*7;MultipleActiveResultSets=True");

        [HttpGet]
        public IActionResult Get()
        {
            var result = conn.Query<IdentityModel>(@"
                SELECT * FROM [IDENTITY_TABLE]
            ")?.ToList();
            return Ok(result);
        }

        //for validation of CCMS if available on CCMS_Employee Database
        [HttpGet("CCMS")]
        public IActionResult GetCCMS(int CCMS_ID)
        {
            var result = conn1.Query(@"
                SELECT [employee_ident]
                      ,[manager_ident]
                FROM [dbo].[CCMS_Employee]
                WHERE [employee_ident]=@CCMS_ID", new
            {
                CCMS_ID = CCMS_ID
            }
               )?.ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IdentityModel model)
        {
            var result = conn.Query<IdentityModel>(@"
                INSERT INTO IDENTITY_TABLE ([IDENTITY_ID]
                  ,[CCMS_ID]
                  ,[MANAGER_IDENT]
                  ,[GENDER]
                  ,[GEO]
                  ,[WORKTYPE]
                  ,[DIRECTOR]
                  ,[STATUS]
                  ,[DATE])
                VALUES
                (@IDENTITY_ID
                 ,@CCMS_ID
                 ,@MANAGER_IDENT
                 ,@GENDER
                 ,@GEO
                 ,@WORKTYPE
                 ,@DIRECTOR
                 ,@STATUS
                 ,@DATE)
            ", new
            {
                IDENTITY_ID = model.IDENTITY_ID,
                CCMS_ID = model.CCMS_ID,
                MANAGER_IDENT = model.MANAGER_IDENT,
                GENDER = model.GENDER,
                GEO = model.GEO,
                WORKTYPE = model.WORKTYPE,
                DIRECTOR = model.DIRECTOR,
                STATUS = model.STATUS,
                DATE = DateTime.UtcNow.ToString("M/d/yyyy hh:mm tt")
            });
            return Ok();
        }

        /**for validation of CCMS if 30 days
        [HttpGet("CCMS")]
        public IActionResult GetCCMS()
        {
            var result = conn.Query(@"
                SELECT [CCMS_ID] FROM [IDENTITY_TABLE] 
            ")?.AsList();
            return Ok(result);
        }**/

        /**[HttpGet("CCMS")]
        public IActionResult GetCCMS()
        {
            var result = conn.Query(@"
                SELECT [CCMS_ID] FROM [IDENTITY_TABLE] 
                WHERE DATEDIFF(day,DATE,GETDATE()) < 31
            ")?.AsList();
            return Ok(result);
        }**/

        /**[HttpGet("{COUNTRY}")]
        public IActionResult Get(string COUNTRY)
        {
            var result = conn.Query(@"
                DECLARE @geo AS VARCHAR(100)=@COUNTRY
                SELECT DISTINCT
                      [GEO_Full_Name]
                      ,[WFM Director]
                  FROM [SURVEY_DB].[dbo].[TEST_TABLE]
                  Where [GEO_Full_Name] = @geo AND [WFM Director] <> ''
            ", new
            {
                COUNTRY = COUNTRY
            }
            )?.ToList();
            return Ok(result);
        }

        [HttpGet("{COUNTRY}")]
        public IActionResult Get(string COUNTRY)
        {
            var result = conn1.Query(@"
                DECLARE @geo AS VARCHAR(100)=@COUNTRY
                SELECT DISTINCT
                    TPS.[GEO_Full_Name]
	                ,EMP.employee_ident
                    ,TPS.[WFM Director]
                FROM [TPSWeb].[dbo].[VW_SC_OWNERSHIP] AS TPS 
                LEFT JOIN [CCMS].[DBO].CCMS_EMPLOYEE AS EMP 
                ON TPS.[WFM Director] = EMP.[employee_common_name]
                Where TPS.[GEO_Full_Name] = @geo AND EMP.[STATUS] <> 'Terminated'
            ", new
            {
                COUNTRY = COUNTRY
            }
            )?.ToList();
            return Ok(result);
        }**/
    }
}
