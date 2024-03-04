using System;
using System.Collections.Generic;
using DefineDatas;

namespace BehaviorTree
{
    // 조건을 감당하는 노드 class (IF Decorator)
    public class Condition : Node 
    {
        // 조건을 담당할 함수(bool)
        protected Func<bool> condition;

        /*
        * 기본 생성자
        */
        public Condition() : base()
        {
            condition = null;
        }

        /*
        * 생성자
        * 조건 구독 및 트리 연결
        * 매개변수: 연결할 트리(Tree), 구독할 조건(Func<bool>), 조건이 참일 경우 실행할 노드(Node)
        */
        public Condition(Tree _tree, Func<bool> _condition, Node _TNode)
            :base(_tree, NodeType.Condition, new List<Node> { _TNode})
        {
            condition += _condition;
            tree = _tree;
        }

        /*
        * 노드 실행 함수
        * 매개변수: X
        * 리턴 값: NodeState
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
