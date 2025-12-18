using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// 单元格扩展测试基类
    /// </summary>
    public abstract class CellExtensionTestBase
    {
        private readonly TestSheetReaderFixtureBase testSheetReaderFixtureBase;
        private readonly TestSheetWriterFixtureBase testSheetWriterFixtureBase;
        private readonly ITestOutputHelper outputHelper;

        public CellExtensionTestBase(TestSheetReaderFixtureBase testSheetReaderFixtureBase, TestSheetWriterFixtureBase testSheetWriterFixtureBase, ITestOutputHelper outputHelper)
        {
            this.testSheetReaderFixtureBase = testSheetReaderFixtureBase;
            this.testSheetWriterFixtureBase = testSheetWriterFixtureBase;
            this.outputHelper = outputHelper;
        }

        #region GetCellValue测试

        /// <summary>
        /// 当单元格对象为空时，单元格值应返回空字符串
        /// </summary>
        public virtual void Test_GetCellValue_WhenCellIsNull()
        {
            ICell? cell = null;
            var value = cell.GetValue();
            Assert.Equal(string.Empty, value);
            Assert.True(cell.IsEmpty());
        }

        /// <summary>
        /// 当单元格类型为布尔值时，单元格值应返回对应的布尔值
        /// </summary>
        public virtual void Test_GetCellValue_WhenCellIsBoolean()
        {
            var row = testSheetReaderFixtureBase.BoolRow;

            var cellTrue = row.GetCell(1);
            var valueTrue = cellTrue.GetValue();
            Assert.Equal(CellType.Boolean, cellTrue.CellType);
            Assert.True((bool)valueTrue);

            var cellFalse = row.GetCell(2);
            var valueFalse = cellFalse.GetValue();
            Assert.Equal(CellType.Boolean, cellFalse.CellType);
            Assert.False((bool)valueFalse);
        }

        /// <summary>
        /// 当单元格类型为数字时，单元格值应返回对应的数字值
        /// </summary>
        public virtual void Test_GetCellValue_WhenCellIsNumeric()
        {
            var row = testSheetReaderFixtureBase.NumRow;

            var cellInt = row.GetCell(1);
            var valueInt = cellInt.GetValue();
            Assert.Equal(CellType.Numeric, cellInt.CellType);
            Assert.Equal(123, (double)valueInt);

            var cellDouble = row.GetCell(2);
            var valueDouble = cellDouble.GetValue();
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
        public virtual void Test_GetCellValue_WhenCellIsDateTime()
        {
            var dateRow = testSheetReaderFixtureBase.DateRow;
            var timeRow = testSheetReaderFixtureBase.TimeRow;

            var cellDate = dateRow.GetCell(1);
            var valueDate = cellDate.GetValue();

            var cellTime = timeRow.GetCell(1);
            var valueTime = cellTime.GetValue();

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
        public virtual void Test_GetCellValue_WhenCellIsString()
        {
            var row = testSheetReaderFixtureBase.TextRow;

            var cell = row.GetCell(1);
            var value = cell.GetValue();

            Assert.Equal(CellType.String, cell.CellType);
            Assert.IsType<string>(value);
            Assert.Equal("Good", (string)value);
        }

        /// <summary>
        /// 当单元格为空时，单元格值应返回空字符串
        /// </summary>
        public virtual void Test_GetCellValue_WhenCellIsEmpty()
        {
            var row = testSheetReaderFixtureBase.EmptyRow;

            var cell = row.GetCell(1);
            var value = cell.GetValue();

            Assert.Equal(CellType.Blank, cell.CellType);
            Assert.IsType<string>(value);
            Assert.Equal(string.Empty, (string)value);
            Assert.True(cell.IsEmpty());
        }

        /// <summary>
        /// 当单元格为公式时，单元格值应返回公式本身或公式计算后的值
        /// </summary>
        public virtual void Test_GetCellValue_WhenCellIsFormula()
        {
            var formulaEvaluator = testSheetReaderFixtureBase.FormulaEvaluator;

            //1. 布尔值
            var boolCell_1 = testSheetReaderFixtureBase.FBoolRow.GetCell(2);
            var boolCell_2 = testSheetReaderFixtureBase.FBoolRow.GetCell(3);
            // a. 不通过公式计算器获取值
            var boolCellVal1WithoutCalc = boolCell_1.GetValue();
            var boolCellVal2WithoutCalc = boolCell_2.GetValue();
            Assert.Equal(CellType.Formula, boolCell_1.CellType);
            Assert.Equal(CellType.Formula, boolCell_2.CellType);
            // 输出公式信息
            outputHelper.WriteLine("布尔值输出: " + (string)boolCellVal1WithoutCalc + ", " + (string)boolCellVal2WithoutCalc);
            // b. 通过公式计算器获取值
            var boolCellVal1WithCalc = boolCell_1.GetValue(formulaEvaluator);
            var boolCellVal2WithCalc = boolCell_2.GetValue(formulaEvaluator);
            Assert.IsType<bool>(boolCellVal1WithCalc);
            Assert.IsType<bool>(boolCellVal2WithCalc);
            Assert.True((bool)boolCellVal1WithCalc);
            Assert.False((bool)boolCellVal2WithCalc);

            //2. 数字值
            var numCell = testSheetReaderFixtureBase.FNumRow.GetCell(2);
            // a. 不通过公式计算器获取值
            var numCellValWithoutCalc = numCell.GetValue();
            Assert.Equal(CellType.Formula, numCell.CellType);
            // 输出公式信息
            outputHelper.WriteLine("数字值输出: " + (string)numCellValWithoutCalc);
            // b. 通过公式计算器获取值
            var numCellValWithCalc = numCell.GetValue(formulaEvaluator);
            Assert.IsType<double>(numCellValWithCalc);
            Assert.Equal(2, (double)numCellValWithCalc);

            //3. 日期值
            var dateCell = testSheetReaderFixtureBase.FDateRow.GetCell(2);
            // a. 不通过公式计算器获取值
            var dateCellValWithoutCalc = dateCell.GetValue();
            Assert.Equal(CellType.Formula, dateCell.CellType);
            // 输出公式信息
            outputHelper.WriteLine("日期值输出: " + (string)dateCellValWithoutCalc);
            // b. 通过公式计算器获取值
            var dateCellValWithCalc = dateCell.GetValue(formulaEvaluator);
            Assert.IsType<DateTime>(dateCellValWithCalc);
            var expectedDate = new DateTime(2025, 1, 1, 12, 0, 0);
            Assert.Equal(expectedDate, (DateTime)dateCellValWithCalc);

            //4. 错误值
            var errorCell = testSheetReaderFixtureBase.FErrorRow.GetCell(2);
            // a. 不通过公式计算器获取值
            var errorCellValWithoutCalc = errorCell.GetValue();
            Assert.Equal(CellType.Formula, errorCell.CellType);
            // 输出公式信息
            outputHelper.WriteLine("错误值输出: " + (string)errorCellValWithoutCalc);
            // b. 通过公式计算器获取值
            var errorCellValWithCalc = errorCell.GetValue(formulaEvaluator);
            Assert.IsType<string>(errorCellValWithCalc);
            Assert.Equal("#DIV/0!", (string)errorCellValWithCalc);

            //5. 文本值
            var textCell = testSheetReaderFixtureBase.FStringRow.GetCell(2);
            var textCell2 = testSheetReaderFixtureBase.FStringRow.GetCell(3);
            // a. 不通过公式计算器获取值
            var textCellValWithoutCalc = textCell.GetValue();
            var textCellVal2WithoutCalc = textCell2.GetValue();
            Assert.Equal(CellType.Formula, textCell.CellType);
            Assert.Equal(CellType.Formula, textCell2.CellType);
            // 输出公式信息
            outputHelper.WriteLine("文本值输出: " + (string)textCellValWithoutCalc + ", 空字符串值输出: " + (string)textCellVal2WithoutCalc);
            // b. 通过公式计算器获取值
            var textCellValWithCalc = textCell.GetValue(formulaEvaluator);
            var textCellVal2WithCalc = textCell2.GetValue(formulaEvaluator);
            Assert.IsType<string>(textCellValWithCalc);
            Assert.IsType<string>(textCellVal2WithCalc);
            Assert.Equal("Good", (string)textCellValWithCalc);
            Assert.Equal(string.Empty, (string)textCellVal2WithCalc);
        }

        #region 日期/时间单元格输出测试

        /// <summary>
        /// 测试当单元格类型为日期时，输出单元格相关信息
        /// </summary>
        public virtual void Test_Output_WhenCellIsDate()
        {
            var row = testSheetReaderFixtureBase.DateRow;
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
        public virtual void Test_Output_WhenCellIsTime()
        {
            var row = testSheetReaderFixtureBase.TimeRow;
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

        #endregion

        #region SetCellValue测试

        /// <summary>
        /// 当单元格对象为空的时候, 应抛出ArgumentNullException
        /// </summary>
        public virtual void Test_SetCellValue_WhenCellIsNull()
        {
            ICell cell = null!;
            object val = "test";
            Assert.Throws<ArgumentNullException>(() => cell.SetValue(val));
        }

        /// <summary>
        /// 当单元格的值为NULL时, 单元格类型为Blank
        /// </summary>
        public virtual void Test_SetCellValue_WhenValIsNull()
        {
            var sheet = testSheetWriterFixtureBase.Sheet1;

            var row = sheet.CreateRow(0);

            var cell = row.CreateCell(0);
            cell.SetValue(null!);

            Assert.Equal(CellType.Blank, cell.CellType);
        }

        /// <summary>
        /// 当设置单元格的值类型为布尔值时, 单元格类型为布尔
        /// </summary>
        public virtual void Test_SetCellValue_WhenValueIsBool()
        {
            var sheet = testSheetWriterFixtureBase.Sheet1;

            var row = sheet.CreateRow(1);

            var cell = row.CreateCell(0);
            object val = true;
            cell.SetValue(val);
            Assert.Equal(CellType.Boolean, cell.CellType);
            Assert.True(cell.BooleanCellValue);

            var cell2 = row.CreateCell(1);
            cell2.SetValue("false");
            Assert.Equal(CellType.Boolean, cell2.CellType);
            Assert.False(cell2.BooleanCellValue);
        }

        /// <summary>
        /// 当设置单元格的值类型为数字时, 单元格类型为数字
        /// </summary>
        public virtual void Test_SetCellValue_WhenValueIsNum()
        {
            var sheet = testSheetWriterFixtureBase.Sheet1;

            var row = sheet.CreateRow(2);

            var cell = row.CreateCell(0);
            object val = 1;
            cell.SetValue(val);
            Assert.Equal(CellType.Numeric, cell.CellType);
            Assert.Equal(1, cell.NumericCellValue);

            var cell2 = row.CreateCell(1);
            cell2.SetValue("2");
            Assert.Equal(CellType.Numeric, cell2.CellType);
            Assert.Equal(2, cell2.NumericCellValue);
        }

        /// <summary>
        /// 当设置单元格的值类型为时间时, 单元格类型为数字, 且日期校验为true
        /// </summary>
        public virtual void Test_SetCellValue_WhenValueIsDateTime()
        {
            var sheet = testSheetWriterFixtureBase.Sheet1;

            var row = sheet.CreateRow(3);

            /**
             * 设置时间格式
             *  (
             *      直接设置时间并不会使单元格直接显示为时间而是数字, 需要额外设置时间格式, 
             *      即使设置了时间格式, 显示也是时间的数值, 但是DateUtil.IsCellDateFormatted依然有可能无法识别其时间格式而返回FALSE
             *  )
             */
            var style = testSheetWriterFixtureBase.Workbook.CreateCellStyle();
            var creationHelper = testSheetWriterFixtureBase.Workbook.GetCreationHelper();
            style.DataFormat = creationHelper.CreateDataFormat().GetFormat("yyyy-MM-dd HH:mm:ss");

            //目标时间
            var targetDateTime = new DateTime(2025, 1, 1, 12, 0, 0);

            var cell = row.CreateCell(0);
            var dt = new DateTime(2025, 1, 1, 12, 0, 0);
            cell.SetValue(dt);            
            cell.CellStyle = style;
            Assert.Equal(CellType.Numeric, cell.CellType);
            Assert.True(DateUtil.IsCellDateFormatted(cell));
            Assert.Equal(targetDateTime, cell.DateCellValue);

            var cell2 = row.CreateCell(1);
            cell2.SetValue("2025-01-01 12:00:00");
            cell2.CellStyle = style;
            Assert.Equal(CellType.Numeric, cell2.CellType);
            Assert.True(DateUtil.IsCellDateFormatted(cell2));
            Assert.Equal(targetDateTime, cell2.DateCellValue);
        }

        /// <summary>
        /// 当设置单元格的值类型为算式时, 单元格类型为算式
        /// </summary>
        public virtual void Test_SetCellValue_WhenValueIsFormula()
        {
            var sheet = testSheetWriterFixtureBase.Sheet1;
            var formulaEvaluator = testSheetWriterFixtureBase.FormulaEvaluator;
            var row = sheet.CreateRow(4);
            for (int i = 0; i < 3; i++)
            {
                var cell = row.CreateCell(i);
                cell.SetCellValue(i + 1);
            }
            var cellSum = row.CreateCell(3);
            cellSum.SetValue("=SUM(A5:C5)");
            formulaEvaluator.EvaluateFormulaCell(cellSum);

            Assert.Equal(CellType.Formula, cellSum.CellType);
            Assert.Equal("SUM(A5:C5)", cellSum.ToString());
            Assert.Equal(6, cellSum.NumericCellValue);
        }

        /// <summary>
        /// 当设置单元格的值类型为字符串时, 单元格类型为字符串
        /// </summary>
        public virtual void Test_SetCellValue_WhenValueIsString()
        {
            var sheet = testSheetWriterFixtureBase.Sheet1;
            var row = sheet.CreateRow(5);
            var cell = row.CreateCell(0);
            cell.SetValue("Test");
            Assert.Equal(CellType.String, cell.CellType);
            Assert.Equal("Test", cell.StringCellValue);
        }

        #endregion
    }
}
