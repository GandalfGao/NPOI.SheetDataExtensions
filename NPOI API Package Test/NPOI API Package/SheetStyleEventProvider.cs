using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI_API_Package
{
    /// <summary>
    /// 工作表样式事件提供器
    /// </summary>
    public class SheetStyleEventProvider
    {
        /// <summary>
        /// 设置表头样式事件
        /// </summary>
        public event Action<IRow, ISheet>? SetHeaderStyleEvent;

        /// <summary>
        /// 设置内容样式事件
        /// </summary>
        public event Action<List<IRow>, ISheet>? SetContentStyleEvent;

        /// <summary>
        /// 设置表头样式事件触发
        /// </summary>
        /// <param name="headerRow"></param>
        /// <param name="sheet"></param>
        public void OnSetHeaderStyleEvent(IRow headerRow, ISheet sheet)
        {
            SetHeaderStyleEvent?.Invoke(headerRow, sheet);
        }

        /// <summary>
        /// 设置内容样式事件触发
        /// </summary>
        /// <param name="contentRows"></param>
        /// <param name="sheet"></param>
        public void OnSetContentStyleEvent(List<IRow> contentRows, ISheet sheet)
        {
            SetContentStyleEvent?.Invoke(contentRows, sheet);
        }
    }
}
