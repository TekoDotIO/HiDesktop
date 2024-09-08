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
using Widgets.MVP.WindowApps.RandomItemsDataModel.ExcelDataExchangeModels;

namespace Widgets.MVP.WindowApps.RandomItemsDataModel
{
    public class RandomItemsDbExcelProcessor
    {
        public string FilePath;
        public string ID_KEY;
        public string Name_KEY;
        public string Tags_KEY, PoolWeight_KEY;
        bool initialized = false;
        public RandomItemsDbExcelProcessor(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("The specific file does not exists.");
            }
            FilePath = filePath;
        }
        public RandomItemsDbExcelProcessor(string filePath, bool createIfNotExist)
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
        /// 从表中读取数据
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public DbDataRowObj[] GetDatas()
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
                int nameCol = -1;
                int tagsCol = -1;
                int pwCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Name_KEY)
                    {
                        nameCol = item.ColumnIndex;
                    }
                    else if (col == Tags_KEY)
                    {
                        tagsCol = item.ColumnIndex;
                    }
                    else if (col == PoolWeight_KEY)
                    {
                        pwCol = item.ColumnIndex;
                    }
                }
                if (idCol == -1 || nameCol == -1 || tagsCol == -1 || pwCol == -1)
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
                    DbDataRowObj obj = new()
                    {
                        ID = Convert.ToInt32(row.GetCell(idCol).ToString()),
                        Name = row.GetCell(nameCol).ToString(),
                        Tags = row.GetCell(tagsCol).ToString(),
                        PoolWeight = Convert.ToInt32(row.GetCell(pwCol).ToString())
                    };
                    result[i] = obj;
                    i++;
                }
                return result;
            }
        }

        /// <summary>
        /// 通过ID写入数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="dataObj">待写入数据</param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public void ModifyByID(DbDataRowObj dataObj)
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
                int nameCol = -1;
                int tagsCol = -1;
                int pwCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Name_KEY)
                    {
                        nameCol = item.ColumnIndex;
                    }
                    else if (col == Tags_KEY)
                    {
                        tagsCol = item.ColumnIndex;
                    }
                    else if (col == PoolWeight_KEY)
                    {
                        pwCol = item.ColumnIndex;
                    }
                }
                if (idCol == -1 || nameCol == -1 || tagsCol == -1 || pwCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }

                foreach (IRow row in data)
                {
                    if (row.GetCell(idCol).ToString() == dataObj.ID.ToString())
                    {
                        row.GetCell(nameCol).SetCellValue(dataObj.Name);
                        row.GetCell(tagsCol).SetCellValue(dataObj.Tags);
                        row.GetCell(pwCol).SetCellValue(dataObj.PoolWeight);
                        using (FileStream writeFS = new(FilePath, FileMode.Create, FileAccess.Write))
                        {
                            wb.Write(writeFS);
                        }
                        
                        return;
                    }
                }
                var row2 = data.CreateRow(data.LastRowNum + 1);
                row2.CreateCell(idCol).SetCellValue(dataObj.ID);
                row2.CreateCell(nameCol).SetCellValue(dataObj.Name);
                row2.CreateCell(tagsCol).SetCellValue(dataObj.Tags);
                row2.CreateCell(pwCol).SetCellValue(dataObj.PoolWeight);
                using (FileStream writeFS = new(FilePath, FileMode.Create, FileAccess.Write))
                {
                    wb.Write(writeFS);
                }
            }
        }

        /// <summary>
        /// 通过Name获取相应的行
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public DbDataRowObj GetRowByName(string name)
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
                int nameCol = -1;
                int tagsCol = -1;
                int pwCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Name_KEY)
                    {
                        nameCol = item.ColumnIndex;
                    }
                    else if (col == Tags_KEY)
                    {
                        tagsCol = item.ColumnIndex;
                    }
                    else if (col == PoolWeight_KEY)
                    {
                        pwCol = item.ColumnIndex;
                    }
                }
                if (idCol == -1 || nameCol == -1 || tagsCol == -1 || pwCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }

                foreach (IRow row in data)
                {
                    if (row.GetCell(nameCol).ToString() == name)
                    {
                        DbDataRowObj obj = new();
                        obj.ID = Convert.ToInt32(row.GetCell(idCol).ToString());
                        obj.Name = row.GetCell(nameCol).ToString();
                        obj.Tags = row.GetCell(tagsCol).ToString();
                        obj.PoolWeight = Convert.ToInt32(row.GetCell(pwCol).ToString());
                        return obj;
                    }
                }

                throw new KeyNotFoundException("The specific Name is not found.");
            }
        }


        /// <summary>
        /// 通过ID获取相应的行
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        /// <exception cref="FileFormatException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="KeyNotFoundException"></exception>
        public DbDataRowObj GetRowByID(int ID)
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
                int nameCol = -1;
                int tagsCol = -1;
                int pwCol = -1;
                foreach (var item in headers)
                {
                    string col = item.ToString();
                    if (col == ID_KEY)
                    {
                        idCol = item.ColumnIndex;
                    }
                    else if (col == Name_KEY)
                    {
                        nameCol = item.ColumnIndex;
                    }
                    else if (col == Tags_KEY)
                    {
                        tagsCol = item.ColumnIndex;
                    }
                    else if (col == PoolWeight_KEY)
                    {
                        pwCol = item.ColumnIndex;
                    }
                }
                if (idCol == -1 || nameCol == -1 || tagsCol == -1 || pwCol == -1)
                {
                    throw new ArgumentException("One or more required columns not found. Check if the config or column name is modified.");
                }

                foreach (IRow row in data)
                {
                    if (row.GetCell(idCol).ToString() == ID.ToString())
                    {
                        DbDataRowObj obj = new();
                        obj.ID = Convert.ToInt32(row.GetCell(idCol).ToString());
                        obj.Name = row.GetCell(nameCol).ToString();
                        obj.Tags=row.GetCell(tagsCol).ToString();
                        obj.PoolWeight = Convert.ToInt32(row.GetCell(pwCol).ToString());
                        return obj;
                    }
                }

                throw new KeyNotFoundException("The specific ID is not found.");
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
                        if (type != "RandomItemsDb")
                        {
                            throw new ArgumentException("Illegal Excel database: Invaild Config type.");
                        }
                    }
                    if (key == "ID_KEY")
                    {
                        ID_KEY = row.GetCell(valueIndex)?.ToString();
                        
                    }
                    if (key == "Name_KEY")
                    {
                        Name_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "Tags_KEY")
                    {
                        Tags_KEY = row.GetCell(valueIndex)?.ToString();

                    }
                    if (key == "PoolWeight_KEY")
                    {
                        PoolWeight_KEY = row.GetCell(valueIndex)?.ToString();

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
                { "Type", "RandomItemsDb" },
                { "Version", "1" },
                { "ID_KEY", "ID" },
                { "Name_KEY", "Name" },
                { "Tags_KEY", "Tags" },
                { "PoolWeight_KEY", "PoolWeight" },
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
            dataHeaderRow.CreateCell(1).SetCellValue("Name");
            dataHeaderRow.CreateCell(2).SetCellValue("Tags");
            dataHeaderRow.CreateCell(3).SetCellValue("PoolWeight");

            //save
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                wb.Write(fileStream);
            }
        }
    }
}
