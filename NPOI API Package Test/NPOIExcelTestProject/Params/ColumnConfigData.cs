using NPOI_API_Package;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace NPOIExcelTestProject.Params
{
    /// <summary>
    /// 列配置参数类
    /// </summary>
    public static class ColumnConfigData
    {
        /// <summary>
        /// 列配置为空的参数集合
        /// </summary>
        public static TheoryData<IEnumerable<ColumnConfigAttribute>?> EmptyColumnConfigParams { get; } =
        [
            null,
            []
        ];
    }
}
