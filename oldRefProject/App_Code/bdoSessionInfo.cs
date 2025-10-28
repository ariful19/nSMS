using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
    /// Summary description for SessionInfo
    /// </summary>
    public class bdoSessionInfo
    {
        string _ColorTheme;
        string _DateFormat;
        string _TimeFormat;
        string _TimeZone;
        string _Button;
        string _Panel;
        int _StudentToClassId;
        int _YearId;
        int _StudentId;
        int _MediumId;
        int _CampusId;
        int _ClassId;
        int _GroupId;
        int _ShiftId;
        int _SectionId;
        int _TeacherId;
        
        public bdoSessionInfo()
        {
        }

        #region Common Members

        public string SessionID
        {
            get { return HttpContext.Current.Session.SessionID; }
        }
        public int TimeOut
        {
            get
            {
                try
                {
                    return HttpContext.Current.Session.Timeout;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session.Timeout = value;
            }
        }
        #endregion

        #region GeneralConfiguration

        public string ColorTheme
        {
            get
            {
                return _ColorTheme;
            }
            set
            {
                _ColorTheme = value;
            }
        }

        public string DateFormat
        {
            get
            {
                return _DateFormat;
            }
            set
            {
                _DateFormat = value;
            }
        }
        public string Button
        {
            get
            {
                return _Button;
            }
            set
            {
                _Button = value;
            }
        }
        public string Panel
        {
            get
            {
                return _Panel;
            }
            set
            {
                _Panel = value;
            }
        }
        public string TimeZone
        {
            get
            {
                return _TimeZone;
            }
            set
            {
                _TimeZone = value;
            }
        }
        #endregion GeneralConfiguration

        #region Class Information
        public int StudentToClassId
        {
            get
            {
                return _StudentToClassId;
            }
            set
            {
                _StudentToClassId = value;
            }
        }

    public int YearId
    {
        get
        {
            return _YearId;
        }
        set
        {
            _YearId = value;
        }
    }
        public int CampusId
        {
            get
            {
                return _CampusId;
            }
            set
            {
                _CampusId = value;
            }
        }
        public int MediumId
        {
            get
            {
                return _MediumId;
            }
            set
            {
                _MediumId = value;
            }
        }
        public int StudentId
        {
            get
            {
                return _StudentId;
            }
            set
            {
                _StudentId = value;
            }
        }
        public int ClassId
        {
            get
            {
                return _ClassId;
            }
            set
            {
                _ClassId = value;
            }
        }
        public int GroupId
        {
            get
            {
                return _GroupId;
            }
            set
            {
                _GroupId = value;
            }
        }
        public int ShiftId
        {
            get
            {
                return _ShiftId;
            }
            set
            {
                _ShiftId = value;
            }
        }
        public int SectionId
        {
            get
            {
                return _SectionId;
            }
            set
            {
                _SectionId = value;
            }
        }

    public int TeacherId
    {
        get { return _TeacherId; }
        set { _TeacherId = value; }
    }
        #endregion
    }
