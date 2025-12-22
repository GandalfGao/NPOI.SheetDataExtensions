using NPOI.SheetDataExtensions;
using NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures;
using NPOI.SS.UserModel;
using System.Data;
using Xunit.Abstractions;

namespace NPOI.SheetDataExtensionsTests.UnitTests
{
    /// <summary>
    /// 工作表写入测试基类
    /// </summary>
    public abstract class SheetWriterTestBase
    {
        private readonly TestSheetWriterFixtureBase testSheetWriterFixtureBase;
        private readonly ITestOutputHelper outputHelper;

        public SheetWriterTestBase(TestSheetWriterFixtureBase testSheetWriterFixtureBase, ITestOutputHelper outputHelper)
        {
            this.testSheetWriterFixtureBase = testSheetWriterFixtureBase;
            this.outputHelper = outputHelper;
        }

        /// <summary>
        /// 当DataTable是null时, 应抛出ArgumentNullException异常
        /// </summary>
        public virtual void Test_Write_WhenDataTableIsNull()
        {
            DataTable dataTable = null!;

            var sheet = testSheetWriterFixtureBase.Sheet2;

            var sheetWriter = new SheetWriter(sheet);
            var ex = Assert.Throws<ArgumentNullException>(() => { sheetWriter.Write(dataTable); });
            outputHelper.WriteLine(ex.Message);
        }

        /// <summary>
        /// 当DataTable列为空时, 应抛出ArgumentException异常
        /// </summary>
        public virtual void Test_Write_WhenDataColumnCountIsZero()
        {
            var dataTable = new DataTable();

            var sheet = testSheetWriterFixtureBase.Sheet2;

            var sheetWriter = new SheetWriter(sheet);
            var ex = Assert.Throws<ArgumentException>(() => { sheetWriter.Write(dataTable); });
            outputHelper.WriteLine(ex.Message);
        }

        /// <summary>
        /// 当首行索引值为负数的时候, 应抛出ArgumentException异常
        /// </summary>
        public virtual void Test_Write_WhenFirstRowIndexIsNegative()
        {
            var dataTable = CreateDataTable();

            var sheet = testSheetWriterFixtureBase.Sheet2;

            var sheetWriter = new SheetWriter(sheet);
            var ex = Assert.Throws<ArgumentException>(() => { sheetWriter.Write(dataTable, firstRowIndex: -1); });
            outputHelper.WriteLine(ex.Message);
        }

        /// <summary>
        /// 当首列索引值为负数的时候, 应抛出ArgumentException异常
        /// </summary>
        public virtual void Test_Write_WhenFirstColIndexIsNegative()
        {
            var dataTable = CreateDataTable();

            var sheet = testSheetWriterFixtureBase.Sheet2;

            var sheetWriter = new SheetWriter(sheet);
            var ex = Assert.Throws<ArgumentException>(() => { sheetWriter.Write(dataTable, firstColIndex: -1); });
            outputHelper.WriteLine(ex.Message);
        }

        /// <summary>
        /// 测试写入工作表
        /// </summary>
        /// <param name="sheetIndex">工作表索引</param>
        /// <param name="firstRowIndex">首行索引</param>
        /// <param name="firstColIndex">首列索引</param>
        /// <param name="hasHeader">是否包含头部</param>
        /// <param name="setSheetStyleFunc">设置工作表样式委托</param>
        public virtual void Test_Write(int sheetIndex, int firstRowIndex, int firstColIndex, bool hasHeader, Action<ISheet>? setSheetStyleFunc)
        {
            var dataTable = setSheetStyleFunc == null ? CreateDataTable() : CreateDataTablePlus(firstRowIndex, firstColIndex);

            var type = testSheetWriterFixtureBase.GetType();
            var prop = type.GetProperty($"Sheet{sheetIndex}");
            var sheet = (ISheet)prop!.GetValue(testSheetWriterFixtureBase)!;

            var sheetWriter = new SheetWriter(sheet);
            sheetWriter.Write(dataTable, firstRowIndex, firstColIndex, hasHeader, setSheetStyleFunc);

            int rowIndex = firstRowIndex;
            int columnIndex = firstColIndex;
            int dataColumnsCount = dataTable.Columns.Count;

            if (hasHeader)
            {
                //校验header是否合理
                var headerRow = sheet.GetRow(rowIndex++);
                //校验column数量
                Assert.Equal(dataColumnsCount, headerRow.Cells.Count);
                //校验column内容
                for (int i = 0; i < dataColumnsCount; i++)
                {
                    var dataColumn = dataTable.Columns[i];
                    var cell = headerRow.GetCell(columnIndex++);
                    Assert.Equal(dataColumn.ColumnName, cell.StringCellValue);
                }
            }

            //校验数据行数是否合理(包含的表头行去掉)
            Assert.Equal(dataTable.Rows.Count, sheet.Count() - (hasHeader ? 1 : 0));
            //校验每行的内容
            for (int i = 0; i < 6; i++)
            {
                var row = sheet.GetRow(rowIndex++);
                var dataRow = dataTable.Rows[i];
                //校验列数是否合理
                Assert.Equal(dataColumnsCount, row.Cells.Count);
                //校验内容
                columnIndex = firstColIndex;
                for (int j = 0; j < dataColumnsCount; j++)
                {
                    var dataVal = dataRow[dataTable.Columns[j]].ToString();
                    var cell = row.GetCell(columnIndex++);
                    var val = cell.ToString();
                    if (cell.CellType != CellType.Formula)
                    {
                        Assert.Equal(dataVal, val);
                    }
                    else
                    {
                        Assert.Equal(dataVal![1..], val);
                    }
                }
            }

            if (setSheetStyleFunc != null)
            {
                var sheetReader = new SheetReader(sheet);
                var resDataTable = sheetReader.Read(7);
                var colsCount = resDataTable.Columns.Count;
                var colsList = new List<string>(colsCount);
                for (int i = 0; i < colsCount; i++)
                {
                    colsList.Add(resDataTable.Columns[i].ColumnName);
                }
                outputHelper.WriteLine(string.Join(", ", colsList));

                foreach (DataRow row in resDataTable.Rows)
                {
                    var list = new List<object>();
                    for (int i = 0; i < colsCount; i++)
                    {
                        var col = resDataTable.Columns[i];
                        var val = row[col];
                        list.Add(val);
                    }
                    outputHelper.WriteLine(string.Join(", ", list));
                }
            }
        }

