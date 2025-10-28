using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxControlToolkit;
using Nano.DataAccessLayer;
using System.Data;

/// <summary>
/// Summary description for dalStudent
/// </summary>
public class dalStudent
{
    DatabaseManager dm = new DatabaseManager();
    public dalStudent()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int StudentToClass(int studentId,string year,int mediumId, int classId, int groupId, int shiftId, int sectionId, int campusId, int rollNo)
    {
        dm.AddParameteres("@StudentId", studentId);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        dm.AddParameteres("@CampusId", campusId);
        dm.AddParameteres("@RollNo", rollNo);
        DataTable dt = dm.ExecuteQuery("USP_StudentToClass_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public int StudentToClassXml(DataTable dt, string year, int classId, int groupId, int shiftId, int sectionId)
    {
        DataSet ds = new DataSet("dsAssign");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@ShiftId", shiftId);
        dm.AddParameteres("@SectionId", sectionId);
        return dm.ExecuteNonQuery("USP_StudentToClass_InsertXML");
    }

 public int Insert(int personId, string regNo, DateTime addmissionDate, string addmissionYear, string createdBy, DateTime createdDate, bool isActive,int statusId,int adaborId,int zigatolaId,int collegeScienceId,int collegeBusinessId, int eMediumAdaborId, int eMediumZigatolaId)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@RegNo", regNo);
        dm.AddParameteres("@AddmissionDate", addmissionDate);
        dm.AddParameteres("@AddmissionYear", addmissionYear);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@CreatedDate", createdDate);
        dm.AddParameteres("@IsActive", isActive);
        dm.AddParameteres("@StatusId", statusId);
        dm.AddParameteres("@AdaborId", adaborId);
        dm.AddParameteres("@ZigatolaId", zigatolaId);
        dm.AddParameteres("@CollegeScience", collegeScienceId);
        dm.AddParameteres("@CollegeBusiness", collegeBusinessId);
        dm.AddParameteres("@EMediumAdaborId", eMediumAdaborId);
        dm.AddParameteres("@EMediumZigatolaId", eMediumZigatolaId);
        DataTable dt = dm.ExecuteQuery("USP_Student_Insert");
        return Convert.ToInt32(dt.Rows[0][0]);
    }

    public DataTable GetByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetByCriteria");
    }
    public DataTable GetStudentByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetStudentByCriteria");
    }
    public DataTable GetByStudentCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_StudentInfo_GetByCriteria");
    }

    public DataTable GetUnassignStudent(int year)
    {
        dm.AddParameteres("@Year", year);
        return dm.ExecuteQuery("USP_Student_UnassignStudent");
    }
    public DataTable GetStudentInformation(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetInformationByCriteria");
    }

    public DataTable GetStudentSubjectWise(int classId, int groupId, int subjectId)
    {
        dm.AddParameteres("@ClassId", classId);
        dm.AddParameteres("@GroupId", groupId);
        dm.AddParameteres("@SubjectId", subjectId);
        return dm.ExecuteQuery("USP_Student_GetStudentSubjectWise");
    }

    public int AttendenceInsert(DataTable dt, string year, string month, string createdBy, DateTime date)
    {
        DataSet ds = new DataSet("dsAttendence");
        ds.Tables.Add(dt);
        string xml = ds.GetXml();
        dm.AddParameteres("@XML", xml);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@Month", month);
        dm.AddParameteres("@CreatedBy", createdBy);
        dm.AddParameteres("@Date", date);
        return dm.ExecuteNonQuery("USP_Student_AttendenceInsertXML");
    }

    public DataTable GetByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        return dm.ExecuteQuery("USP_Student_GetByPersonId");
    }

    public DataTable GetStudentForAllClass(int year)
    {
        dm.AddParameteres("@Year", year);
        return dm.ExecuteQuery("USP_Student_GetByAllClass");
    }

    public DataTable GetStudentForMarksEntry(string criteria, int subjectId)
    {
        dm.AddParameteres("@Criteria", criteria);
        dm.AddParameteres("@SubjectId", subjectId);
        return dm.ExecuteQuery("USP_Student_GetWhoseMarksNotEntry");
    }
    public DataTable GetStudentForMarksEdit(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetForMarksEdit");
    }
    public DataTable GetStudentForDailyMarksEdit(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetForDailyMarksEdit");
    }
    public DataTable ClassInfoByUserName(string userName)
    {
        dm.AddParameteres("@UserName", userName);
        return dm.ExecuteQuery("USP_Student_ClassInfoByUserName");
    }
    public int StudentBasicInformationInsert(string nameEn, string nameBd, int gender, int religionId, DateTime dob, string birthCertificate, string phone
        , string mobile, string phnHome, string mobileHome, int bloodGroup, string userName)
    {
        dm.AddParameteres("@NameEng", nameEn);
        dm.AddParameteres("@NameBan", nameBd);
        dm.AddParameteres("@GenderId", gender);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@BirthCertificate", birthCertificate);
        dm.AddParameteres("@PhoneNo", phone);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@PhoneHome", phnHome);
        dm.AddParameteres("@MobileHome", mobileHome);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        dm.AddParameteres("@UserName", userName);
        DataTable dt = dm.ExecuteQuery("USP_Student_BasicInformationInsert");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public DataTable GetExistingStudentInDaliyMarksEntry(string year, int subjectID, string rolls, int type, string date)
    {
        string sql = "select * from rs_ObtainDailyMarks where YearId='" + year + "' and SubjectID=" + subjectID + " and StudentToClassID  In (" + rolls + ") and ExamTypeID  In (" + type + ") and ExamDate>='" + date + "' and ExamDate<='" + date + "'";
        return dm.NewExecuteQuery(sql);
        //dm.AddParameteres("@YearId", year);
        //dm.AddParameteres("@SubjectID", subjectID);
        //dm.AddParameteres("@StudentToClassID", rolls);
        //dm.AddParameteres("@ExamTypeID", type);
        //dm.AddParameteres("@ExamDate", date);
        //return dm.ExecuteQuery("USP_GetExistingStudentInDaliyMarksEntry");
    }

    public DataTable GetExistingStudentInMarksEntry(string year, int subjectID, string rolls, int type)
    {
        string sql = "select * from rs_ObtainMarks where YearId='" + year + "' and SubjectID=" + subjectID + " and StudentToClassID  In (" + rolls + ") and ExamTypeID  In (" + type + ")";
        return dm.NewExecuteQuery(sql);
    }
    //public DataTable InsertStudentToClassInformation(List<StudentToClass> lstStudentToClass)
    //{
    //    DataTable dt = new DataTable();
    //    foreach (StudentToClass studentToClass in lstStudentToClass)
    //    {
    //        dm.AddParameteres("@StudentId", studentToClass.StudentId);
    //        dm.AddParameteres("@MediumId", studentToClass.MediumId);
    //        dm.AddParameteres("@Year", studentToClass.Year);
    //        dm.AddParameteres("@ClassId", studentToClass.ClassId);
    //        dm.AddParameteres("@CampusId", studentToClass.CampusId);
    //        dm.AddParameteres("@GroupId", studentToClass.GroupId);
    //        dm.AddParameteres("@ShiftId", studentToClass.ShiftId);
    //        dm.AddParameteres("@SectionId", studentToClass.SectionId);
    //        dm.AddParameteres("@RollNo", studentToClass.RollNo);
    //        dm.AddParameteres("@CreatedBy", studentToClass.CreatedBy);
    //        dt = dm.ExecuteQuery("USP_InsertStudentToClass");
    //    }

    //    return dt;

    //}

    public DataTable InsertStudentToClassInformation(List<StudentToClass> lstStudentToClass, bool isPromotion)
    {
        DataTable dt = new DataTable();
        foreach (StudentToClass studentToClass in lstStudentToClass)
        {
            dm.AddParameteres("@StudentId", studentToClass.StudentId);
            dm.AddParameteres("@MediumId", studentToClass.MediumId);
            dm.AddParameteres("@Year", studentToClass.Year);
            dm.AddParameteres("@ClassId", studentToClass.ClassId);
            dm.AddParameteres("@CampusId", studentToClass.CampusId);
            dm.AddParameteres("@GroupId", studentToClass.GroupId);
            dm.AddParameteres("@ShiftId", studentToClass.ShiftId);
            dm.AddParameteres("@SectionId", studentToClass.SectionId);
            dm.AddParameteres("@RollNo", studentToClass.RollNo);
            dm.AddParameteres("@IsPromoted", false);
            dm.AddParameteres("@CreatedBy", studentToClass.CreatedBy);
            dt = dm.ExecuteQuery("USP_InsertStudentToClass");
            if (Convert.ToInt32(dt.Rows[0][0]) > 0 && isPromotion)
            {
                string sql = "update er_StudentToClass set IsPromoted = 1 Where Id=" + studentToClass.StudentToClassID + " and StudentId=" + studentToClass.StudentId;
                dm.NewExecuteQuery(sql);
            }
        }

        return dt;

    }

   


    public DataTable GetStudentToClassByRollNo(string year,int mediumId, int campusID, int classID, int gropID, int shiftID, int sectionID, int rollNo)
    {
        string sql = "Select * from er_StudentToClass where Year='" + year + "' and MediumId="+mediumId+" and ClassID=" + classID + " and CampusId=" + campusID + " and GroupID=" + gropID + " and ShiftID=" + shiftID + " and SectionID=" + sectionID + " and RollNo=" + rollNo;
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetStudentToClassByStudentID(int studentID, int classID)
    {
        string sql = @"Select * from dbo.er_StudentToClass where StudentID=" + studentID + " And ClassID=" + classID;
        return dm.NewExecuteQuery(sql);
    }
    public DataTable GetStudentAttendenceIsPresent(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Report_GetStudentAttendence");
    }
    public DataTable GetAttendenceByCriteria(string criteria, int studentToClassId)
    {
        dm.AddParameteres("@Criteria", criteria);

        if (studentToClassId > 0)
        {
            dm.AddParameteres("@StudentToClassId", studentToClassId);
            return dm.ExecuteQuery("USP_Student_GetAttendenceByCriteria");
        }
        else
        {
            return dm.ExecuteQuery("USP_Show_GetStudentAttendence");
        }

    }
    public DataTable GetStudentForPromotion(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Student_GetByCriteria");
        //return dm.ExecuteQuery("USP_Student_GetMarksForPromotion");
        //// USP_Student_GetForMarksPromotion
    }

    public int UnassignStudent(int id)
    {
        dm.AddParameteres("@id", id);
        return dm.ExecuteNonQuery("USP_Student_AssignStudent_Delete");
    }
    public DataTable GetStudentDuepayment(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Get_Student_DuePement");
    }


    public DataTable GetAllStudents()
    {
        return dm.ExecuteQuery("USP_GetAllStudents");
    }

  public DataTable GetAllStudentByCriteria(int yearId, int campusId)
    {
        dm.AddParameteres("@YearId", yearId);
        dm.AddParameteres("@CampusId", campusId);
        return dm.ExecuteQuery("USP_GetAllStudentsByYearCampus");
    }

    public DataTable GetDailyMarksStuByCriteria(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_Marks_GetDailyMarksStuByCriteria");
    }

    public int InsertStudentAdmission(StudentAdmission student)
    {
        //  dm.AddParameteres("@SerialNo", student.SerialNo);
        dm.AddParameteres("@SurName", student.SurName);
        dm.AddParameteres("@FirstName", student.FirstName);
        dm.AddParameteres("@NameBanlga", student.NameBangla);
        dm.AddParameteres("@FatherNameEng", student.FatherNameEng);
        dm.AddParameteres("@FatherNameBan", student.FatherNameBan);
        dm.AddParameteres("@MotherNameEng", student.MotherNameEng);
        dm.AddParameteres("@MotherNameBan", student.MotherNameBan);
        dm.AddParameteres("@GenderId", student.GenderId);
        dm.AddParameteres("@ReligionId", student.ReligionId);
        dm.AddParameteres("@Nationality", student.Nationality);
        dm.AddParameteres("@DateofBirth", student.DateofBirth);
        dm.AddParameteres("@BirthCertificate", student.BirthCertificate);
        dm.AddParameteres("@PhoneNo", student.PhoneNo);
        dm.AddParameteres("@Mobile", student.Mobile);
        dm.AddParameteres("@PhoneHome", student.PhoneHome);
        dm.AddParameteres("@MobileHome", student.MobileHome);
        dm.AddParameteres("@Email", student.Email);
        dm.AddParameteres("@Fax", student.Fax);
        dm.AddParameteres("@BloodGroup", student.BloodGroup);
        dm.AddParameteres("@FatherNId", student.FatherNId);
        dm.AddParameteres("@MotherNId", student.MotherNId);
        dm.AddParameteres("@IsFreedomFighter", student.IsFreedomFighter);
        dm.AddParameteres("@IsTribal", student.IsTribal);
        dm.AddParameteres("@IsPhysicallyDefect", student.IsPhysicallyDefect);
        dm.AddParameteres("@FatherIncome", student.FatherIncome);
        dm.AddParameteres("@MotherIncome", student.MotherIncome);
        dm.AddParameteres("@FatherPhone", student.FatherPhone);
        dm.AddParameteres("@MotherPhone", student.MotherPhone);
        dm.AddParameteres("@FatherImage", student.FatherImage);
        dm.AddParameteres("@MotherImage", student.MotherImage);
        dm.AddParameteres("@SignatureImage", student.SignatureImage);
        dm.AddParameteres("@FatherQualificationId", student.FatherQualificationId);
        dm.AddParameteres("@FatherProfessionId", student.FatherProfessionId);
        dm.AddParameteres("@MotherQualificationId", student.MotherQualificationId);
        dm.AddParameteres("@MotherProfessionId", student.MotherProfessionId);
        dm.AddParameteres("@PersonImage", student.PersonImage);
        dm.AddParameteres("@PresentDivisionId", student.PresentDivisionId);
        dm.AddParameteres("@PresentDistrictId", student.PresentDistrictId);
        dm.AddParameteres("@PresentThanaId", student.PresentThanaId);
        dm.AddParameteres("@PresentPostOffice", student.PresentPostOffice);
        dm.AddParameteres("@PresentPostalCode", student.PresentPostalCode);
        dm.AddParameteres("@PresentAddress", student.PresentAddress);
        dm.AddParameteres("@DivisionId", student.DivisionId);
        dm.AddParameteres("@DistrictId", student.DistrictId);
        dm.AddParameteres("@ThanaId", student.ThanaId);
        dm.AddParameteres("@PostOffice", student.PostOffice);
        dm.AddParameteres("@PostalCode", student.PostalCode);
        dm.AddParameteres("@Address", student.Address);
        dm.AddParameteres("@AvoidGames", student.AvoidGames);
        dm.AddParameteres("@AvoidGamesDetails", student.AvoidGamesDetails);
        dm.AddParameteres("@ClassApplyingFor", student.ClassApplyingFor);
        dm.AddParameteres("@SchoolVersion", student.Version);
        dm.AddParameteres("@AdmissionYear", student.AdmissionYear);
        dm.AddParameteres("@Remarks", student.Remarks);
        dm.AddParameteres("@IsPayment", student.IsPayment);

        DataTable dt = dm.ExecuteQuery("USP_Student_Admission_Insert");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }

    public int InsertSibling(int studentAdmissionID, string name, string gender, string age, string institutename, string profession, string remarks)
    {
        int id = 0;
        dm.AddParameteres("@StudentAdmissionID", studentAdmissionID);
        dm.AddParameteres("@Name", name);
        dm.AddParameteres("@Gender", gender);
        dm.AddParameteres("@Age", age);
        dm.AddParameteres("@InstituteName", institutename);
        dm.AddParameteres("@Profession", profession);
        dm.AddParameteres("@Remarks", remarks);
        DataTable dt = dm.ExecuteQuery("USP_StudentSiblingsInfo_Insert");
        if (dt.Rows.Count > 0)
        {
            id = Convert.ToInt32(dt.Rows[0][0]);
        }
        return id;
    }

    public int InsertReference(int studentAdmissionID, string referenceName, string refNationality, string refnationalId, string refRelation, string refQualification, string refDesignation, string refProfession, string refMobileNo, string refEmail, string refYearlyIncome, string refAddress)
    {
        int id = 0;
        dm.AddParameteres("@StudentAdmissionID", studentAdmissionID);
        dm.AddParameteres("@ReferenceName", referenceName);
        dm.AddParameteres("@RefNationality", refNationality);
        dm.AddParameteres("@RefNationalID", refnationalId);
        dm.AddParameteres("@RefRelation", refRelation);
        dm.AddParameteres("@RefQualification", refQualification);
        dm.AddParameteres("@RefDesignation", refDesignation);
        dm.AddParameteres("@RefProfession", refProfession);
        dm.AddParameteres("@RefMobileNo", refMobileNo);
        dm.AddParameteres("@RefEmail", refEmail);
        dm.AddParameteres("@RefYearlyIncome", refYearlyIncome);
        dm.AddParameteres("@RefResidenceAddress", refAddress);
        DataTable dt = dm.ExecuteQuery("USP_StudentReferences_Insert");
        if (dt.Rows.Count > 0)
        {
            id = Convert.ToInt32(dt.Rows[0][0]);
        }
        return id;
    }

    public int InsertSchoolFeedback(int studentAdmissionID, string feedback, string chosenCause, string expectation, string isKnown, string knownName, string knownDesignation)
    {
        int id = 0;
        dm.AddParameteres("@StudentAdmissionID", studentAdmissionID);
        dm.AddParameteres("@FeedBack", feedback);
        dm.AddParameteres("@ChosenCauseOfSc", chosenCause);
        dm.AddParameteres("@ExpectationFromSc", expectation);
        dm.AddParameteres("@IsKnownAnyone", isKnown);
        dm.AddParameteres("@KnownName", knownName);
        dm.AddParameteres("@KnowDesignation", knownDesignation);
        DataTable dt = dm.ExecuteQuery("USP_SchoolFeedback_Insert");
        if (dt.Rows.Count > 0)
        {
            id = Convert.ToInt32(dt.Rows[0][0]);
        }
        return id;
    }

    public int InsertPreviousStudy(int studentAdmissionID, string preScName, string preAdress, string preTelephone, string email, string fax, string lastGrade, string preAnnualResult)
    {
        int id = 0;
        dm.AddParameteres("@StudentAdmissionID", studentAdmissionID);
        dm.AddParameteres("@PreSchoolName", preScName);
        dm.AddParameteres("@PreAddress", preAdress);
        dm.AddParameteres("@PreTelephone", preTelephone);
        dm.AddParameteres("@Email", email);
        dm.AddParameteres("@Fax", fax);
        dm.AddParameteres("@LastGradeOrClass", lastGrade);
        dm.AddParameteres("@PreAnnualResult", preAnnualResult);
        DataTable dt = dm.ExecuteQuery("USP_PreviousSchoolInformation_Insert");
        if (dt.Rows.Count > 0)
        {
            id = Convert.ToInt32(dt.Rows[0][0]);
        }
        return id;
    }

    public int InsertHealth(int studentAdmissionID, string height, string weight, DateTime healthDate, string physicalDrawback, string drawbackDetails, string allergyProblem, string avoidForAllergy, string MedicineGroup)
    {
        int id = 0;
        dm.AddParameteres("@StudentAdmissionID", studentAdmissionID);
        dm.AddParameteres("@Height", height);
        dm.AddParameteres("@Weight", weight);
        dm.AddParameteres("@HealthDate", healthDate);
        dm.AddParameteres("@PhysicalDrawBack", physicalDrawback);
        dm.AddParameteres("@DrawBackDetails", drawbackDetails);
        dm.AddParameteres("@AllergyProblem", allergyProblem);
        dm.AddParameteres("@AvoidForAllergy", avoidForAllergy);
        dm.AddParameteres("@MedicinesGropName", MedicineGroup);
        DataTable dt = dm.ExecuteQuery("USP_HealthInformationOfStudent_Insert");
        if (dt.Rows.Count > 0)
        {
            id = Convert.ToInt32(dt.Rows[0][0]);
        }
        return id;
    }
    public DataTable GetAdmissionStuInfo(string table, string criteria)
    {
        dm.AddParameteres("@Table", table);
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_GetByCriteria");
    }
    public DataTable GetStuByAdmisssionId(int admissionId)
    {
        string sql = "Select * from tblStudentAdmission Where StudentAdmissionID='" + admissionId + "'";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable UpdateAdmisssionIsPayment(int admissionId)
    {
        string sql = "update tblStudentAdmission set IsPayment = 1 Where StudentAdmissionID='" + admissionId + "'";
        return dm.NewExecuteQuery(sql);
    }

    public DataTable GetBanglaMediumById(int mId)
    {
        string sql = "Select * from bs_Medium Where Id = 1";
        return dm.NewExecuteQuery(sql);
    }

    public int DeleteStudentById(int ID, string updateBy, DateTime updateDate, bool isActive)
    {
        dm.AddParameteres("@PersonId", ID);
        dm.AddParameteres("@UpdatedBy", updateBy);
        dm.AddParameteres("@UpdatedDate", updateDate);
        dm.AddParameteres("@IsActive", isActive);
        return dm.ExecuteNonQuery("USP_Student_DeleteByPersonId");
    }

    public DataTable GetAllAdmittedStudentInfo(string criteria)
    {
        dm.AddParameteres("@Criteria", criteria);
        return dm.ExecuteQuery("USP_GetAllAdmittedStudentInfoByCriteria");
    }

                                 
}



