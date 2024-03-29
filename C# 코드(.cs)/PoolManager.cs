using System.Collections.Generic;
using UnityEngine;
using System.Text;
using DefineDatas;

namespace Managers
{
    // 오브젝트 풀을 담당하는 PoolManager
    public class PoolManager : SingletonTemplate<PoolManager>
    {
        // PoolObject에 대한 정보 저장
        public struct PoolObjectData
        {
            public PoolObjectName name;
            public GameObject obj;
            public int initObjectNum;
            public int addObjectNum;
            public List<PoolObject> objs;
            public Queue<int> objIDs;
        }

        // PoolObject 정보가 담긴 ScriptableObject
        public PoolManagerData managerData;

        private Dictionary<PoolObjectName, PoolObjectData> poolDatas
             = new Dictionary<PoolObjectName, PoolObjectData>();

        // 초기화 함수
        void Start()
        {
            InitPoolDatas();
            CreateObjects();
        }

        /*
        * managerData에 있는 정보를 poolDatas에 대입
        * 매개변수: X
        * 리턴 값: void
        */
        void InitPoolDatas()
        {
            for (int i = 0; i < managerData.poolObjectDatas.Count; i++)
            {
                PoolObjectData temp = new PoolObjectData();

                temp.name = managerData.poolObjectDatas[i].name;
                temp.obj = managerData.poolObjectDatas[i].obj;
                temp.initObjectNum = managerData.poolObjectDatas[i].initObjectNum;
                temp.addObjectNum = 0;
                temp.objs = new List<PoolObject>();
                temp.objIDs = new Queue<int>();

                poolDatas[temp.name] = temp;
            }
        }

        /*
        * poolDatas에 있는 PoolObjectData로 PoolObject 생성
        * 매개변수: X
        * 리턴 값: void
        */
        void CreateObjects()
        {
            if (poolDatas.Count == 0)
            {
                ErrorManager.Log("소환할 PoolObject가 없습니다.");
                ErrorManager.SaveErrorData(ErrorCode.NoSpawnPoolObjects);
                return;
            }

            for (int i = 0; i < poolDatas.Count; i++)
            {
                PoolObjectData data = poolDatas[(PoolObjectName)i];
                data.addObjectNum = data.initObjectNum;

                for (int j = 0; j < data.initObjectNum; j++)
                {
                    data.objs.Add(CreateNewObject(data, j));
                }

                poolDatas[(PoolObjectName)i] = data;
            }
        }

        /*
        * 새로운 PoolObject 생성
        * 매개변수: 새로 만들 PoolObject의 데이터(PoolObjectData), 새로 부과할 ID(int)
        * 리턴 값: PoolObject
        */
        PoolObject CreateNewObject(PoolObjectData pObjData, int id)
        {
            GameObject gb = Instantiate(pObjData.obj, transform);   // 부모 아래에 소환
            gb.name = MakeObjectName(pObjData.obj.name, id);

            PoolObject poolObj = gb.GetComponent<PoolObject>();
            if (poolObj == null) poolObj = gb.AddComponent<PoolObject>();

            gb.SetActive(false);
            poolObj.SetPoolObjectData(id, pObjData.name, transform);
            poolObj.SetCallback(DisActiveObject);                   // Delegate에 DisActiveObject 함수 구독  
            poolObj.InitPoolObject();
            poolObj.isUsed = false;
            pObjData.objIDs.Enqueue(id);

            return poolObj;
        }

        /*
        * PoolObject 이름 생성
        * 매개변수: PoolObject이름(string), PoolObject ID(int)
        * 리턴 값: string
        */
        string MakeObjectName(string objName, int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(objName).Append("_").Append(id);
            return sb.ToString();
        }

        /*
        * PoolObject 사용 함수
        * 매개변수: 사용자 이름(string), 사용 할 PoolObject 이름(PoolObjectName)
        * 리턴 값: PoolObject
        */
        public PoolObject GetObject(string userName, PoolObjectName name)
        {
            if (!poolDatas.ContainsKey(name))
            {
                ErrorManager.Log("해당 이름의 PoolObject를 찾을 수 없습니다(GetObject(string, PoolObjectName)) : " + name.ToString());
                ErrorManager.SaveErrorData(ErrorCode.CannotFindPoolObjectName, " : " + name.ToString() + "from GetObject(string, PoolObjectName)");
                return null;
            }

            PoolObjectData data = poolDatas[name];
            PoolObject ob;
            if (data.objIDs.Count > 0)
            {
                ob = SearchObject(name, data.objIDs.Peek());
            }

            else
            {
                ob = CreateNewObject(data, ++data.addObjectNum);
                data.objs.Add(ob);
            }

            data.objIDs.Dequeue();
            poolDatas[name] = data;

            ob.userName = userName;
            ob.isUsed = true;
            ob.gameObject.SetActive(true);
            return ob;
        }

