using System.Collections.Generic;
using DefineDatas;

namespace BehaviorTree
{
    // children ������ ���ķ� �����ϴ� Node class
    public class Parallel : Node
    {
        // ���� �� children ����� index
        int index = 0;

         /*
         * ������
         * �Ű�����: ��尡 �����ϴ� Ʈ��(Tree), children ���(List<Node>)
         */
        public Parallel(Tree tree, List<Node> children) : base(tree, NodeType.Parallel, children) { }

        /*
         * ��� ���� �Լ�
         * �Ű�����: X
         * ���� ��: NodeState
         */
        public override NodeState Evaluate()
        {
            // �����ϴ� ActionNode�� �����Ǿ� ���� ������ index ���� 
            if (GetData(NodeData.FixNode) == null)
                index = (index + 1) % children.Count;

            Node node = children[index];

            // ���� �����ϴ� Node ����
            SetDataToRoot(NodeData.NodeStatus, this);

            switch (node.Evaluate())
            {
                case NodeState.Failure:
                    state = NodeState.Failure;
                    return state;
                case NodeState.Success:
                    state = NodeState.Success;
                    return state;
                case NodeState.Running:
                    state = NodeState.Running;
                    return state;
                default:
                    state = NodeState.Failure;
                    return state;
            }
            
        }
    }
}