using Nano.DataAccessLayer;
using System;
using System.Web.UI.WebControls;
using Dapper;

public partial class Pages_Fees_PaymentSlip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load();
        }
    }
    protected void Load()
    {
        string pageNumber = Common.GetPaymentSlipNo();
        Session["recpNo"] = pageNumber;

        ddlYear.DataSource = new Common().GetAll("bs_Year");
        ddlYear.DataBind();
        //var item = ddlYear.Items.FindByText(DateTime.Now.Year.ToString());
        //ddlYear.SelectedIndex = ddlYear.Items.IndexOf(item);

        ddlMedium.DataSource = new Common().GetAll("bs_Medium");
        ddlMedium.DataBind();

        ddlCampus.DataSource = new Common().GetAll("bs_CampusName");
        ddlCampus.DataBind();

        ddlClass.DataSource = new Common().GetAll("bs_ClassName");
        ddlClass.DataBind();

        ddlGroup.DataSource = new Common().GetAll("bs_Group");
        ddlGroup.DataBind();

        ddlShift.DataSource = new Common().GetAll("bs_Shift");
        ddlShift.DataBind();

        ddlSection.DataSource = new Common().GetAll("bs_Section");
        ddlSection.DataBind();

        AddBlank();
    }

    private void AddBlank()
    {
        var ddls = new[] { ddlYear, ddlMedium, ddlCampus, ddlClass, ddlGroup, ddlShift, ddlSection };
        foreach (var item in ddls)
        {
            item.Items.Insert(0, new ListItem("--Select--", ""));
            item.SelectedIndex = 0;
        }
    }
    private void loadStudentFeeForThisMonth(string regNo)
    {
        dalPayment objPayment = new dalPayment();
        //GetMonthlyFees(int StudentToClassId, int yearId, int mediumId, int campusId, int classId, int groupId,int shiftId, int sectionId, int monthId, int paymentTypeId)
        using (var conn = DatabaseManager.Connection)
        {
            var stdInfo = conn.QueryFirstOrDefault(@"select
	ss .PersonId,
	sc.id,
	sc.MediumId,
	sc.ClassId ,
	sc.GroupId ,
	sc.ShiftId ,
	sc.SectionId ,
	sc.CampusId ,
	sc.[Year],
	cn.ClassName ,
	bm.MediumName,
	bs.[Section],
	bg.GroupName,
	bs2.Shift,
	bcn.CampusName,
	sp.NameEng
from
	ss_Student ss
inner join er_StudentToClass sc on
	sc.StudentId = ss.Id
inner join bs_ClassName cn on
	sc.ClassId = cn.Id
INNER join bs_Section bs on
	bs.Id = sc.SectionId
inner join bs_Medium bm on
	bm.Id = sc.MediumId
inner join bs_Group bg on
	bg.Id = sc.GroupId
inner join bs_Shift bs2 on
	bs2.Id = sc.ShiftId
inner join bs_CampusName bcn on
	bcn.Id = sc.CampusId
INNER join st_Person sp on
	sp.Id = ss.PersonId
where
	ss.RegNo = @regNo", new { regNo });
          
        }
    }

}

internal class PmtSlipInfo
{
    public string IDNo { get; set; }
    public dynamic StudentName { get; set; }
    public dynamic Group { get; set; }
    public dynamic Version { get; set; }
    public dynamic Class { get; set; }
    public string TutionFeeFor { get; set; }
    public string HostelFeeFor { get; set; }
    public string OtherFeeFor { get; set; }
    public object TutionFee { get; set; }
    public object HostelFee { get; set; }
    public object OtherFee { get; set; }
    public dynamic CampusName { get; internal set; }
    public dynamic Shift { get; internal set; }
    public dynamic Section { get; internal set; }
}