using System;
using System.Linq;
using System.Collections.Generic;
using CompanyDashboard.BusinessLogic.Interface;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Interface;
using CompanyDashboard.DataAccess;
using CompanyDashboard.BusinessLogic.Exceptions;

namespace CompanyDashboard.BusinessLogic
{
    public class NodeLogic : INode
    {
        private IRepository<Node> repository;
        public NodeLogic(IRepository<Node> repository)
        {
            if (repository == null)
            {
                this.repository = new NodeRepository();
            }
            else
            {
                this.repository = repository;
            }
        }

        public Node AddNode(Node node)
        {
            repository.Add(node);
            repository.Save();
            return node;
        }
        public bool Compare(Node factorLeft, Node factorRight, String logicSign)
        {
            try
            {
                logicSign = logicSign.ToUpperInvariant();
                switch (logicSign)
                {
                    case "=":
                        return this.CompareEqual(factorLeft, factorRight);
                    case ">":
                        return this.CompareBigger(factorLeft, factorRight);
                    case ">=":
                        return this.CompareBiggerEqual(factorLeft, factorRight);
                    case "<":
                        return this.CompareLesser(factorLeft, factorRight);
                    case "<=":
                        return this.CompareLesserEqual(factorLeft, factorRight);
                    case "AND":
                        return this.CompareAND(factorLeft, factorRight);
                    case "OR":
                        return this.CompareOR(factorLeft, factorRight);
                    default: return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool CompareEqual(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] != factorR[0])
            {
                return false;
            }
            {
                return factorL[1] == factorR[1];
            }
        }

        public bool CompareBigger(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] == "5" || factorR[0] == "5")
            {
                return Convert.ToDateTime(factorL[1]) >= Convert.ToDateTime(factorR[1]);
            }
            if (factorL[0] != "2" || factorR[0] != "2")
            {
                return false;
            }
            return Double.Parse(factorL[1]) > Double.Parse(factorR[1]);
        }
        public bool CompareBiggerEqual(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] == "5" || factorR[0] == "5")
            {
                return Convert.ToDateTime(factorL[1]) > Convert.ToDateTime(factorR[1]);
            }
            if (factorL[0] != "2" || factorR[0] != "2")
            {
                return false;
            }
            return Double.Parse(factorL[1]) >= Double.Parse(factorR[1]);
        }
        public bool CompareLesser(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] == "5" || factorR[0] == "5")
            {
                return Convert.ToDateTime(factorL[1]) < Convert.ToDateTime(factorR[1]);
            }
            if (factorL[0] != "2" || factorR[0] != "2")
            {
                return false;
            }
            return Double.Parse(factorL[1]) < Double.Parse(factorR[1]);
        }

        public bool CompareLesserEqual(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] == "5" || factorR[0] == "5")
            {
                return Convert.ToDateTime(factorL[1]) <= Convert.ToDateTime(factorR[1]);
            }
            if (factorL[0] != "2" || factorR[0] != "2")
            {
                return false;
            }
            return Double.Parse(factorL[1]) <= Double.Parse(factorR[1]);
        }
        public bool CompareAND(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] != "4" || factorR[0] != "4")
            {
                return false;
            }
            return (bool.Parse(factorL[1]) && bool.Parse(factorR[1]));
        }

        public bool CompareOR(Node factorLeft, Node factorRight)
        {
            String[] factorL = Evaluate(factorLeft);
            String[] factorR = Evaluate(factorRight);
            if (factorL[0] != "4" || factorR[0] != "4")
            {
                return false;
            }
            return (bool.Parse(factorL[1]) || bool.Parse(factorR[1]));
        }

        public virtual String[] Evaluate(Node factor)
        {

            String[] result = new String[2];
            switch (factor.Type)
            {
                case 1:
                    IEvaluator str = new StringEvaluator();
                    return str.Evaluate(factor);
                case 2:
                    IEvaluator num = new IntEvaluator();
                    return num.Evaluate(factor);
                case 3:
                    IEvaluator sql = new SQLEvaluator();
                    return sql.Evaluate(factor);
                case 4:
                    IEvaluator condition = new BoolEvaluator();
                    return condition.Evaluate(factor);
                case 5:
                    IEvaluator date = new DateEvaluator();
                    return date.Evaluate(factor);
                default: return result;
            }
        }

        public bool Evaluate(BinaryOperator factor)
        {
            if(factor == null || factor.Left == null || factor.Right == null || factor.Sign == null ) return false;
            return this.Compare(factor.Left, factor.Right, factor.Sign);
        }

        public String GetText(Node factor)
        {
            return factor.GetText();
        }
    }
}
