using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DefineDatas;

// ActionNode ���� class
public class ActionNodeMaker : MonoBehaviour
{
    // Action ������ ���� �ʿ��� ������Ʈ��
    public List<Transform> actionObjectTransforms;
    // �� Action Data�� ScriptableObject
    public List<ScriptableObject> datas;
    private BehaviorTree.Tree tree;

    // Awake()
    private void Awake()
    {
        LinkPlayerTree();
    }

    /*
    * ActionNode ������ ���� ActionNode�� ����� �÷��̾� Tree ���� 
    * �Ű�����: X
    * ���� ��: void
    */
    void LinkPlayerTree()
    {
        tree = GetComponent<BehaviorTree.Tree>();
        // null �߻� �� ���� text���Ͽ� ����
        if (tree == null) ErrorManager.SaveErrorData(ErrorCode.AbilityMaker_Tree_NULL);
    }

    /*
    * ActionNode ���� �Լ�
    * �Ű�����: ������ ActionNode�� �̸�(ActionName)
    * ���� ��: ActionNode
    */
    public ActionNode MakeActionNode(ActionName actionName)
    {
        switch (actionName)
        {
            case ActionName.GotoOccupationArea:
                return new GotoOccupationArea(tree, actionObjectTransforms, datas[(int)ActionName.GotoOccupationArea]);
            case ActionName.NormalArrowAttack:
                return new NormalArrowAttack(tree, actionObjectTransforms, datas[(int)ActionName.NormalArrowAttack]);
            case ActionName.BallArrowAttack:
                return new BallArrowAttack(tree, actionObjectTransforms, datas[(int)ActionName.BallArrowAttack]);
            case ActionName.ArrowRainAttack:
                return new ArrowRainAttack(tree, actionObjectTransforms, datas[(int)ActionName.ArrowRainAttack]);
            case ActionName.NONE:
                return new ActionNode();
        }
        return null;
    }
}
