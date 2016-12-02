using Mozlite.Data.Query.Translators;

namespace Mozlite.Data.SqlServer.Query.Translators
{
    /// <summary>
    /// ��дת����
    /// </summary>
    public class StringToUpperTranslator : ParameterlessInstanceMethodCallTranslator
    {
        /// <summary>
        /// ��ʼ����<see cref="StringToUpperTranslator"/>��
        /// </summary>
        public StringToUpperTranslator()
            : base(typeof(string), nameof(string.ToUpper), "UPPER")
        {
        }
    }
}