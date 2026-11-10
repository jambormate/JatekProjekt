using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace JatekProjekt
{

    public class AdatbazisKezelo
    {
        private readonly string connectionString;


        public AdatbazisKezelo()
        {

            this.connectionString = "server=localhost;database=jatekdb;uid=root;pwd=;";
        }

   
        /// Beolvassa az összes karaktert az adatbázisból egy List<Karakter>-be.
       
        public List<Karakter> AdatokBetoltese()
        {
            List<Karakter> karakterek = new List<Karakter>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT * FROM karakterek";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader() as MySqlDataReader)
                    {
                        while (reader.Read())
                        {
                            Karakter karakter = new Karakter(
                                Convert.ToInt32(reader["Id"]),
                                reader["Nev"].ToString(),
                                reader["Osztaly"].ToString(),
                                Convert.ToInt32(reader["Szint"]),
                                Convert.ToInt32(reader["Arany"]),
                                reader["Szerver"].ToString()
                            );

                            karakterek.Add(karakter);
                        }
                    }
                }
            }
            return karakterek;
        }
    }
}
