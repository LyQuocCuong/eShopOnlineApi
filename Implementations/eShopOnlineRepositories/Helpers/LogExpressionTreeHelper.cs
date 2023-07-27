namespace eShopOnlineRepositories.Helpers
{
    internal class LogExpressionTreeHelper
    {
        internal static string PrintExpression(Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                string left = PrintExpression(binaryExpression.Left);
                string right = PrintExpression(binaryExpression.Right);
                string op = GetBinaryOperator(binaryExpression.NodeType);
                return $"({left} {op} {right})";
            }
            else if (expression is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                string methodName = methodCallExpression.Method.Name;
                string target = PrintExpression(methodCallExpression.Object);
                string arguments = string.Join(", ", methodCallExpression.Arguments.Select(PrintExpression));
                return $"{target}.{methodName}({arguments})";
            }
            else if (expression is ConstantExpression constantExpression)
            {
                return constantExpression.Value.ToString();
            }
            else if (expression is UnaryExpression unaryExpression && unaryExpression.NodeType == ExpressionType.Not)
            {
                string operand = PrintExpression(unaryExpression.Operand);
                return $"!{operand}";
            }

            return "Unsupported Expression";
        }

        internal static string GetBinaryOperator(ExpressionType nodeType)
        {
            switch (nodeType)
            {
                case ExpressionType.Equal:
                    return "==";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.AndAlso:
                    return "&&";
                case ExpressionType.OrElse:
                    return "||";
                // Add more cases for other binary operators as needed.
                default:
                    return "Unknown";
            }
        }
    }
}
