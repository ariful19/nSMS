using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano.DataAccessLayer;
using System.Data;

/// <summary>
/// Summary description for dalPerson
/// </summary>
public class dalPerson
{
    DatabaseManager dm = new DatabaseManager();
    public dalPerson()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Insert(Person person)
    {      
        dm.AddParameteres("@NameEng", person.NameEng);
        dm.AddParameteres("@NameBan", person.NameBan);
        dm.AddParameteres("@FatherNameEng", person.FatherNameEng);
        dm.AddParameteres("@FatherNameBan", person.FatherNameBan);
        dm.AddParameteres("@MotherNameEng", person.MotherNameEng);
        dm.AddParameteres("@MotherNameBan", person.MotherNameBan);
        dm.AddParameteres("@GenderId", person.GenderId);
        dm.AddParameteres("@ReligionId", person.ReligionId);
        dm.AddParameteres("@Nationality", person.Nationality);
        dm.AddParameteres("@DateofBirth", person.DateofBirth);
        dm.AddParameteres("@BirthCertificate", person.BirthCertificate);
        dm.AddParameteres("@PhoneNo", person.PhoneNo);
        dm.AddParameteres("@Mobile", person.Mobile);
        dm.AddParameteres("@PhoneHome", person.PhoneHome);
        dm.AddParameteres("@MobileHome", person.MobileHome);
        dm.AddParameteres("@Email", person.Email);
        dm.AddParameteres("@Fax", person.Fax);
        dm.AddParameteres("@BloodGroup", person.BloodGroup);
        dm.AddParameteres("@FatherNId", person.FatherNId);
        dm.AddParameteres("@MotherNId", person.MotherNId);
        dm.AddParameteres("@IsFreedomFighter", person.IsFreedomFighter);
        dm.AddParameteres("@IsTribal", person.IsTribal);
        dm.AddParameteres("@IsPhysicallyDefect", person.IsPhysicallyDefect);
        dm.AddParameteres("@FatherIncome", person.FatherIncome);
        dm.AddParameteres("@MotherIncome", person.MotherIncome);
        dm.AddParameteres("@FatherPhone", person.FatherPhone);
        dm.AddParameteres("@MotherPhone", person.MotherPhone);
        dm.AddParameteres("@FatherImage", person.FatherImage);
        dm.AddParameteres("@MotherImage", person.MotherImage);
        dm.AddParameteres("@FatherQualificationId", person.FatherQualificationId);
        dm.AddParameteres("@FatherProfessionId", person.FatherProfessionId);
        dm.AddParameteres("@MotherQualificationId", person.MotherQualificationId);
        dm.AddParameteres("@MotherProfessionId", person.MotherProfessionId);
        dm.AddParameteres("@PersonImage", person.PersonImage);
        dm.AddParameteres("@UserName", person.UserName);
        dm.AddParameteres("@NameLocalGuardian1", person.NameLocalGuardian1);
        dm.AddParameteres("@NameLocalGuardian2", person.NameLocalGuardian2);
        dm.AddParameteres("@LocalGuardian1Mobile", person.LocalGuardian1Mobile);
        dm.AddParameteres("@LocalGuardian2Mobile", person.LocalGuardian2Mobile);

        DataTable dt = dm.ExecuteQuery("USP_Person_Insert");
        return Convert.ToInt32(dt.Rows[0][0].ToString());
    }
    public int Update(int Id, string nameEn, string nameBd, int gender, int religionId, string nationality, DateTime dob, string birthCertificate, string phone
        , string mobile, string phnHome, string mobileHome, string email, string fax, int bloodGroup)
    {
        dm.AddParameteres("@Id", Id);
        dm.AddParameteres("@NameEng", nameEn);
        dm.AddParameteres("@NameBan", nameBd);
        dm.AddParameteres("@Gender", gender);
        dm.AddParameteres("@ReligionId", religionId);
        dm.AddParameteres("@Nationality", nationality);
        dm.AddParameteres("@DateofBirth", dob);
        dm.AddParameteres("@BirthCertificate", birthCertificate);
        dm.AddParameteres("@PhoneNo", phone);
        dm.AddParameteres("@Mobile", mobile);
        dm.AddParameteres("@PhoneHome", phnHome);
        dm.AddParameteres("@MobileHome", mobileHome);
        dm.AddParameteres("@Email", email);
        dm.AddParameteres("@Fax", fax);
        dm.AddParameteres("@BloodGroup", bloodGroup);
        return dm.ExecuteNonQuery("USP_Person_Update");
    }
    public int AcademicInfoUpdate(int sId, string year, int mediumId, int Campus, int Class, int group, int shift, int section, string NewRoll)
    {
        dm.AddParameteres("@Id", sId);
        dm.AddParameteres("@Year", year);
        dm.AddParameteres("@MediumId", mediumId);
        dm.AddParameteres("@CampusId", Campus);
        dm.AddParameteres("@ClassId", Class);
        dm.AddParameteres("@GroupId", group);
        dm.AddParameteres("@ShiftId", shift);
        dm.AddParameteres("@SectionId", section);
        dm.AddParameteres("@RollNo", NewRoll);


        return dm.ExecuteNonQuery("USP_Student_AcademicInfoUpdate");
    }

