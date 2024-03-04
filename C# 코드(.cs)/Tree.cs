using UnityEngine;

namespace BehaviorTree
{
    // 트리 class
    public class Tree : MonoBehaviour
    {
        // Root Node
        protected Node root = null;

        /*
         * root 노드 초기화 함수
         * 매개변수: X
         * 리턴 값: void
         */
        protected virtual void Start()
        {
            root = SetupTree();
        }

        /*
        * root 노드 매 프레임 실행 함수
        * 매개변수: X
        * 리턴 값: void
        */
        protected virtual void Update()
        {
            if (root != null) root.Evaluate();
        }

        /*
        * root 노드 초기화 함수
        * 매개변수: X
        * 리턴 값: Node
        */
        protected virtual Node SetupTree() 
        {
            return root;
        }
    }
}
