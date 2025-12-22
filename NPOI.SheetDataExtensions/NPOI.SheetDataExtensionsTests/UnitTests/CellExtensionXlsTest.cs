using NPOI.SheetDataExtensionsTests.Fixtures.CollectionFixtures;
using Xunit.Abstractions;

namespace NPOI.SheetDataExtensionsTests.UnitTests
{
    /// <summary>
    /// xls文件单元格扩展类测试
    /// </summary>
    [Collection(nameof(TestSheetXlsFixturesCollection))]
    public class CellExtensionXlsTest : CellExtensionTestBase
    {
        public CellExtensionXlsTest(TestSheetReaderXlsFixture testFileReaderXlsFixture, TestSheetWriterXlsFixture testSheetWriterXlsFixture, ITestOutputHelper outputHelper) 
            : base(testFileReaderXlsFixture, testSheetWriterXlsFixture, outputHelper)
        { }

        #region GetCellValue测试

        /// <summary>
        /// 当单元格类型为布尔值时，单元格值应返回对应的布尔值
        /// </summary>
        [Fact]
        public override void Test_GetCellValue_WhenCellIsBoolean()
        {
            base.Test_GetCellValue_WhenCellIsBoolean();
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
        public override void Test_GetCellValue_WhenCellIsDateTime()
        {
            base.Test_GetCellValue_WhenCellIsDateTime();
        }

        /// <summary>
        /// 当单元格为空时，单元格值应返回空字符串
        /// </summary>
        [Fact]
        public override void Test_GetCellValue_WhenCellIsEmpty()
        {
            base.Test_GetCellValue_WhenCellIsEmpty();
        }

        /// <summary>
        /// 当单元格为公式时，单元格值应返回公式本身或公式计算后的值
        /// </summary>
        [Fact]
        public override void Test_GetCellValue_WhenCellIsFormula()
        {
            base.Test_GetCellValue_WhenCellIsFormula();
        }

        /// <summary>
        /// 当单元格对象为空时，单元格值应返回空字符串
        /// </summary>
        [Fact]
        public override void Test_GetCellValue_WhenCellIsNull()
        {
            base.Test_GetCellValue_WhenCellIsNull();
        }

        /// <summary>
        /// 当单元格类型为数字时，单元格值应返回对应的数字值
        /// </summary>
        [Fact]
        public override void Test_GetCellValue_WhenCellIsNumeric()
        {
            base.Test_GetCellValue_WhenCellIsNumeric();
        }

        /// <summary>
        /// 当单元格类型为文本时，单元格值应返回对应的字符串值
        /// </summary>
        [Fact]
        public override void Test_GetCellValue_WhenCellIsString()
        {
            base.Test_GetCellValue_WhenCellIsString();
        }

        /// <summary>
        /// 测试当单元格类型为日期时，输出单元格相关信息
        /// </summary>
        [Fact]
        public override void Test_Output_WhenCellIsDate()
        {
            base.Test_Output_WhenCellIsDate();
        }

        /// <summary>
        /// 测试当单元格类型为时间时，输出单元格相关信息
        /// </summary>
        [Fact]
        public override void Test_Output_WhenCellIsTime()
        {
            base.Test_Output_WhenCellIsTime();
        }

        #endregion

        #region SetCellValue测试

        /// <summary>
        /// 当单元格对象为空的时候, 应抛出ArgumentNullException
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenCellIsNull()
        {
            base.Test_SetCellValue_WhenCellIsNull();
        }

        /// <summary>
        /// 当单元格的值为NULL时, 应返回空字符串
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenValIsNull()
        {
            base.Test_SetCellValue_WhenValIsNull();
        }

        /// <summary>
        /// 当设置单元格的值类型为布尔值时, 单元格类型为布尔
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenValueIsBool()
        {
            base.Test_SetCellValue_WhenValueIsBool();
        }

        /// <summary>
        /// 当设置单元格的值类型为数字时, 单元格类型为数字
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenValueIsNum()
        {
            base.Test_SetCellValue_WhenValueIsNum();
        }

        /// <summary>
        /// 当设置单元格的值类型为时间时, 单元格类型为数字, 且日期校验为true
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenValueIsDateTime()
        {
            base.Test_SetCellValue_WhenValueIsDateTime();
        }

        /// <summary>
        /// 当设置单元格的值类型为算式时, 单元格类型为算式
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenValueIsFormula()
        {
            base.Test_SetCellValue_WhenValueIsFormula();
        }

        /// <summary>
        /// 当设置单元格的值类型为字符串时, 单元格类型为字符串
        /// </summary>
        [Fact]
        public override void Test_SetCellValue_WhenValueIsString()
        {
            base.Test_SetCellValue_WhenValueIsString();
        }

        #endregion
    }
}
