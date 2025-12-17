using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
{
    /// <summary>
    /// xls测试工作表读取器夹具类
    /// </summary>
    public class TestSheetReaderXlsFixture : TestSheetReaderFixtureBase
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestSheetReaderXlsFixture() : base("TestExcelFiles\\TestXlsFile.xls")
        { }
    }
}