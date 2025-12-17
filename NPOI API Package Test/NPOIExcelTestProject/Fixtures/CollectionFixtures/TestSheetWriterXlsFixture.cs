using NPOI.HSSF.UserModel;

namespace NPOIExcelTestProject.Fixtures.CollectionFixtures
{
    /// <summary>
    /// xls测试工作表写入器类
    /// </summary>
    public class TestSheetWriterXlsFixture : TestSheetWriterFixtureBase
    {
        public TestSheetWriterXlsFixture() : base(new HSSFWorkbook())
        { }
    }
}
