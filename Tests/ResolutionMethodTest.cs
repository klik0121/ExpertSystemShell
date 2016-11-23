using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpertSystemShell.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class ResolutionMethodTest
    {
        LogicalExpressionHelper eh = new LogicalExpressionHelper();
        [TestMethod]
        public void TestExpressionToDNF()
        {
            Expression exp = eh.CreateExpression("!(!'x-1'|!'y-1' & 'z-1')");
            Expression dnf = eh.GetDNF(exp);
            //Assert.IsTrue(CompareExpressions(exp, dnf));
            RecursiveDnfTest(dnf);
        }
        [TestMethod]
        public void TestExpressionToCNF()
        {
            Expression exp = eh.CreateExpression("(!'x-1' | 'y-1') & (!(!!'y-1' | 'z-1') | !'x-1')");
            string str = exp.ToString();
            Expression cnf = eh.GetCNF(exp);
        }
        /// <summary>
        /// Проверяет, является ли заданное выражение ДНФ.
        /// </summary>
        /// <param name="exp">Выражение.</param>
        public void RecursiveDnfTest(Expression exp)
        {
            BinaryOperator bo = exp as BinaryOperator;
            if(bo != null)
            {
                if(bo.Sign == "|") //для дизъюннкций проверяем каждый дизъюнкт
                {
                    foreach(var child in bo.Descendants)
                    {
                        RecursiveDnfTest(child);
                    }
                }
                else if(bo.Sign == "&") 
                {
                    //конъюнкция должна содержать только другие конъюнкции,
                    //литералы и отрицания литералов
                    Stack<Expression> stack = new Stack<Expression>();
                    foreach (var child in bo.Descendants)
                        stack.Push(child);
                    while(stack.Count > 0)
                    {
                        Expression currExp = stack.Pop();
                        BinaryOperator sbo = currExp as BinaryOperator;
                        if(sbo != null) //может содержать только конъюнкции и специальный оператор
                        {
                            Assert.IsTrue(sbo.Sign == "-" || sbo.Sign == "&");
                            stack.Push(sbo.Left);
                            stack.Push(sbo.Right);
                        }
                        else
                        {
                            UnaryOperator uo = currExp as UnaryOperator;
                            if(uo != null)
                            {
                                Assert.IsTrue(uo.Sign == "!" &&
                                    (uo.Left is Constant || uo.Left is Variable));
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Сравнивает два выражения.
        /// </summary>
        /// <param name="exp1">Выражение 1.</param>
        /// <param name="exp2">Выражение 2.</param>
        /// <returns>Возвращает true, если выражения эквиваленты.</returns>
        public bool CompareExpressions(Expression exp1, Expression exp2)
        {
            HashSet<string> variables1 = exp1.VariableNames;
            HashSet<string> variables2 = exp2.VariableNames;
            if (!variables1.SetEquals(variables2)) return false;
            foreach(string varName in variables1) //устаналиваем всё в 0
            {
                exp1.SetVariable<string>(varName, "0");
                exp2.SetVariable<string>(varName, "0");
            }
            string[] vars = variables1.ToArray();
            for (int i = 0; i < Math.Pow(2, vars.Length); i++ )
            {
                string binaryCode = Convert.ToString(i, 2);
                for(int j = 0; j < binaryCode.Length; j++)
                {
                    exp1.SetVariable<string>(vars[vars.Length - j - 1], binaryCode[j].ToString());
                    exp2.SetVariable<string>(vars[vars.Length - j - 1], binaryCode[j].ToString());                    
                }
                if (exp1.Calculate() != exp2.Calculate()) return false;
            }
            return true;
        }
        [TestMethod]
        public void TestNotToAtomary()
        {
            Expression exp = eh.CreateExpression("!(!'x-1'|'y-1')");
            Expression not = eh.NotToAtomary(exp);
            Assert.IsTrue(CompareExpressions(exp, not));
            Stack<Expression> stack = new Stack<Expression>();
            stack.Push(not);
            while(stack.Count > 0)
            {
                Expression currExp = stack.Pop();
                UnaryOperator uo = currExp as UnaryOperator;
                if(uo != null && uo.Sign == "!")
                {
                    foreach(var child in uo.Descendants)
                    {
                        Assert.IsTrue(child is Constant || child is Variable ||
                            (child as BinaryOperator != null) && ((BinaryOperator)child).Sign == "-");
                    }
                }
                else
                {
                    foreach (var child in currExp.Descendants)
                        stack.Push(child);
                }
            }
        }
    }
}
