using UnityEngine;

namespace BehaviorTree
{
    // Ʈ�� class
    public class Tree : MonoBehaviour
    {
        // Root Node
        protected Node root = null;

        /*
         * root ��� �ʱ�ȭ �Լ�
         * �Ű�����: X
         * ���� ��: void
         */
        protected virtual void Start()
        {
            root = SetupTree();
        }

        /*
        * root ��� �� ������ ���� �Լ�
        * �Ű�����: X
        * ���� ��: void
        */
        protected virtual void Update()
        {
            if (root != null) root.Evaluate();
        }

        /*
        * root ��� �ʱ�ȭ �Լ�
        * �Ű�����: X
        * ���� ��: Node
        */
        protected virtual Node SetupTree() 
        {
            return root;
        }
    }
}
