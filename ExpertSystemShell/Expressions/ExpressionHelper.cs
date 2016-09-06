using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using Diggins.Jigsaw;

namespace ExpertSystemShell.Expressions
{
    /// <summary>
    /// Класс для преобразования строки в готовое выражение <see cref="PrLanguages.Expressions.Expression"/>.
    /// </summary>
    public abstract class ExpressionHelper
    {
        #region operator and functions storage and default replacements

        protected static Dictionary<string, UnaryOperator> unaryOperators;
        protected static Dictionary<string, BinaryOperator> binaryOperators;
        protected static Dictionary<string, FunctionCall> functions;
        protected static Dictionary<Regex, string> replacements;

        #endregion

        protected Queue<Expression> output = new Queue<Expression>();
        protected Stack<Expression> stack = new Stack<Expression>();
        protected Stack<int> indexerParamStack = new Stack<int>();
        protected Dictionary<string, Action<KeyValuePair<string, string>>> usedLabels;

        /// <summary>
        /// Получает или задаёт множество функций.
        /// </summary>
        public Dictionary<string, FunctionCall> Functions
        {
            get { return functions; }
            set { functions = value; }
        }
        /// <summary>
        /// Получает или задаёт множество бинарных операторов.
        /// </summary>
        public Dictionary<string, BinaryOperator> BinaryOperators
        {
            get { return binaryOperators; }
            set { binaryOperators = value; }
        }
        /// <summary>
        /// Получает или задаёт множество унарных операторов.
        /// </summary>
        public Dictionary<string, UnaryOperator> UnaryOperators
        {
            get { return unaryOperators; }
            set { unaryOperators = value; }
        }

        /// <summary>
        /// Инициализирует <see cref="ExpressionHelper"/>, используя стандартный набор функций
        /// и операторов.
        /// </summary>
        public ExpressionHelper()
        {
            unaryOperators = new Dictionary<string, UnaryOperator>();
            binaryOperators = new Dictionary<string, BinaryOperator>();
            usedLabels = new Dictionary<string, Action<KeyValuePair<string, string>>>();
            replacements = new Dictionary<Regex, string>();

            CreateDefaultFunctions();
            CreateDefaultOperators();
            CreateDefaultReplacements();
            CreateLabels();
        }

        #region Initialization

