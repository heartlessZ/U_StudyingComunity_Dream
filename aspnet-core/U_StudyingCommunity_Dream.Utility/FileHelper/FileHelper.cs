/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/7 8:37:57
* DESC: <DESCRIPTION>
* **************************************************************/
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace U_StudyingCommunity_Dream.Utility.FileHelper
{
    /// <summary>
    /// 文件操作帮助类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取文件后缀名（不包含.）
        /// </summary>
        /// <returns></returns>
        public static string GetFileExtension(string fileName)
        {
            string ext = string.Empty;
            ext = fileName.Substring(fileName.LastIndexOf(".") + 1, (fileName.Length - fileName.LastIndexOf(".") - 1));
            return ext;
        }

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public static byte[] GetFileData(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                return buffur;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (fs != null)
                {

                    //关闭资源
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePaths">文件路径集合</param>
        public static void DeleteFiles(List<string> filePaths)
        {
            foreach (var path in filePaths)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        //public static string GetBase64FromUri(string uri)
        //{
        //    string imgaeBase64Str = string.Empty;
        //    try
        //    {
        //        var bytes = GetBytes(uri);
        //        if (bytes != null)
        //        {
        //            imgaeBase64Str = Convert.ToBase64String(bytes);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return imgaeBase64Str;
        //}

        //public static byte[] GetBytes(string url)
        //{
        //    RequestSimulator request = new RequestSimulator();
        //    var bytes = request.GetBytes(url);
        //    return bytes;
        //}

        /// <summary>
        /// 文件夹下所有文件删除
        /// </summary>
        /// <param name="folder"></param>
        public static void DeleteDir(string folder)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(folder);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        subdir.Delete(true);          //删除子目录和文件
                    }
                    else
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取目录下指定后缀名的文件列表
        /// </summary>
        /// <param name="dir">文件目录</param>
        /// <param name="extension">后缀名(多个后缀名用逗号分隔，".jpg,.bmp")</param>
        /// <returns></returns>
        public static List<FileInfo> GetFileByExtension(string dir, string extension)
        {
            List<FileInfo> result = new List<FileInfo>();
            DirectoryInfo di = new DirectoryInfo(dir);
            var files = di.GetFiles();
            foreach (var file in files)
            {
                if (extension.ToUpper().Contains(file.Extension.ToUpper()))
                {
                    result.Add(file);
                }
            }
            return result;
        }


        /// <summary>
        /// 生成二维码byte数组
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] CreateQrCode(string source)
        {
            //QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(source, QRCodeGenerator.ECCLevel.Q);
            //QRCode qrcode = new QRCode(qrCodeData);
            //Bitmap qrCodeImage = qrcode.GetGraphic(5, Color.Black, Color.White, null, 15, 6, false);
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    qrCodeImage.Save(ms, ImageFormat.Jpeg);
            //    return ms.GetBuffer();
            //}
            return null;
        }


        /// <summary>
        /// 将base64转换为图片
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static Image ConvertImageFromBase64(string base64Str)
        {
            Image image = null;
            try
            {
                byte[] arr = Convert.FromBase64String(base64Str);
                using (MemoryStream ms = new MemoryStream(arr))
                {
                    image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return image;
        }

        public static void SaveImageFromBase64(string base64Str, string filePath)
        {
            byte[] arr = Convert.FromBase64String(base64Str);
            using (MemoryStream ms = new MemoryStream(arr))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    ms.WriteTo(fs);
                }
            }
        }

        /// <summary>
        /// 将本地图片转换为Base64
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ConvertBase64FromPath(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileDir">生成文件目录</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="itemList">对象列表</param>
        /// <param name="fileType">导出文件类型</param>
        /// <param name="author">作者</param>
        /// <returns></returns>
        public static bool ExportFile<T>(string fileDir, string fileName, List<T> itemList, int fileType, string author = null) where T : class
        {
            var result = false;
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            var data = Excel.GetData(itemList);//表格数据
            var header = Excel.GetColumNames(typeof(T));
            switch (fileType)
            {
                case 0:
                    result = Excel.Save(Path.Combine(fileDir, fileName), data, header, author);
                    break;
                case 1:
                    result = Pdf.Save(Path.Combine(fileDir, fileName), data, header, author);
                    break;
                default:
                    break;
            }
            return result;
        }


        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns>MD5值</returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open))
                {
                    return GetMD5FromStream(file);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }

        public static string GetMD5FromStream(Stream stream)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(stream);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 将字节数组转换为md5
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetMD5FromBytes(byte[] bytes)
        {
            using (Stream stream = new MemoryStream(bytes))
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(stream);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString().ToUpper();
            }
        }

        /// <summary>
        /// 验证文件名是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ValidFileName(string name)
        {
            return !Path.GetInvalidPathChars().Any(name.Contains);
        }
    }
}
