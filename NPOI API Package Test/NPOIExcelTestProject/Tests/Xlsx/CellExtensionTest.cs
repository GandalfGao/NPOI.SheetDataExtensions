using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.ClassFixtures.Xlsx;
using Org.BouncyCastle.Security.Certificates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests.Xlsx
{
    /// <summary>
    /// xls文件单元格扩展类测试
    /// </summary>
    public class CellExtensionTest : IClassFixture<TestExcelFileReaderFixture>
    {
        private readonly TestExcelFileReaderFixture testXlsxFileReaderFixture;
        private readonly ITestOutputHelper outputHelper;

        public CellExtensionTest(TestExcelFileReaderFixture testXlsxFileReaderFixture, ITestOutputHelper outputHelper)
        {
            this.testXlsxFileReaderFixture = testXlsxFileReaderFixture;
            this.outputHelper = outputHelper;
        }

        /// <summary>
        /// 当单元格对象为空时，单元格值应返回空字符串
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsNull()
        {
            ICell cell = null!;
            var value = cell.GetCellValue();
            Assert.Equal(string.Empty, value);
        }

        /// <summary>
        /// 当单元格类型为布尔值时，单元格值应返回对应的布尔值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsBoolean()
        {
            var row = testXlsxFileReaderFixture.BoolRow;

            var cellTrue = row.GetCell(1);
            var valueTrue = cellTrue.GetCellValue();
            Assert.Equal(CellType.Boolean, cellTrue.CellType);
            Assert.True((bool)valueTrue);

            var cellFalse = row.GetCell(2);
            var valueFalse = cellFalse.GetCellValue();
            Assert.Equal(CellType.Boolean, cellFalse.CellType);
            Assert.False((bool)valueFalse);
        }

        /// <summary>
        /// 当单元格类型为数字时，单元格值应返回对应的数字值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsNumeric()
        {
            var row = testXlsxFileReaderFixture.NumRow;

            var cellInt = row.GetCell(1);
            var valueInt = cellInt.GetCellValue();
            Assert.Equal(CellType.Numeric, cellInt.CellType);
            Assert.Equal(123, (double)valueInt);

            var cellDouble = row.GetCell(2);
            var valueDouble = cellDouble.GetCellValue();
            Assert.Equal(CellType.Numeric, cellDouble.CellType);
            Assert.Equal(123.456, (double)valueDouble);
        }

        /// <summary>
        /// 当单元格类型为日期或时间时，单元格值应返回对应的DateTime值
        /// </summary>
        /// <remarks>
        /// NPOI中的DateUtil.IsCellDateFormatted函数并不能完全准确判断单元格是否为日期类型，
        /// 因此需要在此基础上结合单元格的格式进行综合判断
        /// 部分无法识别的时间格式可以参考“日期/时间单元格输出测试”
        /// </remarks>
        [Fact]
        public void Test_GetCellValue_WhenCellIsDateTime()
        { 
            var dateRow = testXlsxFileReaderFixture.DateRow;
            var timeRow = testXlsxFileReaderFixture.TimeRow;

            var cellDate = dateRow.GetCell(1);
            var valueDate = cellDate.GetCellValue();

            var cellTime = timeRow.GetCell(1);
            var valueTime = cellTime.GetCellValue();

            Assert.Equal(CellType.Numeric, cellDate.CellType);
            Assert.Equal(CellType.Numeric, cellTime.CellType);

            Assert.IsType<DateTime>(valueDate);
            Assert.IsType<DateTime>(valueTime);

            var expectedDateTime = new DateTime(2025, 1, 1, 12, 0, 0);

            Assert.Equal(expectedDateTime, ((DateTime)valueDate));
            Assert.Equal(expectedDateTime, ((DateTime)valueTime));
        }

        /// <summary>
        /// 当单元格类型为文本时，单元格值应返回对应的字符串值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsString()
        {
            var row = testXlsxFileReaderFixture.TextRow;
            var cell = row.GetCell(1);
            var value = cell.GetCellValue();

            Assert.Equal(CellType.String, cell.CellType);
            Assert.IsType<string>(value);
            Assert.Equal("Good", (string)value);
        }

        /// <summary>
        /// 当单元格为空时，单元格值应返回空字符串
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsEmpty()
        {
            var row = testXlsxFileReaderFixture.EmptyRow;
            var cell = row.GetCell(1);
            var value = cell.GetCellValue();

            Assert.Equal(CellType.Blank, cell.CellType);
            Assert.IsType<string>(value);
            Assert.Equal(string.Empty, (string)value);
        }

        /// <summary>
        /// 当单元格为公式时，单元格值应返回公式本身或公式计算后的值
        /// </summary>
        [Fact]
        public void Test_GetCellValue_WhenCellIsFormula()
        {
            var formulaEvaluator =  testXlsxFileReaderFixture.FormulaEvaluator;

            //1. 布尔值
            var boolCell_1 = testXlsxFileReaderFixture.FBoolRow.GetCell(2);
            var boolCell_2 = testXlsxFileReaderFixture.FBoolRow.GetCell(3);
            // a. 不通过公式计算器获取值
            var boolCellVal1WithoutCalc = boolCell_1.GetCellValue();
            var boolCellVal2WithoutCalc = boolCell_2.GetCellValue();
            Assert.Equal(CellType.Formula, boolCell_1.CellType);
            Assert.Equal(CellType.Formula, boolCell_2.CellType);
            // 输出公式信息
            outputHelper.WriteLine("布尔值输出: " + (string)boolCellVal1WithoutCalc + ", " + (string)boolCellVal2WithoutCalc);
            // b. 通过公式计算器获取值
            var boolCellVal1WithCalc = boolCell_1.GetCellValue(formulaEvaluator);
            var boolCellVal2WithCalc = boolCell_2.GetCellValue(formulaEvaluator);
            Assert.IsType<bool>(boolCellVal1WithCalc);
            Assert.IsType<bool>(boolCellVal2WithCalc);
            Assert.True((bool)boolCellVal1WithCalc);
            Assert.False((bool)boolCellVal2WithCalc);

            //2. 数字值
            var numCell = testXlsxFileReaderFixture.FNumRow.GetCell(2);
            // a. 不通过公式计算器获取值
            var numCellValWithoutCalc = numCell.GetCellValue();
            Assert.Equal(CellType.Formula, numCell.CellType);
            // 输出公式信息
            outputHelper.WriteLine("数字值输出: " + (string)numCellValWithoutCalc);
            // b. 通过公式计算器获取值
            var numCellValWithCalc = numCell.GetCellValue(formulaEvaluator);
            Assert.IsType<double>(numCellValWithCalc);
            Assert.Equal(2, (double)numCellValWithCalc);

            //3. 日期值
            var dateCell = testXlsxFileReaderFixture.FDateRow.GetCell(2);
            // a. 不通过公式计算器获取值
            var dateCellValWithoutCalc = dateCell.GetCellValue();
            Assert.Equal(CellType.Formula, dateCell.CellType);
            // 输出公式信息
            outputHelper.WriteLine("日期值输出: " + (string)dateCellValWithoutCalc);
            // b. 通过公式计算器获取值
            var dateCellValWithCalc = dateCell.GetCellValue(formulaEvaluator);
            Assert.IsType<DateTime>(dateCellValWithCalc);
            var expectedDate = new DateTime(2025, 1, 1, 12, 0, 0);
            Assert.Equal(expectedDate, (DateTime)dateCellValWithCalc);

            //4. 错误值
            var errorCell = testXlsxFileReaderFixture.FErrorRow.GetCell(2);
            // a. 不通过公式计算器获取值
            var errorCellValWithoutCalc = errorCell.GetCellValue();
            Assert.Equal(CellType.Formula, errorCell.CellType);
            // 输出公式信息
            outputHelper.WriteLine("错误值输出: " + (string)errorCellValWithoutCalc);
            // b. 通过公式计算器获取值
            var errorCellValWithCalc = errorCell.GetCellValue(formulaEvaluator);
            Assert.IsType<string>(errorCellValWithCalc);
            Assert.Equal("#DIV/0!", (string)errorCellValWithCalc);

            //5. 文本值
            var textCell = testXlsxFileReaderFixture.FStringRow.GetCell(2);
            var textCell2 = testXlsxFileReaderFixture.FStringRow.GetCell(3);
            // a. 不通过公式计算器获取值
            var textCellValWithoutCalc = textCell.GetCellValue();
            var textCellVal2WithoutCalc = textCell2.GetCellValue();
            Assert.Equal(CellType.Formula, textCell.CellType);
            Assert.Equal(CellType.Formula, textCell2.CellType);
            // 输出公式信息
            outputHelper.WriteLine("文本值输出: " + (string)textCellValWithoutCalc + ", 空字符串值输出: " + (string)textCellVal2WithoutCalc);
            // b. 通过公式计算器获取值
            var textCellValWithCalc = textCell.GetCellValue(formulaEvaluator);
            var textCellVal2WithCalc = textCell2.GetCellValue(formulaEvaluator);
            Assert.IsType<string>(textCellValWithCalc);
            Assert.IsType<string>(textCellVal2WithCalc);
            Assert.Equal("Good", (string)textCellValWithCalc);
            Assert.Equal(string.Empty, (string)textCellVal2WithCalc);
        }

        #region 日期/时间单元格输出测试

        /// <summary>
        /// 测试当单元格类型为日期时，输出单元格相关信息
        /// </summary>
        [Fact]
        public void Test_Output_WhenCellIsDate()
        {
            var row = testXlsxFileReaderFixture.DateRow;
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
            var row = testXlsxFileReaderFixture.TimeRow;
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
