using DefineDatas;
namespace BehaviorTree
{
    // 행동트리 Action(Leaf) Node의 Class
    public class ActionNode : Node
    {
        public ActionName actionName;
        public CoolTimer coolTimer;

        /*
        * 기본 생성자
        */
        public ActionNode():base()
        { }

        /*
        * 생성자
        * Action이름 설정
        * 매개변수: 노드를 사용하는 트리(Tree), Action이름(ActionName)
        */
        public ActionNode(Tree tree, ActionName name)
            : base(tree, NodeType.Action, null)
        {
            actionName = name;
        }

        /*
        * 생성자
        * Action이름 및 쿨 타임 설정
        * 매개변수: 노드를 사용하는 트리(Tree), Action이름(ActionName), 쿨 타임(float)
        */
        public ActionNode(Tree tree, ActionName name, float coolTime) 
            : base(tree, NodeType.Action, null)
        {
            if(coolTime > 0.0f) coolTimer = new CoolTimer(coolTime);
            actionName = name;
        }
    }
}