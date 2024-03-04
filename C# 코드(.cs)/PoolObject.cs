using UnityEngine;
using DefineDatas;

namespace Managers
{
    // 오브젝트 풀을 적용할 object의 class
    public class PoolObject : MonoBehaviour
    {
        // 오브젝트 반납 함수 구독
        public delegate void Callback_Disapear(PoolObjectName name, int id);
        Callback_Disapear onDisapear;

        [HideInInspector]
        public bool isUsed;
        [HideInInspector]
        public string userName;
        public int ObjID { get; private set; } = -1;
        protected PoolObjectName objectName;
        // 소환 위치
        protected Transform objTrans;

        /*
         * PoolManager로 부터 PoolObject 설정하는 함수
         * 매개변수: 오브젝트 아이디(int), PoolObject이름(PoolObjectName), 소환 위치(Transform)
         * 리턴 값: void
         */
        public void SetPoolObjectData(int _id, PoolObjectName _name, Transform _trans)
        {
            ObjID = _id;
            objectName = _name;
            objTrans = _trans;
        }

        /*
        * PoolObject 초기화 함수
        * 매개변수: X
        * 리턴 값: void
        */
        public virtual void InitPoolObject() { }

        /*
        * PoolObject 위치, 회전 값 설정 함수
        * 매개변수: 설정할 위치, 회전 값(Transform)
        * 리턴 값: void
        */
        public virtual void SetPoolObjectTransform(Transform trans) { }

        /*
        * PoolObject 비활성화 함수
        * 매개변수: X
        * 리턴 값: void
        */
        public virtual void DisActive()
        {
            onDisapear(objectName, ObjID);  
        }

        /*
        * PoolObject 비활성화 함수 구독
        * 매개변수: 구독 할 비활성화 함수(Callback_Disapear)  
        * 리턴 값: void
        */
        public void SetCallback(Callback_Disapear callback_OnDisapear)
        {
            onDisapear = callback_OnDisapear;
        }
    }
}