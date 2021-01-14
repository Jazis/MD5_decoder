using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using MySql.Data.MySqlClient;


//202cb962ac59075b964b07152d234b70 = 123
//4abbedd62b6f1a99d4153bcb3f691b96 = 150000
//ac95c3e7a5e1685f4f63172cd680f7e6 = soda



namespace HasherClient
{

    class Program
    {
        //public static string inside = "ac95c3e7a5e1685f4f63172cd680f7e6";
        public static Thread thread0;
        public static Thread thread1;
        public static bool _isWorking = false;
        public static int i = 0;

        public static void letters(int id, string inside)
        {
            string line = "";
            string charsets = " 1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            int i = 0;
            int[] arr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            while (line.Length != 10)
            {
                for (int co0 = 0; co0 < charsets.Length; co0++)
                {
                    for (int co1 = 0; co1 < charsets.Length; co1++)
                    {
                        for (int co2 = 0; co2 < charsets.Length; co2++)
                        {
                            for (int co3 = 0; co3 < charsets.Length; co3++)
                            {
                                for (int co4 = 0; co4 < charsets.Length; co4++)
                                {
                                    for (int co5 = 0; co5 < charsets.Length; co5++)
                                    {
                                        for (int co6 = 0; co6 < charsets.Length; co6++)
                                        {
                                            for (int co7 = 0; co7 < charsets.Length; co7++)
                                            {
                                                for (int co8 = 0; co8 < charsets.Length; co8++)
                                                {
                                                    for (int co9 = 0; co9 < charsets.Length; co9++)
                                                    {
                                                        line = charsets[co9].ToString() + charsets[co8].ToString() + charsets[co7].ToString() + charsets[co6].ToString() + charsets[co5].ToString() + charsets[co4].ToString() + charsets[co3].ToString() + charsets[co2].ToString() + charsets[co1].ToString() + charsets[co0].ToString();
                                                        line = line.Replace(" ", "");
                                                        MD5CryptoServiceProvider MD5Code = new MD5CryptoServiceProvider();
                                                        byte[] byteDizisi = Encoding.UTF8.GetBytes(line);
                                                        byteDizisi = MD5Code.ComputeHash(byteDizisi);
                                                        StringBuilder sb = new StringBuilder();
                                                        foreach (byte ba in byteDizisi)
                                                        {
                                                            sb.Append(ba.ToString("x2").ToLower());
                                                        }
                                                        //Console.WriteLine(sb.ToString() + " | " + line);
                                                        if (sb.ToString() == inside)
                                                        {
                                                            //Console.WriteLine(sb.ToString() + " | " + line + "<- good");
                                                            result(id, line);
                                                            //Thread.CurrentThread.Abort();
                                                            Thread.CurrentThread.Abort();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        public static void check(int i)
        {
            Console.WriteLine($"I - {i} | This i");
            try
            {
                string cs = @"server=localhost;userid=hahs;password=147147Zxc;database=db_hasher";
                var con = new MySqlConnection(cs);
                con.Open();
                var stm = $"SELECT * FROM `queue` ORDER BY id LIMIT 1 OFFSET {i};";
                var cmd = new MySqlCommand(stm, con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                try
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr.FieldCount);
                        Console.WriteLine("{0} {1}  {2} {3}", rdr.GetInt32(0), rdr.GetString(1),
                                rdr.GetInt32(2), rdr.GetInt32(3).ToString());
                    }
                }
                catch
                {
                    int id = int.Parse(rdr.GetString(0));
                    Console.WriteLine($"ID =>{id}");
                    rdr.Read();
                    Console.WriteLine("Nothing found");
                    Console.WriteLine("Try to decode...  | Hash => {0}", rdr.GetString(1));
                    letters(id, rdr.GetString(1));
                }
            }
            catch
            {
                Console.WriteLine($"Error - {i}");
                i = 0;
            }
            _isWorking = false;
        }

        public static void mysql_con()
        {
            i = 0;
            while (true)
            {
                if (_isWorking == false)
                {
                    _isWorking = true;
                    i++;
                    Thread thread0 = new Thread(() => check(i));
                    thread0.Start();
                }
                else { }
            }

        }

        public static void result(int id, string line)
        {
            string cs = @"server=localhost;userid=hahs;password=147147Zxc;database=db_hasher";
            var con = new MySqlConnection(cs);
            con.Open();
            var stm = $"UPDATE `queue` SET `de_hash` = '{line}' WHERE `queue`.`id` = {id}";
            var cmd = new MySqlCommand(stm, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine($"Hash by id={id} Decoded - {line}");
            _isWorking = false;
            Thread.CurrentThread.Abort();
        }

        public static void first_step()
        {
            string cs = @"server=localhost;userid=hahs;password=147147Zxc;database=db_hasher";
            var con = new MySqlConnection(cs);
            con.Open();
            var stm = $"SELECT * FROM `queue` ORDER BY id LIMIT 1 OFFSET 20 AND de_hash IS NULL;";
            var cmd = new MySqlCommand(stm, con);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr.FieldCount);
                Console.WriteLine("{0} {1}  {2}", rdr.GetInt32(0), rdr.GetString(1),
                        rdr.GetInt32(2));
            }
        }


        static void Main(string[] args)
        {
            //first_step();
            mysql_con();
        }
    }
}
