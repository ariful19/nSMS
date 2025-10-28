using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Person
/// </summary>
public class Person
{
    public int Id { get; set; }

    private string _NameEng = string.Empty;
    public string NameEng
    {
        get { return _NameEng; }
        set { _NameEng = value; }
    }

    private string _NameBan = string.Empty;
    public string NameBan
    {
        get { return _NameBan; }
        set { _NameBan = value; }
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
    public string DateofBirth { get; set; }
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
    public string UserName { get; set; }
    public string RegNo { get; set; }
    public int GenderId { get; set; }
    public string BirthCertificate { get; set; }
    public string NameLocalGuardian1 { get; set; }
    public string NameLocalGuardian2 { get; set; }
    public string LocalGuardian1Mobile { get; set; }
    public string LocalGuardian2Mobile { get; set; }

}