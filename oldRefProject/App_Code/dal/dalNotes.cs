using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalNotes
/// </summary>
public class dalNotes
{
    DatabaseManager dm = new DatabaseManager();
	public dalNotes()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int Insert(News news)
    {
        dm.AddParameteres("@SubjectToClassId", news.Id);
        dm.AddParameteres("@TitleInBangla", news.TitleInBangla);
        dm.AddParameteres("@Title", news.Title);
        dm.AddParameteres("@ShortDescriptionInBangla", news.ShortDescriptionInBangla);
        dm.AddParameteres("@ShortDescription", news.ShortDescription);
        dm.AddParameteres("@DetailsInBangla", news.DetailsInBangla);
        dm.AddParameteres("@Details", news.Details);
        dm.AddParameteres("@Date", news.Date);        
        dm.AddParameteres("@CreatedBy", news.CreatedBy);
        dm.AddParameteres("@CreatedDate", news.CreatedDate);
        DataTable dt = dm.ExecuteQuery("USP_Notes_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int Update(int id, News news)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@SubjectToClassId", news.Id);
        dm.AddParameteres("@TitleInBangla", news.TitleInBangla);
        dm.AddParameteres("@Title", news.Title);
        dm.AddParameteres("@ShortDescriptionInBangla", news.ShortDescriptionInBangla);
        dm.AddParameteres("@ShortDescription", news.ShortDescription);
        dm.AddParameteres("@DetailsInBangla", news.DetailsInBangla);
        dm.AddParameteres("@Details", news.Details);
        dm.AddParameteres("@Date", news.Date);  
        dm.AddParameteres("@UpdatedBy", news.UpdatedBy);
        dm.AddParameteres("@UpdatedDate", news.UpdatedDate);
        return dm.ExecuteNonQuery("USP_Notes_Update");
    }
    public DataTable GetById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Notes_GetById");
    }

    public DataTable GetLatest()
    {
        return dm.ExecuteQuery("USP_Notes_GetLatest");
    }

    public DataTable GetByIdBangla(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Notes_GetByIdBangla");
    }
    public DataTable GetByIdEnglish(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Notes_GetByIdEnglish");
    }

    public DataTable GetBanglaNotes(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_NotesBangla_GetByCriteria");
    }
    //public DataTable GetLatestEnglish()
    //{
    //    return dm.ExecuteQuery("USP_NewsEnglish_GetLatest");
    //}

    public DataTable GetByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Notes_GetByCriteria");
    }
}