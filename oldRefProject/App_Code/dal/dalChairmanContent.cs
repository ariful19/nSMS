using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalPageContent
/// </summary>
public class dalChairmanContent
{
    DatabaseManager dm = new DatabaseManager();
    public int Insert(ChairmanContent chairmanContent)
    {
        dm.AddParameteres("@Titel", chairmanContent.Titel);
        dm.AddParameteres("@TitleBangla", chairmanContent.TitleBangla);
        dm.AddParameteres("@Name", chairmanContent.Name);
        dm.AddParameteres("@NameBangla", chairmanContent.NameBangla);
        dm.AddParameteres("@Designation", chairmanContent.Designation);
        dm.AddParameteres("@DesignationBangla", chairmanContent.DesignationBangla);
        dm.AddParameteres("@Address", chairmanContent.Address);
        dm.AddParameteres("@AddressBangla", chairmanContent.AddressBangla);
        dm.AddParameteres("@Photo", chairmanContent.Photo);
        dm.AddParameteres("@Description", chairmanContent.Description);
        dm.AddParameteres("@DescriptionBangla", chairmanContent.DescriptionBangla);
        DataTable dt = dm.ExecuteQuery("USP_InsertChairmanContent");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(ChairmanContent chairmanContent)
    {
        dm.AddParameteres("@ChairmanContentID", chairmanContent.ChairmanContentID);
        dm.AddParameteres("@Titel", chairmanContent.Titel);
        dm.AddParameteres("@TitleBangla", chairmanContent.TitleBangla);
        dm.AddParameteres("@Name", chairmanContent.Name);
        dm.AddParameteres("@NameBangla", chairmanContent.NameBangla);
        dm.AddParameteres("@Designation", chairmanContent.Designation);
        dm.AddParameteres("@DesignationBangla", chairmanContent.DesignationBangla);
        dm.AddParameteres("@Address", chairmanContent.Address);
        dm.AddParameteres("@AddressBangla", chairmanContent.AddressBangla);
        dm.AddParameteres("@Photo", chairmanContent.Photo);
        dm.AddParameteres("@Description", chairmanContent.Description);
        dm.AddParameteres("@DescriptionBangla", chairmanContent.DescriptionBangla);
        DataTable dt = dm.ExecuteQuery("USP_UpdateChairmanContent");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int InsertAboutUs(string desEnglish, string desBangla, string remarks, string textType)
    {
        dm.AddParameteres("@AboutUsEnglish", desEnglish);
        dm.AddParameteres("@AboutUsbangla", desBangla);
        dm.AddParameteres("@Remarks", remarks);
        dm.AddParameteres("@TextType", textType);

        DataTable dt = dm.ExecuteQuery("USP_InsertAboutUs");
        
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int UpdateAboutUs(int aboutUsID, string desEnglish, string desBangla, string remarks, string textType)
    {
        dm.AddParameteres("@AboutUsID", aboutUsID);
        dm.AddParameteres("@AboutUsEnglish", desEnglish);
        dm.AddParameteres("@AboutUsbangla", desBangla);
        dm.AddParameteres("@Remarks", remarks);
        dm.AddParameteres("@TextType", textType);
        DataTable dt = dm.ExecuteQuery("USP_UpdateAboutUs");
        
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetBangla()
    {
       return dm.ExecuteQuery("USP_ChairmanContentBangla");
    }

    public DataTable GetData()
    {
        string sql = "Select * from tblChairmanContent";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetData(string driv)
    {
        string sql = "Select * from tblAboutUs Where TextType = " + "'" + driv + "'";
        return dm.NewExecuteQuery(sql);
    }

}