using NPOI.HSSF.UserModel;

namespace NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures
{
    /// <summary>
    /// xls测试工作表写入器类
    /// </summary>
    public class TestSheetWriterXlsFixture : TestSheetWriterFixtureBase
    {
        public TestSheetWriterXlsFixture() : base(new HSSFWorkbook())
        { }

        /// <summary>
        /// 保存文件
        /// </summary>
        protected override void Save()
        {
            string file = "TestOutput.xls";
            if (File.Exists(file))
            {
                File.Delete(file);
            }
            using var fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            this.workbook.Write(fs);
        }
    }
}
