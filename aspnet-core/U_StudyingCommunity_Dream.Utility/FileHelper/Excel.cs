/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/7 8:42:52
* DESC: <DESCRIPTION>
* **************************************************************/
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace U_StudyingCommunity_Dream.Utility.FileHelper
{
    /// <summary>
    /// Excel操作类
    /// </summary>
    public class Excel
    {
        private Excel() { }

        /// <summary>
        /// Excel文档流转换成DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="isXslx">是否为.xlsx</param>
        /// <returns></returns>
        public static DataTable RenderFromExcel(Stream excelFileStream, bool isXslx = true, int headerRowIndex = 0)
        {
            using (excelFileStream)
            {
                IWorkbook workbook = null;
                if (isXslx)
                {
                    workbook = new XSSFWorkbook(excelFileStream);
                }
                else
                {
                    workbook = new HSSFWorkbook(excelFileStream);
                }
                ISheet sheet = workbook.GetSheetAt(0);
                DataTable table = new DataTable();
                IRow headerRow = sheet.GetRow(headerRowIndex);//第一行为标题行  
                int cellCount = headerRow.LastCellNum;//LastCellNum = PhysicalNumberOfCells  
                int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1  

                //handling header.  
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    if (headerRow.GetCell(i) != null)
                    {
                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        table.Columns.Add(column);
                    }
                }
                for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    if (row != null && row.Cells.Count > 0)
                    {
                        for (int j = row.FirstCellNum; j < table.Columns.Count; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = GetCellValue(row.GetCell(j));
                            }
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                return table;
            }
        }

        /// <summary>
        /// 将Excel文件流，转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="extension"></param>
        /// <returns></returns>
        //public static List<T> ConvertExcelFromStream<T>(Stream stream, string extension, int headerRowIndex = 0) where T : new()
        //{
        //    DataTable data = new DataTable();
        //    data = RenderFromExcel(stream, extension.Contains("xlsx"), headerRowIndex);
        //    return data.ToModelList<T>();
        //}

        /// <summary>
        /// 获取指定单元格的数据
        /// </summary>
        /// <param name="excelFileStream"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="isXslx"></param>
        /// <returns></returns>
        public static string GetCustomCellValue(Stream excelFileStream, int rowIndex = 0, int columnIndex = 0, bool isXslx = true)
        {
            string value = string.Empty;
            IWorkbook workbook = null;
            if (isXslx)
            {
                workbook = new XSSFWorkbook(excelFileStream);
            }
            else
            {
                workbook = new HSSFWorkbook(excelFileStream);
            }
            ISheet sheet = workbook.GetSheetAt(0);
            IRow row = sheet.GetRow(rowIndex);//第一行为标题行  
            int rowCount = sheet.LastRowNum;//LastRowNum = PhysicalNumberOfRows - 1  
            if (rowCount >= columnIndex + 1)
            {
                value = row.GetCell(columnIndex).StringCellValue;
            }
            return value;
        }

        /// <summary>
        /// 把数据保存到Excel文件
        /// </summary>
        /// <param name="path">目标文件全名（如：c:\data\output.xls）</param>
        /// <param name="data">待保存的数据</param>
        /// <param name="header">表头，为空时则无表头</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns></returns>
        public static bool Save(string path, IList<List<string>> data, IList<String> header = null, string author = null, string sheetName = "Sheet1")
        {
            Boolean result = false;

            IWorkbook workbook = GenerateXLSX(data, header, author, sheetName);
            result = Save2File(path, workbook);
            workbook.Close();

            return result;
        }

        /// <summary>
        /// 生成EXCEL文件(.xlsx)
        /// </summary>
        /// <param name="data">待保存的数据</param>
        /// <param name="header">表头，为空时则无表头</param>
        /// <returns></returns>
        private static XSSFWorkbook GenerateXLSX(IList<List<string>> data, IList<string> header = null, string author = null, string sheetName = "Sheet1")
        {
            XSSFWorkbook xlsx = new XSSFWorkbook();
            ISheet sheet = xlsx.CreateSheet(sheetName);

            Int32 rowID = 0;
            // 创建表头
            if (header != null && 0 < header.Count)
            {
                IRow row = sheet.CreateRow(rowID);
                for (Int32 col = 0; col < header.Count; ++col)
                {
                    row.CreateCell(col).SetCellValue(header[col]);
                }
            }

            // 填充数据
            foreach (var rowData in data)
            {
                rowID += 1;
                IRow row = sheet.CreateRow(rowID);
                for (Int32 col = 0; col < rowData.Count; ++col)
                {
                    //row.CreateCell(col).SetCellValue(rowData[col]);
                    //row.CreateCell(col).SetCellType(CellType.Numeric);
                    //如果为数字，转换为double型
                    if (rowData[col] != null
                        && Regex.IsMatch(rowData[col], @"^[+-]?/d*[.]?/d*$")
                        && !Regex.IsMatch(rowData[col], @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ")
                    )
                    {
                        row.CreateCell(col).SetCellType(CellType.Numeric);
                        row.CreateCell(col).SetCellValue(double.Parse(rowData[col]));
                    }
                    else
                    {
                        row.CreateCell(col).SetCellValue(rowData[col]);
                    }

                }
            }
            #region 设置作者
            if (!string.IsNullOrEmpty(author))
            {
                rowID += 2;
                IRow rowAuthor = sheet.CreateRow(rowID);
                IRow rowDate = sheet.CreateRow(++rowID);
                rowAuthor.CreateCell(data[0].Count - 2).SetCellValue("Author:");
                rowAuthor.CreateCell(data[0].Count - 1).SetCellValue(author);
                rowDate.CreateCell(data[0].Count - 2).SetCellValue("Date");
                rowDate.CreateCell(data[0].Count - 1).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            }
            #endregion
            return xlsx;
        }

        /// <summary>
        /// 保存Excel到文件
        /// </summary>
        /// <param name="path">目标文件全名（如：c:\data\output.xls）</param>
        /// <param name="workbook">Excel</param>
        /// <returns></returns>
        private static bool Save2File(string path, IWorkbook workbook)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    workbook.Write(fs);
                }
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("无保存权限，{0}", ex.Message);
                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据Excel列类型获取列的值
        /// </summary>
        /// <param name="cell">Excel列</param>
        /// <returns></returns>
        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
                return string.Empty;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString();
                    }
                    else
                    {
                        return cell.NumericCellValue.ToString();
                    }
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        /// <summary>
        /// 获取列标题
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetColumNames(Type type, bool includeReference = true)
        {
            List<string> titles = new List<string>();
            var propertiries = type.GetProperties();
            var stringType = typeof(string);
            foreach (var item in propertiries)
            {
                if (!(item.PropertyType.IsValueType || item.PropertyType == stringType))
                {
                    if (!includeReference)
                        continue;
                }
                //var display = item.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault();
                //if (display != null)
                //{
                //    titles.Add(display.DisplayName);
                //}
                //else
                //{
                //    titles.Add(item.Name);
                //}
            }
            return titles;
        }

        /// <summary>
        /// 通过反射获取属性值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static List<string> GetValues(Type type)
        {
            List<string> titles = new List<string>();
            var propertiries = type.GetProperties();
            foreach (var item in propertiries)
            {
                titles.Add(item.GetValue(item, null).ToString());
            }
            return titles;
        }

        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static List<List<string>> GetData<T>(List<T> items, bool includeReference = true) where T : class
        {
            List<List<String>> data = new List<List<string>>();
            var propertiries = typeof(T).GetProperties();
            var stringType = typeof(string);
            foreach (var item in items)
            {
                List<String> dataLine = new List<string>();
                foreach (var pi in propertiries)
                {
                    if (!(pi.PropertyType.IsValueType || pi.PropertyType == stringType))
                    {
                        if (!includeReference)
                            continue;
                    }
                    var obj = pi.GetValue(item, null);
                    dataLine.Add(obj == null ? "" : obj.ToString());
                }
                data.Add(dataLine);
            }
            return data;
        }

        public static byte[] SaveToBuffer<T>(List<T> items, bool includeReference = true, string sheetName = "Sheet1") where T : class
        {
            var data = GetData(items, includeReference);
            var colums = GetColumNames(typeof(T), includeReference);
            var xlsx = GenerateXLSX(data, colums, sheetName);
            byte[] result = null;
            using (var stream = new MemoryStream())
            {
                xlsx.Write(stream);
                result = stream.GetBuffer();
            }
            return result;
        }

        /// <summary>
        /// 动态类型输出excel
        /// </summary>
        /// <param name="items"></param>
        /// <param name="includeReference"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static byte[] SaveToBuffer(List<dynamic> items, bool includeReference = true, string sheetName = "Sheet1")
        {
            if (items == null)
            {
                return null;
            }
            var colums = new List<string>();
            var data = new List<List<string>>();
            var stringType = typeof(string);
            //var properties = items[0].GetType().GetProperties();
            var properties = items[0].ChildrenTokens;
            foreach (var property in properties)
            {
                colums.Add(property.Name);
            }
            foreach (var item in items)
            {
                List<String> dataLine = new List<string>();
                foreach (var property in properties)
                {
                    if (!(property.PropertyType.IsValueType || property.PropertyType == stringType))
                    {
                        if (!includeReference)
                            continue;
                    }
                    var obj = property.GetValue(item, null);
                    dataLine.Add(obj == null ? "" : obj.ToString());
                }
                data.Add(dataLine);
            }
            var xlsx = GenerateXLSX(data, colums, sheetName);
            byte[] result = null;
            using (var stream = new MemoryStream())
            {
                xlsx.Write(stream);
                result = stream.GetBuffer();
            }
            return result;
        }

        public static byte[] SaveToBuffer(Dictionary<string, string> items, bool includeReference = true, string sheetName = "Sheet1")
        {
            if (items == null)
            {
                return null;
            }
            var colums = new List<string>();
            var data = new List<string>();
            var stringType = typeof(string);
            foreach (var property in items)
            {
                colums.Add(property.Key);
                data.Add(property.Value);
            }
            var xlsx = GenerateXLSX(new List<List<string>>() { data }, colums, sheetName);
            byte[] result = null;
            using (var stream = new MemoryStream())
            {
                xlsx.Write(stream);
                result = stream.GetBuffer();
            }
            return result;
        }

        public static void SaveToFile<T>(List<T> items, string filePath, bool includeReference = true, string sheetName = "Sheet1") where T : class
        {
            var data = GetData(items, includeReference);
            var colums = GetColumNames(typeof(T), includeReference);
            Save(filePath, data, colums, sheetName);
        }
    }
}
