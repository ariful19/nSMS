using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalLibraryBasic
/// </summary>
public class dalLibraryBasic
{
    DatabaseManager dm = new DatabaseManager();
	public dalLibraryBasic()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Category
    public int InsertCategory(string name)
    {
        dm.AddParameteres("@Category", name);
        DataTable dt = dm.ExecuteQuery("USP_Category_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateCategory(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Category", name);
        return dm.ExecuteNonQuery("USP_Category_Update");
    }
    #endregion

    #region Sub-category
    public int InsertSubCategory(int categoryId,string name)
    {
        dm.AddParameteres("@CategoryId", categoryId);
        dm.AddParameteres("@SubCategory", name);
        DataTable dt = dm.ExecuteQuery("USP_SubCategory_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateSubCategory(int id,int categoryId, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@CategoryId", categoryId);
        dm.AddParameteres("@SubCategory", name);
        return dm.ExecuteNonQuery("USP_SubCategory_Update");
    }
    public DataTable GetById( int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_SubCategory_GetById");
    }

    public DataTable GetByCategoryId(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_SubCategory_GetSubCategoryByCategoryId");
    }
    #endregion

    #region Country
    public int InsertCountry(string name)
    {
        dm.AddParameteres("@Country", name);
        DataTable dt = dm.ExecuteQuery("USP_Country_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateCountry(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Country", name);
        return dm.ExecuteNonQuery("USP_Country_Update");
    }
    #endregion

    #region Language
    public int InsertLanguage(string name)
    {
        dm.AddParameteres("@Language", name);
        DataTable dt = dm.ExecuteQuery("USP_Language_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateLanguage(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Language", name);
        return dm.ExecuteNonQuery("USP_Language_Update");
    }
    #endregion

    #region Publisher
    public int InsertPublisher(string name)
    {
        dm.AddParameteres("@Publisher", name);
        DataTable dt = dm.ExecuteQuery("USP_Publisher_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdatePublisher(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Publisher", name);
        return dm.ExecuteNonQuery("USP_Publisher_Update");
    }
    #endregion

    #region Edition
    public int InsertEdition(string name)
    {
        dm.AddParameteres("@Edition", name);
        DataTable dt = dm.ExecuteQuery("USP_Edition_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int UpdateEdition(int id, string name)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@Edition", name);
        return dm.ExecuteNonQuery("USP_Edition_Update");
    }
    #endregion

    #region Common
    public DataTable GetById(string table, int id)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Library_BasicSetup_GetById");
    }
    #endregion
}