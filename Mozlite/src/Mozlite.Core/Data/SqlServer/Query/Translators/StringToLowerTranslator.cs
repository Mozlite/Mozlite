using Mozlite.Data.Query.Translators;

namespace Mozlite.Data.SqlServer.Query.Translators
{
    /// <summary>
    /// Сдת����
    /// </summary>
    public class StringToLowerTranslator : ParameterlessInstanceMethodCallTranslator
    {
        /// <summary>
        /// ��ʼ����<see cref="StringToLowerTranslator"/>��
        /// </summary>
        public StringToLowerTranslator()
            : base(typeof(string), nameof(string.ToLower), "LOWER")
        {
        }
    }
}