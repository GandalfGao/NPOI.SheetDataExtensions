using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures.ClassFixtures
{
    /// <summary>
    /// 测试Xls文件读取器夹具
    /// </summary>
    public class TestXlsFileReaderFixture : IDisposable
    {
        /// <summary>
        /// Excel文件对象
        /// </summary>
        private readonly IWorkbook workbook;
        /// <summary>
        /// 包含布尔值数据的行对象
        /// </summary>
        private readonly IRow boolRow;
        /// <summary>
        /// 包含数字值数据的行对象
        /// </summary>
        private readonly IRow numRow;

        public TestXlsFileReaderFixture()
        {
            workbook = WorkbookFactory.Create("TestExcelFiles\\TestXlsFile.xls");

            int i = 0;
            var sheet = workbook.GetSheetAt(0);
            boolRow = sheet.GetRow(i++);
            numRow = sheet.GetRow(i++);
        }

        /// <summary>
        /// 包含布尔值数据的行对象属性
        /// </summary>
        public IRow BoolRow => boolRow;

        /// <summary>
        /// 包含数字值数据的行对象属性
        /// </summary>
        public IRow NumRow => numRow;

        public void Dispose()
        {
            workbook.Dispose();
        }
    }
}
