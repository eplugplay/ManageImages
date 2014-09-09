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
                    cmd.CommandText = "SELECT * FROM mybusiness_categories";
                    cmd.ExecuteNonQuery();
                    var dt = new DataTable();
                    var da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static void SaveImageToDb(string filename, string description, string gender, string folder, int length)
        {
            int id = 0;
            DataTable dt = new DataTable();
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM mybusiness_images";
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    using (var cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT MAX(id) + 1 FROM mybusiness_images";
                        id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                else
                {
                    id = 1;
                }
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO mybusiness_images (id, filename, description, gender, folder, length) VALUES (@id, @filename, @description, @gender, @folder, @length)";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("folder", folder);
                    cmd.Parameters.AddWithValue("length", length);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteImageDb(string filename, string gender, string folder)
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM mybusiness_images WHERE filename=@filename AND gender=@gender AND folder=@folder";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("folder", folder);
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
                    cmd.CommandText = "SELECT gender, description, length FROM mybusiness_images WHERE filename=@filename AND folder=@folder";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("folder", folder);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            return dt;
        }

        // check if already exists
        public static bool CheckImgExist(string filename, string folder)
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) AS count FROM mybusiness_images WHERE filename=@filename AND folder=@folder";
                    cmd.Parameters.AddWithValue("filename", filename);
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

        public static void UpdateImageDb(string filename, string gender, string description)
        {
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE mybusiness_images SET gender=@gender, description=@description WHERE filename=@filename";
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
