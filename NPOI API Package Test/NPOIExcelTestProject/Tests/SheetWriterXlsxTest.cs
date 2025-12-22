using NPOI.SS.UserModel;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// 工作表写入器测试类
    /// </summary>
    [Collection(nameof(TestSheetXlsxFixturesCollection))]
    public class SheetWriterXlsxTest : SheetWriterTestBase
    {
        public SheetWriterXlsxTest(TestSheetWriterXlsxFixture testSheetWriterXlsxFixture, ITestOutputHelper outputHelper) :
            base(testSheetWriterXlsxFixture, outputHelper)
        { }

        /// <summary>
        /// 当DataTable是null时, 应抛出ArgumentNullException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenDataTableIsNull()
        {
            base.Test_Write_WhenDataTableIsNull();
        }

        /// <summary>
        /// 当DataTable列为空时, 应抛出ArgumentException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenDataColumnCountIsZero()
        {
            base.Test_Write_WhenDataColumnCountIsZero();
        }

        /// <summary>
        /// 当首行索引值为负数的时候, 应抛出ArgumentException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenFirstRowIndexIsNegative()
        {
            base.Test_Write_WhenFirstRowIndexIsNegative();
        }

        /// <summary>
        /// 当首列索引值为负数的时候, 应抛出ArgumentException异常
        /// </summary>
        [Fact]
        public override void Test_Write_WhenFirstColIndexIsNegative()
        {
            base.Test_Write_WhenFirstColIndexIsNegative();
        }

        /// <summary>
        /// 测试写入工作表
        /// </summary>
        /// <param name="sheetIndex">工作表索引</param>
        /// <param name="firstRowIndex">首行索引</param>
        /// <param name="firstColIndex">首列索引</param>
        /// <param name="hasHeader">是否包含头部</param>
        /// <param name="setSheetStyleFunc">设置工作表样式委托</param>
        [Theory]
        [MemberData(nameof(ParamsData.WriteToSheetParam), MemberType = typeof(ParamsData))]
        public override void Test_Write(int sheetIndex, int firstRowIndex, int firstColIndex, bool hasHeader, Action<ISheet>? setSheetStyleFunc)
        {
            base.Test_Write(sheetIndex, firstRowIndex, firstColIndex, hasHeader, setSheetStyleFunc);
        }
    }
}
