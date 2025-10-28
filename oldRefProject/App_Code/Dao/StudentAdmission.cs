using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StudentAdmission
/// </summary>
public class StudentAdmission
{
    public int StudentAdmissionID { get; set; }
    public int SerialNo { get; set; }

    private string _SurName = string.Empty;
    public string SurName
    {
        get { return _SurName; }
        set { _SurName = value; }
    }

    private string _FirstName = string.Empty;
    public string FirstName
    {
        get { return _FirstName; }
        set { _FirstName = value; }
    }

    private string _NameBangla = string.Empty;
    public string NameBangla
    {
        get { return _NameBangla; }
        set { _NameBangla = value; }
    }
    public string FatherNameEng { get; set; }

    private string _FatherNameBan = string.Empty;
    public string FatherNameBan
    {
        get { return _FatherNameBan; }
        set { _FatherNameBan = value; }
    }
    public string MotherNameEng { get; set; }

    private string _MotherNameBan = string.Empty;
    public string MotherNameBan
    {
        get { return _MotherNameBan; }
        set { _MotherNameBan = value; }
    }
    public int ReligionId { get; set; }

    private string _Nationality = string.Empty;
    public string Nationality
    {
        get { return _Nationality; }
        set { _Nationality = value; }
    }
    //public string DateofBirth { get; set; }
    public DateTime DateofBirth { get; set; }
    public string PhoneNo { get; set; }
    public string Mobile { get; set; }
    public string PhoneHome { get; set; }
    public string MobileHome { get; set; }
    private string _Email = string.Empty;
    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

    private string _Fax = string.Empty;
    public string Fax
    {
        get { return _Fax; }
        set { _Fax = value; }
    }
   
    public string BloodGroup { get; set; }
    public string FatherNId { get; set; }
    public string MotherNId { get; set; }
    public bool IsFreedomFighter { get; set; }
    public bool IsTribal { get; set; }
    public bool IsPhysicallyDefect { get; set; }
    public int FatherIncome { get; set; }
    public int MotherIncome { get; set; }
    public string FatherPhone { get; set; }
    public string MotherPhone { get; set; }
    public string FatherImage { get; set; }
    public string MotherImage { get; set; }
    public int FatherQualificationId { get; set; }
    public int FatherProfessionId { get; set; }
    public int MotherQualificationId { get; set; }
    public int MotherProfessionId { get; set; }
    public string PersonImage { get; set; }
    public string SignatureImage { get; set; }
    public int GenderId { get; set; }
    public string BirthCertificate { get; set; }
    public int PresentDivisionId { get; set; }
    public int PresentDistrictId { get; set; }
    public int PresentThanaId { get; set; }
    public string PresentPostOffice { get; set; }
    public string PresentPostalCode { get; set; }
    public string PresentAddress { get; set; }
    public int DivisionId { get; set; }
    public int DistrictId { get; set; }
    public int ThanaId { get; set; }
    public string PostOffice { get; set; }
    public string PostalCode { get; set; }
    public string Address { get; set; }
    public string AvoidGames { get; set; }
    public string AvoidGamesDetails { get; set; }
    public string Version { get; set; }
    public string ClassApplyingFor { get; set; }
    public string AdmissionYear { get; set; } 
    public string Remarks { get; set; } 
    public bool IsPayment { get; set; }
}