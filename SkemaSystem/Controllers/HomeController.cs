﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkemaSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["skeamsysdb"].ConnectionString);
            List<String> strings = new List<String>();
            var sql = "SELECT * FROM test";

            sqlCon.Open();
            var cmd = new SqlCommand(sql, sqlCon);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                strings.Add(reader["test"] as String);
            }

            sqlCon.Close();
            reader.Close();
            return View(strings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a quite awesome about page!";
            ViewBag.DifferentMessage = "How are you doing?";

            return View();
        }
    }
}