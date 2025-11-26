using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.ClassFixtures;
using Org.BouncyCastle.Security.Certificates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// 当单元格对象为空时，获取单元格值应返回空字符串
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsNull()
        {
            ICell cell = null!;
            var value = cell.GetCellValue();
            Assert.Equal(string.Empty, value);
        }

        /// <summary>
        /// 当单元格类型为布尔值时，获取单元格值应返回对应的布尔值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsBoolean()
        {
            var row = testXlsFileReaderFixture.BoolRow;

            var cellTrue = row.GetCell(1);
            var valueTrue = cellTrue.GetCellValue();
            Assert.True((bool)valueTrue);

            var cellFalse = row.GetCell(2);
            var valueFalse = cellFalse.GetCellValue();
            Assert.False((bool)valueFalse);
        }

        /// <summary>
        /// 当单元格类型为数字时，获取单元格值应返回对应的数字值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsNumeric()
        {
            var row = testXlsFileReaderFixture.NumRow;

            var cellInt = row.GetCell(1);
            var valueInt = cellInt.GetCellValue();
            Assert.Equal(123, (double)valueInt);

            var cellDouble = row.GetCell(2);
            var valueDouble = cellDouble.GetCellValue();
            Assert.Equal(123.456, (double)valueDouble);
        }

        /// <summary>
        /// 当单元格类型为日期或时间时，获取单元格值应返回对应的DateTime值
        /// </summary>
        /// <remarks>
        /// NPOI中的DateUtil.IsCellDateFormatted函数并不能完全准确判断单元格是否为日期类型，
        /// 因此需要在此基础上结合单元格的格式进行综合判断
        /// 部分无法识别的时间格式可以参考“日期/时间单元格输出测试”
        /// </remarks>
        [Fact]
        public void Test_GetCellValue_WhenCellIsDateTime()
        { 
            var dateRow = testXlsFileReaderFixture.DateRow;
            var timeRow = testXlsFileReaderFixture.TimeRow;

            var cellDate = dateRow.GetCell(1);
            var valueDate = cellDate.GetCellValue();

            var cellTime = timeRow.GetCell(1);
            var valueTime = cellTime.GetCellValue();

            Assert.IsType<DateTime>(valueDate);
            Assert.IsType<DateTime>(valueTime);

            var expectedDateTime = new DateTime(2025, 1, 1, 12, 0, 0);

            Assert.Equal(expectedDateTime, ((DateTime)valueDate));
            Assert.Equal(expectedDateTime, ((DateTime)valueTime));
        }

        /// <summary>
        /// 当单元格类型为文本时，获取单元格值应返回对应的字符串值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsString()
        {
            var row = testXlsFileReaderFixture.TextRow;
            var cell = row.GetCell(1);
            var value = cell.GetCellValue();

            Assert.IsType<string>(value);
            Assert.Equal("Good", (string)value);
        }

        /// <summary>
        /// 当单元格为空时，获取单元格值应返回空字符串
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsEmpty()
        {
            var row = testXlsFileReaderFixture.EmptyRow;
            var cell = row.GetCell(1);
            var value = cell.GetCellValue();

            Assert.IsType<string>(value);
            Assert.Equal(string.Empty, (string)value);
        }

        #region 日期/时间单元格输出测试

        /// <summary>
        /// 测试当单元格类型为日期时，输出单元格相关信息
        /// </summary>
        [Fact]
        public void Test_Output_WhenCellIsDate()
        {
            var row = testXlsFileReaderFixture.DateRow;
            //获取日期单元格集合
            var dateCells = row.Cells;
            //遍历单元格集合
            for (int i = 1; i < dateCells.Count; i++)
            {
                var cell = dateCells[i];
                var cellInfo = $"cell index: {i}, cell type: {cell.CellType}, is date? {DateUtil.IsCellDateFormatted(cell)}, format: {cell.CellStyle.GetDataFormatString()}, date value: {cell.DateCellValue}, str val: {cell}";
                outputHelper.WriteLine(cellInfo);
            }
        }

        /// <summary>
        /// 测试当单元格类型为时间时，输出单元格相关信息
        /// </summary>
        [Fact]
        public void Test_Output_WhenCellIsTime()
        { 
            var row = testXlsFileReaderFixture.TimeRow;
            //获取时间单元格集合
            var timeCells = row.Cells;
            //遍历单元格集合
            for (int i = 1; i < timeCells.Count; i++)
            {
                var cell = timeCells[i];
                var cellInfo = $"cell index: {i}, cell type: {cell.CellType}, is date? {DateUtil.IsCellDateFormatted(cell)}, format: {cell.CellStyle.GetDataFormatString()}, date value: {cell.DateCellValue}, str val: {cell}";
                outputHelper.WriteLine(cellInfo);
            }
        }

        #endregion
    }
}
