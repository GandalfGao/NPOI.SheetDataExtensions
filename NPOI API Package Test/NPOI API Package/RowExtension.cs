using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI_API_Package
{
    /// <summary>
    /// sheet行扩展类
    /// </summary>
    public static class RowExtension
    {
        /// <summary>
        /// 判断行是否为空
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool IsEmpty(this IRow row)
        {
            if (row == null)
            {
                return true;
            }

            foreach (var cell in row.Cells)
            {
                if (!cell.IsEmpty())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
