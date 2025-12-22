using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures
{
    /// <summary>
    /// xlsx测试工作表读取器夹具类
    /// </summary>
    public class TestSheetReaderXlsxFixture : TestSheetReaderFixtureBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TestSheetReaderXlsxFixture() : base("TestExcelFiles\\TestXlsxFile.xlsx")
        { }
    }
}