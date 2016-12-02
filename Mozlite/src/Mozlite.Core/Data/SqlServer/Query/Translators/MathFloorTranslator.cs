using System;
using Mozlite.Data.Query.Translators;

namespace Mozlite.Data.SqlServer.Query.Translators
{
    /// <summary>
    /// Math.Floorת������
    /// </summary>
    public class MathFloorTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        /// <summary>
        /// ��ʼ����<see cref="MathFloorTranslator"/>��
        /// </summary>
        public MathFloorTranslator()
            : base(typeof(Math), nameof(Math.Floor), "FLOOR")
        {
        }
    }
}