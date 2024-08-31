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

namespace Widgets.MVP.WindowApps.RandomItemsDataModel
{
    public class RandomItemsDbExcelProcessor
    {
        public string FilePath;
        public string ID_KEY;
        public string Name_KEY;
        public string Tags_KEY, PoolWeight_KEY;
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
                    if (headerRow.GetCell(i).ToString() == "Key")
                        keyIndex = i;
                    if (headerRow.GetCell(i).ToString() == "Value")
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
                { "PoolWeight_KEY", "PoolWeight" }
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
