using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpertSystemShell.Expressions;

namespace ExpertSystemShell.KnowledgeBases.ProductionModel
{
    public class StartNode: ReteNode
    {

        public StartNode(): base(0, 0)
        {

        }

        public override void AddStatement(ProductionRule statement, ReteNode parent, AgendaNode end)
        {
            AddStatement(statement.Condition, statement, end);
        }

        public void AddStatement(Expression expression, ProductionRule statement, AgendaNode end)
        {
            Expression left = null;
            Expression right = null;
            if(expression.SplitOr(out left, out right)) //если в корне выражения оператор "или"
            {
                //добавляем две новые ветви с текущим правилом в конце
                AddStatement(left, statement, end);
                AddStatement(right, statement, end);
            }
            else //в корне либо конъюнкция, либо атомарное условие
            {
                //получаем все конъюнкты
                IEnumerable<Expression> simpleConditions = null;
                expression.SplitFactConjunction(out simpleConditions);
                //и пытаемся слить их вместе в новую ветвь
                AddAndMerge(simpleConditions, statement, end);
            }
        }

        protected void AddAndMerge(IEnumerable<Expression> conditions, ProductionRule statement,
            AgendaNode end)
        {
            List<ReteNode> exist = new List<ReteNode>(); //список уже существующих нодов
            List<ReteNode> add = new List<ReteNode>(); // список новых нодов
            foreach(var c in conditions) //находим уже сущеcтвующие Alpha ноды
            {
                AlphaNode n = null;
                if (FindNode(c, out n)) // если нод существует, то добавляем его к списку существующих
                    exist.Add(n);
                else 
                {
                    AlphaNode n1 = new AlphaNode(); //иначе создаём новый
                    n1.Predicate = c;
                    n1.Inputs[0] = this; //связываем его с корнем
                    this.outputs.Add(n1);
                    add.Add(n1); //добавляем его в список новых нодов
                }
            }
            exist = Commonize(exist); //находим наиболее общих наследников
            foreach (var n in add) //теперь всё в одном списке
                exist.Add(n);
            while(exist.Count > 1) //объединяем всё, что осталось
            {
                ReteNode n1 = exist[0];
                ReteNode n2 = exist[1];
                exist.RemoveAt(0);
                exist.RemoveAt(0);
                ReteNode unuion = n1.Merge(n2);
                exist.Add(unuion);
            }
            BetaMemoryNode bm =
                (BetaMemoryNode)exist[0].Outputs.FirstOrDefault((a) => { return a is BetaMemoryNode; });
            if(bm == null)
            {
                bm = new BetaMemoryNode();
                bm.Outputs[0] = end;
                bm.Inputs.Add(exist[0]);
                exist[0].Outputs.Add(bm);
            }
            if (!bm.BetaMemory.Contains(statement.Name))
                bm.BetaMemory.Add(statement.Name);
        }

        protected bool FindNode(Expression expression, out AlphaNode node)
        {
            Stack<AlphaNode> stack = new Stack<AlphaNode>();
            node = null;
            IData sampleFact = expression.GetFact();
            foreach (var n in outputs)
                stack.Push((AlphaNode)n);
            while(stack.Count > 0)
            {
                AlphaNode currNode = stack.Pop();
                if(currNode.Predicate.IsFact())
                {
                    IData fact = currNode.Predicate.GetFact();
                    if(fact.Equals(sampleFact))
                    {
                        node = currNode;
                        return true;
                    }
                }
                foreach(var n in currNode.Outputs)
                {
                    AlphaNode child = n as AlphaNode;
                    if (child != null)
                        stack.Push(child);
                }
            }
            return false;
        }
        protected List<ReteNode> Commonize(List<ReteNode> exist)
        {
            bool changed = true;
            //на каждой итерации цикла множество уменьшается на 1 элемент, либо остаётся неизменным
            //если множество осталось неизменным, то алгоритм заканчивает работу
            while (changed)
            {
                changed = false; // на каждой итерации проверяем обновилось ли множество
                for (int i = 0; i < exist.Count; i++)
                {
                    for (int j = i + 1; j < exist.Count; j++) //для каждой пары [i, j]
                    {
                        ReteNode commonOutput = null; //пытаемся найти общего наследника
                        if (exist[i].HasCommonOutputWith(exist[j], out commonOutput)) //если наследник существует
                        {
                            exist.RemoveAt(j); //удаляем кандидатов из множества
                            exist.RemoveAt(i);
                            exist.Add(commonOutput); //вставляем найденного наследника
                            changed = true;
                            break;
                        }
                    }
                    if (changed == true) break; //если множество изменилось, то переходим к следующей итерации
                }
            }
            return exist;
        }

        public override string ToString()
        {
            return "START";
        }
    }
}
