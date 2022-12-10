using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace PSC_HRM.Module
{
    public static class FptProvider
    {
        /// <summary>
        /// Upload file to server using FTP UTF8
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filename">file name</param>
        public static void UploadFileUTF8(string ftppath, string username, string password, string file, string fileName_Next)
        {
            try
            {
                {//Tiến hành upload file lên máy chủ

                    FtpWebRequest ftp = CreateFTP(ftppath, username, password, fileName_Next , WebRequestMethods.Ftp.UploadFile);

                    if (!CheckFileExists(ftppath, username, password, fileName_Next))
                    {
                        StreamReader sourceStream = new StreamReader(file);
                        byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                        sourceStream.Close();
                        ftp.ContentLength = fileContents.Length;

                        Stream requestStream = ftp.GetRequestStream();
                        requestStream.Write(fileContents, 0, fileContents.Length);
                        requestStream.Close();
                    }
                    else
                        throw new Exception("File đã tồn tại trên máy chủ. Vui lòng đổi tên file rồi thử lại.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình ghi tập tin: \r\n" + ex.Message);
            }
        }

        private static bool CheckFileExists(string ftppath, string username, string password, string filename)
        {
            try
            {
                FtpWebRequest ftp = CreateFTP(ftppath, username, password, filename, WebRequestMethods.Ftp.DownloadFile);
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                return response != null;
            }
            catch
            {
                return false;
            }
        }

        private static bool CheckMultiFileExists(string ftppath, string username, string password, string filename)
        {
            try
            {
                FtpWebRequest ftp = CreateFTPMultiData(ftppath, username, password, filename, WebRequestMethods.Ftp.GetFileSize);
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                return response != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Upload file to server using FTP
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filename">file name</param>
        public static void UploadFile(string ftppath, string username, string password, string file, string fileName_Next)
        {
            try
            {
                {//Tiến hành upload file lên máy chủ

                    FtpWebRequest ftp = CreateFTP(ftppath, username, password, fileName_Next, WebRequestMethods.Ftp.UploadFile);

                    if (!CheckFileExists(ftppath, username, password, fileName_Next))
                    {
                        using (FileStream fs = File.OpenRead(file))
                        {
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);
                            fs.Close();
                            Stream requestStream = ftp.GetRequestStream();
                            requestStream.Write(buffer, 0, buffer.Length);
                            requestStream.Close();
                            requestStream.Flush();
                        }
                    }
                    else
                        throw new Exception("File đã tồn tại trên máy chủ. Vui lòng liên hệ với quản trị phần mền.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình ghi tập tin: \r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Upload file to server using FTP
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filename">file name</param>
        public static void UploadFileMultiData(string ftppath, string username, string password, string file, string fileName_Next)
        {
            try
            {
                {//Tiến hành upload file lên máy chủ

                    FtpWebRequest ftp = CreateFTPMultiData(ftppath, username, password, fileName_Next, WebRequestMethods.Ftp.UploadFile);

                    if (!CheckMultiFileExists(ftppath, username, password, fileName_Next))
                    {
                        using (FileStream fs = File.OpenRead(file))
                        {
                            byte[] buffer = new byte[fs.Length];
                            fs.Read(buffer, 0, buffer.Length);
                            fs.Close();
                            Stream requestStream = ftp.GetRequestStream();                            
                            requestStream.Write(buffer, 0, buffer.Length);
                            requestStream.Close();
                            requestStream.Flush();
                        }                                          
                    }
                    else
                        throw new Exception("File đã tồn tại trên máy chủ. Vui lòng liên hệ với quản trị phần mền.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình ghi tập tin: \r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftppath"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static byte[] DownloadFile(string ftppath, string username, string password)
        {
            try
            {
                WebClient request = new WebClient();
                request.Credentials = new NetworkCredential(username, password);
                byte[] newFileData = request.DownloadData(ftppath);
                return newFileData;
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình đọc tập tin: \r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Create FTP
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filepath">file path</param>
        /// <param name="method">ftp method</param>
        /// <returns></returns>
        private static FtpWebRequest CreateFTP(string ftppath, string username, string password, string filepath, string method)
        {            
                FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(ftppath + filepath);
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.UseBinary = true;
                ftp.Method = method;
                return ftp;         
        }

        /// <summary>
        /// Create FTP
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filepath">file path</param>
        /// <param name="method">ftp method</param>
        /// <returns></returns>
        private static FtpWebRequest CreateFTPMultiData(string ftppath, string username, string password, string filepath, string method)
        {
            try
            {
                FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(ftppath + filepath);
                ftp.Credentials = new NetworkCredential(username, password);
                ftp.KeepAlive = false;
                ftp.UseBinary = true;
                ftp.Method = method;
                ftp.UsePassive = true;
                ftp.Proxy = null;
                ftp.Timeout = 500000;
                ftp.ReadWriteTimeout = 500000;
                return ftp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Tạo đường dẫn và thư mục chứa văn bản
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <returns></returns>
        public static void CreateForder(string directory, string userName, string pass)
        {
            bool directoryExists;

            var request = (FtpWebRequest)WebRequest.Create(directory);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(userName, pass);

            try
            {
                using (request.GetResponse())
                {
                    directoryExists = true;
                }
            }
            catch (WebException)
            {
                directoryExists = false;
            }

            try
            {
                if (!directoryExists)
                {
                    WebRequest req = WebRequest.Create(directory);
                    req.Method = WebRequestMethods.Ftp.MakeDirectory;
                    req.Credentials = new NetworkCredential(userName, pass);
                    using (var resp = (FtpWebResponse)req.GetResponse())
                    {
                        Console.WriteLine(resp.StatusCode);
                    }
                }
            }
            catch (Exception ex) { throw; }
        }
        
        /// <summary>
        /// Tạo đường dẫn và thư mục chứa văn bản
        /// </summary>
        /// <param name="Filepath">file path</param>
        /// <returns></returns>
        public static void CreateForder(string directoryServer)
        {
            try
            {
                System.IO.DirectoryInfo dInfo = new DirectoryInfo(directoryServer);
                if (!dInfo.Exists)
                {
                    dInfo.Create();
                }
            }
            catch (Exception ex) { throw; }
        }

        /// <summary>
        /// Tiến hành xóa file trên máy chủ
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <returns></returns>
        public static void DeleteFileOnServer(string url, string ftpusername, string ftppassword )
        {
            try
            {

                FtpWebRequest requestFileDelete = (FtpWebRequest)WebRequest.Create(url);
                requestFileDelete.Credentials = new NetworkCredential(ftpusername, ftppassword);
                requestFileDelete.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse responseFileDelete = (FtpWebResponse)requestFileDelete.GetResponse();
            }
            catch (Exception ex)
            {
                DialogUtil.ShowError("Không thể xóa tập tin trong máy chủ." + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tên file mới nhất trong database
        /// </summary>
        public static string GetFileName_Next(string tenLoaiGiayTo, Guid maLoaiGiayTo, string fileName)
        {
            string fileName_Next = string.Empty;
            int soThuTuLonNhat = 0;

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@MaLoaiGiayTo", maLoaiGiayTo);

            try
            {
                object obj = DataProvider.GetObject("spd_GiayTo_LayGiayToMoiNhat", CommandType.StoredProcedure, param);
                if (obj != null && obj.ToString() != "")
                {
                    //int lenght = tenLoaiGiayTo.Trim().Length;
                    ////
                    //soThuTuLonNhat = Convert.ToInt32(obj.ToString().Substring(lenght + 1).Split('.')[0]);

                    //string temp;
                    //temp = obj.ToString().Substring(obj.ToString().LastIndexOf("_") + 1);
                    //temp = temp.Replace(".pdf", String.Empty);
                    soThuTuLonNhat = Convert.ToInt32(obj.ToString().Substring(obj.ToString().LastIndexOf("_") + 1).Replace(".pdf", String.Empty)) + 1;
                }
                else
                    soThuTuLonNhat = 1;

                //Lấy tên file kế tiếp theo loại giấy tờ
                fileName_Next = string.Format("{0}_{1}_{2}", tenLoaiGiayTo, fileName, soThuTuLonNhat);
            }
            catch (Exception ex)
            {
                fileName_Next = string.Empty;
            }

            return fileName_Next;
        }
    }
}
