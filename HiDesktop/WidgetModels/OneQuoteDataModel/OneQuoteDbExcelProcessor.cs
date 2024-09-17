using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.XSSF;
using NPOI.SS.Util;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using System.Windows.Forms;
using System.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;

namespace Widgets.MVP.WidgetModels.OneQuoteDataModel
{
    public class OneQuoteDbExcelProcessor
    {
        public string FilePath;
        public string ID_KEY, Text_KEY, Author_KEY, Date_KEY, Ahead_KEY, TextFont_KEY, AuthorFont_KEY, ColorID_KEY, BackColor_KEY, TextColor_KEY, AuthorColor_KEY;

        bool initialized = false;
        public OneQuoteDbExcelProcessor(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("The specific file does not exists.");
            }
            FilePath = filePath;
        }
        public OneQuoteDbExcelProcessor(string filePath, bool createIfNotExist)
        {
            if (!File.Exists(filePath))
            {
                if (createIfNotExist)
                {
                    CreateExcelDb(filePath);
                }
                else
                {
                    throw new ArgumentException("The specific file does not exists.");
                }
                
            }
            FilePath = filePath;
        }

        /// <summary>
        /// 从Data表中读取数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DbDataRowObj[] GetDatasFromDatas()
        {
            if (!initialized)
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                XSSFWorkbook wb = new(fs);
                ISheet data = wb.GetSheet("Data");
                if (data == null)
                {
                    throw new FileFormatException("Sheet 'Data' not found.");
                }

                IRow headers = data.GetRow(0);
                int idCol = -1;
                int textCol = -1;
                int authorCol = -1;
                int dateCol = -1;
                int aheadCol = -1;
                int textFontCol = -1;
                int authorFontCol = -1;
                int colorIDCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Text_KEY)
                    {
                        textCol = item.ColumnIndex;
                    }
                    else if (col == Author_KEY)
                    {
                        authorCol = item.ColumnIndex;
                    }
                    else if (col == Date_KEY)
                    {
                        dateCol = item.ColumnIndex;
                    }
                    else if (col == Ahead_KEY)
                    {
                        aheadCol = item.ColumnIndex;
                    }
                    else if (col == TextFont_KEY)
                    {
                        textFontCol = item.ColumnIndex;
                    }
                    else if (col == AuthorFont_KEY)
                    {
                        authorFontCol = item.ColumnIndex;
                    }
                    else if (col == ColorID_KEY)
                    {
                        colorIDCol = item.ColumnIndex;
                    }
                }
                if (idCol == -1 || textCol == -1 || authorCol == -1 || dateCol == -1 || aheadCol == -1 || textFontCol == -1 || authorFontCol == -1 || colorIDCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }


                DbDataRowObj[] result = new DbDataRowObj[data.LastRowNum];
                int i = 0;
                bool firstFlag = true;
                foreach (IRow row in data) 
                {
                    if (firstFlag)
                    {
                        firstFlag = false;
                        continue;
                    }
                    DbDataRowObj obj = new();
                    if ((row.GetCell(idCol) == null ? "" : row.GetCell(idCol).ToString()) == "") 
                    {
                        continue;
                    }
                    obj.ID = Convert.ToInt32((row.GetCell(idCol) == null ? "" : row.GetCell(idCol).ToString()));
                    obj.Text = (row.GetCell(textCol) == null ? "" : row.GetCell(textCol).ToString());
                    obj.Author = (row.GetCell(authorCol) == null ? "" : row.GetCell(authorCol).ToString());
                    obj.Date = (row.GetCell(dateCol) == null ? "" : row.GetCell(dateCol).ToString());
                    obj.Ahead = (row.GetCell(aheadCol) == null ? "" : row.GetCell(aheadCol).ToString());
                    obj.TextFont = (row.GetCell(textFontCol) == null ? "" : row.GetCell(textFontCol).ToString());
                    obj.AuthorFont = (row.GetCell(authorFontCol) == null ? "" : row.GetCell(authorFontCol).ToString());
                    obj.ColorID = (row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString());
                    result[i] = obj;
                    i++;
                }
                return result;
            }
        }


        /// <summary>
        /// 从Color表中读取数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DbDataColorObj[] GetDatasFromColor()
        {
            if (!initialized)
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                XSSFWorkbook wb = new(fs);
                ISheet data = wb.GetSheet("Colors");
                if (data == null)
                {
                    throw new FileFormatException("Sheet 'Colors' not found.");
                }

                IRow headers = data.GetRow(0);
                int colorIDCol = -1;
                int backColorCol = -1;
                int textColorCol = -1;
                int authorColorCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ColorID_KEY)
                    {
                        colorIDCol = item.ColumnIndex;
                    }
                    else if (col == BackColor_KEY)
                    {
                        backColorCol = item.ColumnIndex;
                    }
                    else if (col == TextColor_KEY)
                    {
                        textColorCol = item.ColumnIndex;
                    }
                    else if (col == AuthorColor_KEY)
                    {
                        authorColorCol = item.ColumnIndex;
                    }
                }
                if (colorIDCol == -1 || backColorCol == -1 || textColorCol == -1 || authorColorCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }


                DbDataColorObj[] result = new DbDataColorObj[data.LastRowNum];
                int i = 0;
                bool firstFlag = true;
                foreach (IRow row in data)
                {
                    if (firstFlag)
                    {
                        firstFlag = false;
                        continue;
                    }
                    if ((row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString()) == "")
                    {
                        continue;
                    }
                    DbDataColorObj obj = new();
                    obj.ColorID = (row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString());
                    obj.BackColor = (row.GetCell(backColorCol) == null ? "" : row.GetCell(backColorCol).ToString());
                    obj.TextColor = (row.GetCell(textColorCol) == null ? "" : row.GetCell(textColorCol).ToString());
                    obj.AuthorColor = (row.GetCell(authorColorCol) == null ? "" : row.GetCell(authorColorCol).ToString());
                    result[i] = obj;
                    i++;
                }
                return result;
            }
        }


        /// <summary>
        /// 通过ID写入数据到Data
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dataObj">待写入数据</param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void ModifyToDataByID(DbDataRowObj dataObj)
        {
            if (!initialized) 
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new(fs);
                ISheet data = wb.GetSheet("Data");
                if (data == null)
                {
                    throw new FileFormatException("Sheet 'Data' not found.");
                }

                IRow headers = data.GetRow(0);
                int idCol = -1;
                int textCol = -1;
                int authorCol = -1;
                int dateCol = -1;
                int aheadCol = -1;
                int textFontCol = -1;
                int authorFontCol = -1;
                int colorIDCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Text_KEY)
                    {
                        textCol = item.ColumnIndex;
                    }
                    else if (col == Author_KEY)
                    {
                        authorCol = item.ColumnIndex;
                    }
                    else if (col == Date_KEY)
                    {
                        dateCol = item.ColumnIndex;
                    }
                    else if (col == Ahead_KEY)
                    {
                        aheadCol = item.ColumnIndex;
                    }
                    else if (col == TextFont_KEY)
                    {
                        textFontCol = item.ColumnIndex;
                    }
                    else if (col == AuthorFont_KEY)
                    {
                        authorFontCol = item.ColumnIndex;
                    }
                    else if (col == ColorID_KEY)
                    {
                        colorIDCol = item.ColumnIndex;
                    }
                }
                if (idCol == -1 || textCol == -1 || authorCol == -1 || dateCol == -1 || aheadCol == -1 || textFontCol == -1 || authorFontCol == -1 || colorIDCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }


                foreach (IRow row in data)
                {
                    if ((row.GetCell(idCol) == null ? "" : row.GetCell(idCol).ToString()) == dataObj.ID.ToString())
                    {
                        (row.GetCell(textCol) ?? row.CreateCell(textCol)).SetCellValue(dataObj.Text);
                        (row.GetCell(authorCol) ?? row.CreateCell(authorCol)).SetCellValue(dataObj.Author);
                        (row.GetCell(dateCol) ?? row.CreateCell(dateCol)).SetCellValue(dataObj.Date);
                        (row.GetCell(aheadCol) ?? row.CreateCell(aheadCol)).SetCellValue(dataObj.Ahead);
                        (row.GetCell(textFontCol) ?? row.CreateCell(textFontCol)).SetCellValue(dataObj.TextFont);
                        (row.GetCell(authorFontCol) ?? row.CreateCell(authorFontCol)).SetCellValue(dataObj.AuthorFont);
                        (row.GetCell(colorIDCol) ?? row.CreateCell(colorIDCol)).SetCellValue(dataObj.ColorID);
                        row.GetCell(textCol).SetCellValue(dataObj.Text);
                        row.GetCell(authorCol).SetCellValue(dataObj.Author);
                        row.GetCell(dateCol).SetCellValue(dataObj.Date);
                        row.GetCell(aheadCol).SetCellValue(dataObj.Ahead);
                        row.GetCell(textFontCol).SetCellValue(dataObj.TextFont);
                        row.GetCell(authorFontCol).SetCellValue(dataObj.AuthorFont);
                        row.GetCell(colorIDCol).SetCellValue(dataObj.ColorID);

                        using (FileStream writeFS = new(FilePath, FileMode.Create, FileAccess.Write))
                        {
                            wb.Write(writeFS);
                        }
                        
                        return;
                    }
                }
                var row2 = data.CreateRow(data.LastRowNum + 1);
                row2.CreateCell(idCol).SetCellValue(dataObj.ID);
                row2.CreateCell(textCol).SetCellValue(dataObj.Text);
                row2.CreateCell(authorCol).SetCellValue(dataObj.Author);
                row2.CreateCell(dateCol).SetCellValue(dataObj.Date);
                row2.CreateCell(aheadCol).SetCellValue(dataObj.Ahead);
                row2.CreateCell(textFontCol).SetCellValue(dataObj.TextFont);
                row2.CreateCell(authorFontCol).SetCellValue(dataObj.AuthorFont);
                row2.CreateCell(colorIDCol).SetCellValue(dataObj.ColorID);

                using (FileStream writeFS = new(FilePath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(writeFS);
                }
            }
        }


        /// <summary>
        /// 通过ColorID写入数据到Colors
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dataObj">待写入数据</param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void ModifyToColorByColorID(DbDataColorObj dataObj)
        {
            if (!initialized)
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new(fs);
                ISheet data = wb.GetSheet("Colors");
                if (data == null)
                {
                    throw new FileFormatException("Sheet 'Colors' not found.");
                }

                IRow headers = data.GetRow(0);
                int colorIDCol = -1;
                int backColorCol = -1;
                int textColorCol = -1;
                int authorColorCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ColorID_KEY)
                    {
                        colorIDCol = item.ColumnIndex;
                    }
                    else if (col == BackColor_KEY)
                    {
                        backColorCol = item.ColumnIndex;
                    }
                    else if (col == TextColor_KEY)
                    {
                        textColorCol = item.ColumnIndex;
                    }
                    else if (col == AuthorColor_KEY)
                    {
                        authorColorCol = item.ColumnIndex;
                    }
                }
                if (colorIDCol == -1 || backColorCol == -1 || textColorCol == -1 || authorColorCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }


                foreach (IRow row in data)
                {
                    if ((row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString()) == dataObj.ColorID.ToString())
                    {
                        (row.GetCell(backColorCol) ?? row.CreateCell(backColorCol)).SetCellValue(dataObj.BackColor);
                        (row.GetCell(textColorCol) ?? row.CreateCell(textColorCol)).SetCellValue(dataObj.TextColor);
                        (row.GetCell(authorColorCol) ?? row.CreateCell(authorColorCol)).SetCellValue(dataObj.AuthorColor);
                        row.GetCell(colorIDCol).SetCellValue(dataObj.ColorID);
                        row.GetCell(backColorCol).SetCellValue(dataObj.BackColor);
                        row.GetCell(textColorCol).SetCellValue(dataObj.TextColor);
                        row.GetCell(authorColorCol).SetCellValue(dataObj.AuthorColor);

                        using (FileStream writeFS = new(FilePath, FileMode.Create, FileAccess.Write))
                        {
                            wb.Write(writeFS);
                        }

                        return;
                    }
                }
                var row2 = data.CreateRow(data.LastRowNum + 1);
                row2.CreateCell(colorIDCol).SetCellValue(dataObj.ColorID);
                row2.CreateCell(backColorCol).SetCellValue(dataObj.BackColor);
                row2.CreateCell(textColorCol).SetCellValue(dataObj.TextColor);
                row2.CreateCell(authorColorCol).SetCellValue(dataObj.AuthorColor);

                using (FileStream writeFS = new(FilePath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(writeFS);
                }
            }
        }




        /// <summary>
        /// 通过ID从Data获取相应的行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public DbDataRowObj GetRowFromDataByID(int ID)
        {
            if (!initialized)
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new(fs);
                ISheet data = wb.GetSheet("Data");
                if (data == null)
                {
                    throw new FileFormatException("Sheet 'Data' not found.");
                }

                IRow headers = data.GetRow(0);
                int idCol = -1;
                int textCol = -1;
                int authorCol = -1;
                int dateCol = -1;
                int aheadCol = -1;
                int textFontCol = -1;
                int authorFontCol = -1;
                int colorIDCol = -1;

                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Text_KEY)
                    {
                        textCol = item.ColumnIndex;
                    }
                    else if (col == Author_KEY)
                    {
                        authorCol = item.ColumnIndex;
                    }
                    else if (col == Date_KEY)
                    {
                        dateCol = item.ColumnIndex;
                    }
                    else if (col == Ahead_KEY)
                    {
                        aheadCol = item.ColumnIndex;
                    }
                    else if (col == TextFont_KEY)
                    {
                        textFontCol = item.ColumnIndex;
                    }
                    else if (col == AuthorFont_KEY)
                    {
                        authorFontCol = item.ColumnIndex;
                    }
                    else if (col == ColorID_KEY)
                    {
                        colorIDCol = item.ColumnIndex;
                    }
                }

                if (idCol == -1 || textCol == -1 || authorCol == -1 || dateCol == -1 || aheadCol == -1 || textFontCol == -1 || authorFontCol == -1 || colorIDCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }


                foreach (IRow row in data)
                {
                    if ((row.GetCell(idCol) == null ? "" : row.GetCell(idCol).ToString()) == ID.ToString())
                    {
                        DbDataRowObj obj = new();
                        obj.ID = Convert.ToInt32((row.GetCell(idCol) == null ? "" : row.GetCell(idCol).ToString()));
                        obj.Text = (row.GetCell(textCol) == null ? "" : row.GetCell(textCol).ToString());
                        obj.Author = (row.GetCell(authorCol) == null ? "" : row.GetCell(authorCol).ToString());
                        obj.Date = (row.GetCell(dateCol) == null ? "" : row.GetCell(dateCol).ToString());
                        obj.Ahead = (row.GetCell(aheadCol) == null ? "" : row.GetCell(aheadCol).ToString());
                        obj.TextFont = (row.GetCell(textFontCol) == null ? "" : row.GetCell(textFontCol).ToString());
                        obj.AuthorFont = (row.GetCell(authorFontCol) == null ? "" : row.GetCell(authorFontCol).ToString());
                        obj.ColorID = (row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString());

                        return obj;
                    }
                }

                throw new KeyNotFoundException("The specific ID is not found.");
            }
        }

        /// <summary>
        /// 通过ColorID从Colors获取相应的行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public DbDataColorObj GetRowFromColorByID(string ColorID)
        {
            if (!initialized)
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new(fs);
                ISheet data = wb.GetSheet("Colors");
                if (data == null)
                {
                    throw new FileFormatException("Sheet 'Colors' not found.");
                }

                IRow headers = data.GetRow(0);
                int colorIDCol = -1;
                int backColorCol = -1;
                int textColorCol = -1;
                int authorColorCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ColorID_KEY)
                    {
                        colorIDCol = item.ColumnIndex;
                    }
                    else if (col == BackColor_KEY)
                    {
                        backColorCol = item.ColumnIndex;
                    }
                    else if (col == TextColor_KEY)
                    {
                        textColorCol = item.ColumnIndex;
                    }
                    else if (col == AuthorColor_KEY)
                    {
                        authorColorCol = item.ColumnIndex;
                    }
                }
                if (colorIDCol == -1 || backColorCol == -1 || textColorCol == -1 || authorColorCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }

                if (colorIDCol == -1 || backColorCol == -1 || textColorCol == -1 || authorColorCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }


                foreach (IRow row in data)
                {
                    if ((row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString()) == ColorID)
                    {
                        DbDataColorObj obj = new();
                        obj.ColorID = (row.GetCell(colorIDCol) == null ? "" : row.GetCell(colorIDCol).ToString());
                        obj.BackColor = (row.GetCell(backColorCol) == null ? "" : row.GetCell(backColorCol).ToString());
                        obj.TextColor = (row.GetCell(textColorCol) == null ? "" : row.GetCell(textColorCol).ToString());
                        obj.AuthorColor = (row.GetCell(authorColorCol) == null ? "" : row.GetCell(authorColorCol).ToString());

                        return obj;
                    }
                }

                throw new KeyNotFoundException("The specific ColorID is not found.");
            }
        }


        /// <summary>
        /// 绑定数据到DataGridView
        /// </summary>
        /// <param name="dgv">目标控件</param>
        /// <exception cref="FileFormatException"></exception>
        public void BindDataToDataGridView(DataGridView dgv)
        {
            if (!initialized)
            {
                throw new ArgumentNullException("The excel processor has not been initialized yet.");
            }
            DataTable dataTable = new DataTable();

            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(fileStream);
                ISheet sheet = workbook.GetSheet("Data"); // 读取名为"Data"的表

                if (sheet == null)
                {
                    throw new FileFormatException("Sheet 'Data' not found.");
                }

                IRow headerRow = sheet.GetRow(0);

                // 添加列到DataTable
                foreach (ICell cell in headerRow.Cells)
                {
                    dataTable.Columns.Add(cell.ToString());
                }

                // 添加行到DataTable
                for (int rowIndex = 1; rowIndex <= sheet.LastRowNum; rowIndex++)
                {
                    IRow row = sheet.GetRow(rowIndex);
                    if (row == null) continue;

                    DataRow dataRow = dataTable.NewRow();
                    for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                    {
                        dataRow[cellIndex] = row.GetCell(cellIndex)?.ToString();
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            // 将DataTable绑定到DataGridView
            dgv.DataSource = dataTable;
        }


        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FileFormatException"></exception>
        public void Initialize()
        {
            using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new(fileStream);
                ISheet configSheet = workbook.GetSheet("Config");
                if (configSheet == null) 
                {
                    throw new ArgumentException("Illegal Excel database: Config not found.");
                }
                // 第一行是标题行
                IRow headerRow = configSheet.GetRow(0);
                int keyIndex = -1;
                int valueIndex = -1;

                // 寻找Key和Value列的索引
                for (int i = 0; i < headerRow.Cells.Count; i++)
                {
                    if (headerRow.GetCell(i).ToString() == "KEY")
                        keyIndex = i;
                    if (headerRow.GetCell(i).ToString() == "VALUE")
                        valueIndex = i;
                }

                if (keyIndex == -1 || valueIndex == -1)
                {
                    throw new FileFormatException("Key or Value column not found.");
                    
                }

                // 读取每一行
                for (int rowIndex = 1; rowIndex <= configSheet.LastRowNum; rowIndex++)
                {
                    IRow row = configSheet.GetRow(rowIndex);
                    if (row == null) continue;

                    string key = row.GetCell(keyIndex)?.ToString();

                    if (key == "Type")
                    {
                        string type = row.GetCell(valueIndex)?.ToString();
                        if (type != "OneQuoteDb")
                        {
                            throw new ArgumentException("Illegal Excel database: Invaild Config type.");
                        }
                    }
                    if (key == "ID_KEY")
                    {
                        ID_KEY = row.GetCell(valueIndex)?.ToString();
                        
                    }
                    if (key == "Text_KEY")
                    {
                        Text_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "Author_KEY")
                    {
                        Author_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "Date_KEY")
                    {
                        Date_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "Ahead_KEY")
                    {
                        Ahead_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "TextFont_KEY")
                    {
                        TextFont_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "AuthorFont_KEY")
                    {
                        AuthorFont_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "ColorID_KEY")
                    {
                        ColorID_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "BackColor_KEY")
                    {
                        BackColor_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "TextColor_KEY")
                    {
                        TextColor_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "AuthorColor_KEY")
                    {
                        AuthorColor_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                }
            }
            initialized = true;
        }


        /// <summary>
        /// 创建模拟数据库表
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void CreateExcelDb(string filePath)
        {
            XSSFWorkbook wb = new XSSFWorkbook();
            ISheet configSheet = wb.CreateSheet("Config");
            IRow headers = configSheet.CreateRow(0);
            headers.CreateCell(0).SetCellValue("KEY");
            headers.CreateCell(1).SetCellValue("VALUE");
            string[,] configData = new string[,]
            {
                { "Reminder", "如不清楚用途，请勿修改此表内容，否则数据库结构可能遭到损坏！" },
                { "Type", "OneQuoteDb" },
                { "Version", "1" },
                { "ID_KEY", "ID" },
                { "Text_KEY", "Text" },
                { "Author_KEY", "Author" },
                { "Date_KEY", "Date" },
                { "Ahead_KEY", "Ahead" },
                { "TextFont_KEY", "TextFont" },
                { "AuthorFont_KEY", "AuthorFont" },
                { "ColorID_KEY", "ColorID" },
                { "BackColor_KEY", "BackColor" },
                { "TextColor_KEY", "TextColor" },
                { "AuthorColor_KEY", "AuthorColor" },
                { "SoftwareCopyright", "HiDesktop.Widgets.MVP by teko.IO SisTemS 2024" }
            };
            for (int i = 0; i < configData.GetLength(0); i++)
            {
                IRow row = configSheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(configData[i, 0]);
                row.CreateCell(1).SetCellValue(configData[i, 1]);
            }
            // 创建“Data”工作表
            ISheet dataSheet = wb.CreateSheet("Data");

            // 创建“Data”表头行
            IRow dataHeaderRow = dataSheet.CreateRow(0);
            dataHeaderRow.CreateCell(0).SetCellValue("ID");
            dataHeaderRow.CreateCell(1).SetCellValue("Text");
            dataHeaderRow.CreateCell(2).SetCellValue("Author");
            dataHeaderRow.CreateCell(3).SetCellValue("Date");
            dataHeaderRow.CreateCell(4).SetCellValue("Ahead");
            dataHeaderRow.CreateCell(5).SetCellValue("TextFont");
            dataHeaderRow.CreateCell(6).SetCellValue("AuthorFont");
            dataHeaderRow.CreateCell(7).SetCellValue("ColorID");

            IRow dataHeaderRowEx = dataSheet.CreateRow(1);
            dataHeaderRowEx.CreateCell(0).SetCellValue("1");
            dataHeaderRowEx.CreateCell(1).SetCellValue("只是那生命的大雨纷飞，​总不济于这片虚幻到不真实的万里晴空就是了。");
            dataHeaderRowEx.CreateCell(2).SetCellValue("- 幻愿Recovery -");

            // 创建“Colors”工作表
            ISheet dataSheet2 = wb.CreateSheet("Colors");
            

            // 创建“Colors”表头行
            IRow dataHeaderRow2 = dataSheet2.CreateRow(0);
            dataHeaderRow2.CreateCell(0).SetCellValue("ColorID");
            dataHeaderRow2.CreateCell(1).SetCellValue("BackColor");
            dataHeaderRow2.CreateCell(2).SetCellValue("TextColor");
            dataHeaderRow2.CreateCell(3).SetCellValue("AuthorColor");
            IRow dataHeaderRowEx2 = dataSheet2.CreateRow(1);
            dataHeaderRowEx2.CreateCell(0).SetCellValue("Example");
            dataHeaderRowEx2.CreateCell(1).SetCellValue("#C2D7F3");
            dataHeaderRowEx2.CreateCell(2).SetCellValue("#7778CC");
            dataHeaderRowEx2.CreateCell(3).SetCellValue("#7778CC");


            //save
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                wb.Write(fileStream);
            }
        }
    }
}
