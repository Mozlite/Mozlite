using System;
using Mozlite.Data.Query.Translators;

namespace Mozlite.Data.SqlServer.Query.Translators
{
    /// <summary>
    /// Math.Ceilingת������
    /// </summary>
    public class MathCeilingTranslator : MultipleOverloadStaticMethodCallTranslator
    {
        /// <summary>
        /// ��ʼ����<see cref="MathCeilingTranslator"/>��
        /// </summary>
        public MathCeilingTranslator()
            : base(typeof(Math), nameof(Math.Ceiling), "CEILING")
        {
        }
    }
}