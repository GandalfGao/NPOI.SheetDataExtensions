using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
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
            var dataTable = CreateExpectedDataTable();

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
            var dataTable = CreateExpectedDataTable();

            var sheet = testSheetWriterFixtureBase.Sheet2;

            var sheetWriter = new SheetWriter(sheet);
            var ex = Assert.Throws<ArgumentException>(() => { sheetWriter.Write(dataTable, firstColIndex: -1); });
            outputHelper.WriteLine(ex.Message);
        }

        /// <summary>
        /// 创建DataTable数据
        /// </summary>
        /// <returns></returns>
        private DataTable CreateExpectedDataTable()
        {
            var table = new DataTable();

            //添加列
            table.Columns.Add("序号");
            table.Columns.Add("姓名");
            table.Columns.Add("年龄");

            //添加行
            var row1 = table.NewRow();
            row1.ItemArray = [1, "张三", 20];
            table.Rows.Add(row1);

            var row2 = table.NewRow();
            row2.ItemArray = [2, "李四", 21];
            table.Rows.Add(row2);

            var row3 = table.NewRow();
            row3.ItemArray = [3, "王五", 25];
            table.Rows.Add(row3);

            var row4 = table.NewRow();
            row4.ItemArray = [4, "赵六", 19];
            table.Rows.Add(row4);

            var row5 = table.NewRow();
            row5.ItemArray = [5, "田七", 23];
            table.Rows.Add(row5);

            var row6 = table.NewRow();
            row6.ItemArray = [6, "刘八", 24];
            table.Rows.Add(row6);

            return table;
        }
    }
}
