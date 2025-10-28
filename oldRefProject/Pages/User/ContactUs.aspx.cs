using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_User_ContactUs : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
//        String Locations = "map.addOverlay(new GMarker(new GLatLng(23.732027, 90.4293921)";
//        Literal1.Text = @"<script type='text/javascript'>
//                            function initialize() {
//                              if (GBrowserIsCompatible()) {
//                                var map = new GMap2(document.getElementById('map_canvas'));
//                                map.setCenter(new GLatLng(23.732027, 90.4293921), 10); 
//                                " + Locations + @"
//                                map.setUIToDefault();
//                              }
//                            }
//                            </script> ";
   }


    private void BuildScript()
    {
        String Locations = "map.addOverlay(new GMarker(new GLatLng(23.732027, 90.4293921)";
        //foreach (DataRow r in tbl.Rows)
        //{
        //    // bypass empty rows	 	
        //    if (r["Latitude"].ToString().Trim().Length == 0)
        //        continue;

        //    string Latitude = r["Latitude"].ToString();
        //    string Longitude = r["Longitude"].ToString();

        //    // create a line of JavaScript for marker on map for this record	
        //    Locations += Environment.NewLine + " map.addOverlay(new GMarker(new GLatLng(" + Latitude + "," + Longitude + ")));";
        //}

        // construct the final script
//        Literal1.Text = @"<script type='text/javascript'>
//                            function initialize() {
//                              if (GBrowserIsCompatible()) {
//                                var map = new GMap2(document.getElementById('map_canvas'));
//                                map.setCenter(new GLatLng(23.732027, 90.4293921), 10); 
//                                " + Locations + @"
//                                map.setUIToDefault();
//                              }
//                            }
//                            </script> ";
    }

}