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
    /// 工作表读取器测试类
    /// </summary>
    [Collection(nameof(TestFileReaderXlsxCollection))]
    public class SheetReaderXlsxTest
    {
        private readonly TestFileReaderXlsxFixture testXlsxFileReaderFixture;
        private readonly ITestOutputHelper outputHelper;

        public SheetReaderXlsxTest(TestFileReaderXlsxFixture testXlsxFileReaderFixture, ITestOutputHelper outputHelper)
        {
            this.testXlsxFileReaderFixture = testXlsxFileReaderFixture;
            this.outputHelper = outputHelper;
        }

        /// <summary>
        /// 测试当首行索引值小于0时抛出异常
        /// </summary>
        [Fact]
        public void Test_Read_WhenFirstRowIndexIsNegative()
        {
            var sheetReader = new SheetReader(testXlsxFileReaderFixture.Sheet3);
            var ex = Assert.Throws<ArgumentException>(() => sheetReader.Read(6, firstRowIndex: -1));
            outputHelper.WriteLine("当首行索引值小于0时的异常信息: " + ex.Message);
        }

        /// <summary>
        /// 测试当hasHeader为false且columnConfigs为null或空集合时抛出异常
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.EmptyColumnConfigParams), MemberType = typeof(ParamsData))]
        public void Test_Read_WhenHasHeaderIsFalseAndColumnConfigsIsNullOrEmpty(IEnumerable<ColumnConfigAttribute>? columnConfigs)
        {
            var sheetReader = new SheetReader(testXlsxFileReaderFixture.Sheet3);
            var ex = Assert.Throws<ArgumentNullException>(() => sheetReader.Read(6, firstRowIndex: 0, hasHeader: false, columnConfigs: columnConfigs));
            outputHelper.WriteLine("当hasHeader为false且columnConfigs为null或空集合时的异常信息: " + ex.Message);
        }

        /// <summary>
        /// 测试当hasHeader为true且columnConfigs为null或空集合时的读取结果
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.EmptyColumnConfigParams), MemberType = typeof(ParamsData))]
        public void Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty(IEnumerable<ColumnConfigAttribute>? columnConfigs)
        {
            var sheetReader = new SheetReader(testXlsxFileReaderFixture.Sheet3);
            //实际table数据
            var actualTable = sheetReader.Read(6, firstRowIndex: 1, hasHeader: true, columnConfigs: columnConfigs);
            //预计table数据
            var expectedTable = CreateExpectedDataTableWithoutColumnConfigs();

            //校验列数
            Assert.Equal(expectedTable.Columns.Count, actualTable.Columns.Count);
            //校验列信息
            for (int i = 0; i < expectedTable.Columns.Count; i++)
            {
                var expectedColumn = expectedTable.Columns[i];
                var actualColumn = actualTable.Columns[i];
                Assert.Equal(expectedColumn.ColumnName, actualColumn.ColumnName);
                Assert.Equal(expectedColumn.DataType, actualColumn.DataType);
            }

            //校验行数
            Assert.Equal(expectedTable.Rows.Count, actualTable.Rows.Count);
            //校验行数据
            for (int i = 0; i < expectedTable.Rows.Count; i++)
            {
                var expectedRow = expectedTable.Rows[i];
                var actualRow = actualTable.Rows[i];
                for (int j = 0; j < expectedTable.Columns.Count; j++)
                {
                    Assert.Equal(expectedRow[j], actualRow[j]);
                }
            }
        }

        /// <summary>
        /// 测试columnConfigs不为空集合时的读取结果
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.HasHeaderAndColumnConfigsParam), MemberType = typeof(ParamsData))]
        public void Test_Read_WhenColumnConfigsIsNotEmpty(bool hasHeader, IEnumerable<ColumnConfigAttribute> columnConfigs)
        {
            var sheetReader = new SheetReader(testXlsxFileReaderFixture.Sheet3);
            //实际table数据
            int firstRowIndex = hasHeader ? 1 : 2;
            var actualTable = sheetReader.Read(6, firstRowIndex: firstRowIndex, hasHeader: hasHeader, columnConfigs: columnConfigs);
            //预计table数据
            var expectedTable = CreateExpectedDataTableWithColumnConfigs();
            //校验列数
            Assert.Equal(expectedTable.Columns.Count, actualTable.Columns.Count);
            //校验列信息
            for (int i = 0; i < expectedTable.Columns.Count; i++)
            {
                var expectedColumn = expectedTable.Columns[i];
                var actualColumn = actualTable.Columns[i];
                Assert.Equal(expectedColumn.ColumnName, actualColumn.ColumnName);
                Assert.Equal(expectedColumn.DataType, actualColumn.DataType);
            }
            //校验行数
            Assert.Equal(expectedTable.Rows.Count, actualTable.Rows.Count);
            //校验行数据
            for (int i = 0; i < expectedTable.Rows.Count; i++)
            {
                var expectedRow = expectedTable.Rows[i];
                var actualRow = actualTable.Rows[i];
                for (int j = 0; j < expectedTable.Columns.Count; j++)
                {
                    Assert.Equal(expectedRow[j], actualRow[j]);
                }
            }
        }

        /// <summary>
        /// 在工作表中包含空白行和空白列的情况下，测试当hasHeader为true且columnConfigs为null或空集合时的读取结果
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.EmptyColumnConfigParams), MemberType = typeof(ParamsData))]
        public void Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty_WithHasBlankRowsAndCols(IEnumerable<ColumnConfigAttribute>? columnConfigs)
        {
            var sheetReader = new SheetReader(testXlsxFileReaderFixture.Sheet4);
            //实际table数据(包含两个空行)
            var actualTable = sheetReader.Read(6 + 2, firstRowIndex: 1, hasHeader: true, columnConfigs: columnConfigs);
            //预计table数据
            var expectedTable = CreateExpectedDataTableWithoutColumnConfigs();

            //校验列数
            Assert.Equal(expectedTable.Columns.Count, actualTable.Columns.Count);
            //校验列信息
            for (int i = 0; i < expectedTable.Columns.Count; i++)
            {
                var expectedColumn = expectedTable.Columns[i];
                var actualColumn = actualTable.Columns[i];
                Assert.Equal(expectedColumn.ColumnName, actualColumn.ColumnName);
                Assert.Equal(expectedColumn.DataType, actualColumn.DataType);
            }

            //校验行数
            Assert.Equal(expectedTable.Rows.Count, actualTable.Rows.Count);
            //校验行数据
            for (int i = 0; i < expectedTable.Rows.Count; i++)
            {
                var expectedRow = expectedTable.Rows[i];
                var actualRow = actualTable.Rows[i];
                for (int j = 0; j < expectedTable.Columns.Count; j++)
                {
                    Assert.Equal(expectedRow[j], actualRow[j]);
                }
            }
        }

        /// <summary>
        /// 创建预期的数据表(不包含列配置信息)
        /// </summary>
        /// <returns></returns>
        private DataTable CreateExpectedDataTableWithoutColumnConfigs()
        {
            var table = new DataTable();

            //添加列
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Columns.Add("Age");

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

        /// <summary>
        /// 创建预期的数据表(包含列配置信息)
        /// </summary>
        /// <returns></returns>
        private DataTable CreateExpectedDataTableWithColumnConfigs()
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
