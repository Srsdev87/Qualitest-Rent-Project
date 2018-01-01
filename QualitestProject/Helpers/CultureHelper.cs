using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QualitestProject.Helpers
{
    public class CultureHelper
    {
        public static Dictionary<string, string> CountryList()
        {
            //Creating Dictionary
            Dictionary<string, string> cultureList = new Dictionary<string, string>();

            //getting the specific CultureInfo from CultureInfo class
            CultureInfo[] getCultureInfo = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (CultureInfo getCulture in getCultureInfo)
            {
                //creating the object of RegionInfo class
                RegionInfo getRegionInfo = new RegionInfo(getCulture.LCID);
                //adding each country Name into the Dictionary
                if (!(cultureList.ContainsKey(getRegionInfo.Name)))
                {
                    cultureList.Add(getRegionInfo.Name, getRegionInfo.EnglishName);
                }
            
              
            }
         
            return cultureList;
        }
    }
}