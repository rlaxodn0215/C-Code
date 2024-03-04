using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DefineDatas;

// ActionNode 제작 class
public class ActionNodeMaker : MonoBehaviour
{
    // Action 제작을 위해 필요한 오브젝트들
    public List<Transform> actionObjectTransforms;
    // 각 Action Data의 ScriptableObject
    public List<ScriptableObject> datas;
    private BehaviorTree.Tree tree;

    // Awake()
    private void Awake()
    {
        LinkPlayerTree();
    }

    /*
    * ActionNode 생성을 위해 ActionNode를 사용할 플레이어 Tree 연결 
    * 매개변수: X
    * 리턴 값: void
    */
    void LinkPlayerTree()
    {
        tree = GetComponent<BehaviorTree.Tree>();
        // null 발생 시 에러 text파일에 저장
        if (tree == null) ErrorManager.SaveErrorData(ErrorCode.AbilityMaker_Tree_NULL);
    }

    /*
    * ActionNode 생성 함수
    * 매개변수: 생성할 ActionNode의 이름(ActionName)
    * 리턴 값: ActionNode
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
