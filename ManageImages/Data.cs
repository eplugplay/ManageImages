using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace ManageImages
{
    public static class Data
    {
        public static DataTable LoadSections()
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM mybusiness_categories ORDER BY category ASC";
                    cmd.ExecuteNonQuery();
                    var dt = new DataTable();
                    var da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static DataTable GetAllCategories()
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id FROM mybusiness_categories";
                    cmd.ExecuteNonQuery();
                    var dt = new DataTable();
                    var da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void SaveImageToDb(string filename, string description, string gender, string folder, int length, bool hidden)
        {
            int hiddenValue = 0;
            switch (hidden)
            {
                case true: hiddenValue = 1; break;
            }
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO mybusiness_images (filename, description, gender, folder, length, hidden) VALUES (@filename, @description, @gender, @folder, @length, @hidden)";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("folder", folder);
                    cmd.Parameters.AddWithValue("length", length);
                    cmd.Parameters.AddWithValue("hidden", hiddenValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteImageDb(string filename, string gender, string folder, int length)
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM mybusiness_images WHERE filename=@filename AND gender=@gender AND folder=@folder AND length=@length";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("folder", folder);
                    cmd.Parameters.AddWithValue("length", length);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // get gender
        public static DataTable GetInfo(string filename, string folder)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT filename, gender, description, length, hidden FROM mybusiness_images WHERE filename=@filename AND folder=@folder";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("folder", folder);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // check if already exists
        public static bool CheckImgExist(string filename, int length, string folder)
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) AS count FROM mybusiness_images WHERE filename=@filename AND folder=@folder AND length=@length";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("length", length);
                    cmd.Parameters.AddWithValue("folder", folder);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int cnt = Convert.ToInt32(rdr["count"]);
                            if (cnt > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        // check if already exists in db when uploading
        public static string CheckImgAllExist(string filename, int length)
        {
            string toReturn = "";
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT folder FROM mybusiness_images WHERE filename=@filename AND length=@length";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("length", length);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                toReturn = rdr["folder"].ToString();
                            }
                        }
                    }
                }
            }
            return toReturn;
        }


        public static void UpdateImageDb(string filename, string folder, int length, string gender, string description, bool hidden, bool isMove)
        {
            int hiddenValue = 0;
            switch (hidden)
            {
                case true: hiddenValue = 1; break;
            }
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    if (isMove == true)
                    {
                        cmd.CommandText = "UPDATE mybusiness_images SET gender=@gender, description=@description, hidden=@hidden, folder=@folder WHERE filename=@filename AND length=@length";
                        cmd.Parameters.AddWithValue("folder", folder);
                    }
                    else
                    {
                        cmd.CommandText = "UPDATE mybusiness_images SET gender=@gender, description=@description, hidden=@hidden WHERE filename=@filename AND length=@length";
                    }
                    cmd.Parameters.AddWithValue("description", description);

                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("length", length);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("hidden", hiddenValue);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
