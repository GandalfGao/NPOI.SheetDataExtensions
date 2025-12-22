using NPOI.XSSF.UserModel;

namespace NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures
{
    /// <summary>
    /// xlsx测试工作表写入器类
    /// </summary>
    public class TestSheetWriterXlsxFixture : TestSheetWriterFixtureBase
    {
        public TestSheetWriterXlsxFixture() : base(new XSSFWorkbook())
        { }

        /// <summary>
        /// 保存文件
        /// </summary>
        protected override void Save()
        {
            string file = "TestOutput.xlsx";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            using var fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            this.workbook.Write(fs);
        }
    }
}
