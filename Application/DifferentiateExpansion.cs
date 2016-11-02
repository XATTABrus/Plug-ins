using System;
using System.Linq.Expressions;

namespace Application
{
    public static class DifferentiateExpansion
    {
        /// <summary>
        /// Дифференцирует заданное лямбда выражение.
        /// </summary>
        /// <param name="source">Исходное лямбда выражение</param>
        public static Expression<Func<double, double>> Differentiate(this Expression<Func<double, double>> source)
        {
            return Expression.Lambda(ParsingExpression(source.Body), source.Parameters[0]) as Expression<Func<double, double>>;
        }

        /// <summary>
        /// Дифференцирует заданное лямбда выражение и возвращает скомпилированную лямбду.
        /// </summary>
        /// <param name="source">Исходное лямбда выражение</param>
        public static Func<double, double> DifferentiateAndCompile(this Expression<Func<double, double>> source)
        {
            return Expression.Lambda(ParsingExpression(source.Body), source.Parameters[0]).Compile() as Func<double, double>;
        }

        /// <summary>
        /// Дифференцирование исходного вырожения. 
        /// </summary>
        /// <param name="expression">Исходное выражение</param>
        private static Expression ParsingExpression(Expression expression)
        {
            // Производная от константы = 0
            if (expression is ConstantExpression) { return Expression.Constant(0.0); }

            // Производная от x = 1
            if (expression is ParameterExpression) { return Expression.Constant(1.0); }

            // Производная от вырожений типа + и *
            if (expression is BinaryExpression)
            {
                var ex = expression as BinaryExpression;

                switch (ex.NodeType)
                {
                    case ExpressionType.Add:
                        return Expression.Add(ParsingExpression(ex.Left), ParsingExpression(ex.Right));

                    case ExpressionType.Multiply:
                        return Expression.Add(
                            Expression.Multiply(ex.Left, ParsingExpression(ex.Right)),
                            Expression.Multiply(ex.Right, ParsingExpression(ex.Left)));

                    default:
                        throw new ArgumentException("Поддерживается только сложение и умножение!");
                }
            }

            // Производная от Sin(x)
            if (expression is MethodCallExpression)
            {
                var ex = expression as MethodCallExpression;
                
                if (ex.Method.Name == "Sin" && ex.Method.DeclaringType.FullName == "System.Math")
                {
                    var arg = ex.Arguments[0];
                    return Expression.Multiply(Expression.Call(typeof(Math).GetMethod("Cos"), arg), ParsingExpression(arg));
                }

                throw new ArgumentException("Поддерживается только функиця Sin(x)!");
            }

            throw new ArgumentException("Не верное выражение!");
        }
    }
}