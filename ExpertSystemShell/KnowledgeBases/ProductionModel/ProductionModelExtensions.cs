using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    /// <summary>
    /// Методы-расширения для выражений продукционной модели.
    /// </summary>
    public static class ProductionModelExtensions
    {
        /// <summary>
        /// Проверяее, является ли заданное выражение фактом.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <returns>Возвращает <c>true</c>, если заданное выражение является фактом.</returns>
        public static bool IsFact(this Expression expression)
        {
            BinaryOperator bo = expression as BinaryOperator;
            if(bo != null)
            {
                if(bo.Sign == "-")
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Определяет, является ли заданное выражение логическим "или", и получает его операнды.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <param name="left">Левый операнд.</param>
        /// <param name="right">Правый операнд.</param>
        /// <returns>Возвращает <c>true</c>, если заданное выражение является логическим "или".
        /// </returns>
        public static bool SplitOr(this Expression expression,
            out Expression left, out Expression right)
        {
            BinaryOperator bo = expression as BinaryOperator;
            left = null;
            right = null;
            if(bo != null && bo.Sign == "|")
            {
                left = bo.Left;
                right = bo.Right;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Определяет, является ли заданное выражение логическим "и", и получает его операнды.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <param name="left">Левый операнд.</param>
        /// <param name="right">Правый операнд.</param>
        /// <returns>Возвращает <c>true</c>, если заданное выражение является логическим "и".
        /// </returns>
        public static bool SplitAnd(this Expression expression,
            out Expression left, out Expression right)
        {
            BinaryOperator bo = expression as BinaryOperator;
            left = null;
            right = null;
            if (bo != null && bo.Sign == "&")
            {
                left = bo.Left;
                right = bo.Right;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Возвращает факт, содержащийся в выражении. Если выражение не является фактом 
        /// возвращает <c>null</c>.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <returns>Возвращает факт, содержащийся в выражении. Если выражение не является фактом 
        /// возвращает <c>null</c>.</returns>
        public static ProductionFact GetFact(this Expression expression)
        {
            BinaryOperator bo = expression as BinaryOperator;
            if (bo != null)
            {
                if (bo.Sign == "-")
                {
                    Variable variable = bo.Left as Variable;
                    return new ProductionFact(variable.VariableNames.First(),
                        bo.Right.Calculate());
                }
            }
            return null;
        }

        public static bool SplitFactConjunction(this Expression expression,
            out IEnumerable<Expression> facts)
        {
            List<Expression> result = new List<Expression>();
            facts = new List<Expression>();
            Stack<Expression> stack = new Stack<Expression>();
            stack.Push(expression);
            while(stack.Count > 0)
            {
                Expression currExpression = stack.Pop();
                if(currExpression.IsFact())
                {
                    if (!facts.Contains(currExpression))
                        result.Add(currExpression.Copy());
                }
                else
                {
                    Expression left = null;
                    Expression right = null;
                    if(currExpression.SplitAnd(out left, out right))
                    {
                        stack.Push(left);
                        stack.Push(right);
                    }
                    else return false;
                }                
            }
            facts = result;
            return true;
        }


    }
}