        /*
        * PoolObject 사용 함수 (사용 위치, 회전 설정)
        * 매개변수: 사용자 이름(string), 사용 할 PoolObject 이름(PoolObjectName), 사용 위치(Vector3), 회전 값(Quaternion)
        * 리턴 값: PoolObject
        */
        public PoolObject GetObject(string userName, PoolObjectName name, Vector3 pos, Quaternion rot)
        {
            if (!poolDatas.ContainsKey(name))
            {
                ErrorManager.Log("해당 이름의 PoolObject를 찾을 수 없습니다(GetObject(string, PoolObjectName, Vector3, Quaternion)) : " + name.ToString());
                ErrorManager.SaveErrorData(ErrorCode.CannotFindPoolObjectName, " : " + name.ToString() + "from GetObject(string, PoolObjectName, Vector3, Quaternion)");
                return null;
            }

            PoolObjectData data = poolDatas[name];
            PoolObject ob;
            if (data.objIDs.Count > 0)
            {
                ob = SearchObject(name, data.objIDs.Peek());
            }

            else
            {
                ob = CreateNewObject(data, ++data.addObjectNum);
                data.objs.Add(ob);
            }

            data.objIDs.Dequeue();
            poolDatas[name] = data;

            ob.transform.SetPositionAndRotation(pos, rot);
            ob.userName = userName;
            ob.isUsed = true;
            ob.gameObject.SetActive(true);
            return ob;
        }

        /*
        * PoolObject 반환 함수
        * 매개변수: 반환 할 PoolObject 이름(PoolObjectName), 반환 할 PoolObject ID(int)
        * 리턴 값: void
        */
        public void DisActiveObject(PoolObjectName name, int id)
        {
            if (!poolDatas.ContainsKey(name))
            {
                ErrorManager.Log("해당 이름의 PoolObject를 찾을 수 없습니다(DisActiveObject(PoolObjectName, int)) : " + name.ToString());
                ErrorManager.SaveErrorData(ErrorCode.CannotFindPoolObjectName, " : " + name.ToString() + "from DisActiveObject(PoolObjectName, int)");
                return;
            }

            PoolObject ob = SearchObject(name, id);
            if (ob == null)
            {
                ErrorManager.Log("해당 ID 의 PoolObject를 찾을 수 없습니다(DisActiveObject(PoolObjectName, int)) : " + id.ToString());
                ErrorManager.SaveErrorData(ErrorCode.CannotFindPoolObjectID, " : " + id.ToString() + "from DisActiveObject(PoolObjectName, int)");
                return;
            }

            if (!ob.isUsed) return;

            ob.isUsed = false;
            ob.userName = null;
            ob.gameObject.SetActive(false);
            poolDatas[name].objIDs.Enqueue(id);
        }

        /*
        * PoolObject 탐색 함수
        * 매개변수: 탐색 할 PoolObject 이름(PoolObjectName), 탐색 할 PoolObject ID(int)
        * 리턴 값: PoolObject
        */
        public PoolObject SearchObject(PoolObjectName name, int id)
        {
            if (!poolDatas.ContainsKey(name))
            {
                ErrorManager.Log("해당 이름의 PoolObject를 찾을 수 없습니다(SearchObject(PoolObjectName, int)) : " + name.ToString());
                ErrorManager.SaveErrorData(ErrorCode.CannotFindPoolObjectName, " : " + name.ToString() + "from SearchObject(PoolObjectName, int)");
                return null;
            }

            // ID와 리스트 인덱스 번호 동일
            PoolObject ob = poolDatas[name].objs[id];
            if (ob == null)
            {
                ErrorManager.Log("해당 ID 의 PoolObject를 찾을 수 없습니다(SearchObject(PoolObjectName, int)) : " + id.ToString());
                ErrorManager.SaveErrorData(ErrorCode.CannotFindPoolObjectID, " : " + id.ToString() + "from SearchObject(PoolObjectName, int)");
                return null;
            }

            return ob;
        }
    }
}