    public int UpdateTeacher(int ID, string NId, int desigId, int Campus, int gradeId, int levelId)
    {
        dm.AddParameteres("@Id", ID);
        dm.AddParameteres("@NId", NId);
        dm.AddParameteres("@DesignationId", desigId);
        dm.AddParameteres("@CampusId", Campus);
        dm.AddParameteres("@GradeId", gradeId);
        dm.AddParameteres("@LevelId", levelId);       
        return dm.ExecuteNonQuery("USP_Teacher_Update");
    }

    public int UserInsert(string userName, string email)
    {
        dm.AddParameteres("@UserName", userName);
        dm.AddParameteres("@Email", email);
        return dm.ExecuteNonQuery("USP_User_Insert");
    }
    public int UpdateFatherInfo(int personId, string fatherNameEn, string fatherNameBd, string fatherNID, int fatherIncome, string fatherPhn, int fatherEducation, int fatherProfession)
    {
        dm.AddParameteres("@Id", personId);
        dm.AddParameteres("@FatherNameEng", fatherNameEn);
        dm.AddParameteres("@FatherNameBan", fatherNameBd);
        dm.AddParameteres("@FatherNId", fatherNID);
        dm.AddParameteres("@FatherIncome", fatherIncome);
        dm.AddParameteres("@FatherPhone", fatherPhn);
        dm.AddParameteres("@FatherQualificationId", fatherEducation);
        dm.AddParameteres("@FatherProfessionId", fatherProfession);
        return dm.ExecuteNonQuery("USP_FatherInformation_Update");
    }
    public int UpdateMotherInfo(int personId, string motherNameEn, string motherNameBd, string motherNID, int motherIncome, string motherPhn, int motherEducation, int motherProfession)
    {
        dm.AddParameteres("@Id", personId);
        dm.AddParameteres("@MotherNameEng", motherNameEn);
        dm.AddParameteres("@MotherNameBan", motherNameBd);
        dm.AddParameteres("@MotherNId", motherNID);
        dm.AddParameteres("@MotherIncome", motherIncome);
        dm.AddParameteres("@MotherPhone", motherPhn);
        dm.AddParameteres("@MotherQualificationId", motherEducation);
        dm.AddParameteres("@MotherProfessionId", motherProfession);
        return dm.ExecuteNonQuery("USP_MotherInformation_Update");
    }
    public int UpdateOtherInfo(int personId, bool IsFreeDom, bool isTribal, bool isPhyDef)
    {
        dm.AddParameteres("@Id", personId);
        dm.AddParameteres("@IsFreedomFighter", IsFreeDom);
        dm.AddParameteres("@IsTribal", isTribal);
        dm.AddParameteres("@IsPhysicallyDefect", isPhyDef);
        return dm.ExecuteNonQuery("USP_OtherInfo_Update");
    }

    public int PresentAddressInsert(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PresentAddressInsert");
    }
    public int PresentAddressUpdate(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PresentAddressUpdate");
    }
    public int PermanentAddressInsert(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PermanentAddressInsert");
    }

