using System.Collections.Generic;
using DefineDatas;

namespace BehaviorTree
{
    // �ൿƮ���� Node class
    public class Node
    {
        // ��� ����(NodeState)
        protected NodeState state;
        // ��� ����(NodeType)
        public NodeType nodeType = NodeType.NONE;

        // ���� Ʈ��(Tree)
        public Tree tree;
        // �θ� ���(Node)
        public Node parent;
        // �ڽ� ���(List<Node>)
        protected List<Node> children = new List<Node>();
        // ��忡 ������ ������(Dictionary<NodeData, Node>)
        private Dictionary<NodeData, Node> nodeData = new Dictionary<NodeData, Node>();

        /*
         * �⺻ ������
         */
        public Node()
        {
            parent = null;
        }

        /*
         * ������
         * �Ű�����: ���� �� Ʈ��(Tree), ��� ����(NodeType), �ڽ� ���(List<Node>)
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
         * ��� ���� �Լ�
         * �Ű�����: ���� �� ���(Node)
         * ���� ��: void
         */
        protected void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /*
        * ��� ���� �Լ�
        * �Ű�����: X
        * ���� ��: NodeState
        */
        public virtual NodeState Evaluate()
        {
            return NodeState.Failure;
        }

        /*
        * �ش� �����͸� ���� ��忡 ����
        * �Ű�����: ���� �� ��� ������ ����(NodeData), ���� ��(Node)
        * ���� ��: void
        */
        public void SetData(NodeData key, Node value)
        {
            nodeData[key] = value;
        }

        /*
        * �ش� �����͸� root ��忡 ����
        * �Ű�����: ���� �� ��� ������ ����(NodeData), ���� ��(Node)
        * ���� ��: void
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
        * ���� �� ������ ��������
        * �Ű�����: ������ ��� ������ ����(NodeData)
        * ���� ��: Node
        */
        public Node GetData(NodeData key)
        {
            Node value = null;
            if (nodeData.TryGetValue(key, out value))
                return value;

            // �θ� ��忡 �ش� �����Ͱ� �ִ��� Ȯ��
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
        * ���� �� ������ �����
        * �Ű�����: ���� ��� ������ ����(NodeData)
        * ���� ��: bool (������ ���� Ȯ��)
        */
        public bool ClearData(NodeData key)
        {
            if (nodeData.ContainsKey(key))
            {
                nodeData.Remove(key);
                return true;
            }

            // �θ� ��忡 �ش� �����Ͱ� �ִ��� Ȯ��
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

