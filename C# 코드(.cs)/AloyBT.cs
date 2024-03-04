using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DefineDatas;
using System;

// Aloy ĳ���� �ൿƮ��
public class AloyBT : BehaviorTree.Tree
{
    // ĳ���� �̵� ����
    public ActionName characterMoveAction;
    // ĳ���� ��ų ����
    public List<ActionName> characterSkills;

    [HideInInspector]
    public Transform lookTransform;
    private Transform aimingTrasform;
    private Vector3 lookPosition;
    private List<ActionNode> actions = new List<ActionNode>();
    private ActionNodeMaker actionNodeMaker;
    private Animator animator;

    /*
    * Field �� ActionNode �ʱ�ȭ
    * �Ű�����: X
    * ���� ��: void
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
    * Tree �ʱ�ȭ �Լ�
    * �Ű�����: X
    * ���� ��: Node(�ൿƮ���� Root Node)
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
    * null �߻��� ErrorMessage ����
    * �Ű�����: X
    * ���� ��: void
    */
    void NullCheck()
    {
        if (animator == null)           ErrorManager.SaveErrorData(ErrorCode.AloyBT_animator_NULL);
        if (actionNodeMaker == null)    ErrorManager.SaveErrorData(ErrorCode.AloyBT_abilityMaker_NULL);
        if (aimingTrasform == null)     ErrorManager.SaveErrorData(ErrorCode.AloyBT_aimingTrasform_NULL);
    }

    /*
    * Error �߻� Ȯ��
    * �Ű�����: X
    * ���� ��: void
    */
    void NullErrorCheck()
    {
        try
        {
            // ���� �߻� Ȯ��
            if (ErrorManager.isErrorOccur) 
                throw new Exception("NULL Reference �߻����� ���� ���� ����");
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
    * ActionNode �� Ÿ�� ���
    * �Ű�����: X
    * ���� ��: IEnumerator
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
    * IK animation ���
    * �Ű�����: X
    * ���� ��: void
    */
    void OnAnimatorIK()
    {
        SetLookAtObj();
    }

    /*
    * ĳ���� ��ü�� lookPosition�������� �ٶ󺸰� �ϴ� �Լ�
    * �Ű�����: X
    * ���� ��: void
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
    * ĳ���� �ൿƮ�� ��� ���¸� ȭ�鿡 �����ִ� �Լ�
    * �Ű�����: X
    * ���� ��: void
    */
#if UNITY_EDITOR
    private void OnGUI()
    {
        GUI.Box(new Rect(0, 375, 150, 25), "<color=magenta>" + "AI Status" + "</color>");
        GUI.Box(new Rect(0, 400, 150, 25), "<color=orange>" + ((ActionNode)root.GetData(NodeData.NodeStatus)).actionName + "</color>");
    }
#endif
}
