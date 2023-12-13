using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
namespace QL_KTX
{
    public class HamChucNang
    {
        public static void Export(DataGridView dataGridView, string filePath)
        {
            try
            {
                // Tạo một DataTable để lưu trữ dữ liệu từ DataGridView
                DataTable dataTable = new DataTable();

                // Thêm các cột vào DataTable
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    dataTable.Columns.Add(column.HeaderText, column.ValueType);
                }

                // Thêm các dòng vào DataTable
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        dataRow[cell.ColumnIndex] = cell.Value;
                    }

                    dataTable.Rows.Add(dataRow);
                }

                // Ghi dữ liệu từ DataTable vào tệp Excel
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
                {
                    // Tạo một bảng tính mới
                    WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet();

                    Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                    Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };
                    sheets.Append(sheet);

                    // Thêm dữ liệu từ DataTable vào bảng tính
                    SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                    Row headerRow = new Row();

                    // Thêm tiêu đề cột
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        cell.CellValue = new CellValue(column.ColumnName);
                        headerRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(headerRow);

                    // Thêm dữ liệu từ DataTable
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Row newRow = new Row();

                        foreach (var value in dataRow.ItemArray)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(value.ToString());
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }

                    workbookPart.Workbook.Save();
                }

                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
