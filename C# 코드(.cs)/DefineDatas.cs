using UnityEngine;

// 상수 정의 namespace
namespace DefineDatas
{
    //플레이어 Animation Layer의 Index 값
    public enum PlayerAniLayerIndex
    {
        BaseLayer,
        MoveLayer,
        AttackLayer
    }
    // 노드 상태
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    // 노드 종류
    public enum NodeType
    {
        NONE,
        Sequence,
        Selector,
        Parallel,
        Condition,
        Action
    }
    // 노드에 저장하는 데이터 종류
    public enum NodeData
    {
        NodeStatus,
        FixNode
    }
    // Action에 필요한 object
    public enum ActionObjectName
    {
        CharacterTransform,
        CharacterTree,
        BowAniObject,
        ArrowAniObject,
        AimingTransform,
        ArrowStrikeStartPoint,
        DownArrowTransform,
        OccupationPoint
    }
    // Action 종류
    public enum ActionName
    {
        GotoOccupationArea,
        NormalArrowAttack,
        BallArrowAttack,
        ArrowRainAttack,
        NONE
    }
    // 오브젝트 풀을 적용할 object이름
    public enum PoolObjectName
    {
        Arrow,
        ArrowExplode,
        ArrowStuck,
        Ball,
        BallExplode,
        Strike,
        StrikeExplode,
        StrikeStuck
    }
    // Offset값 정의
    static class Offset
    {
        public static Vector3 AimOffset = new Vector3(0.0f, 0.5f, 0.0f);
    }
    // 캐릭터 이름
    static class CharacterName
    {
        public static string Character_1                             = "Aeterna";
        public static string Character_2                             = "Aloy";
    }
    // LayerMask 이름
    static class LayerMaskName
    {
        public static string Player                                 = "Player";
        public static string Wall                                   = "Wall";
    }
    // Tag 이름
    static class TagName
    {
        public static string TeamA                                  = "TeamA";
        public static string TeamB                                  = "TeamB";
    }
    // IK weight 값
    static class PlayerLookAtWeight
    {
        public static float weight                                  = 1.0f;
        public static float bodyWeight                              = 0.9f;
    }
    // Player Animation 종류
    static class PlayerAniState
    {
        public readonly static int Move                             = Animator.StringToHash("Move");
        public readonly static int CheckEnemy                       = Animator.StringToHash("CheckEnemy");
        public readonly static int AvoidRight                       = Animator.StringToHash("AvoidRight");
        public readonly static int AvoidLeft                        = Animator.StringToHash("AvoidLeft");
        public readonly static int AvoidForward                     = Animator.StringToHash("AvoidForward");
        public readonly static int AvoidBack                        = Animator.StringToHash("AvoidBack");
        public readonly static int Aim                              = Animator.StringToHash("Aim");
        public readonly static int Draw                             = Animator.StringToHash("Draw");
        public readonly static int Shoot                            = Animator.StringToHash("Shoot");
    }
    // 정의된 상수값
    static class DefineNumber
    {
        public static int RandomInitNum                             = 7;
        public static int AvoidWayCount                             = 4;
        public static int BitMove4                                  = 4;
        public static float ZeroCount                               = 0.0f;
        public static float MaxWallDistance                         = 0.5f;
        public static float AvoidRotateAngle                        = 15.0f;
        public static float AttackAngleDifference                   = 10.0f;
        public static float SightHeightRatio                        = 1.5f;
    }
    //에러 코드
    public enum ErrorCode
    {
        // NULL ERROR
        AbilityMaker_Tree_NULL,
        AbilityMaker_data_NULL,
        AloyBT_animator_NULL,
        AloyBT_abilityMaker_NULL,
        AloyBT_aimingTrasform_NULL,
        EnemyDetector_condition_NULL,
        EnemyDetector_playerTransform_NULL,
        EnemyDetector_bowAnimator_NULL,
        EnemyDetector_animator_NULL,
        NormalArrowAttack_playerTransform_NULL,
        NormalArrowAttack_attackTransform_NULL,
        NormalArrowAttack_bowAnimator_NULL,
        NormalArrowAttack_arrowAniObj_NULL,
        NormalArrowAttack_agent_NULL,
        NormalArrowAttack_animator_NULL,
        NormalArrowAttack_avoidTimer_NULL,
        GotoOccupationArea_arrowAniObj_NULL,
        GotoOccupationArea_agent_NULL,
        GotoOccupationArea_animator_NULL

        // PoolManager ERROR
        NoSpawnPoolObjects,
        CannotFindPoolObjectName,
        CannotFindPoolObjectID
    }
}
