using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace SimpleCM.Tools
{
    class SQLiteHelper
    {
        private static readonly string[] intFileds = new string[] {
            "int_field1",
            "int_field2",
            "int_field3",
            "int_field4",
            "int_field5",
            "int_field6",
            "int_field7",
            "int_field8",
        };
        private static readonly string[] textFields = new string[]
        {
            "text_field1",
            "text_field2",
            "text_field3",
            "text_field4",
            "text_field5",
            "text_field6",
            "text_field7",
            "text_field8",
            "text_field9",
            "text_field10",
        };

        private const string QUERY_TABLE_COUNT = "SELECT COUNT(*) FROM sqlite_master WHERE type = 'table' and name = @name";

        private object sqlLock = new object();
        private string connectionString = string.Empty;
        public string dbFile;

        public SQLiteHelper(string dbPath, string dbName)
        {
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }
            dbFile = dbPath + "\\" + dbName;
            connectionString = "Data Source=" + dbFile;
        }

        public bool IsTableExist(string tableName)
        {
            SQLiteParameter[] para = new SQLiteParameter[] { new SQLiteParameter("@name", tableName) };
            string sql = QUERY_TABLE_COUNT;
            int count = ExecuteScalarToInt(sql, para);
            if (count == 1)
            {
                return true;
            }
            return false;
        }

        public void CreateTable(string tableName)
        {
            bool notExist = false;
            if (!File.Exists(dbFile))
            {
                notExist = true;
            }
            else
            {
                notExist = !IsTableExist(tableName);
            }
            if (notExist)
            {
                string sql = @"CREATE TABLE if not exists " + tableName + "( [id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT";
                foreach (string field in intFileds)
                {
                    sql += ",[" + field + "] INTEGER";
                }
                foreach (string field in textFields)
                {
                    sql += ",[" + field + "] TEXT ";
                }
                sql += ")";
                try
                {
                    Log.D("", sql);
                    ExecuteNonQuery(sql, null);
                }
                catch (Exception e)
                {
                    Log.E("", e.Message);
                }
            }
        }

        public string ReadString(SQLiteDataReader reader, string field)
        {
            string re = "";
            int ind = 0;
            try
            {
                ind = reader.GetOrdinal(field);
                re = reader.GetString(ind);
            }
            catch
            {
            }
            return re;
        }

        public long ReadLong(SQLiteDataReader reader, string field)
        {
            long re = 0;
            int ind = 0;
            try
            {
                ind = reader.GetOrdinal(field);
                re = reader.GetInt64(ind);
            }
            catch
            {
            }
            return re;
        }

        #region sqlite api
        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        lock (sqlLock)
                        {
                            // sqlite don't support concurrent write operation.
                            affectedRows = command.ExecuteNonQuery();
                        }

                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public SQLiteDataReader ExecuteReader(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteScalarToInt(string sql, SQLiteParameter[] parameters)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            int ret = 0;

            try
            {
                connection.Open();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                object x = command.ExecuteScalar();
                ret = Convert.ToInt32(x);
            }
            catch (Exception e)
            {
                Log.E("", e.Message + e.StackTrace);
            }
            finally
            {
                connection.Close();
            }
            return ret;
        }
        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }

        }
        /// <summary> 
        /// 执行一个查询语句，返回查询结果的第一行第一列 
        /// </summary> 
        /// <param name="sql">要执行的查询语句</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, SQLiteParameter[] parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
        /// 查询数据库中的所有数据类型信息 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetSchema()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                return data;
            }
        }
        #endregion sqlite api
    }
}
