using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.KnowledgeBases;
using ExpertSystemShell.KnowledgeBases.ProductionModel;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class ResolutionMethod
    {
        protected LogicalExpressionHelper eh = new LogicalExpressionHelper();

        public ResolutionMethod()
        {

        }

        public bool CheckConflict(ProductionRule rule1, ProductionRule rule2)
        {
            List<Expression> sentences = new List<Expression>();
            //строим КНФ из условия первого выражения и зачёркиваем знаки конъюнкции
            foreach(var exp in eh.GetConjunctCollection(eh.GetCNF(rule1.Condition)))
                sentences.Add(exp);
            //делаем то же самое со вторым правилом
            foreach (var exp in eh.GetConjunctCollection(eh.GetCNF(rule2.Condition)))
                sentences.Add(exp);
            //для каждого действия в правой части первого правила
            foreach(var action in rule1.Actions)
            {
                var addFact = action as AddFactAction;
                //если действие в правой части - добавление факта
                if (addFact != null)
                    //строим отрицание выражения по факту и добавляем его
                    sentences.Add(eh.CreateExpression("!" + addFact.ToString())); 
            }
            //для каждого действия в правой части второго правила
            foreach (var action in rule2.Actions)
            {
                var addFact = action as AddFactAction;
                //если действие в правой части - добавление факта
                if (addFact != null)
                    //строим отрицание выражения по факту и добавляем его
                    sentences.Add(eh.CreateExpression("!" + addFact.ToString())); 
            }
            return Resolve(sentences);
        }
        private bool Resolve(List<Expression> sentences)
        {
            bool changed = true;
            while(changed) //пока находятся резольвенты
            {
                changed = false;
                for(int i = 0; i < sentences.Count; i++)
                {
                    for(int j = i + 1; j < sentences.Count; j++) //для всех пар
                    {
                        Expression resolvent = null;
                        if(GetResolvent(sentences[i], sentences[j], out resolvent))
                        {
                            changed = true;
                            //если резольвента - пустой дизъюнкт, успешно завершаем алгоритм
                            if (resolvent == null) return true;
                            //иначе удаляем родительские высказыванияи заменяем их резольвентой
                            sentences.RemoveAt(j);
                            sentences.RemoveAt(i);
                            sentences.Add(resolvent);
                        }
                    }
                    if(changed) break;
                }
            }
            //если все резольвенты найдены и не выведен пустой дизъюнкт, завершаем с ошибкой
            return false;
        }

        private bool GetResolvent(Expression first, Expression second, out Expression resolvent)
        {
            resolvent = null;
            //получаем множества дизъюнктов
            List<Expression> firstExp = eh.GetDisjunctCollection(first);
            List<Expression> secondExp = eh.GetDisjunctCollection(second);
            for(int i = 0; i < firstExp.Count; i++)
            {
                for(int j = 0; j < secondExp.Count; j++) //для всех пар
                {
                    ProductionFact fact1 = firstExp[i].GetFact();
                    ProductionFact fact2 = secondExp[j].GetFact();
                    if(firstExp[i] is UnaryOperator) //если выражение имеет вид !(факт)
                    {
                        //если найдена контрарная пара
                        if (IsContariesCouple(firstExp[i], secondExp[j]))
                        {
                            firstExp.RemoveAt(i);
                            secondExp.RemoveAt(j);
                            //если списки пусты, то получен пустой дизъюнкт, иначе строим резольвенту
                            if (firstExp.Count != 0 && secondExp.Count != 0)
                                resolvent = eh.CreateDisjunction(firstExp.Union(secondExp).ToList());
                        }
                    }
                }
            }
            return false;
        }
        private bool IsContariesCouple(Expression firstExp, Expression secondExp)
        {
            ProductionFact fact1 = firstExp is UnaryOperator? firstExp.Descendants[0].GetFact() :firstExp.GetFact();
            ProductionFact fact2 = secondExp is UnaryOperator ? secondExp.Descendants[0].GetFact() : secondExp.GetFact();
            if (firstExp is UnaryOperator) //если выражение имеет вид !(факт)
            {
                if (!(secondExp is UnaryOperator)) //если выражение имеет вид 'имя факта - значение'
                {
                    //отрицание факта и факт
                    if ((fact1.Name == fact2.Name) && (fact1.Value.Equals(fact2.Value)))
                        return true;
                }
            }
            else //выражение имеет вид 'имя факта - значение'
            {
                if (secondExp is UnaryOperator)
                {
                    //факт и отрицание факта
                    if ((fact1.Name == fact2.Name) && (fact1.Value.Equals(fact2.Value))) 
                        return true;
                }
                else
                {
                    //если имена фактов равны, а значения нет
                    if ((fact1.Name == fact2.Name) && !(fact1.Value.Equals(fact2.Value)))
                        return true;
                }
            }
            return false;
        }
    }
}
