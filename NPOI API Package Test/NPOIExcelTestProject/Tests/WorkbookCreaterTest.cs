using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI_API_Package;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    public class WorkbookCreaterTest
    {
        [Fact]
        public void Test_Create_NewXlsFile()
        { 
            using var workbook = WorkbookCreater.Create(ExcelType.Xls);
            Assert.IsType<HSSFWorkbook>(workbook);
        }

        [Fact]
        public void Test_Create_NewXlsxFile()
        {
            using var workbook = WorkbookCreater.Create(ExcelType.Xlsx);
            Assert.IsType<XSSFWorkbook>(workbook);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Test_Create_WhenFileIsNullOrEmpty(string? file)
        {
            Assert.Throws<ArgumentNullException>(() => WorkbookCreater.Create(file!));
        }

        [Fact]
        public void Test_Create_WhenFileIsNotExist()
        {
            var file = "NotExistFile.xlsx";
            Assert.Throws<FileNotFoundException>(() => WorkbookCreater.Create(file));
        }

        [Theory]
        [InlineData("TestExcelFiles\\TestXlsFile")]
        [InlineData("TestExcelFiles\\TestXlsFile.xls")]
        public void Test_Create_XlsFile(string file)
        { 
            using var workbook = WorkbookCreater.Create(file);
            Assert.IsType<HSSFWorkbook>(workbook);
        }

        [Theory]
        [InlineData("TestExcelFiles\\TestXlsxFile")]
        [InlineData("TestExcelFiles\\TestXlsxFile.xlsx")]
        public void Test_Create_XlsxFile(string file)
        {
            using var workbook = WorkbookCreater.Create(file);
            Assert.IsType<XSSFWorkbook>(workbook);
        }
    }
}