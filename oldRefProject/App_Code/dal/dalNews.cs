using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNews
/// </summary>
public class dalNews
{
    DatabaseManager dm = new DatabaseManager();
	public dalNews()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int Insert(News news)
    {
        dm.AddParameteres("@TitleInBangla", news.TitleInBangla);
        dm.AddParameteres("@Title", news.Title);
        dm.AddParameteres("@ShortDescriptionInBangla", news.ShortDescriptionInBangla);
        dm.AddParameteres("@ShortDescription", news.ShortDescription);
        dm.AddParameteres("@DetailsInBangla", news.DetailsInBangla);
        dm.AddParameteres("@Details", news.Details);
        dm.AddParameteres("@Date", news.Date);
        dm.AddParameteres("@Photo", news.Photo);
        dm.AddParameteres("@CreatedBy", news.CreatedBy);
        dm.AddParameteres("@CreatedDate", news.CreatedDate);
        DataTable dt = dm.ExecuteQuery("USP_News_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, News news)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@TitleInBangla", news.TitleInBangla);
        dm.AddParameteres("@Title", news.Title);
        dm.AddParameteres("@ShortDescriptionInBangla", news.ShortDescriptionInBangla);
        dm.AddParameteres("@ShortDescription", news.ShortDescription);
        dm.AddParameteres("@DetailsInBangla", news.DetailsInBangla);
        dm.AddParameteres("@Details", news.Details);
        dm.AddParameteres("@Date", news.Date);
        dm.AddParameteres("@Photo", news.Photo);
        dm.AddParameteres("@UpdatedBy", news.UpdatedBy);
        dm.AddParameteres("@UpdatedDate", news.UpdatedDate);
        return dm.ExecuteNonQuery("USP_News_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_News_GetById");
    }

    public DataTable GetLatest()
    {
        return dm.ExecuteQuery("USP_News_GetLatest");
    }

    public DataTable GetByIdBangla(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_News_GetByIdBangla");
    }
    public DataTable GetByIdEnglish(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_News_GetByIdEnglish");
    }

    public DataTable GetLatestBangla()
    {
        return dm.ExecuteQuery("USP_NewsBangla_GetLatest");
    }
    public DataTable GetLatestEnglish()
    {
        return dm.ExecuteQuery("USP_NewsEnglish_GetLatest");
    }
}