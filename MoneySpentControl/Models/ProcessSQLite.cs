using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace MoneySpentControl.Models
{
    public class ProcessSQLite
    {
        static string path = Directory.GetCurrentDirectory() + @"\spentearn.db";
        static string cs = "Data Source=" + path + "; Version=3;";

        public static void InitTable()
        {
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS spents(id INTEGER PRIMARY KEY, amount INTEGER, " +
                "dateTime TEXT, category TEXT, description TEXT, isProfit INTEGER)";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void InsertOperation(Operation operation)
        {
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            int isProf = operation.IsProfit ? 1 : 0;
            string desc = operation.Description == null ? "-" : operation.Description;
            cmd.CommandText = $"INSERT INTO spents(amount, dateTime, category, description, isProfit) VALUES (" +
                $"{operation.Amount}, \"{operation.DateTime.ToString()}\", \"{operation.Category}\", " +
                $"\"{desc}\", {isProf})";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteById(int id)
        {
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = $"DELETE FROM spents WHERE id={id}";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        /// <summary>
        /// Get All records
        /// </summary>
        /// <returns>Use rdr.GetInt32 and rdr.GetString with index</returns>
        public static List<Operation> GetAllHistory()
        {
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM spents";

            List<Operation> operations = new List<Operation>();

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                int summa = rdr.GetInt32(1);
                string dateTime = rdr.GetString(2);
                string cat = rdr.GetString(3);
                string desc = rdr.GetString(4);
                int isProfit = rdr.GetInt32(5);
                operations.Add(new Operation(cat, summa, isProfit, dateTime, desc));
            }
            return operations;
        }
    }
}
