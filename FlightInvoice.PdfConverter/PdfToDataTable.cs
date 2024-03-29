using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.xmp.impl;
using System.Data;
using System.Text;


namespace FlightInvoice.PdfConverter;

public class PdfToDataTable(Stream data)
{
    private Stream _data = data;

    public DataSet ConvertDusseldorfInvoice()
    {
        StringBuilder text = new StringBuilder();

        using (PdfReader reader = new PdfReader(_data))
        {
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }
        }

        string[] parser = text.ToString().Split("\n");

        DataSet dataSet = new DataSet();
        DataTable table = new DataTable();
        bool invoiceInfoColumnCreated = false;
        bool invoiceInfoCompleted = false;
        bool invoiceDetailColumnCreated = false;

        foreach (string row in parser)
        {
            var columns = row.Split(' ');

            if (columns.Length == 3 && !invoiceInfoCompleted)
            {
                string column = columns[0];

                if (!string.IsNullOrEmpty(column) && String.Equals(column, "Nummer"))
                {
                    string column1 = columns[1];
                    string column2 = columns[2];

                    table.Columns.Add(column);
                    table.Columns.Add(column1);
                    table.Columns.Add(column2);
                    invoiceInfoColumnCreated = true;
                    table.TableName = "InvoiceInfo";
                }
                else if (invoiceInfoColumnCreated)
                {
                    invoiceInfoColumnCreated = false;
                    table.Rows.Add(columns[0], columns[1], columns[2]);
                    dataSet.Tables.Add(table);
                    table = new DataTable();
                    invoiceInfoCompleted = true;
                }
            }
            else if (columns.Length == 9 && !invoiceDetailColumnCreated)
            {
                string column = columns[0];

                if (!string.IsNullOrEmpty(column) && String.Equals(column, "Season"))
                {
                    table.Columns.Add("Season");
                    table.Columns.Add("VT");
                    table.Columns.Add("Flugdatum");
                    table.Columns.Add("CarrierCode");
                    table.Columns.Add("Flug Nr");
                    table.Columns.Add("Routing");
                    table.Columns.Add("Anzahl");
                    table.Columns.Add("Einzelpreis");
                    table.Columns.Add("Betrag in EUR");
                    table.Columns.Add("Summen in EUR");
                    invoiceDetailColumnCreated = true;
                    table.TableName = "InvoiceDetail";
                }
            }
            else if (columns.Length == 10 && invoiceDetailColumnCreated)
            {
                string column = columns[0];
                table.Rows.Add(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5] + " " + columns[6], columns[7], columns[8], columns[9]);
            }
            else if (columns.Length >= 17 && invoiceDetailColumnCreated)
            {
                string column = columns[0];
                table.Rows.Add(columns[0], columns[1], columns[2], columns[3], columns[4], columns[5] + " " + columns[6], columns[7], columns[8], columns[9], columns[columns.Length - 1]);
            }
        }

        dataSet.Tables.Add(table);

        if (dataSet.Tables.Count != 2)
            throw new Exception("Missing Table Count");


        if (dataSet.Tables[0].Rows.Count != 1)
            throw new Exception("Missing Invoice Info Table");


        if (dataSet.Tables[1].Rows.Count == 0)
            throw new Exception("Missing Invoice Detail Table");

        return dataSet;
    }
}
