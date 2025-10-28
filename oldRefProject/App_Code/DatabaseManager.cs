using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DatabaseManager
/// Created by: Debashish Mondal
/// Create Date: 28th September 2015
/// </summary>
/// 
namespace Nano.DataAccessLayer
{
    public class DatabaseManager : IDisposable
    {
        private String conString;
        private SqlConnection _cn;
        private DataTable _dt;
        private SqlCommand _cmd;
        private SqlDataReader _reader;
        private Dictionary<string, object> ParamList;
        public DatabaseManager()
        {
            conString = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            ParamList = new Dictionary<string, object>();
        }

        public static SqlConnection Connection
        {
            get
            {
                var cs = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;

                return new SqlConnection(cs);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_cn != null)
                {
                    _cn.Dispose();
                    _cn = null;
                }
                if (_dt != null)
                {
                    _dt.Dispose();
                    _dt = null;
                }
                if (_cmd != null)
                {
                    _cmd.Dispose();
                    _cmd = null;
                }
                if (_reader != null)
                {
                    _reader.Dispose();
                    _reader = null;
                }
            }
        }

        ~DatabaseManager()
        {
            Dispose(false);
        }

        private static readonly Lazy<DatabaseManager> VLazy = new Lazy<DatabaseManager>(() => new DatabaseManager());

        public static DatabaseManager Instance
        {
            get { return VLazy.Value; }
        }

        public int ClearParameters()
        {
            if (ParamList != null)
            {
                if (ParamList.Count > 0)
                    ParamList.Clear();
            }
            if (_cmd != null)
            {
                if (_cmd.Parameters.Count > 0)
                    _cmd.Parameters.Clear();
            }
            return 1;
        }

        public void AddParameteres(string param, object value)
        {
            ParamList.Add(param, value);
        }


        public int NewExecuteNonQueryForPerson(string strSql)
        {
            int intResult;
            try
            {

                using (_cn = new SqlConnection(conString))
                {
                    _cn.Open();
                    using (_cmd = new SqlCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        foreach (KeyValuePair<string, object> dc in ParamList)
                        {
                            _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        }

                        intResult = _cmd.ExecuteNonQuery();

                        string sql = "Select Max(ID) ID from dbo.st_Person";
                        _cmd = new SqlCommand(sql);
                        intResult = _cmd.ExecuteNonQuery();


                        return intResult;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                ClearParameters();
            }
        }


        public int ExecuteNonQuery(string strSql)
        {
            int intResult;
            try
            {

                using (_cn = new SqlConnection(conString))
                {
                    _cn.Open();
                    using (_cmd = new SqlCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        foreach (KeyValuePair<string, object> dc in ParamList)
                        {
                            _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        }

                        intResult = _cmd.ExecuteNonQuery();
                        return intResult;
                    }
                }

            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                ClearParameters();
            }

        }

        public DataTable ExecuteQuery(string strSql)
        {
            try
            {
                using (_cn = new SqlConnection(conString))
                {
                    _cn.Open();


                    using (_cmd = new SqlCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.StoredProcedure;
                        foreach (KeyValuePair<string, object> dc in ParamList)
                        {
                            _cmd.Parameters.AddWithValue(dc.Key, dc.Value);
                        }

                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable();
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch { }
                        }
                    }
                }
                return _dt;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                ClearParameters();
            }
        }
        public DataTable ExecuteQueryDt(string strSql)
        {
            try
            {
                using (_cn = new SqlConnection(conString))
                {
                    _cn.Open();

                    using (_cmd = new SqlCommand(strSql, _cn))
                    {
                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable();
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch { }
                        }
                    }
                }
                return _dt;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {
                ClearParameters();
            }
        }
        public DataTable NewExecuteQuery(string strSql)
        {
            try
            {
                using (_cn = new SqlConnection(conString))
                {
                    _cn.Open();


                    using (_cmd = new SqlCommand(strSql, _cn))
                    {
                        _cmd.CommandType = CommandType.Text;

                        using (_reader = _cmd.ExecuteReader())
                        {
                            _dt = new DataTable();
                            try
                            {
                                _dt.Load(_reader);
                            }
                            catch { }
                        }
                    }
                }

                return _dt;
            }
            catch (Exception ex)
            {



                throw (new Exception(ex.Message));
            }
            finally
            {
                _cn.Close();
            }
        }

        public static int ExecuteNoneQuery(SqlCommand cmd)
        {
            using (var conn = Connection)
            {
                conn.Open();
                cmd.Connection = conn;
                var res = cmd.ExecuteNonQuery();
                conn.Close();
                return res;
            }
        }
    }
}