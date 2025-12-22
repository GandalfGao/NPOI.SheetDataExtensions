using NPOI.SS.UserModel;

namespace NPOI.SheetDataExtensions
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
        public static bool IsEmpty(this IRow? row)
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
