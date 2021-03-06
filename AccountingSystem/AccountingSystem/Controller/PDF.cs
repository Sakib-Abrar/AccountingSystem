﻿using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;

namespace AccountingSystem.Controller
{
    class PDF
    {
        Table table;
        Document doc;
        public PDF(string title,float[] pdfSize,string[] headers)

        {
            string filename=title+" "+DateTime.Now.ToString("dd MMM yyyy HH_mm");

            PdfWriter writer = new PdfWriter(Path.GetFullPath("PDF/" + filename + ".pdf"));

            PdfDocument pdf = new PdfDocument(writer);
            doc = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.HELVETICA);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            PdfFont italic = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLDOBLIQUE);
            Paragraph p1 = new Paragraph("Rasulganj Multipurpose Co-operative Society Ltd").SetFont(bold).SetFontSize(15).SetTextAlignment(TextAlignment.CENTER);
            Paragraph p2 = new Paragraph(title).SetFont(bold).SetFontSize(14).SetTextAlignment(TextAlignment.CENTER);
            doc.Add(p1);
            doc.Add(p2);
            table = new Table(pdfSize);
            table.SetWidthPercent(100);
            for (int i = 0; i < headers.Length; i++)
            {
                table.AddHeaderCell(new Cell().Add(new Paragraph(headers[i]).SetFont(italic)));
            }
        }

        public void AddToTable(string Data) {
            table.AddCell(new Cell().Add(new Paragraph(Data)));
        }
        public void AddParagraph(string Data) {
            Paragraph p1 = new Paragraph(Data);
            doc.Add(p1);
        }
        public void Done() {
            doc.Add(table);
            doc.Close();
            System.Windows.MessageBox.Show("PDF Created Successfully");
        }
    }
}
