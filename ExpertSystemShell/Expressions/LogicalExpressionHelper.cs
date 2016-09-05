using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using System.Text.RegularExpressions;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Построитель выражений для модели продукционных правил.
    /// </summary>
    /// <seealso cref="ExpertSystemShell.Expressions.ExpressionHelper" />
    public class LogicalExpressionHelper: ExpressionHelper
    {
        /// <summary>
        /// Создаёт новый объект <see cref="LogicalExpressionHelper"/>.
        /// </summary>
        public LogicalExpressionHelper(): base()
        {

        }

        /// <summary>
        /// Создаёт множество используемых меток.
        /// </summary>
        protected override void CreateLabels()
        {
            base.CreateLabels();
            usedLabels.Add(ProductionExprGrammar.LeftParan.Name,
                (a) => { stack.Push(new Constant(ProductionExprGrammar.LeftParan.Name)); });
            usedLabels.Add(ProductionExprGrammar.RightParan.Name,
                HandleRightParan);
            usedLabels.Add(ProductionExprGrammar.BinaryOp.Name,
                HandleBinaryOperator);
            usedLabels.Add(ProductionFactGrammar.Property.Name,
                (a) => { output.Enqueue(new Variable(a.Value)); });
            usedLabels.Add(ProductionFactGrammar.Value.Name,
                (a) => { output.Enqueue(new Constant(a.Value)); });
            usedLabels.Add(ProductionFactGrammar.EqualityOp.Name,
                HandleBinaryOperator);
            usedLabels.Add(ProductionExprGrammar.NotOp.Name,
                (a) => { stack.Push((UnaryOperator)unaryOperators["!"].Clone()); });
        }
        /// <summary>
        /// Создаёт множество стандартных операторов.
        /// </summary>
        protected override void CreateDefaultOperators()
        {
            //no mathematics here
            CreateBooleanOperators();
        }
        /// <summary>
        /// Создает множество логических операторов.
        /// </summary>
        protected override void CreateBooleanOperators()
        {
            base.CreateBooleanOperators();
            binaryOperators.Add("-", new BinaryOperator((a, b) =>
                { return a == b;} , "-", Associativity.Left, 10));
        }

        /// <summary>
        /// Вспомогательная функция для обработки бинарного оператора.
        /// </summary>
        /// <param name="pair">Пара метка - значение.</param>
        protected void HandleBinaryOperator(KeyValuePair<string, string> pair)
        {
            Operator o = binaryOperators[pair.Value];
            if (o as UnaryOperator == null)
            {
                Operator currOperator = stack.Count > 0 ? stack.Peek() as Operator : null;
                while (currOperator != null &&
                    ((o.Associativity == Associativity.Left && o.Precendence <= currOperator.Precendence) ||
                    (o.Associativity == Associativity.Right && o.Precendence < currOperator.Precendence)))
                {
                    output.Enqueue(stack.Pop());
                    currOperator = stack.Count > 0 ? stack.Peek() as Operator : null;
                }
            }
            stack.Push((Operator)o.Clone());
        }
        /// <summary>
        /// Всопомгательная функция для обработки закрывающий скобки.
        /// </summary>
        /// <param name="pair">Пара метка - значение.</param>
        protected void HandleRightParan(KeyValuePair<string, string> pair)
        {
            Constant currOperand = stack.Count > 0 ? stack.Peek() as Constant : null;
            while (currOperand == null || 
                !ProductionExprGrammar.LeftParan.Name.Equals(currOperand.Calculate().ToString()))
            {
                output.Enqueue(stack.Pop());
                currOperand = stack.Peek() as Constant;
            }
            stack.Pop(); //left parenthesis is expected
            if (stack.Count > 0 && stack.Peek() is FunctionCall)
                output.Enqueue(stack.Pop());
        }

        /// <summary>
        /// Разбивает на лексемы заданную строку.
        /// </summary>
        /// <param name="expression">Строка, содержащая выражение.</param>
        /// <returns>
        /// Возвращает перечисление, содержащее все лексемы в текущей строке.
        /// </returns>
        protected override IEnumerable<KeyValuePair<string, string>> Tokenize(string expression)
        {
            expression = expression.Trim();
            foreach(var item in replacements)
            {
                expression = item.Key.Replace(expression, item.Value);
            }
            return Tokenize(ProductionExprGrammar.Expression.Parse(expression)[0]);
        }
    }
}
