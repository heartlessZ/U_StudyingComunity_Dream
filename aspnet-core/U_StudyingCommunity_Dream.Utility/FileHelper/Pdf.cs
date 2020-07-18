/****************************************************************
* MACHINE: KANGCHEN-ZHAO
* NAME: boomclub
* CREATEDATE: 2019/10/7 8:52:20
* DESC: <DESCRIPTION>
* **************************************************************/
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Document = iText.Layout.Document;

namespace U_StudyingCommunity_Dream.Utility.FileHelper
{
    /// <summary>
    /// Pdf操作帮助类
    /// </summary>
    public static class Pdf
    {

        /// <summary>
        /// 把数据保存到Excel文件
        /// </summary>
        /// <param name="path">目标文件全名（如：c:\data\output.xls）</param>
        /// <param name="data">待保存的数据</param>
        /// <param name="header">表头，为空时则无表头</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns></returns>
        public static Boolean Save(String path, IList<List<String>> data, IList<String> header = null, string author = null)
        {
            PdfWriter writer = new PdfWriter(path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate());

            document.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            document.SetMargins(20, 20, 20, 20);

            Table table = new Table(header.Count);
            List<List<String>> dataset = new List<List<string>>();
            dataset.Add(header.ToList());
            dataset.AddRange(data);
            foreach (var items in dataset)
            {
                foreach (var field in items)
                {
                    var cell = new Cell();
                    cell.SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                    cell.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER); //水平居中
                    cell.SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE); //垂直居中
                    cell.Add(new Paragraph(field));
                    table.AddCell(cell);
                }
            }
            document.Add(table);
            if (!string.IsNullOrEmpty(author))
            {
                var otherParagraph = new Paragraph();
                otherParagraph.SetMargin(10);
                otherParagraph.Add($"Author:{author}       ");
                otherParagraph.Add($"Date:{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}");
                document.Add(otherParagraph);
            }
            document.Close();
            return true;
        }

        /// <summary>
        /// 获取pdf文件数组
        /// </summary>
        /// <param name="data"></param>
        /// <param name="header"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        public static byte[] SaveToBuffer(IList<List<String>> data, IList<String> header = null, string author = null)
        {
            var buffer = new byte[1024 * 1024];
            Stream stream = new MemoryStream(buffer);
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf, PageSize.A4.Rotate());

            document.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            document.SetMargins(20, 20, 20, 20);

            Table table = new Table(header.Count);
            List<List<String>> dataset = new List<List<string>>();
            dataset.Add(header.ToList());
            dataset.AddRange(data);
            foreach (var items in dataset)
            {
                foreach (var field in items)
                {
                    var cell = new Cell();
                    cell.SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE);
                    cell.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER); //水平居中
                    cell.SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE); //垂直居中
                    cell.Add(new Paragraph(field));
                    table.AddCell(cell);
                }
            }
            document.Add(table);
            if (!string.IsNullOrEmpty(author))
            {
                var otherParagraph = new Paragraph();
                otherParagraph.SetMargin(10);
                otherParagraph.Add($"Author:{author}       ");
                otherParagraph.Add($"Date:{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}");
                document.Add(otherParagraph);
            }
            stream.Close();
            stream.Dispose();
            writer.Close();
            writer.Dispose();
            pdf.Close();
            document.Close();
            return buffer;
        }
    }
}
