using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOIExcelTestProject.Fixtures
{
    public class TestXlsFileReaderFixture : IDisposable
    {
        private readonly IWorkbook workbook;
        private readonly IRow boolRow;
        private readonly IRow numRow;
        private readonly IRow timeRow;
        private readonly IRow otherRow;

        public TestXlsFileReaderFixture()
        {
            string file = "TestExcelFiles\\TestXlsFile.xls";
            workbook = WorkbookFactory.Create(file);

            var sheet = workbook.GetSheetAt(0);

            int rowIndex = 0;
            boolRow = sheet.GetRow(rowIndex++);
            numRow = sheet.GetRow(rowIndex++);
            timeRow = sheet.GetRow(rowIndex++);
            otherRow = sheet.GetRow(rowIndex++);
        }

        public IRow BoolRow => boolRow;

        public IRow NumRow => numRow;

        public IRow TimeRow => timeRow;

        public IRow OtherRow => otherRow;

        public void Dispose()
        {
            workbook.Dispose();
        }
    }
}
