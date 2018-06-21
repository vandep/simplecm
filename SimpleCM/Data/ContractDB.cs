﻿using SimpleCM.Tools;
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
        private const string COLUMN_CM_DESCRIPTION = "text_field3";
        private const string COLUMN_CM_COMPANY = "text_field4";
        private const string COLUMN_CM_COOPERATOR = "text_field5";
        
        private const string COLUMN_CM_COST = "int_field2";
        private const string COLUMN_CM_PE_1 = "int_field3";
        private const string COLUMN_CM_PE_2 = "int_field4";
        private const string COLUMN_CM_PE_3 = "int_field5";

        private const string COLUMN_CM_ADDTIONAL_1 = "text_field6";
        private const string COLUMN_CM_ADDTIONAL_2 = "text_field7";
        private const string COLUMN_CM_ADDTIONAL_3 = "text_field8";
        private const string COLUMN_CM_BILL_NOTE = "text_field9";
        private const string SELECT_ALL_CONTRACTS = "select * from " + CM_TABLE_NAME + " order by " + COLUMN_CM_DATE + " desc";


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

        public int Insert(Contract contract)
        {
            string sql = "INSERT INTO " + CM_TABLE_NAME + "("
                    + COLUMN_CM_NUMBER + ","
                    + COLUMN_CM_NAME + ","
                    + COLUMN_CM_DATE + ","
                    + COLUMN_CM_DESCRIPTION + ","
                    + COLUMN_CM_COMPANY + ","
                    + COLUMN_CM_COOPERATOR + ","
                    + COLUMN_CM_COST + ","
                    + COLUMN_CM_PE_1 + ","
                    + COLUMN_CM_PE_2 + ","
                    + COLUMN_CM_PE_3 + ","
                    + COLUMN_CM_ADDTIONAL_1 + ","
                    + COLUMN_CM_ADDTIONAL_2 + ","
                    + COLUMN_CM_ADDTIONAL_3 + ","
                    + COLUMN_CM_BILL_NOTE
                    + ") values("
                    + "@" + COLUMN_CM_NUMBER + ","
                    + "@" + COLUMN_CM_NAME + ","
                    + "@" + COLUMN_CM_DATE + ","
                    + "@" + COLUMN_CM_DESCRIPTION + ","
                    + "@" + COLUMN_CM_COMPANY + ","
                    + "@" + COLUMN_CM_COOPERATOR + ","
                    + "@" + COLUMN_CM_COST + ","
                    + "@" + COLUMN_CM_PE_1 + ","
                    + "@" + COLUMN_CM_PE_2 + ","
                    + "@" + COLUMN_CM_PE_3 + ","
                    + "@" + COLUMN_CM_ADDTIONAL_1 + ","
                    + "@" + COLUMN_CM_ADDTIONAL_2 + ","
                    + "@" + COLUMN_CM_ADDTIONAL_3 + ","
                    + "@" + COLUMN_CM_BILL_NOTE
                    + ")";

            SQLiteParameter[] parameters = new SQLiteParameter[]{
                new SQLiteParameter("@" + COLUMN_CM_NUMBER, contract.ContractNumber),
                new SQLiteParameter("@" + COLUMN_CM_NAME, contract.ContractName),
                new SQLiteParameter("@" + COLUMN_CM_DATE, contract.ContractDate),
                new SQLiteParameter("@" + COLUMN_CM_DESCRIPTION, contract.ProjectDescription),
                new SQLiteParameter("@" + COLUMN_CM_COMPANY, contract.ContractCompany),
                new SQLiteParameter("@" + COLUMN_CM_COOPERATOR, contract.CooperatorCompany),
                new SQLiteParameter("@" + COLUMN_CM_COST, contract.Cost),
                new SQLiteParameter("@" + COLUMN_CM_PE_1, contract.Peceivables_1),
                new SQLiteParameter("@" + COLUMN_CM_PE_2, contract.Peceivables_2),
                new SQLiteParameter("@" + COLUMN_CM_PE_3, contract.Peceivables_3),
                new SQLiteParameter("@" + COLUMN_CM_ADDTIONAL_1, contract.Aditional_1),
                new SQLiteParameter("@" + COLUMN_CM_ADDTIONAL_2, contract.Aditional_2),
                new SQLiteParameter("@" + COLUMN_CM_ADDTIONAL_3, contract.Aditional_3),
                new SQLiteParameter("@" + COLUMN_CM_BILL_NOTE, contract.BillNoteInfo)

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
                        Contract c = ReadContract(reader);
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

        private Contract ReadContract(SQLiteDataReader reader)
        {
            Contract c = new Contract();

            c.ContractNumber = sQLiteHelper.ReadString(reader, COLUMN_CM_NUMBER);
            c.ContractName = sQLiteHelper.ReadString(reader, COLUMN_CM_NAME);
            c.ContractDate = sQLiteHelper.ReadLong(reader, COLUMN_CM_DATE);
            c.ContractCompany = sQLiteHelper.ReadString(reader, COLUMN_CM_COMPANY);
            c.CooperatorCompany = sQLiteHelper.ReadString(reader, COLUMN_CM_COOPERATOR);
            c.ProjectDescription = sQLiteHelper.ReadString(reader, COLUMN_CM_DESCRIPTION);
            c.Cost = sQLiteHelper.ReadLong(reader, COLUMN_CM_COST);
            c.Peceivables_1 = sQLiteHelper.ReadLong(reader, COLUMN_CM_PE_1);
            c.Peceivables_2 = sQLiteHelper.ReadLong(reader, COLUMN_CM_PE_2);
            c.Peceivables_3 = sQLiteHelper.ReadLong(reader, COLUMN_CM_PE_3);
            c.Aditional_1 = sQLiteHelper.ReadString(reader, COLUMN_CM_ADDTIONAL_1);
            c.Aditional_2 = sQLiteHelper.ReadString(reader, COLUMN_CM_ADDTIONAL_2);
            c.Aditional_3 = sQLiteHelper.ReadString(reader, COLUMN_CM_ADDTIONAL_3);
            c.BillNoteInfo = sQLiteHelper.ReadString(reader, COLUMN_CM_BILL_NOTE);
            return c;
        }
    }
}
