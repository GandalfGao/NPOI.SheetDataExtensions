using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI_API_Package
{
    /// <summary>
    /// 列的配置类
    /// </summary>
    public class ColumnConfigAttribute : Attribute
    {
        /// <summary>
        /// 列映射
        /// </summary>
        public string ColumnMapping { get; set; } = string.Empty;

        /// <summary>
        /// 列索引(从0开始)
        /// </summary>
        public int ColumnIndex { get; set; }
    }
}
