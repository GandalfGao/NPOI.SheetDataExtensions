using NPOI.XSSF.UserModel;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
{
    /// <summary>
    /// xlsx测试工作表写入器类
    /// </summary>
    public class TestSheetWriterXlsxFixture : TestSheetWriterFixtureBase
    {
        public TestSheetWriterXlsxFixture() : base(new XSSFWorkbook())
        { }
    }

    /// <summary>
    /// 夹具集合定义类
    /// </summary>
    public class TestSheetWriterXlsxCollection : ICollectionFixture<TestSheetWriterXlsxFixture>
    { }
}
