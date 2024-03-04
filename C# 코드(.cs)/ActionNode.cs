using DefineDatas;
namespace BehaviorTree
{
    // �ൿƮ�� Action(Leaf) Node�� Class
    public class ActionNode : Node
    {
        public ActionName actionName;
        public CoolTimer coolTimer;

        /*
        * �⺻ ������
        */
        public ActionNode():base()
        { }

        /*
        * ������
        * Action�̸� ����
        * �Ű�����: ��带 ����ϴ� Ʈ��(Tree), Action�̸�(ActionName)
        */
        public ActionNode(Tree tree, ActionName name)
            : base(tree, NodeType.Action, null)
        {
            actionName = name;
        }

        /*
        * ������
        * Action�̸� �� �� Ÿ�� ����
        * �Ű�����: ��带 ����ϴ� Ʈ��(Tree), Action�̸�(ActionName), �� Ÿ��(float)
        */
        public ActionNode(Tree tree, ActionName name, float coolTime) 
            : base(tree, NodeType.Action, null)
        {
            if(coolTime > 0.0f) coolTimer = new CoolTimer(coolTime);
            actionName = name;
        }
    }
}