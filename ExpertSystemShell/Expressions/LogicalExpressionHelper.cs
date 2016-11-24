using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Parsers.Grammars.ProductionModel;
using System.Text.RegularExpressions;
using ExpertSystemShell.KnowledgeBases.ProductionModel;

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
        /// По выражению получает новое выражение, эквивалентное исходному, в ДНФ.
        /// </summary>
        /// <param name="expression">Исходное выражение.</param>
        /// <returns>Возвращает ДНФ для заданного выражения.</returns>
        public Expression GetDNF(Expression expression)
        {
            Expression e = expression.Copy();
            return GetDNFRec(NotToAtomary(e));
        }
        /// <summary>
        /// "Спускает" логическое НЕ до уровня литералов.
        /// </summary>
        /// <param name="expression">Исходное выражение.</param>
        /// <returns>Возвращает выражение, в котором логическое НЕ стоит толькое перед литералами.</returns>
        public Expression NotToAtomary(Expression expression)
        {
            return (NotToAtomaryRec(expression.Copy()));
        }

        public Expression GetCNF(Expression expression)
        {
            Expression e = expression.Copy();
            e = NotToAtomary(e);
            return GetCNFRec(e);
        }

        private Expression GetCNFRec(Expression expression)
        {
            BinaryOperator bo = expression as BinaryOperator;
            if(bo != null)
            {
                if(bo.Sign == "&")
                {
                    bo.Left = GetCNFRec(bo.Left);
                    bo.Right = GetCNFRec(bo.Right);
                }
                else if(bo.Sign == "|")
                {
                    if(!IsCNFOr(bo)) //если дизъюнкция не является элементарной
                    {
                        return AbsorptionRule(bo);
                    }
                }
            }
            return bo;
        }
        private static List<Expression> GetDisjunctCollection(Expression expression)
        {
            BinaryOperator bo = expression as BinaryOperator;
            if (bo != null && bo.Sign == "|")
            {
                List<Expression> disjunctCollection = new List<Expression>();
                disjunctCollection.Add(bo.Left);
                disjunctCollection.Add(bo.Right);
                bool exists = true;
                while (exists) //пока есть не атомарная дизюнкция
                {
                    exists = false;
                    foreach (var item in disjunctCollection)
                    {
                        Expression left = null;
                        Expression right = null;
                        if (item.SplitOr(out left, out right))
                        {
                            disjunctCollection.Add(left);
                            disjunctCollection.Add(right);
                            exists = true;
                        }
                    }
                }
                return disjunctCollection;
            }
            return new List<Expression>();
        }

        private static List<Expression> GetConjunctCollection(Expression expression)
        {
            BinaryOperator bo = expression as BinaryOperator;
            if (bo != null && bo.Sign == "&")
            {
                List<Expression> conjunctCollection = new List<Expression>();
                conjunctCollection.Add(bo.Left);
                conjunctCollection.Add(bo.Right);
                bool exists = true;
                while (exists) //пока есть не атомарная дизюнкция
                {
                    exists = false;
                    for (int i = 0; i < conjunctCollection.Count; i++)
                    {
                        Expression left = null;
                        Expression right = null;
                        if (conjunctCollection[i].SplitAnd(out left, out right))
                        {                            
                            if (!conjunctCollection.Contains(left))
                                conjunctCollection.Add(left);
                            if (!conjunctCollection.Contains(right))
                                conjunctCollection.Add(right);
                            exists = true;
                            conjunctCollection.RemoveAt(i);
                            break;
                        }
                    }
                }
                return conjunctCollection;
            }
            return new List<Expression>();
        }

        private Expression AbsorptionRule(BinaryOperator expression)
        {
            List<Expression> disjunctCollection = GetDisjunctCollection(expression);
            for(int i = 0; i < disjunctCollection.Count(); i++) //сначала (x | x) = x
            {
                for(int j = i + 1; j < disjunctCollection.Count(); j++)
                {
                    if(disjunctCollection[i].Equals(disjunctCollection[j]))
                    {
                        disjunctCollection.RemoveAt(j);
                        j--;
                    }
                }
            }
            bool changed = true;
            while(changed) //по закону поглощения
            {
                changed = false;
                for(int i = 0; i < disjunctCollection.Count; i++)
                {
                    for(int j = 0; j < disjunctCollection.Count; j++)
                    {
                        if(i != j)
                        {
                            BinaryOperator bo = disjunctCollection[j] as BinaryOperator;
                            if(bo != null)
                            {
                                List<Expression> conjunctCollection =
                                    GetConjunctCollection(disjunctCollection[j]);
                                // проверяем (a | (a & b)) = a
                                if (conjunctCollection.Any((a)
                                    => { return a.Equals(disjunctCollection[i]); })) 
                                {
                                    changed = true;
                                    disjunctCollection.RemoveAt(j);
                                    break;
                                }
                            }
                        }
                    }
                    if (changed) break;
                }
            }
            //если остался неатомарный дизъюнкт
            if (disjunctCollection.Any((a) =>
                { return (a is BinaryOperator && !IsCNFOr((BinaryOperator)a)); }))
                    return DistributionRule(disjunctCollection); 
            return CreateDisjunction(disjunctCollection);          
        }
        private Expression DistributionRule(List<Expression> disjunctCollection)
        {
            Expression conj = null;
            for (int i = 0; i < disjunctCollection.Count; i++)
            {
                if (disjunctCollection[i] is BinaryOperator &&
                    (!IsCNFOr((BinaryOperator)disjunctCollection[i])))
                {
                    conj = disjunctCollection[i];
                    disjunctCollection.RemoveAt(i);
                    break;
                }
            }
            if (conj != null)
            {
                return DistributionRule(conj, CreateDisjunction(disjunctCollection));
            }
            else return CreateDisjunction(disjunctCollection);
        }

        private Expression DistributionRule(Expression conj, Expression disj)
        {
            List<Expression> conjunctCollection = GetConjunctCollection(conj);
            for(int i = 0; i < conjunctCollection.Count; i++)
            {
                BinaryOperator bo = (BinaryOperator)BinaryOperators["|"].Clone();
                bo.Left = conjunctCollection[i];
                bo.Right = disj;
                conjunctCollection[i] = bo;
            }
            if (conjunctCollection.Count == 1)
                return conjunctCollection[0];
            else return GetCNFRec(CreateConjunction(conjunctCollection));
        }

        private Expression CreateConjunction(List<Expression> conjunctCollection)
        {
            if (conjunctCollection.Count() == 1) return conjunctCollection.First();
            BinaryOperator result = (BinaryOperator)binaryOperators["&"].Clone();
            BinaryOperator currOperator = result;
            for (int i = 0; i < conjunctCollection.Count - 2; i++)
            {
                currOperator.Left = conjunctCollection[i];
                currOperator.Right = (BinaryOperator)binaryOperators["&"].Clone();
            }
            currOperator.Left = conjunctCollection[conjunctCollection.Count - 2];
            currOperator.Right = conjunctCollection[conjunctCollection.Count - 1];
            return result;
        }
        private Expression CreateDisjunction(List<Expression> disjunctCollection)
        {
            if (disjunctCollection.Count() == 1) return disjunctCollection.First();
            BinaryOperator result = (BinaryOperator)binaryOperators["|"].Clone();
            BinaryOperator currOperator = result;
            for (int i = 0; i < disjunctCollection.Count - 2; i++)
            {
                currOperator.Left = disjunctCollection[i];
                currOperator.Right = (BinaryOperator)binaryOperators["|"].Clone();
            }
            currOperator.Left = disjunctCollection[disjunctCollection.Count - 2];
            currOperator.Right = disjunctCollection[disjunctCollection.Count - 1];
            return result;
        }

        public bool IsCNF(Expression expression)
        {
            foreach(var item in GetConjunctCollection(expression))
            {
                if (((item is UnaryOperator || item is UnaryOperator) && IsAtomic(item)) || 
                    (item is BinaryOperator && IsCNFOr((BinaryOperator)item))) return false;
            }
            return true;
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
            string v = pair.Value;
            foreach (var r in replacements)
                v = r.Key.Replace(v, r.Value);
            Operator o = binaryOperators[v];
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

        /// <summary>
        /// Рекурсивная процедура "спуска" логического НЕ до уровня литералов.
        /// </summary>
        /// <param name="expression">Исходное выражение.</param>
        /// <returns>Возвращает выражение, в котором логическое НЕ стоит толькое перед литералами.</returns>
        private Expression NotToAtomaryRec(Expression expression)
        {
            UnaryOperator uo = expression as UnaryOperator;
            if (uo != null)
            {
                if (uo.Sign == "!")
                {
                    if(IsAtomic(uo))
                    {
                        return Reverse(uo.Left);
                    }
                    else return NotToAtomary(Reverse(uo.Left));
                }
            }
            else
            {
                BinaryOperator bo = expression as BinaryOperator;
                if (bo != null)
                {
                    bo.Right = NotToAtomaryRec(bo.Right);
                    bo.Left = NotToAtomaryRec(bo.Left);
                    return bo;
                }
            }
            return expression;
        }
        /// <summary>
        /// Применяет логическое НЕ к заданному выражению.
        /// </summary>
        /// <param name="expression">Исходное выражение.</param>
        /// <returns>Возвращает НЕ(выражение).</returns>
        private Expression Reverse(Expression expression)
        {
            Expression e = expression.Copy();
            BinaryOperator bo = e as BinaryOperator;
            if (bo != null)
            {
                switch (bo.Sign)
                {
                    case ("&"):
                        {
                            bo.Sign = "|";
                            bo.Action = binaryOperators["|"].Action;
                            bo.Left = Reverse(bo.Left);
                            bo.Right = Reverse(bo.Right);
                            break;
                        }
                    case ("|"):
                        {
                            bo.Sign = "&";
                            bo.Action = binaryOperators["&"].Action;
                            bo.Left = Reverse(bo.Left);
                            bo.Right = Reverse(bo.Right);
                            break;
                        }
                    case ("-"):
                        {
                            UnaryOperator uo = (UnaryOperator)unaryOperators["!"].Clone();
                            uo.Left = bo;
                            return uo;
                        }
                }
                return bo;
            }
            else
            {
                UnaryOperator uo = expression as UnaryOperator;
                if (uo.Sign == "!")
                {
                    return uo.Left;
                }
            }
            return e;
        }
        /// <summary>
        /// проверяет, является ли заданное выражение атомарным (константа, переменная, оператор "-").
        /// </summary>
        /// <param name="exp">Выражение.</param>
        /// <returns>Возвращает <c>true</c>, если заданное выражение является атомарным.</returns>
        private bool IsAtomic(Expression exp)
        {
            UnaryOperator uo = exp as UnaryOperator;
            if (uo != null)
            {
                return uo.Left is UnaryOperator ||
                        (uo.Left as BinaryOperator != null && ((BinaryOperator)uo.Left).Sign == "-");
            }
            BinaryOperator bo = exp as BinaryOperator;
            if (bo != null && bo.Sign == "-") return true;
            return exp is Constant || exp is Variable;
        }
        /// <summary>
        /// Рекурсивная процедура составления ДНФ.
        /// </summary>
        /// <param name="expression">Выражение.</param>
        /// <returns>Возвращает ДНФ для заданного выражения.</returns>
        private Expression GetDNFRec(Expression expression)
        {
            BinaryOperator bo = expression as BinaryOperator;
            if (bo != null)
            {
                if (bo.Sign == "&")
                {
                    if (!IsDNFAnd(bo))
                    {
                        UnaryOperator notAnd = (UnaryOperator)unaryOperators["!"].Clone();
                        notAnd.Left = bo;
                        return GetDNFRec(NotToAtomary(notAnd));
                    }
                }
                else
                {
                    bo.Right = GetDNFRec(bo.Right);
                    bo.Left = GetDNFRec(bo.Left);
                }
            }
            else
            {
                UnaryOperator uo = expression as UnaryOperator;
                if (uo != null)
                {
                    uo.Left = GetDNFRec(uo.Left);
                }
            }
            return expression;
        }
        /// <summary>
        /// Проверяет, является ли заданная конъюнкция элементарной.
        /// </summary>
        /// <param name="bo">Конъюнкция.</param>
        /// <returns>Возвращает <c>true</c>, если заданная коъюнкция является элементарной.</returns>
        private bool IsDNFAnd(BinaryOperator bo)
        {
            Stack<Expression> stack = new Stack<Expression>();
            stack.Push(bo.Left);
            stack.Push(bo.Right);
            while (stack.Count > 0)
            {
                Expression currExp = stack.Pop();
                BinaryOperator sbo = currExp as BinaryOperator;
                if (sbo != null)
                {
                    if (sbo.Sign == "|") return false;
                    stack.Push(sbo.Left);
                    stack.Push(sbo.Right);
                }
                else
                {
                    UnaryOperator uo = currExp as UnaryOperator;
                    if (uo != null && uo.Sign == "!")
                    {
                        if (!IsAtomic(uo.Left)) return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Проверяет, является ли заданная дизъюнкция атомарной.
        /// </summary>
        /// <param name="bo">Дихъюнкция.</param>
        /// <returns>Возвращет <c>true</c>, если дизъюнкция является атомарной.</returns>
        private bool IsCNFOr(BinaryOperator bo)
        {
            if (bo.Sign != "|") return false;
            Stack<Expression> stack = new Stack<Expression>();
            stack.Push(bo.Left);
            stack.Push(bo.Right);
            while (stack.Count > 0)
            {
                Expression currExp = stack.Pop();
                BinaryOperator sbo = currExp as BinaryOperator;
                if (sbo != null)
                {
                    if (sbo.Sign == "&") return false;
                    stack.Push(sbo.Left);
                    stack.Push(sbo.Right);
                }
                else
                {
                    UnaryOperator uo = currExp as UnaryOperator;
                    if (uo != null && uo.Sign == "!")
                    {
                        if (!IsAtomic(uo.Left)) return false;
                    }
                }
            }
            return true;
        }
    }
}
