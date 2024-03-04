using UnityEngine;
using DefineDatas;

namespace Managers
{
    // ������Ʈ Ǯ�� ������ object�� class
    public class PoolObject : MonoBehaviour
    {
        // ������Ʈ �ݳ� �Լ� ����
        public delegate void Callback_Disapear(PoolObjectName name, int id);
        Callback_Disapear onDisapear;

        [HideInInspector]
        public bool isUsed;
        [HideInInspector]
        public string userName;
        public int ObjID { get; private set; } = -1;
        protected PoolObjectName objectName;
        // ��ȯ ��ġ
        protected Transform objTrans;

        /*
         * PoolManager�� ���� PoolObject �����ϴ� �Լ�
         * �Ű�����: ������Ʈ ���̵�(int), PoolObject�̸�(PoolObjectName), ��ȯ ��ġ(Transform)
         * ���� ��: void
         */
        public void SetPoolObjectData(int _id, PoolObjectName _name, Transform _trans)
        {
            ObjID = _id;
            objectName = _name;
            objTrans = _trans;
        }

        /*
        * PoolObject �ʱ�ȭ �Լ�
        * �Ű�����: X
        * ���� ��: void
        */
        public virtual void InitPoolObject() { }

        /*
        * PoolObject ��ġ, ȸ�� �� ���� �Լ�
        * �Ű�����: ������ ��ġ, ȸ�� ��(Transform)
        * ���� ��: void
        */
        public virtual void SetPoolObjectTransform(Transform trans) { }

        /*
        * PoolObject ��Ȱ��ȭ �Լ�
        * �Ű�����: X
        * ���� ��: void
        */
        public virtual void DisActive()
        {
            onDisapear(objectName, ObjID);  
        }

        /*
        * PoolObject ��Ȱ��ȭ �Լ� ����
        * �Ű�����: ���� �� ��Ȱ��ȭ �Լ�(Callback_Disapear)  
        * ���� ��: void
        */
        public void SetCallback(Callback_Disapear callback_OnDisapear)
        {
            onDisapear = callback_OnDisapear;
        }
    }
}