using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.ClassFixtures;
using NPOI_API_Package;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    public class CellExtensionTest : IClassFixture<TestXlsFileReaderFixture>
    {
        private readonly TestXlsFileReaderFixture testXlsFileReaderFixture;
        private readonly ITestOutputHelper outputHelper;

        public CellExtensionTest(TestXlsFileReaderFixture testXlsFileReaderFixture, ITestOutputHelper outputHelper)
        {
            this.testXlsFileReaderFixture = testXlsFileReaderFixture;
            this.outputHelper = outputHelper;
        }

        [Fact]
        public void Test_GetCellValue_WhenCellIsNull()
        {
            ICell? cell = null;
            var val = cell!.GetCellValue();
            Assert.Equal(string.Empty, val);
        }

        [Fact]
        public void Test_GetCellValue_WhenCellIsBoolean()
        {
            var row = testXlsFileReaderFixture.BoolRow;
            var trueCell = row.GetCell(1);
            var falseCell = row.GetCell(2);

            var trueVal = trueCell.GetCellValue();
            Assert.IsType<bool>(trueVal);
            Assert.Equal(true, trueVal);

            var falseVal = falseCell.GetCellValue();
            Assert.IsType<bool>(falseVal);
            Assert.Equal(false, falseVal);
        }

        [Fact]
        public void Test_GetCellValue_WhenCellIsNumeric()
        {
            var row = testXlsFileReaderFixture.NumRow;
            var intCell = row.GetCell(1);
            var doubleCell = row.GetCell(2);

            var intVal = intCell.GetCellValue();
            Assert.IsType<double>(intVal);
            Assert.Equal(123.0, intVal);

            var doubleVal = doubleCell.GetCellValue();
            Assert.IsType<double>(doubleVal);
            Assert.Equal(123.456, doubleVal);
        }
    }
}
