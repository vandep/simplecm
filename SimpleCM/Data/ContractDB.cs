using SimpleCM.Tools;
using System;
using System.Data.SQLite;

namespace SimpleCM.Data
{
    public class ContractDB
    {
        private const string CM_DB_NAME = "cm.db3";
        private const string CM_TABLE_NAME = "cm_list";
        private const string COLUMN_CM_NUMBER = "text_field1";
        private const string COLUMN_CM_NAME = "text_field2";
        private const string COLUMN_CM_DATE = "int_field1";
      
        private const string COLUMN_CM_ADDTIONAL = "text_field3";
        private const string COLUMN_CM_BILL_NOTE = "text_field4";
        private const string COLUMN_PROJECT_CATE = "text_field5";
        private const string COLUMN_BASE_INFO = "text_field6";
        private const string SELECT_ALL_CONTRACTS = "select * from " + CM_TABLE_NAME + " order by " + COLUMN_CM_DATE + " desc";
        private const string DELETE_CONTRACT = "delete from " + CM_TABLE_NAME + " where " + COLUMN_CM_NUMBER + "=@" + COLUMN_CM_NUMBER;
        private const string UPDATE_CONTRACT = "UPDATE cm_list set "
                                                + COLUMN_CM_NUMBER + "=@" + COLUMN_CM_NUMBER + ","
                                                + COLUMN_CM_NAME + "=@" + COLUMN_CM_NAME + ","
                                                + COLUMN_CM_DATE + "=@" + COLUMN_CM_DATE + ","
                                                + COLUMN_CM_ADDTIONAL + "=@" + COLUMN_CM_ADDTIONAL + ","
                                                + COLUMN_CM_BILL_NOTE + "=@" + COLUMN_CM_BILL_NOTE + ","
                                                + COLUMN_PROJECT_CATE + "=@" + COLUMN_PROJECT_CATE + ","
                                                + COLUMN_BASE_INFO + "=@" + COLUMN_BASE_INFO
                                                + " where id=@id";


        private readonly SQLiteHelper sQLiteHelper;

        public static ContractDB Instance { get; } = new ContractDB();

        private ContractDB()
        {
            sQLiteHelper = new SQLiteHelper(PathUtils.GetConfigPath(), CM_DB_NAME);
        }

        public void CreateTable()
        {
            sQLiteHelper.CreateTable(CM_TABLE_NAME);
        }

        public int InsertItem(Contract contract)
        {
            string sql = "INSERT INTO " + CM_TABLE_NAME + "("
                    + COLUMN_CM_NUMBER + ","
                    + COLUMN_CM_NAME + ","
                    + COLUMN_CM_DATE + ","
                    + COLUMN_CM_ADDTIONAL + ","
                    + COLUMN_CM_BILL_NOTE + ","
                    + COLUMN_PROJECT_CATE + ","
                    + COLUMN_BASE_INFO
                    + ") values("
                    + "@" + COLUMN_CM_NUMBER + ","
                    + "@" + COLUMN_CM_NAME + ","
                    + "@" + COLUMN_CM_DATE + ","
                    + "@" + COLUMN_CM_ADDTIONAL + ","
                    + "@" + COLUMN_CM_BILL_NOTE + ","
                    + "@" + COLUMN_PROJECT_CATE + ","
                    + "@" + COLUMN_BASE_INFO
                    + ")";

            SQLiteParameter[] parameters = new SQLiteParameter[]{
                new SQLiteParameter("@" + COLUMN_CM_NUMBER, contract.ContractNumber),
                new SQLiteParameter("@" + COLUMN_CM_NAME, contract.ContractName),
                new SQLiteParameter("@" + COLUMN_CM_DATE, contract.ContractDate),
                new SQLiteParameter("@" + COLUMN_BASE_INFO, contract.BaseInfo),
                new SQLiteParameter("@" + COLUMN_CM_ADDTIONAL, contract.Aditionals),
                new SQLiteParameter("@" + COLUMN_CM_BILL_NOTE, contract.BillNoteInfo),
                new SQLiteParameter("@" + COLUMN_PROJECT_CATE, contract.ProjectCategory)

            };
            try
            {
                int rows = sQLiteHelper.ExecuteNonQuery(sql, parameters);
                if (rows <= 0)
                {
                    Log.E("", "Insert  item failed");
                    return -1;
                }
            }
            catch(Exception e)
            {
                Log.E("", "Insert item failed:" + e.Message);
                return -1;
            }
            return 0;
        }

