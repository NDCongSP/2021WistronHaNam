using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ipc_hanam
{
    public class DataProvider
    {
        //connection string kết nối DB server
        public string connectionStr = "";//$"Data Source=localhost;Database=solar_db;UID=root;Password=100100; Min Pool Size=0;Max Pool Size=1000;Pooling=true; Connect Timeout=65535;";


        #region tạo Degin Parttern Singleton
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DataProvider();
                }
                return _instance;
            }
            private set => _instance = value;
        }
        public DataProvider()
        {
            connectionStr = ConfigurationManager.AppSettings["DbConnectionString"];
        }
        #endregion

        /// <summary>
        /// Method query DB trả về bảng dữ liệu kiểu DataTable.
        /// SELECT.
        /// </summary>
        /// <param name="query">câu truy vấn, có thể là câu query binh thường hoặc store procedure.</param>
        /// <param name="parametter">Object chứa các giá trị của parametter truyền vào, dùng cho query dùng store procedure, nếu sql bình thường thì để trống properties này.
        ///  /// Lưu ý: khi truyền store procedure thì các para phải cách cách dấu ',' và 1 khoảng trắng.
        /// Ex: @abc , @def.</param>
        /// <returns>Trả về bảng dữ liệu kiểu DataTable. ==null or rows.count==0 là ko có data hoặc error.</returns>
        public DataTable ExecuteQuery(string query, object[] parametter = null, [CallerMemberName] string memberCall = "", [CallerFilePath] string memberClass = "", [CallerLineNumber] int memberLine = 0)
        {
            DataTable res = new DataTable();

            //try
            //{
            using (MySqlConnection connection = new MySqlConnection(connectionStr))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                //trường hợp câu query là store procedure
                //set object giá trị tham số truyền vào, nếu khác null thì tiến hành add các giá trị cho tham số
                if (parametter != null)
                {
                    int i = 0;
                    string[] arrayParas = query.Split(' ');
                    foreach (string item in arrayParas)
                    {
                        if (item.Contains("@"))
                        {
                            if (parametter[i] != null)
                            {
                                command.Parameters.AddWithValue(item, parametter[i]);
                            }
                            else
                            {
                                command.Parameters.AddWithValue(item, DBNull.Value);
                            }
                            i++;
                        }

                    }
                }
                //chạy câu query fill vào DataTable
                MySqlDataAdapter adaptor = new MySqlDataAdapter(command);
                adaptor.Fill(res);

                //đóng kết nối hủy đối tượng connection đến DB server
                connection.Close();
                connection.Dispose();
            }
            //}
            //catch (Exception ex)
            //{
            //    //XtraMessageBox.Show($"Lỗi DataProvider: {ex.Message}.{Environment.NewLine}Lỗi xảy ra ở: {memberClass}/{memberCall}/Line:{memberLine}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            return res;
        }

        /// <summary>
        /// Method query ko trả về bảng dữ liệu, chỉ trả về trạng thái thành công hay thất bại.
        /// INSERT, UPDATE, DELETE.
        /// </summary>
        /// <param name="query">Câu query truyền vào.</param>
        /// <param name="parametter">object chứa các giá trị của parametter.có thể để trống properties này.</param>
        /// <returns>1 là thành công, 0 thất bại.</returns>
        public int ExecuteNonQuery(string query, object[] parametter = null, [CallerMemberName] string memberCall = "", [CallerFilePath] string memberClass = "", [CallerLineNumber] int memberLine = 0)
        {
            int res = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(query, connection);

                    if (parametter != null)
                    {
                        int i = 0;
                        string[] arrayParas = query.Split(' ');
                        foreach (string item in arrayParas)
                        {
                            if (item.Contains("@"))
                            {
                                if (parametter[i] != null)
                                {
                                    command.Parameters.AddWithValue(item, parametter[i]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue(item, DBNull.Value);
                                }
                                i++;
                            }
                        }
                    }

                    res = command.ExecuteNonQuery();

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Lỗi DataProvider: {ex.Message}.{Environment.NewLine}Lỗi xảy ra ở: {memberClass}/{memberCall}/Line:{memberLine}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }

        /// <summary>
        /// Method dem so record trong table.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parametter"></param>
        /// <returns>Object chứa số record đếm đc.</returns>
        public object ExecuteScalar(string query, object[] parametter = null, [CallerMemberName] string memberCall = "", [CallerFilePath] string memberClass = "", [CallerLineNumber] int memberLine = 0)
        {
            object res = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    if (parametter != null)
                    {
                        int i = 0;
                        string[] arrayParas = query.Split(' ');
                        foreach (string item in arrayParas)
                        {
                            if (item.Contains("@"))
                            {
                                if (parametter[i] != null)
                                {
                                    command.Parameters.AddWithValue(item, parametter[i]);
                                }
                                else
                                {
                                    command.Parameters.AddWithValue(item, DBNull.Value);
                                }
                                i++;
                            }
                        }
                    }
                    res = command.ExecuteScalar();

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi DataProvider: {ex.Message}.{Environment.NewLine}Lỗi xảy ra ở: {memberClass}/{memberCall}/Line:{memberLine}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return res;
        }

        /// <summary>
        /// Method dung de get  ra ID identity.
        /// </summary>
        /// <param name="queryStoreProcedure"></param>
        /// <returns></returns>
        public int ExecuteNonQuery_GetIdIdentity(string queryStoreProcedure, [CallerMemberName] string memberCall = "", [CallerFilePath] string memberClass = "", [CallerLineNumber] int memberLine = 0)
        {
            int result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(queryStoreProcedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter returnParameter = new SqlParameter("@LastIdentity", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParameter);

                    command.ExecuteNonQuery();

                    result = (int)command.Parameters["@LastIdentity"].Value;

                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi DataProvider: {ex.Message}.{Environment.NewLine}Lỗi xảy ra ở: {memberClass}/{memberCall}/Line:{memberLine}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }
    }
}
