using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
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

    /// <summary>
    /// 夹具集合定义类
    /// </summary>
    [CollectionDefinition(nameof(TestSheetReaderXlsxCollection))]
    public class TestSheetReaderXlsxCollection : ICollectionFixture<TestSheetReaderXlsxFixture>
    { }
}