    public int PermanentAddressUpdate(int PersonId, int divisionId, int districtId, int thanaId, string postOffice, string postCode, string address)
    {
        dm.AddParameteres("@PersonId", PersonId);
        dm.AddParameteres("@DivisionId", divisionId);
        dm.AddParameteres("@DistrictId", districtId);
        dm.AddParameteres("@ThanaId", thanaId);
        dm.AddParameteres("@PostOffice", postOffice);
        dm.AddParameteres("@PostCode", postCode);
        dm.AddParameteres("@Address", address);
        return dm.ExecuteNonQuery("USP_Person_PermanentAddressUpdate");
    }

    public DataTable GetByUserName(string UserName)
    {
        dm.AddParameteres("@UserName", UserName);
        return dm.ExecuteQuery("USP_Person_GetByUserName");

       // return dm.ExecuteQuery("USP_Person_GetByRegNo");
    }
    public DataTable GetByRegNo(string RegNo)
    {
        dm.AddParameteres("@RegNo", RegNo);
        return dm.ExecuteQuery("USP_Student_GetByRegNo");
    }
    public int PersonImageUpdate(int personId, string img)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@Photo", img);
        return dm.ExecuteNonQuery("USP_Person_ImageUpdate");
    }
    public int FatherImageUpdate(int personId, string img)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@Photo", img);
        return dm.ExecuteNonQuery("USP_Person_FatherImageUpdate");
    }
    public int MotherImageUpdate(int personId, string img)
    {
        dm.AddParameteres("@PersonId", personId);
        dm.AddParameteres("@Photo", img);
        return dm.ExecuteNonQuery("USP_Person_MotherImageUpdate");
    }
    public DataTable GetStudentByRoll(int studentRoll)
    {
        dm.AddParameteres("@PersonId", studentRoll);
        return dm.ExecuteQuery("USP_Student_GetByPersonId");
    }
    public DataTable GetByPersonID(int personID)
    {
        dm.AddParameteres("@PersonID", personID);
        return dm.ExecuteQuery("USP_Person_GetByID");
    }
    public DataTable GetByStudentID(int personID)
    {
        dm.AddParameteres("@ID", personID);
        return dm.ExecuteQuery("USP_Student_GetByID");
    }
    public DataTable  GetStudentByPersonId(int personId)
    {
        dm.AddParameteres("@PersonId", personId);
        return dm.ExecuteQuery("USP_Person_GetByPersonId");
    }
    public int OtherInfoUpdate(int PersonId, bool isFreedomFighter, bool isTribal, bool isPhysicallyDefect)
    {
        dm.AddParameteres("@Id", PersonId);
        dm.AddParameteres("@IsFreedomFighter", isFreedomFighter);
        dm.AddParameteres("@IsTribal", isTribal);
        dm.AddParameteres("@IsPhysicallyDefect", isPhysicallyDefect);

        return dm.ExecuteNonQuery("USP_Person_OtherInfoUpdate");
    }

    public int LocalGuardianUpdate(int PersonId, string guardian1, string guardian2, string mobileNo1, string mobileNo2)
    {
        dm.AddParameteres("Id", PersonId);
        dm.AddParameteres("@NameLocalGuardian1", guardian1);
        dm.AddParameteres("@NameLocalGuardian2", guardian2);
        dm.AddParameteres("@LocalGuardian1Mobile", mobileNo1);
        dm.AddParameteres("@LocalGuardian2Mobile", mobileNo2);

        return dm.ExecuteNonQuery("USP_Person_LocalGuardianInfoUpdate");
    }



   public int UpdateStudentShip(int statusId,int studentId)
    {
        dm.AddParameteres("@StatusId", statusId);
        dm.AddParameteres("@StudentId", studentId);
        dm.AddParameteres("@Date", DateTime.Now.Date.ToString());
        return dm.ExecuteNonQuery("USP_Student_UpdateStudentShip");
    }

public int DeletePresentAdd(int PersonId)
    {
        dm.AddParameteres("@PersonId", PersonId);
        return dm.ExecuteNonQuery("USP_PresentAdd_Delete");
    }

    public int DeletePermanentAdd(int PersonId)
    { 
       
       dm.AddParameteres("@PersonId", PersonId);
       return dm.ExecuteNonQuery("USP_PermanentAdd_Delete");
    }

    public int DeleteTeacherByPersonId(int PersonId)
    {
        
       dm.AddParameteres("@PersonId", PersonId);
       return dm.ExecuteNonQuery("USP_Teacher_Delete");
    }



}




