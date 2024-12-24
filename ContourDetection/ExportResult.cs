using Excel = Microsoft.Office.Interop.Excel;

namespace ContourDetection
{
    internal static class ExportResult
    {
        public static void ExportToExcel(DataGridView dataGridView)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                Excel.Application application = new Excel.Application();
                application.Visible = false;
                Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
                try
                {
                    for (int i = 1; i < dataGridView.Columns.Count + 1; i++)
                    {
                        application.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dataGridView.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView.Columns.Count; j++)
                        {
                            application.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                        }
                    }

                    // Save the Excel file
                    workbook.SaveAs(saveFileDialog.FileName, Excel.XlFileFormat.xlWorkbookDefault);
                    workbook.Close(true);
                    application.Quit();

                    MessageBox.Show("Дані успішно експортовано!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка: " + ex.Message);
                }
                finally
                {
                    GC.Collect();
                }
            }
        }
    }
}
