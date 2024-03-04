using System;
using System.Collections.Generic;
using DefineDatas;

namespace BehaviorTree
{
    // ������ �����ϴ� ��� class (IF Decorator)
    public class Condition : Node 
    {
        // ������ ����� �Լ�(bool)
        protected Func<bool> condition;

        /*
        * �⺻ ������
        */
        public Condition() : base()
        {
            condition = null;
        }

        /*
        * ������
        * ���� ���� �� Ʈ�� ����
        * �Ű�����: ������ Ʈ��(Tree), ������ ����(Func<bool>), ������ ���� ��� ������ ���(Node)
        */
        public Condition(Tree _tree, Func<bool> _condition, Node _TNode)
            :base(_tree, NodeType.Condition, new List<Node> { _TNode})
        {
            condition += _condition;
            tree = _tree;
        }

        /*
        * ��� ���� �Լ�
        * �Ű�����: X
        * ���� ��: NodeState
        */
        public override NodeState Evaluate()
        {
            //TRUE
            if (condition())
            {
                if(children[0] != null)
                    children[0].Evaluate();
                return NodeState.Success;
            }

            //FALSE
            else
            {
                return NodeState.Failure;
            }
        }

    }
}