        /// <summary>
        /// 创建DataTable数据
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            var table = new DataTable();

            //添加列
            table.Columns.Add("序号");
            table.Columns.Add("姓名");
            table.Columns.Add("年龄");

            //添加行
            var row1 = table.NewRow();
            row1.ItemArray = ["1", "张三", 20];
            table.Rows.Add(row1);

            var row2 = table.NewRow();
            row2.ItemArray = ["2", "李四", 21];
            table.Rows.Add(row2);

            var row3 = table.NewRow();
            row3.ItemArray = ["3", "王五", 25];
            table.Rows.Add(row3);

            var row4 = table.NewRow();
            row4.ItemArray = ["A", "赵六", 19];
            table.Rows.Add(row4);

            var row5 = table.NewRow();
            row5.ItemArray = ["B", "田七", 23];
            table.Rows.Add(row5);

            var row6 = table.NewRow();
            row6.ItemArray = ["C", "刘八", 24];
            table.Rows.Add(row6);

            return table;
        }

        /// <summary>
        /// 创建DataTable数据
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTablePlus(int firstRowIndex, int firstColIndex)
        {
            //设置行数和列数
            int rowNo = firstRowIndex + 3;
            int startColNo = firstColIndex + 5;
            int endColNo = firstRowIndex + 8;
            //设置列字母的ascii码值
            char startColCode = (char)(startColNo + 64);
            char endColCode = (char)(endColNo + 64);

            var table = new DataTable("employees");

            //添加列
            table.Columns.Add("部门");

            table.Columns.Add("工号");
            table.Columns.Add("姓名");
            table.Columns.Add("年龄");

            table.Columns.Add("奖金");
            table.Columns.Add("");
            table.Columns.Add("");
            table.Columns.Add("");
            table.Columns.Add("");

            //添加行
            var row1 = table.NewRow();
            row1.ItemArray = ["", "", "", "", "第一季度", "第二季度", "第三季度", "第四季度", "今年奖金总数"];
            table.Rows.Add(row1);

            var row2 = table.NewRow();
            row2.ItemArray = ["销售部", 1, "张明伟", 25, 100, 200, 500, 600, $"=SUM({startColCode}{rowNo}:{endColCode}{rowNo++})"];
            table.Rows.Add(row2);

            var row3 = table.NewRow();
            row3.ItemArray = ["销售部", 2, "王晓琳", 23, 150, 100, 510, 100, $"=SUM({startColCode}{rowNo}:{endColCode}{rowNo++})"];
            table.Rows.Add(row3);

            var row4 = table.NewRow();
            row4.ItemArray = ["销售部", 3, "李国华", 26, 80, 120, 400, 300, $"=SUM({startColCode}{rowNo}:{endColCode}{rowNo++})"];
            table.Rows.Add(row4);

            var row5 = table.NewRow();
            row5.ItemArray = ["技术部", 'A', "刘思雨", 22, 180, 220, 440, 170, $"=SUM({startColCode}{rowNo}:{endColCode}{rowNo++})"];
            table.Rows.Add(row5);

            var row6 = table.NewRow();
            row6.ItemArray = ["技术部", 'B', "黄俊杰", 22, 380, 420, 140, 190, $"=SUM({startColCode}{rowNo}:{endColCode}{rowNo++})"];
            table.Rows.Add(row6);

            var row7 = table.NewRow();
            row7.ItemArray = ["技术部", 'C', "赵雅婷", 24, 310, 240, 430, 930, $"=SUM({startColCode}{rowNo}:{endColCode}{rowNo++})"];
            table.Rows.Add(row7);

            return table;
        }
    }
}