        public void LoadAll()
        {
            Contracts.Instance.Clear();
            using (SQLiteDataReader reader = sQLiteHelper.ExecuteReader(SELECT_ALL_CONTRACTS, null))
            {
                while (reader.Read())
                {
                    try
                    {
                        Contract c = ReadItem(reader);
                        if (c != null && !string.IsNullOrEmpty(c.ContractName))
                        {
                            Contracts.Instance.Add(c);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.E("", "LoadAll item failed:" + e.Message);
                    }
                }
            }
        }

        public int DeleteItem(Contract contract)
        {
            SQLiteParameter[] paras = new SQLiteParameter[]
            {
                new SQLiteParameter("@" + COLUMN_CM_NUMBER, contract.ContractNumber)
            };
            return sQLiteHelper.ExecuteNonQuery(DELETE_CONTRACT, paras);
            
        }

        private Contract ReadItem(SQLiteDataReader reader)
        {
            Contract c = new Contract();
            c.Id = reader.GetInt64(0);
            c.ContractNumber = sQLiteHelper.ReadString(reader, COLUMN_CM_NUMBER);
            c.ContractName = sQLiteHelper.ReadString(reader, COLUMN_CM_NAME);
            c.ContractDate = sQLiteHelper.ReadLong(reader, COLUMN_CM_DATE);
            c.BaseInfo = sQLiteHelper.ReadString(reader, COLUMN_BASE_INFO);
            c.Aditionals = sQLiteHelper.ReadString(reader, COLUMN_CM_ADDTIONAL);
            c.BillNoteInfo = sQLiteHelper.ReadString(reader, COLUMN_CM_BILL_NOTE);
            c.ProjectCategory = sQLiteHelper.ReadString(reader, COLUMN_PROJECT_CATE);
            c.FromBaseInfo();
            c.FromBillNoteString();
            return c;
        }

        public void Search(string year, string type, string key)
        {
            Contracts.Instance.Clear();
            string sql = "select * from " + CM_TABLE_NAME + " where ";
            bool isAdd = false;
            if (!string.IsNullOrEmpty(year))
            {
                try
                {
                    long start = Util.GetTimeMillis(int.Parse(year), 1, 1);
                    long end = Util.GetTimeMillis(int.Parse(year) + 1, 1, 1);
                    sql += COLUMN_CM_DATE + ">= " + start
                        + " AND " + COLUMN_CM_DATE + "<" + end;
                    isAdd = true;
                }
                catch
                {

                }

            }
            if (!string.IsNullOrEmpty(type))
            {
                if (isAdd)
                {
                    sql += " AND " + COLUMN_PROJECT_CATE + @"='" + type + @"'"; ;
                }
                else
                {
                    sql += COLUMN_PROJECT_CATE + @"='" + type + @"'";
                    isAdd = true;
                }
            }
            if (!string.IsNullOrEmpty(key))
            {
                if (isAdd)
                {
                    sql += " AND " + COLUMN_BASE_INFO + @" LIKE '%" + key + @"%'";
                }
                else
                {
                    sql += COLUMN_BASE_INFO + @" LIKE '%" + key + @"%'";
                    isAdd = true;
                }
            }

            using (SQLiteDataReader reader = sQLiteHelper.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    try
                    {
                        Contract c = ReadItem(reader);
                        if (c != null && !string.IsNullOrEmpty(c.ContractName))
                        {
                            Contracts.Instance.Add(c);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.E("", "LoadAll item failed:" + e.Message);
                    }
                }
            }
        }

        public bool UpdateItem(long id, Contract contract)
        {
            SQLiteParameter[] parameters = new SQLiteParameter[]{
                new SQLiteParameter("@" + COLUMN_CM_NUMBER, contract.ContractNumber),
                new SQLiteParameter("@" + COLUMN_CM_NAME, contract.ContractName),
                new SQLiteParameter("@" + COLUMN_CM_DATE, contract.ContractDate),
                new SQLiteParameter("@" + COLUMN_BASE_INFO, contract.BaseInfo),
                new SQLiteParameter("@" + COLUMN_CM_ADDTIONAL, contract.Aditionals),
                new SQLiteParameter("@" + COLUMN_CM_BILL_NOTE, contract.BillNoteInfo),
                new SQLiteParameter("@" + COLUMN_PROJECT_CATE, contract.ProjectCategory),
                new SQLiteParameter("@id", id)
            };
            try
            {
                int rows = sQLiteHelper.ExecuteNonQuery(UPDATE_CONTRACT, parameters);
                if (rows > 0)
                {
                    return true;
                };
                return false;
            }
            catch (Exception e)
            {
                Log.E("", e.Message);
                return false;
            }
        }
    }
}
