using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for dalLibrary
/// </summary>
public class dalLibrary
{
    DatabaseManager dm = new DatabaseManager();
	public dalLibrary()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertBook(int categoryId,int subCategoryId,int countryId, int publisherId, int languageId, int editionId,string title, string author, string isbn,string volume,
        string selfNo, string cellNo, bool isAvailable, int stock, string subTitle, string keyWord, string description, string coverPhoto, DateTime publishedDate, string createBy, DateTime createdDate)
    {
         dm.AddParameteres("@CategoryId",categoryId);
         dm.AddParameteres("@SubCategoryId",subCategoryId); 
         dm.AddParameteres("@CountryId",countryId);
         dm.AddParameteres("@PublisherId",publisherId);
         dm.AddParameteres("@LanguageId",languageId);
         dm.AddParameteres("@EditionId",editionId);
         dm.AddParameteres("@Title",title);
         dm.AddParameteres("@Author",author);
         dm.AddParameteres("@ISBN",isbn);
         dm.AddParameteres("@VolumeNo",volume);
         dm.AddParameteres("@SelfNo",selfNo);
         dm.AddParameteres("@CellNo",cellNo);
         dm.AddParameteres("@IsAvailable",isAvailable);
         dm.AddParameteres("@Stock",stock);
         dm.AddParameteres("@SubTitle",subTitle);
         dm.AddParameteres("@KeyWords",keyWord);
         dm.AddParameteres("@Description",description);
         dm.AddParameteres("@CoverPhoto",coverPhoto);
         dm.AddParameteres("@PublishedDate",publishedDate);
         dm.AddParameteres("@CreatedBy",createBy);
         dm.AddParameteres("@CratedDate",createdDate);
         return dm.ExecuteNonQuery("USP_Book_Insert");
    }

    public int UpdateBook(int id,int categoryId, int subCategoryId, int countryId, int publisherId, int languageId, int editionId, string title, string author, string isbn, string volume,
        string selfNo, string cellNo, bool isAvailable, int stock, string subTitle, string keyWord, string description, string coverPhoto, DateTime publishedDate, string createBy, DateTime createdDate)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@CategoryId", categoryId);
        dm.AddParameteres("@SubCategoryId", subCategoryId);
        dm.AddParameteres("@CountryId", countryId);
        dm.AddParameteres("@PublisherId", publisherId);
        dm.AddParameteres("@LanguageId", languageId);
        dm.AddParameteres("@EditionId", editionId);
        dm.AddParameteres("@Title", title);
        dm.AddParameteres("@Author", author);
        dm.AddParameteres("@ISBN", isbn);
        dm.AddParameteres("@VolumeNo", volume);
        dm.AddParameteres("@SelfNo", selfNo);
        dm.AddParameteres("@CellNo", cellNo);
        dm.AddParameteres("@IsAvailable", isAvailable);
        dm.AddParameteres("@Stock", stock);
        dm.AddParameteres("@SubTitle", subTitle);
        dm.AddParameteres("@KeyWords", keyWord);
        dm.AddParameteres("@Description", description);
        dm.AddParameteres("@CoverPhoto", coverPhoto);
        dm.AddParameteres("@PublishedDate", publishedDate);
        dm.AddParameteres("@UpdatedBy", createBy);
        dm.AddParameteres("@UpdatedDate", createdDate);
        return dm.ExecuteNonQuery("USP_Book_Update");
    }
    public DataTable GetBook(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Book_GetByCriteria");
    }
    public DataTable GetBookById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_Book_GetById");
    }


    public int IssueBookInsert(DataTable dt, int personId, DateTime issueDate, DateTime returnDate, string issuedBy)
    {
        DataSet ds = new DataSet("dsIssueBook");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@IssueDate", issueDate);        
        dm.AddParameteres("@ReturnDate", returnDate);
        dm.AddParameteres("@IssuedBy", issuedBy);
        return dm.ExecuteNonQuery("USP_Library_IssueBookInserted");
    }

    public DataTable GetTeacherIssuedBook(string criteria, bool isReturn)
    {
        dm.AddParameteres("@Criteria", criteria);
        dm.AddParameteres("@IsReturn", isReturn);
        return dm.ExecuteQuery("USP_Library_TeacherIssuedBookByCriteria");
    }

    public DataTable GetStudentIssedBook(string criteria, bool isReturn)
    {
        dm.AddParameteres("@Criteria", criteria);
        dm.AddParameteres("@IsReturn", isReturn);
        return dm.ExecuteQuery("USP_Library_StudentIssuedBookByCriteria");
    }

    public int ReturnBook(List<IssueBook> issuedBookList)
    {
        int id = 0;
        foreach (IssueBook issueBook in issuedBookList)
        {
            dm.AddParameteres("@Id", issueBook.Id);
            dm.AddParameteres("@ReceivedBy", issueBook.ReceivedBy);
            dm.AddParameteres("@ReceivedDate", issueBook.ReceivedDate);
            dm.AddParameteres("@IsReturn", issueBook.IsReturn);

           id= dm.ExecuteNonQuery("USP_Library_IssuedBook_Update");                       
        }

        return id;
    }

    public int UpdateBookStock(int id, int stock, bool isAvailable)
    {
        dm.AddParameteres("@Id", id);
        dm.AddParameteres("@IsAvailable", isAvailable);
        dm.AddParameteres("@Stock", stock);

        return dm.ExecuteNonQuery("USP_Book_Stock_Update");
    }
}