using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [HttpGet()]
        public ActionResult demo()
        {
            return View();
        }


        [HttpPost()]
        public ActionResult demo(FormCollection rs)
        {

            string cname = (rs["cname"] != null ? rs["cname"].ToString() : "");
            string zage =(rs["zage"] != null ? rs["zage"].ToString():"");
            string birthday = (rs["birthday"] != null ? rs["birthday"].ToString() : "");
            string kind = rs["go"].ToString();


            string sql = "";

            if (kind == "建立帳號")
            {
                string sn = DateTime.Now.ToString("yyyyMMddHHmmss");
                sql = $"insert demo.dbo.tb1 (sn,cname, zage,birthday) values ('{sn}' ,'{cname}','{zage}','{birthday}')";
                DataTable dt = sp_executesql(sql);
            }
            if (kind == "修改帳號")
            {
                string sn = rs["sn"].ToString();
                sql = $"update demo.dbo.tb1 set cname = '{cname }', zage ='{zage}', birthday ='{birthday}' where sn = '{sn}'";
                DataTable dt = sp_executesql(sql);
            }

            if (kind == "刪除帳號")
            {
                string sn = rs["sn"].ToString().Replace(",", "");
                sql = $"delete demo.dbo.tb1 where sn = '{sn}'";
                DataTable dt = sp_executesql(sql);
            }


            DataTable dt2 = sp_executesql("select sn, cname, zage,format(birthday,'yyyy/MM/dd') as birthday from demo.dbo.tb1");
            ViewBag.dt = dt2.AsEnumerable().ToList();

            return View();
        }

        public DataTable sp_executesql(string sql)
        {
            DataTable dt = new DataTable();
            string connstring = "Data Source=home.john0955.idv.tw,60001;User ID=demo;Password=demo;";
            using (SqlConnection conn = new SqlConnection(connstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Close();
            }
            return dt;

        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}