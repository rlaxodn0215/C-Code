using System.Collections.Generic;
using DefineDatas;

namespace BehaviorTree
{
    // children 노드들을 병렬로 실행하는 Node class
    public class Parallel : Node
    {
        // 실행 할 children 노드의 index
        int index = 0;

         /*
         * 생성자
         * 매개변수: 노드가 연결하는 트리(Tree), children 노드(List<Node>)
         */
        public Parallel(Tree tree, List<Node> children) : base(tree, NodeType.Parallel, children) { }

        /*
         * 노드 실행 함수
         * 매개변수: X
         * 리턴 값: NodeState
         */
        public override NodeState Evaluate()
        {
            // 실행하는 ActionNode가 고정되어 있지 않으면 index 변경 
            if (GetData(NodeData.FixNode) == null)
                index = (index + 1) % children.Count;

            Node node = children[index];

            // 현재 접근하는 Node 저장
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