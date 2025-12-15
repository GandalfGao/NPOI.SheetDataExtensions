using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
{
    /// <summary>
    /// 测试Xls文件读取器夹具
    /// </summary>
    public class TestSheetReaderXlsFixture : TestSheetReaderFixtureBase
    {
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TestSheetReaderXlsFixture() : base("TestExcelFiles\\TestXlsFile.xls")
        { }
    }

    /// <summary>
    /// 夹具集合定义类
    /// </summary>
    [CollectionDefinition(nameof(TestSheetReaderXlsCollection))]
    public class TestSheetReaderXlsCollection : ICollectionFixture<TestSheetReaderXlsFixture>
    { }
}