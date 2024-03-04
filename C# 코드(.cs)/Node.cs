using System.Collections.Generic;
using DefineDatas;

namespace BehaviorTree
{
    // 행동트리의 Node class
    public class Node
    {
        // 노드 상태(NodeState)
        protected NodeState state;
        // 노드 종류(NodeType)
        public NodeType nodeType = NodeType.NONE;

        // 연결 트리(Tree)
        public Tree tree;
        // 부모 노드(Node)
        public Node parent;
        // 자식 노드(List<Node>)
        protected List<Node> children = new List<Node>();
        // 노드에 저장할 데이터(Dictionary<NodeData, Node>)
        private Dictionary<NodeData, Node> nodeData = new Dictionary<NodeData, Node>();

        /*
         * 기본 생성자
         */
        public Node()
        {
            parent = null;
        }

        /*
         * 생성자
         * 매개변수: 연결 할 트리(Tree), 노드 종류(NodeType), 자식 노드(List<Node>)
         */
        public Node(Tree useTree, NodeType type, List<Node> children)
        {
            tree = useTree;
            nodeType = type;
            if(children != null)
            foreach (Node child in children)
                Attach(child);
        }

        /*
         * 노드 연결 함수
         * 매개변수: 연결 할 노드(Node)
         * 리턴 값: void
         */
        protected void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /*
        * 노드 실행 함수
        * 매개변수: X
        * 리턴 값: NodeState
        */
        public virtual NodeState Evaluate()
        {
            return NodeState.Failure;
        }

        /*
        * 해당 데이터를 현재 노드에 저장
        * 매개변수: 저장 할 노드 데이터 종류(NodeData), 저장 값(Node)
        * 리턴 값: void
        */
        public void SetData(NodeData key, Node value)
        {
            nodeData[key] = value;
        }

        /*
        * 해당 데이터를 root 노드에 저장
        * 매개변수: 저장 할 노드 데이터 종류(NodeData), 저장 값(Node)
        * 리턴 값: void
        */
        public void SetDataToRoot(NodeData key, Node value)
        {
            Node temp = parent;
            Node root = this;

            while (temp != null)
            {
                root = temp;
                temp = temp.parent;
            }

            root.nodeData[key] = value;
        }

        /*
        * 저장 한 데이터 가져오기
        * 매개변수: 가져올 노드 데이터 종류(NodeData)
        * 리턴 값: Node
        */
        public Node GetData(NodeData key)
        {
            Node value = null;
            if (nodeData.TryGetValue(key, out value))
                return value;

            // 부모 노드에 해당 데이터가 있는지 확인
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }

            return null;
        }

        /*
        * 저장 한 데이터 지우기
        * 매개변수: 지울 노드 데이터 종류(NodeData)
        * 리턴 값: bool (데이터 삭제 확인)
        */
        public bool ClearData(NodeData key)
        {
            if (nodeData.ContainsKey(key))
            {
                nodeData.Remove(key);
                return true;
            }

            // 부모 노드에 해당 데이터가 있는지 확인
            Node node = parent;
            while (node != null)
            {
                if (node.ClearData(key))
                    return true;
                node = node.parent;
            }

            return false;
        }
    }
}

