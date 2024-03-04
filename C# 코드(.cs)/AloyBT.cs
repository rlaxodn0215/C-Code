using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DefineDatas;
using System;

// Aloy 캐릭터 행동트리
public class AloyBT : BehaviorTree.Tree
{
    // 캐릭터 이동 설정
    public ActionName characterMoveAction;
    // 캐릭터 스킬 설정
    public List<ActionName> characterSkills;

    [HideInInspector]
    public Transform lookTransform;
    private Transform aimingTrasform;
    private Vector3 lookPosition;
    private List<ActionNode> actions = new List<ActionNode>();
    private ActionNodeMaker actionNodeMaker;
    private Animator animator;

    /*
    * Field 및 ActionNode 초기화
    * 매개변수: X
    * 리턴 값: void
    */
    void InitData()
    {
        animator = GetComponent<Animator>();
        actionNodeMaker = GetComponent<ActionNodeMaker>();
        aimingTrasform = actionNodeMaker.actionObjectTransforms[(int)ActionObjectName.AimingTransform];
        NullCheck();
        actions.Add(actionNodeMaker.MakeActionNode(characterMoveAction));
        for (int i = 0; i < characterSkills.Count; i++)
            actions.Add(actionNodeMaker.MakeActionNode(characterSkills[i]));      
    }

    /*
    * Tree 초기화 함수
    * 매개변수: X
    * 리턴 값: Node(행동트리의 Root Node)
    */
    protected override Node SetupTree()
    {
        InitData();
        Node root = new Selector(this, new List<Node>
        {
            new EnemyDetector(this,
                new Parallel(this, new List<Node>
                {
                    actions[(int)ActionName.NormalArrowAttack],
                    actions[(int)ActionName.BallArrowAttack],
                    actions[(int)ActionName.ArrowRainAttack]
                }),
            actionNodeMaker.actionObjectTransforms),
            actions[(int)ActionName.GotoOccupationArea]
        }
        );
        root.SetDataToRoot(NodeData.NodeStatus, root);
        StartCoroutine(CoolTimer());
        NullErrorCheck();
        return root;
    }

    /*
    * null 발생시 ErrorMessage 저장
    * 매개변수: X
    * 리턴 값: void
    */
    void NullCheck()
    {
        if (animator == null)           ErrorManager.SaveErrorData(ErrorCode.AloyBT_animator_NULL);
        if (actionNodeMaker == null)    ErrorManager.SaveErrorData(ErrorCode.AloyBT_abilityMaker_NULL);
        if (aimingTrasform == null)     ErrorManager.SaveErrorData(ErrorCode.AloyBT_aimingTrasform_NULL);
    }

    /*
    * Error 발생 확인
    * 매개변수: X
    * 리턴 값: void
    */
    void NullErrorCheck()
    {
        try
        {
            // 에러 발생 확인
            if (ErrorManager.isErrorOccur) 
                throw new Exception("NULL Reference 발생으로 인한 게임 종료");
            else
                ErrorManager.SaveErrorData("No NULL Reference");
        }

        catch (Exception e)
        {
            ErrorManager.SaveErrorData(e.Message);
            Application.Quit();
        }
    }

    /*
    * ActionNode 쿨 타임 담당
    * 매개변수: X
    * 리턴 값: IEnumerator
    */
    IEnumerator CoolTimer()
    {
        while (true)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if(actions[i].coolTimer != null)
                    actions[i].coolTimer.UpdateCoolTime(Time.deltaTime);
            }

            yield return null;
        }
    }

    /*
    * IK animation 담당
    * 매개변수: X
    * 리턴 값: void
    */
    void OnAnimatorIK()
    {
        SetLookAtObj();
    }

    /*
    * 캐릭터 상체가 lookPosition방향으로 바라보게 하는 함수
    * 매개변수: X
    * 리턴 값: void
    */
    void SetLookAtObj()
    {
        if (animator == null || lookTransform == null) return;      
        animator.SetLookAtWeight(PlayerLookAtWeight.weight, PlayerLookAtWeight.bodyWeight);
        lookPosition = lookTransform.position + Offset.AimOffset;
        animator.SetLookAtPosition(lookPosition);
        aimingTrasform.LookAt(lookPosition);
    }

    /*
    * 캐릭터 행동트리 노드 상태를 화면에 보여주는 함수
    * 매개변수: X
    * 리턴 값: void
    */
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 375, 150, 25), "<color=magenta>" + "AI Status" + "</color>");
        GUI.Box(new Rect(0, 400, 150, 25), "<color=orange>" + ((ActionNode)root.GetData(NodeData.NodeStatus)).actionName + "</color>");
    }
#endif
}
