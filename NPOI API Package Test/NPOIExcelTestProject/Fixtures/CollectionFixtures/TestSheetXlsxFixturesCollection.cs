using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
{
    /// <summary>
    /// 夹具集合定义类
    /// </summary>
    [CollectionDefinition(nameof(TestSheetXlsxFixturesCollection))]
    public class TestSheetXlsxFixturesCollection : ICollectionFixture<TestSheetReaderXlsxFixture>, ICollectionFixture<TestSheetWriterXlsxFixture>
    { }
}
