using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// Excel读取器测试类
    /// </summary>
    [Collection(nameof(TestFileReaderXlsxCollection))]
    public class ExcelReaderXlsxTest
    {
        private readonly TestFileReaderXlsxFixture testXlsxFileReaderFixture;
        private readonly ITestOutputHelper outputHelper;

        public ExcelReaderXlsxTest(TestFileReaderXlsxFixture testXlsxFileReaderFixture, ITestOutputHelper outputHelper)
        {
            this.testXlsxFileReaderFixture = testXlsxFileReaderFixture;
            this.outputHelper = outputHelper;
        }

        /// <summary>
        /// 测试当 firstRowIndex 为负数时，Read 方法应抛出 ArgumentException 异常。
        /// </summary>
        [Fact]
        public void Test_Read_WhenFirstRowIndexIsNegative()
        {
            var excelReader = new ExcelReader(testXlsxFileReaderFixture.Workbook, testXlsxFileReaderFixture.Sheet3);
            var ex = Assert.Throws<ArgumentException>(() => excelReader.Read(rowsCount: 6, firstRowIndex: -1));
            outputHelper.WriteLine(ex.Message);
        }

        /// <summary>
        /// 测试当 hasHeader 为 false 且 columnConfigs 为 null 或空集合时，Read 方法应抛出 ArgumentNullException 异常。
        /// </summary>
        [Fact]
        public void Test_Read_WhenHasHeaderIsFalseAndColumnConfigsIsNullOrEmpty()
        {
            var excelReader = new ExcelReader(testXlsxFileReaderFixture.Workbook, testXlsxFileReaderFixture.Sheet3);
            var ex = Assert.Throws<ArgumentNullException>(() => excelReader.Read(rowsCount: 6, hasHeader: false, columnConfigs: null));
            outputHelper.WriteLine(ex.Message);

            var ex2 = Assert.Throws<ArgumentNullException>(() => excelReader.Read(rowsCount: 6, hasHeader: false, columnConfigs: []));
            outputHelper.WriteLine(ex2.Message);
        }

        [Fact]
        public void Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty()
        { 
            var excelReader = new ExcelReader(testXlsxFileReaderFixture.Workbook, testXlsxFileReaderFixture.Sheet3);
            var dataTable = excelReader.Read(rowsCount: 6, firstRowIndex: 1, hasHeader: true);
        }
    }
}
