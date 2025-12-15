using NPOI.SS.UserModel;
using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using Org.BouncyCastle.Security.Certificates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// xls文件单元格扩展类测试
    /// </summary>
    [Collection(nameof(TestSheetReaderXlsCollection))]
    public class CellExtensionXlsTest : CellExtensionTestBase
    {
        public CellExtensionXlsTest(TestSheetReaderXlsFixture testXlsFileReaderFixture, ITestOutputHelper outputHelper) : base(testXlsFileReaderFixture, outputHelper)
        { }

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
    }
}
