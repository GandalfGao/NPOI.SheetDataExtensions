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
}
