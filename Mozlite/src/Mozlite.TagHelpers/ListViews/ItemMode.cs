using System;

namespace Mozlite.TagHelpers.ListViews
{
    /// <summary>
    /// ��ģʽ��
    /// </summary>
    [Flags]
    public enum ItemMode
    {
        /// <summary>
        /// ������
        /// </summary>
        Normal = 0,
        /// <summary>
        /// ���С�
        /// </summary>
        First = 1,
        /// <summary>
        /// β�С�
        /// </summary>
        Last = 2,
        /// <summary>
        /// ���С�
        /// </summary>
        Line = First | Last,
    }
}