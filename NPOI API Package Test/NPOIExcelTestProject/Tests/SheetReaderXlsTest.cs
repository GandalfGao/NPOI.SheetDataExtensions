using NPOI_API_Package;
using NPOIExcelTestProject.Fixtures.CollectionFixtures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit.Abstractions;

namespace NPOIExcelTestProject.Tests
{
    /// <summary>
    /// 工作表读取器测试类
    /// </summary>
    [Collection(nameof(TestSheetXlsFixturesCollection))]
    public class SheetReaderXlsTest : SheetReaderTestBase
    {
        public SheetReaderXlsTest(TestSheetReaderXlsFixture testXlsFileReaderFixture, ITestOutputHelper outputHelper) : base(testXlsFileReaderFixture, outputHelper)
        { }

        /// <summary>
        /// 测试columnConfigs不为空集合时的读取结果
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.HasHeaderAndColumnConfigsParam), MemberType = typeof(ParamsData))]
        public override void Test_Read_WhenColumnConfigsIsNotEmpty(bool hasHeader, IEnumerable<ColumnConfig> columnConfigs)
        {
            base.Test_Read_WhenColumnConfigsIsNotEmpty(hasHeader, columnConfigs);
        }

        /// <summary>
        /// 当工作表中包含空白行和空白列的情况下，测试columnConfigs不为空集合时的读取结果
        /// </summary>
        /// <param name="hasHeader"></param>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.HasHeaderAndColumnConfigsParam_WithHasBlankRowsAndCols), MemberType = typeof(ParamsData))]
        public override void Test_Read_WhenColumnConfigsIsNotEmpty_WithHasBlankRowsAndCols(bool hasHeader, IEnumerable<ColumnConfig> columnConfigs)
        {
            base.Test_Read_WhenColumnConfigsIsNotEmpty_WithHasBlankRowsAndCols(hasHeader, columnConfigs);
        }

        /// <summary>
        /// 测试当首行索引值小于0时抛出异常
        /// </summary>
        [Fact]
        public override void Test_Read_WhenFirstRowIndexIsNegative()
        {
            base.Test_Read_WhenFirstRowIndexIsNegative();
        }

        /// <summary>
        /// 测试当hasHeader为false且columnConfigs为null或空集合时抛出异常
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.EmptyColumnConfigParams), MemberType = typeof(ParamsData))]
        public override void Test_Read_WhenHasHeaderIsFalseAndColumnConfigsIsNullOrEmpty(IEnumerable<ColumnConfig>? columnConfigs)
        {
            base.Test_Read_WhenHasHeaderIsFalseAndColumnConfigsIsNullOrEmpty(columnConfigs);
        }

        /// <summary>
        /// 测试当hasHeader为true且columnConfigs为null或空集合时的读取结果
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.EmptyColumnConfigParams), MemberType = typeof(ParamsData))]
        public override void Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty(IEnumerable<ColumnConfig>? columnConfigs)
        {
            base.Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty(columnConfigs);
        }

        /// <summary>
        /// 在工作表中包含空白行和空白列的情况下，测试当hasHeader为true且columnConfigs为null或空集合时的读取结果
        /// </summary>
        /// <param name="columnConfigs"></param>
        [Theory]
        [MemberData(nameof(ParamsData.EmptyColumnConfigParams), MemberType = typeof(ParamsData))]
        public override void Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty_WithHasBlankRowsAndCols(IEnumerable<ColumnConfig>? columnConfigs)
        {
            base.Test_Read_WhenHasHeaderIsTrueAndColumnConfigsIsNullOrEmpty_WithHasBlankRowsAndCols(columnConfigs);
        }
    }
}
