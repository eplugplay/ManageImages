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

        public static void SaveImageToDb(string filename, string description, string gender, string folder)
        {
            int id = 0;
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT MAX(id) + 1 FROM mybusiness_images";
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO mybusiness_images (id, filename, description, gender, folder) VALUES (@id, @filename, @description, @gender, @folder)";
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("description", description);
                    cmd.Parameters.AddWithValue("gender", gender);
                    cmd.Parameters.AddWithValue("folder", folder);
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
        public static string GetGenderInfo(string filename, string folder)
        {
            string gender = "";
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT gender FROM mybusiness_images WHERE filename=@filename AND folder=@folder";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("folder", folder);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            gender = rdr["gender"].ToString();
                        }
                    }
                }
            }
            return gender;
        }

        // get description
        public static string GetDescriptionInfo(string filename, string folder)
        {
            string Desc = "";
            using (MySqlConnection cnn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ToString()))
            {
                cnn.Open();
                using (var cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "SELECT description FROM mybusiness_images WHERE filename=@filename AND folder=@folder";
                    cmd.Parameters.AddWithValue("filename", filename);
                    cmd.Parameters.AddWithValue("folder", folder);
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Desc = rdr["description"].ToString();
                        }
                    }
                }
            }
            return Desc;
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
