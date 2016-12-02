using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mozlite.Data.Query.Expressions;

namespace Mozlite.Data.Query.Translators
{
    /// <summary>
    /// �޲����ķ�������ת�������ࡣ
    /// </summary>
    public abstract class ParameterlessInstanceMethodCallTranslator : IMethodCallTranslator
    {
        private readonly Type _declaringType;
        private readonly string _clrMethodName;
        private readonly string _sqlFunctionName;
        /// <summary>
        /// ��ʼ����<see cref="ParameterlessInstanceMethodCallTranslator"/>��
        /// </summary>
        /// <param name="declaringType">�������͡�</param>
        /// <param name="clrMethodName">CLR�������ơ�</param>
        /// <param name="sqlFunctionName">SQL�������ơ�</param>
        protected ParameterlessInstanceMethodCallTranslator([NotNull] Type declaringType, [NotNull] string clrMethodName, [NotNull] string sqlFunctionName)
        {
            _declaringType = declaringType;
            _clrMethodName = clrMethodName;
            _sqlFunctionName = sqlFunctionName;
        }

        /// <summary>
        /// ת�����ʽ��
        /// </summary>
        /// <param name="methodCallExpression">�������ñ��ʽ��</param>
        /// <returns>����ת����ı��ʽ��</returns>
        public virtual Expression Translate([NotNull] MethodCallExpression methodCallExpression)
        {
            var methodInfo = _declaringType.GetTypeInfo()
                .GetDeclaredMethods(_clrMethodName).SingleOrDefault(m => !m.GetParameters().Any());

            if (methodInfo == methodCallExpression.Method)
            {
                var sqlArguments = new[] { methodCallExpression.Object }.Concat(methodCallExpression.Arguments);
                return new SqlFunctionExpression(_sqlFunctionName, methodCallExpression.Type, sqlArguments);
            }

            return null;
        }
    }
}