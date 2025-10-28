using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Controller
/// </summary>
public class Controller
{
    public Controller()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
    {
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;
        int destWidth = (int)size.Width;
        int destHeight = (int)size.Height;
        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage(b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.CompositingQuality = CompositingQuality.HighQuality;
        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();
        imgToResize.Dispose();
        return (System.Drawing.Image)b;
    }

    public static int RoleIdByUserName(string userName)
    {
        DataTable dt = new dalRole().GetIdByUserName(userName);
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public static string RoleNameById(int id)
    {
        DataTable dt = new dalRole().RoleNameById(id);
        return dt.Rows[0][0].ToString();
    }

    public static int RegistrationNo()
    {
        DataTable dt = new dalCommon().GetRegistrationNo();
        int regNo = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0][0]));
        return regNo;
    }
    public static int TeacherPin()
    {
        DataTable dt = new dalCommon().GetTeacherPin();
        int PIN = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0][0]));
        return PIN;
    }

    public static int AutoStudentId()
    {
        DataTable dt = new dalCommon().GetStudentId();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }

    public static int AutoStudentIdforZigatola()
    {
        DataTable dt = new dalCommon().GetStudentIdforZigatola();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1000 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }

 public static int AutoCollegeScienceStudentId()
    {
        DataTable dt = new dalCommon().GetStudentIdForCollegeScience();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }

    public static int AutoCollegeBusinessStudentId()
    {
        DataTable dt = new dalCommon().GetStudentIdForCollegeBusiness();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }

 public static int AutoStudentIdforAdaborEnglishMedium()
    {
        DataTable dt = new dalCommon().GetAutoStudentIdforAdaborEnglishMedium();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }

    public static int AutoStudentIdforZigatolaEnglishMedium()
    {
        DataTable dt = new dalCommon().GetAutoStudentIdforZigatolaEnglishMedium();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }


    public static int TransactionNumber()
    {
        DataTable dt = new dalCommon().GetTransactionNumber();
        int PIN = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0][0]));
        return PIN;
    }
    public static int UpdateRegistrationNo()
    {
        return new dalCommon().UpdateRegistrationNo();
    }
    public static void DeleteFile(string path)
    {
        try
        {
            System.IO.File.Delete(path);
        }
        catch (Exception)
        {
        }
    }
    public static int Delete(string table, int id)
    {
        return new Common().Delete(table, id);
    }
    public static int PersonIdByUserName(string userName)
    {
        return new Common().PersonIdByUserName(userName);
    }

    public static int TecherIdByPersonId(int personId)
    {
        return new Common().TecherIdByPersonId(personId);
    }
    public static int StudentIdByPersonalId(int personId)
    {
        return new Common().StudentIdByPersonId(personId);
    }
    public static int GetLastRoll(string criteria)
    {
        return new Common().GetLastRoll(criteria);
    }
    public static int TecherIdByPersonalId(int personId)
    {
        return new Common().TecherIdByPersonId(personId);
    }

    public static string RegistrationNoPrefix(int mId)
    {

        return new dalCommon().RegistrationNoPrefix(mId);
    }
  public static int GetBillNo()
    {
        DataTable dt = new dalCommon().GetBillNo();
        int stuID = (string.IsNullOrEmpty(dt.Rows[0][0].ToString()) ? 1 : Convert.ToInt32(dt.Rows[0][0]));
        return stuID;
    }
}



