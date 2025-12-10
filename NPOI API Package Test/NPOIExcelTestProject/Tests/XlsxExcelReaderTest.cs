using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// Excel读取器测试类
    /// </summary>
    [Collection(nameof(TestXlsxFileReaderCollection))]
    public class XlsxExcelReaderTest
    {
        private readonly TestXlsxFileReaderFixture testXlsxFileReaderFixture;

        public XlsxExcelReaderTest(TestXlsxFileReaderFixture testXlsxFileReaderFixture)
        {
            this.testXlsxFileReaderFixture = testXlsxFileReaderFixture;
        }

        /// <summary>
        /// 测试当 firstRowIndex 为负数时，Read 方法应抛出 ArgumentException 异常。
        /// </summary>
        [Fact]
        public void Test_Read_WhenFirstRowIndexIsNegative()
        {
            var excelReader = new ExcelReader(testXlsxFileReaderFixture.Workbook, testXlsxFileReaderFixture.Sheet3);
            Assert.Throws<ArgumentException>(() => excelReader.Read(firstRowIndex: -1));
        }
    }
}
