using Dapper;
using Nano.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Data;

public class SMController : ApiController
{
    [HttpGet]
    [Route("api/SM/Test")]
    public string Test()
    {
        return "Done";
    }
    [HttpPost]
    [Route("api/SM/StudentSearch")]
    public JsonResult<object> StudentSearch(StudentSearchModel model)
    {
        using (var conn = DatabaseManager.Connection)
        {
            var criteria = new List<string>();
            var dynParam = new DynamicParameters();
            if (!string.IsNullOrEmpty(model.StudentName))
            {
                criteria.Add("(sp.NameEng like '%'+@StudentName+'%' or sp.NameBan like '%'+@StudentName+'%')");
                dynParam.Add("StudentName", model.StudentName);
            }
            if (!string.IsNullOrEmpty(model.Year))
            {
                criteria.Add("estc.[Year] =@Year");
                dynParam.Add("Year", model.Year);
            }
            if (!string.IsNullOrEmpty(model.Medium))
            {
                criteria.Add("estc.MediumId =@MediumId");
                dynParam.Add("MediumId", model.Medium);
            }
            if (!string.IsNullOrEmpty(model.Group))
            {
                criteria.Add("estc.GroupId =@Group");
                dynParam.Add("Group", model.Group);
            }
            if (!string.IsNullOrEmpty(model.Class))
            {
                criteria.Add("estc.ClassId =@ClassId");
                dynParam.Add("ClassId", model.Class);
            }
            if (!string.IsNullOrEmpty(model.Campus))
            {
                criteria.Add("estc.CampusId =@CampusId");
                dynParam.Add("CampusId", model.Campus);
            }
            if (!string.IsNullOrEmpty(model.RegNo))
            {
                criteria.Add("ss.RegNo like '%'+@RegNo+'%'");
                dynParam.Add("RegNo", model.RegNo);
            }
            var blank = new int[0];
            if (!criteria.Any()) return Json<object>(blank);
            var res = conn.Query(@"select " + (model.Limit > 0 ? "top " + model.Limit : "") + @"
	                    sp.*,
	                    br.Religion,
	                    ss.RegNo,
	                    ss.AdmissionDate,
	                    ss.AdmissionYear,
	                    bm.MediumName,
	                    bcn.ClassName,
	                    bg.GroupName,
	                    bs.Shift ,
	                    bs2.[Section],
	                    bcn2.CampusName ,
	                    by2.[Year]
                    from
	                    er_StudentToClass estc
                    inner join ss_Student ss on
	                    estc.StudentId = ss.Id
                    INNER join bs_Medium bm on
	                    bm.Id = estc.MediumId
                    inner join bs_ClassName bcn on
	                    bcn.Id = estc.ClassId
                    inner join bs_Group bg on
	                    bg.Id = estc.GroupId
                    inner join bs_Shift bs on
	                    bs.Id = estc.ShiftId
                    inner join bs_Section bs2 on
	                    bs2.Id = estc.SectionId
                    inner join bs_CampusName bcn2 on
	                    bcn2.Id = estc.CampusId
                    inner join bs_Year by2 on
	                    by2.Id = estc.[Year]
                    INNER join st_Person sp on
	                    sp.Id = ss.PersonId
                    inner join bs_Religion br on
	                    br.Id = sp.ReligionId where " + string.Join(" and ", criteria), dynParam);
            return Json<object>(res);
        }
    }

[HttpPost]
    [Route("api/SM/StudentDueList")]
    public JsonResult<object> StudentDueList(StudentSearchModel model)
    {
        using (var conn = DatabaseManager.Connection)
        {
            var criteria = new List<string>();
            var dynParam = new DynamicParameters();
            if (!string.IsNullOrEmpty(model.StudentName))
            {
                criteria.Add("(spp.NameEng like '%'+@StudentName+'%' or spp.NameBan like '%'+@StudentName+'%')");
                dynParam.Add("StudentName", model.StudentName);
            }
            if (!string.IsNullOrEmpty(model.Year))
            {
                criteria.Add("estc.[Year] =@Year");
                dynParam.Add("Year", model.Year);
            }
            if (!string.IsNullOrEmpty(model.Medium))
            {
                criteria.Add("estc.MediumId =@MediumId");
                dynParam.Add("MediumId", model.Medium);
            }
            if (!string.IsNullOrEmpty(model.Group))
            {
                criteria.Add("estc.GroupId =@Group");
                dynParam.Add("Group", model.Group);
            }
            if (!string.IsNullOrEmpty(model.Class))
            {
                criteria.Add("estc.ClassId =@ClassId");
                dynParam.Add("ClassId", model.Class);
            }
            if (!string.IsNullOrEmpty(model.Campus))
            {
                criteria.Add("estc.CampusId =@CampusId");
                dynParam.Add("CampusId", model.Campus);
            }
            if (!string.IsNullOrEmpty(model.RegNo))
            {
                criteria.Add("ss.RegNo like '%'+@RegNo+'%'");
                dynParam.Add("RegNo", model.RegNo);
            }
            if (model.FromMonth == model.ToMonth)
            {
                criteria.Add("t.Month=@Month");
                dynParam.Add("Month", model.FromMonth + 1);
            }
            else
            {
                criteria.Add("(t.Month >= @MonthF and t.Month <= @MonthT)");
                dynParam.Add("MonthF", model.FromMonth);
                dynParam.Add("MonthT", model.ToMonth + 1);
            }

            var blank = new int[0];
            if (!criteria.Any()) return Json<object>(blank);
            var sql = @"select
	ss.RegNo,
	bm.MediumName,
	bm.Id MedId,
	bcn.ClassName,
	bg.GroupName,
	bs.Shift ,
	bs.Id  ShiftId ,
	bs2.[Section],
	bs2.Id  SectionId,
	bcn2.CampusName,
	bcn2.Id CmpId,
	by2.[Year] YearYear,
	spp.NameEng,
	t.Amount Payable,
	t.[Month] PMonth,
	t.PaymentTypeId,
	fpt.PaymentType ,
	sp.*
from
	fee_PaymentForStudent t
inner join er_StudentToClass estc on
	t.StudentToClassId = estc.Id
INNER JOIN ss_Student ss on
	ss.Id = estc.StudentId
left outer join ss_Payment sp on
	sp.StudentToClassId = t.StudentToClassId
	and (t.[Month] = sp.[Month]
		or t.[Month] = 0 )
	and t.PaymentTypeId = sp.PaymentTypeId
left outer join fee_PaymentType fpt on
	fpt.Id = t.PaymentTypeId
INNER join st_Person spp on
	spp.Id = ss.PersonId
INNER join bs_Medium bm on
	bm.Id = estc.MediumId
inner join bs_ClassName bcn on
	bcn.Id = estc.ClassId
inner join bs_Group bg on
	bg.Id = estc.GroupId
inner join bs_Shift bs on
	bs.Id = estc.ShiftId
inner join bs_Section bs2 on
	bs2.Id = estc.SectionId
inner join bs_CampusName bcn2 on
	bcn2.Id = estc.CampusId
inner join bs_Year by2 on
	by2.Id = estc.[Year]
where t.Amount>0 and " + string.Join(" and ", criteria) + "  order by t.Month, t.PaymentTypeId";
            var res = conn.Query(sql, dynParam).Select(o => new PmtHistory
            {
                Amount = (double?)o.Amount,
                BillNo = o.BillNo,
                CampusName = o.CampusName,
                CampusId = o.CmpId,
                ClassId = o.ClassId,
                ClassName = o.ClassName,
                Date = o.Date,
                DueAmount = (double?)o.DueAmount,
                GroupName = o.GroupName,
                Id = o.Id,
                MediumName = o.MediumName,
                MediumId = o.MedId,
                Month = o.Month,
                MonthYear = o.MonthYear,
                NameEng = o.NameEng,
                Payable = (double)o.Payable,
                PaymentType = o.PaymentType,
                PaymentTypeId = o.PaymentTypeId,
                PMonth = o.PMonth,
                RegNo = o.RegNo,
                Section = o.Section,
                SectionId = o.SectionId,
                Shift = o.Shift,
                ShiftId = o.ShiftId,
                StudentId = o.StudentId,
                StudentToClassId = o.StudentToClassId,
                TotalGiven = (double?)o.TotalGiven,
                Year = o.Year,
                YearYear = o.YearYear
            }).ToList();
            dalPaymentType objpaymentType = new dalPaymentType();
            DataTable dtMonthly = objpaymentType.GetPaymentTypeByIsDefault(true, true);
            var first = new PmtHistory();
            if (res == null || !res.Any())
            {
                var fd = conn.Query(@"select
	ss.RegNo,
	bm.MediumName,
	bm.Id MedId,
	bcn.ClassName,
	bg.GroupName,
	bs.Shift ,
	bs.Id ShiftId,
	bs2.[Section],
	bs2.Id SectionId,
	bcn2.CampusName,
	bcn2.Id CmpId,
	by2.[Year] YearYear,
	spp.NameEng,
    estc.ClassId
from
	 er_StudentToClass estc 
INNER JOIN ss_Student ss on
	ss.Id = estc.StudentId
INNER join st_Person spp on
	spp.Id = ss.PersonId
INNER join bs_Medium bm on
	bm.Id = estc.MediumId
inner join bs_ClassName bcn on
	bcn.Id = estc.ClassId
inner join bs_Group bg on
	bg.Id = estc.GroupId
inner join bs_Shift bs on
	bs.Id = estc.ShiftId
inner join bs_Section bs2 on
	bs2.Id = estc.SectionId
inner join bs_CampusName bcn2 on
	bcn2.Id = estc.CampusId
inner join bs_Year by2 on
	by2.Id = estc.[Year] where ss.RegNo like '%'+@RegNo+'%' or (spp.NameEng like '%'+@StudentName+'%' or spp.NameBan like '%'+@StudentName+'%') order by estc.[Year] desc", new { model.RegNo, StudentName = (string.IsNullOrEmpty(model.StudentName) ? "none is none" : model.StudentName) }).Select(o => new PmtHistory
                {//RegNo	MediumName	MedId	ClassName	GroupName	Shift	ShiftId	Section	SectionId	CampusName	CmpId	YearYear	NameEng
                    CampusName = o.CampusName,
                    CampusId = o.CmpId,
                    ClassName = o.ClassName,
                    ClassId = o.ClassId,
                    GroupName = o.GroupName,
                    MediumName = o.MediumName,
                    MediumId = o.MedId,
                    NameEng = o.NameEng,
                    RegNo = o.RegNo,
                    Section = o.Section,
                    SectionId = o.SectionId,
                    Shift = o.Shift,
                    ShiftId = o.ShiftId,
                    YearYear = o.YearYear
                });
                if (fd.Any())
                {
                    first = fd.First();
                }
            }
            if ((res != null && res.Any()) || !string.IsNullOrEmpty(first.NameEng))
            {
                first = res.Any() ? res.First() : first;
                for (int i = model.FromMonth; i <= model.ToMonth; i++)
                {
                    foreach (DataRow row in dtMonthly.Rows)
                    {
                        string paymentType = dtMonthly.Rows[0]["PaymentType"].ToString();
                        var paymentTypeId = Convert.ToInt32(row["Id"]);
                        var exist = conn.QueryFirstOrDefault(" Select * from fee_PaymentForStudent where (@PaymentTypeId=-1 or PaymentTypeId in (@PaymentTypeId)) and Month=@MonthId and StudentToClassId = @StudentToClassId", new { MonthId = i, first.StudentToClassId, PaymentTypeId = paymentTypeId });
                        if (exist == null)
                        {
                            var mpl = conn.Query("Select '0' as StudentToClassId, fee_PaymentSetup.*,fee_PaymentType.PaymentType from fee_PaymentSetup        inner join fee_PaymentType on fee_PaymentSetup.PaymentTypeId=fee_PaymentType.Id         where YearId=" + (string.IsNullOrEmpty(first.Year) ? model.Year : first.Year) + " and MediumId=" + first.MediumId + " and CampusId=" + first.CampusId + " and  ClassId=" + first.ClassId + "  and ShiftId=" + first.ShiftId + " and SectionId=" + first.SectionId + " and (@PaymentTypeId=-1 or PaymentTypeId in (@PaymentTypeId))", new { PaymentTypeId = paymentTypeId });
                            foreach (var item in mpl)
                            {
                                var pmth = (PmtHistory)first.Clone();
                                pmth.Amount = (double)item.Amount;
                                pmth.Payable = (double)item.Amount;
                                pmth.TotalGiven = 0;
                                pmth.DueAmount = (double)item.Amount;
                                pmth.Month = new DateTime(2023, i + 1, 1).ToString("MMMM");
                                pmth.PMonth = (i + 1).ToString();
                                pmth.PaymentType = paymentType;
                                pmth.PaymentTypeId = paymentTypeId;
                                res.Add(pmth);
                            }
                        }
                    }
                }


                var grp = res.GroupBy(o => o.RegNo).First().ToList();
                grp.ForEach(o =>
                {
                    int pMonth = 1;
                    var mparsed = int.TryParse(o.PMonth, out pMonth);
                    o.MonthYear = mparsed && pMonth > 0 ? new DateTime(2022, pMonth, 1).ToString("MMMM") + "-" + o.YearYear : "Academic";
                });
                return Json<object>(grp);
            }
            return Json<object>(res);
        }
    }


    private List<string> GetMonthYears(string yearYear)
    {
        var list = new List<string>();
        for (int i = 1; i <= 12; i++)
        {
            var m = new DateTime(1, i, 1).ToString("MMMM") + "-" + yearYear;
            list.Add(m);
        }
        return list;
    }

    private IEnumerable<fee_PaymentSetup> GetSetupList(StudentSearchModel model, SqlConnection conn)
    {
        var criteria = new List<string>();
        var dynParam = new DynamicParameters();
        if (!string.IsNullOrEmpty(model.Year))
        {
            criteria.Add("fps.[YearId]  =@Year");
            dynParam.Add("Year", model.Year);
        }
        if (!string.IsNullOrEmpty(model.Medium))
        {
            criteria.Add("fps.MediumId =@MediumId");
            dynParam.Add("MediumId", model.Medium);
        }
        if (!string.IsNullOrEmpty(model.Group))
        {
            criteria.Add("fps.GroupId =@Group");
            dynParam.Add("Group", model.Group);
        }
        if (!string.IsNullOrEmpty(model.Class))
        {
            criteria.Add("fps.ClassId =@ClassId");
            dynParam.Add("ClassId", model.Class);
        }
        if (!string.IsNullOrEmpty(model.Campus))
        {
            criteria.Add("fps.CampusId =@CampusId");
            dynParam.Add("CampusId", model.Campus);
        }
        if (model.FromMonth == model.ToMonth)
        {
            criteria.Add("fps.StartMonth<=@Month and fps.EndMonth>=@Month");
            dynParam.Add("Month", model.FromMonth);
        }
        else
        {
            criteria.Add("(fps.StartMonth >= @MonthF and fps.EndMonth>=@MonthT)");
            dynParam.Add("MonthF", model.FromMonth);
            dynParam.Add("MonthT", model.ToMonth);
        }
        var sql = "select * from fee_PaymentSetup fps where " + string.Join(" and ", criteria);
        return conn.Query<fee_PaymentSetup>(sql, dynParam);
    }
}
public class StudentSearchModel
{
    public string RegNo { get; set; }
    public string Year { get; set; }
    public string Medium { get; set; }
    public string Campus { get; set; }
    public string Class { get; set; }
    public string StudentName { get; set; }
    public string Group { get; set; }
    public string Shift { get; set; }
    public string Section { get; set; }
    public string ExamType { get; set; }
    public int Limit { get; set; }
    public int FromMonth { get; set; }
    public int ToMonth { get; set; }
}

public class PmtHistory : ICloneable
{
     public string RegNo { get; set; }
    public string MediumName { get; set; }
    public string ClassName { get; set; }
    public string GroupName { get; set; }
    public string Shift { get; set; }
    public string Section { get; set; }
    public string CampusName { get; set; }
    public string YearYear { get; set; }
    public string NameEng { get; set; }
    public double Payable { get; set; }
    public string PMonth { get; set; }
    public int? PaymentTypeId { get; set; }
    public string PaymentType { get; set; }
    public int? Id { get; set; }
    public int? StudentToClassId { get; set; }
    public object StudentId { get; set; }
    public string Year { get; set; }
    public string Month { get; set; }
    public string MonthYear { get; set; }
    public DateTime? Date { get; set; }
    public double? DueAmount { get; set; }
    public double? Amount { get; set; }
    public double? TotalGiven { get; set; }
    public string BillNo { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public object UpdatedBy { get; set; }
    public object UpdatedDate { get; set; }
    public int? ClassId { get; set; }
    public object Remarks { get; set; }
    public int MediumId { get; set; }
    public int CampusId { get; internal set; }
    public int SectionId { get; internal set; }
    public int ShiftId { get; internal set; }

    public object Clone()
    {
        return Newtonsoft.Json.JsonConvert.DeserializeObject<PmtHistory>(Newtonsoft.Json.JsonConvert.SerializeObject(this));
    }
}

public class fee_PaymentType
{
    public int Id { get; set; }
    public string PaymentType { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsMonthly { get; set; }
    public bool IsDefault { get; set; }
}

public class fee_PaymentSetup
{
    public int Id { get; set; }
    public int YearId { get; set; }
    public int MediumId { get; set; }
    public int CampusId { get; set; }
    public int ClassId { get; set; }
    public int GroupId { get; set; }
    public int ShiftId { get; set; }
    public int SectionId { get; set; }
    public int PaymentTypeId { get; set; }
    public decimal Amount { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string UpdateBy { get; set; }
    public DateTime UpdateDate { get; set; }
    public int StartMonth { get; set; }
    public int EndMonth { get; set; }
}