using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Nano.DataAccessLayer;
using System.Web.UI.WebControls;
using System.Web.UI;
using Dapper;
/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    DatabaseManager dm = new DatabaseManager();
    public Common()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bdoSessionInfo SessionInfo
    {
        get
        {
            try
            {
                if (HttpContext.Current.Session["SessionInfo"] != null)
                    return HttpContext.Current.Session["SessionInfo"] as bdoSessionInfo;
                else
                    return null;
            }
            catch (Exception)
            {

                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["SessionInfo"] = value;
        }
    }
    public static string Button()
    {
        return Common.SessionInfo.Button;
    }
    public DataTable GetAll(string table)
    {
        dm.AddParameteres("@Table", table);
        return dm.ExecuteQuery("USP_Get_All");
    }

    public string GetRolesByUserName(string userName)
    {
        dm.AddParameteres("UserName", userName);
        DataTable dt = dm.ExecuteQuery("USP_GetRolesByUser");
        return (dt.Rows[0][0]).ToString();
    }

    public DataTable GetCampusByUserName(string userName)
    {
        dm.AddParameteres("UserName", userName);
        return dm.ExecuteQuery("USP_GetCampusByUserName");
    }
    public int Delete(string table, int id)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Id", id);
        return dm.ExecuteNonQuery("USP_Delete");
    }
    public DataTable GetById(string table, string column, int condition)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Column", column);
        dm.AddParameteres("@Condition", condition);
        return dm.ExecuteQuery("USP_GetById");
    }

    public DataTable GetWarningDateById(int id)
    {
        dm.AddParameteres("@Id", id);
        return dm.ExecuteQuery("USP_GetWarningDateById");
    }

    public int DeleteStudentToClassById(string table, int id)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Id", id);
        return dm.ExecuteNonQuery("USP_DeleteStudentToClassById");
    }
    public int PersonIdByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        DataTable dt = dm.ExecuteQuery("USP_PersonId_GetByUserName");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int TecherIdByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        DataTable dt = dm.ExecuteQuery("USP_TecherId_GetByPersonId");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int StudentIdByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        DataTable dt = dm.ExecuteQuery("USP_StudentId_GetByPersonId");
        return Convert.ToInt32(dt.Rows[0][0]);
    }
    public int GetLastRoll(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        DataTable dt = dm.ExecuteQuery("USP_Student_GetLastRoll");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public static void LoadDropdown(DropDownList ddl, string table, int dataTextField, int dataValueField)
    {
        DataTable dt = new dalCommon().LoadDropdown(table);
        ddl.Items.Add(new ListItem("", ""));
        ddl.DataSource = dt;
        ddl.DataTextField = dt.Columns[dataTextField].ToString();
        ddl.DataValueField = dt.Columns[dataValueField].ToString();
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("---Select---", string.Empty));
        ddl.SelectedIndex = 0;
    }
    public static void DropdownByCondition(DropDownList ddl, string table, string tableColumn, string whereby, int dataTextField, int dataValueField)
    {
        DataTable dt = new dalCommon().DropdownByCondition(table, tableColumn, whereby);
        ddl.Items.Add(new ListItem("", ""));
        ddl.DataSource = dt;
        ddl.DataTextField = dt.Columns[dataTextField].ToString();
        ddl.DataValueField = dt.Columns[dataValueField].ToString();
        ddl.DataBind();
        if (dt.Rows.Count > 0)
        {
            ddl.Items.Insert(0, new ListItem("---Select---", string.Empty));
        }
        else
        {
            ddl.Items.Insert(0, new ListItem("---No Student Found---", string.Empty));
        }
        ddl.SelectedIndex = 0;
    }
    public static void DropdownByConditionMulti(DropDownList ddl, string table, string Column, string Column1, string Column2, string Column3, string Column4, string Column5, string Column6, string Column7, string whereby, string whereby1, string whereby2, string whereby3, string whereby4, string whereby5, string whereby6, int dataTextField, int dataValueField, bool isOrderByReg = false)
    {
        DataTable dt = new dalCommon().DropdownByCondition(table, Column, Column1, Column2, Column3, Column4, Column5, Column6, Column7, whereby, whereby1, whereby2, whereby3, whereby4, whereby5, whereby6);

        if (isOrderByReg && dt.Rows.Count > 0)
        {
            dt = dt.Rows.OfType<DataRow>()
                .OrderBy(o =>
                    int.Parse(string.Join("", Regex.Matches(o["RegNo"].ToString(), "\\d+")
                    .Cast<Match>()
                    .Select(m => m.Value))))
                 .CopyToDataTable();
        }
        ddl.Items.Add(new ListItem("", ""));
        ddl.DataSource = dt;
        ddl.DataTextField = dt.Columns[dataTextField].ToString();
        ddl.DataValueField = dt.Columns[dataValueField].ToString();
        ddl.DataBind();
        if (dt.Rows.Count > 0)
        {
            ddl.Items.Insert(0, new ListItem("---Select---", string.Empty));
        }
        else
        {
            ddl.Items.Insert(0, new ListItem("---No Student Found---", string.Empty));
        }
        ddl.SelectedIndex = 0;
    }

    public static void DropdownByDiCondition(DropDownList ddl, string table, string Column, string Column1, string Column2, string whereBy, string whereBy1, string whereBy2, int dataTextField, int dataValueField)
    {
        DataTable dt = new dalCommon().DropdownByConditionWithThree(table, Column, Column1, Column2, whereBy, whereBy1, whereBy2);

        if (dt.Rows.Count > 0)
        {
            dt = dt.Rows.OfType<DataRow>()
                .OrderBy(o =>
                    (string.Join("", Regex.Matches(o["EmployeeId"].ToString(), "\\d+")
                    .Cast<Match>()
                    .Select(m => m.Value))))
                 .CopyToDataTable();
        }
        ddl.Items.Add(new ListItem("", ""));
        ddl.DataSource = dt;
        ddl.DataTextField = dt.Columns[dataTextField].ToString();
        ddl.DataValueField = dt.Columns[dataValueField].ToString();
        ddl.DataBind();
        if (dt.Rows.Count > 0)
        {
            ddl.Items.Insert(0, new ListItem("---Select---", string.Empty));
        }
        else
        {
            ddl.Items.Insert(0, new ListItem("---No List Found---", string.Empty));
        }
        ddl.SelectedIndex = 0;
    }


    public static int GetGenderByRegNo(string p)
    {
        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"select st_Person.GenderId from ss_Student
                inner join st_Person on ss_Student.PersonId=st_Person.Id
                where ss_Student.RegNo=@Reg", conn);
            cmd.Parameters.Add("@Reg", p);
            var rdr = cmd.ExecuteReader();
            var gId = 0;
            while (rdr.Read())
            {
                gId = rdr.GetInt32(0);
                break;
            }
            rdr.Close();
            conn.Close();
            return gId;
        }

    }

    public static void CanAccess(Page page)
    {
        if (SessionInfo != null)
        {
            var path = page.Request.Url.AbsolutePath;
            using (var conn = DatabaseManager.Connection)
            {
                conn.Open();
                var cmd = new SqlCommand("select sa_TaskManager.id from sa_TaskManager where sa_TaskManager.url like Concat('%',@pUrl,'%')", conn);
                cmd.Parameters.AddWithValue("@pUrl", path);
                using (var rdr = cmd.ExecuteReader())
                {
                    if (!rdr.HasRows) return;
                }

                var cmd1 = new SqlCommand(@"select RoleName,id from sa_Role", conn);
                var roleid = 0;
                using (var rdr = cmd1.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        if (page.User.IsInRole(rdr.GetString(0)))
                        {
                            roleid = rdr.GetInt32(1);
                            break;
                        }
                    }
                }
                var cmd2 = new SqlCommand(@"select sa_TaskManager.id
                    from sa_TaskManager
                    INNER JOIN sa_RoleToTask on sa_RoleToTask.TaskId = sa_TaskManager.id
                    where sa_TaskManager.url like  Concat('%',@pUrl,'%')
	                    and sa_RoleToTask.RoleId = @pRole", conn);
                cmd2.Parameters.AddWithValue("@pUrl", path);
                cmd2.Parameters.AddWithValue("@pRole", roleid);
                using (var rdr = cmd2.ExecuteReader())
                {
                    if (!rdr.HasRows)
                    {
                        page.Response.Redirect("/Login.aspx");
                    }
                }



                conn.Close();
            }
        }
        else
        {
            page.Response.Redirect("/Login.aspx");
        }
    }

    public static string GetPaymentSlipNo(bool update = false)
    {
        using (var conn = DatabaseManager.Connection)
        {
            int q = conn.QueryFirstOrDefault("select SlipNo from pmtSlipNo").SlipNo;
            if (update)
                conn.Execute("update pmtSlipNo set SlipNo = @nps", new { nps = q + 1 });
            return q.ToString().PadLeft(6, '0');
        }
    }
internal static List<fee_PaymentType> GetPaymentTypes(SqlConnection conn)
    {
        return conn.Query<fee_PaymentType>("select * from fee_PaymentType").ToList();
    }
}