        /// <summary>
        /// Создаёт множество стандартных операторов.
        /// </summary>
        protected virtual void CreateDefaultOperators()
        {
            CreateMathematicOperators();
            CreateBooleanOperators();
        }
        /// <summary>
        /// Создает множество логических операторов.
        /// </summary>
        protected virtual void CreateBooleanOperators()
        {
            binaryOperators.Add("&", new BinaryOperator((a, b) => { return ((bool)a) & ((bool)b); }, "&", Associativity.Left, 4));
            binaryOperators.Add("|", new BinaryOperator((a, b) => { return ((bool)a) | ((bool)b); }, "|", Associativity.Left, 4));
            binaryOperators.Add("!=", new BinaryOperator((a, b) => {return a!=b;}, "!=", Associativity.Left, 3));
            //dynamic objects does not support '!' operator
            unaryOperators.Add("!", new UnaryOperator((a) => { if (a.Equals(true)) return false; else return true; }, "!", Associativity.Left, 5));
            binaryOperators.Add("==", new BinaryOperator((a, b) => { return a == b; }, "==", Associativity.Left, 3));
            binaryOperators.Add(">=", new BinaryOperator((a, b) => { return a >= b; }, ">=", Associativity.Left, 3));
            binaryOperators.Add("<=", new BinaryOperator((a, b) => { return a <= b; }, "<=", Associativity.Left, 3));
            binaryOperators.Add("<", new BinaryOperator((a, b) => { return a < b; }, "<", Associativity.Left, 3));
            binaryOperators.Add(">", new BinaryOperator((a, b) => { return a > b; }, ">", Associativity.Left, 3));
        }
        /// <summary>
        /// Создаёт множество математических операторов.
        /// </summary>
        protected void CreateMathematicOperators()
        {
            binaryOperators.Add("+", new BinaryOperator((a, b) => { return a + b; }, "+", Associativity.Left, 9));
            binaryOperators.Add("-", new BinaryOperator((a, b) => { return a - b; }, "-", Associativity.Left, 9));
            binaryOperators.Add("*", new BinaryOperator((a, b) => { return a * b; }, "*", Associativity.Left, 10));
            binaryOperators.Add("/", new BinaryOperator((a, b) => { return a / b; }, "/", Associativity.Left, 10));
            binaryOperators.Add("^", new BinaryOperator((a, b) => { return Math.Pow(a, b); }, "^", Associativity.Left, 7));
            binaryOperators.Add("%", new BinaryOperator((a, b) => { return a % b; }, "%", Associativity.Left, 10));
            binaryOperators.Add(">>", new BinaryOperator((a, b) => { return a >> b; }, ">>", Associativity.Left, 6));
            binaryOperators.Add("<<", new BinaryOperator((a, b) => { return a << b; }, ">>", Associativity.Left, 6));

            unaryOperators.Add("++", new UnaryOperator((a) => { return a + 1; }, "++", Associativity.Left, 5));
            unaryOperators.Add("--", new UnaryOperator((a) => { return a - 1; }, "--", Associativity.Left, 5));
            unaryOperators.Add("-", new UnaryOperator((a) => { return a * (-1); }, "-", Associativity.Right, 11));
        }
        /// <summary>
        /// Множество замен по умолчанию включает в себя замены для логических операторов.
        /// Замены применяются для того, чтобы не определять много одинаковых операторов с 
        /// разными именами.
        /// </summary>
        protected virtual void CreateDefaultReplacements()
        {
            replacements = new Dictionary<Regex, string>();

            replacements.Add(new Regex(@"\bили\b",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant), "|");
            replacements.Add(new Regex("@\bне\b",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant), "!");
            replacements.Add(new Regex(@"\bи\b",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant), "&");
        }
        /// <summary>
        /// Создаёт множество функций по умолчанию. Это множество включает в себя функции из класса
        /// <see cref="System.Math"/> и специальныю функцию для вызовы методов-индексаторов.
        /// </summary>
        protected void CreateDefaultFunctions()
        {
            functions = new Dictionary<string, FunctionCall>();
            functions.Add("Round", new FunctionCall((a) => { return Math.Round((double)a[0], (int)a[1]); }, "Round", 2));
            functions.Add("PI", new FunctionCall((a) => { return Math.PI; }, "PI", 0));
            functions.Add("Abs", new FunctionCall((a) => {return Math.Abs(a[0]);}, "Abs", 1));
            functions.Add("Acos", new FunctionCall((a) => {return Math.Acos(a[0]);}, "Acos", 1));
            functions.Add("Asin", new FunctionCall((a) => { return Math.Asin(a[0]); }, "Asin", 1));
            functions.Add("Atan", new FunctionCall((a) => {return Math.Atan(a[0]);}, "Atan", 1));
            functions.Add("Atan2", new FunctionCall((a) => {return Math.Atan2(a[0], a[1]);}, "Atan2", 2));
            functions.Add("BigMul", new FunctionCall((a) => { return Math.BigMul(a[0], a[1]); }, "BigMul", 2));
            functions.Add("Celling", new FunctionCall((a) => { return Math.BigMul(a[0], a[1]); }, "Celling", 2));
            functions.Add("Cos", new FunctionCall((a) => { return Math.Cos(a[0]); }, "Cos", 1));
            functions.Add("Cosh", new FunctionCall((a) => { return Math.Cosh(a[0]); }, "Cosh", 1));
            functions.Add("DivRem", new FunctionCall((a) =>
                { long o = a[2]; var res = Math.DivRem(a[0], a[1], out o); a[2] = o; return res; }, "DivRem", 3));
            functions.Add("E", new FunctionCall((a) => { return Math.E; }, "E", 0));
            functions.Add("Equals", new FunctionCall((a) => { return Math.Equals(a[0], a[1]); }, "Equals", 2));
            functions.Add("Exp", new FunctionCall((a) => { return Math.Exp(a[0]); }, "Exp", 1));
            functions.Add("Floor", new FunctionCall((a) => { return Math.Floor(a[0]); }, "Floor", 1));
            functions.Add("IEEEReminder", new FunctionCall((a) => { return Math.IEEERemainder(a[0], a[1]); }, "IEEEReminder", 2));
            functions.Add("Log", new FunctionCall((a) => { return Math.Log(a[0], a[1]); }, "Log", 2));
            functions.Add("Log10", new FunctionCall((a) => { return Math.Log10(a[0]); }, "Log10", 1));
            functions.Add("Max", new FunctionCall((a) => { return Math.Max(a[0], a[1]); }, "Max", 2));
            functions.Add("Min", new FunctionCall((a) => { return Math.Min(a[0], a[1]); }, "Min", 2));
            functions.Add("Pow", new FunctionCall((a) => { return Math.Pow(a[0], a[1]); }, "Pow", 2));
            functions.Add("ReferenceEquals", new FunctionCall((a) => { return Math.ReferenceEquals(a[0], a[1]); }, "ReferenceEquals", 2));
            functions.Add("Sign", new FunctionCall((a) => { return Math.Sign(a[0]); }, "Sing", 1));
            functions.Add("Sin", new FunctionCall((a) => { return Math.Sin(a[0]); }, "Sin", 1));
            functions.Add("Sinh", new FunctionCall((a) => { return Math.Sinh(a[0]); }, "Sinh", 1));
            functions.Add("Sqrt", new FunctionCall((a) => { return Math.Sqrt(a[0]); }, "Sqrt", 1));
            functions.Add("Tan", new FunctionCall((a) => { return Math.Tan(a[0]); }, "Tan", 1));
            functions.Add("Tanh", new FunctionCall((a) => { return Math.Tanh(a[0]); }, "Tanh", 1));
            functions.Add("Truncate", new FunctionCall((a) => { return Math.Truncate(a[0]); }, "Truncate", 1));

            //special function for calling indexers
            //number of operand will be set during runtime
            functions.Add("[", new FunctionCall((a) => { dynamic[] args = a.Take(a.Length - 1).ToArray(); return IndexerCaller.GetIndexerValue(a[a.Length - 1], args); }, "[", 1));
        }
        /// <summary>
        /// Создаёт множество используемых меток.
        /// </summary>
        protected virtual void CreateLabels()
        {
            
        }

