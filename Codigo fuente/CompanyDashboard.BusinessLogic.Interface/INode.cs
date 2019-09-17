using CompanyDashboard;
using System;
using System.Collections.Generic;
using CompanyDashboard.Domain;
using CompanyDashboard.BusinessLogic;


namespace CompanyDashboard.BusinessLogic.Interface
{
    public interface INode
    {
        Node AddNode(Node node);
        bool Compare(Node factorLeft, Node factorRight, String logicSign);
        bool CompareEqual(Node factorLeft, Node factorRightn);
        String[] Evaluate(Node factor);
    }
    
}

