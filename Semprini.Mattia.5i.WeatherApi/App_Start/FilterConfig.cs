﻿using System.Web;
using System.Web.Mvc;

namespace Semprini.Mattia._5i.WeatherApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ErrorHandler.AiHandleErrorAttribute());
        }
    }
}