        #endregion

        /// <summary>
        /// По заданной строке получает вычисляемое выражение. Строка может содержать операторы,
        /// функции, индексаторы и переменные.
        /// </summary>
        /// <param name="expression">Строка, содержащаяя выражение.</param>
        /// <returns>Возвращает выражение, содержащееся в строке.</returns>
        public Expression CreateExpression(string expression)
        {
            stack = new Stack<Expression>();
            indexerParamStack = new Stack<int>();
            output = new Queue<Expression>();
            foreach(var pair in Tokenize(expression))
            {
                usedLabels[pair.Key](pair);
            }
            while (stack.Count > 0) output.Enqueue(stack.Pop());
            return ConstructExpressionTree().Simplify();
        }
        /// <summary>
        /// По заданному дереву разбора получает вычисляемое выражение.
        /// </summary>
        /// <param name="expression">дерево разбора.</param>
        /// <returns>Возвращает выражение, содержащееся в дереве.</returns>
        public Expression CreateExpression(Node expression)
        {
            stack = new Stack<Expression>();
            indexerParamStack = new Stack<int>();
            output = new Queue<Expression>();
            foreach (var pair in Tokenize(expression))
            {
                usedLabels[pair.Key](pair);
            }
            while (stack.Count > 0) output.Enqueue(stack.Pop());
            return ConstructExpressionTree().Simplify();
        }
        /// <summary>
        /// Восстанавливает дерево выражения.
        /// </summary>
        /// <returns>Возвращает готовое выражение.</returns>
        private Expression ConstructExpressionTree()
        {
            Expression currExp = null;
            Stack<Expression> stack = new Stack<Expression>();
            while (output.Count > 0)
            {
                currExp = output.Dequeue();
                FunctionCall func = currExp as FunctionCall;
                if (func != null) //function on the top (requieres some arguments)
                {
                    for (int i = func.ArgumentsCount - 1; i >= 0; i--)
                    {
                        func.Arguments[i] = stack.Pop();
                    }
                }
                else
                {
                    UnaryOperator so = currExp as UnaryOperator;
                    if (so != null) so.Left = stack.Pop();
                    else
                    {
                        BinaryOperator bo = currExp as BinaryOperator;
                        if (bo != null)
                        {
                            bo.Right = stack.Pop();
                            bo.Left = stack.Pop();
                        }
                    }
                }
                stack.Push(currExp);
            }
            if (stack.Count > 1) throw new ArgumentException("provided string contains incorrect expression.");
            return stack.Peek();
        }

        #region Parsing

        /// <summary>
        /// Разбивает на лексемы заданную строку.
        /// </summary>
        /// <param name="expression">Строка, содержащая выражение.</param>
        /// <returns>Возвращает перечисление, содержащее все лексемы в текущей строке.</returns>
        protected abstract IEnumerable<KeyValuePair<string, string>> Tokenize(string expression);
        /// <summary>
        /// Проходит по дереву разбора и возвращает токены и их метки.
        /// </summary>
        /// <param name="root">Корень дерева разбора.</param>
        /// <returns>Возвращает перечисление, содержащее пары метка-токен.</returns>
        protected IEnumerable<KeyValuePair<string, string>> Tokenize(Node root)
        {
            Stack<Node> nodes = new Stack<Node>();
            Stack<Node> visited = new Stack<Node>();
            nodes.Push(root);
            while (nodes.Count > 0)
            {
                Node currNode = nodes.Pop();
                visited.Push(currNode);
                foreach (var child in currNode.Descendants)
                    if (currNode.Label != child.Label) nodes.Push(child);
            }
            foreach (var node in visited)
                if (node.IsLeaf) 
                    yield return new KeyValuePair<string, string>(node.Label, node.Text);
        }

        #endregion

    }
}
