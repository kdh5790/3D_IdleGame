using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// enemy가 존재하는 방을 저장해둘 래퍼 클래스
public class RoomInfo
{
    public GameObject enemy;
    public int roomIndex;

    public RoomInfo(GameObject enemy, int roomIndex)
    {
        this.enemy = enemy;
        this.roomIndex = roomIndex;
    }
}

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>(); // 스테이지 별 생성할 몬스터를 저장해둘 리스트
    [SerializeField] private List<RoomBehaviour> rooms = new List<RoomBehaviour>(); // 현재 생성된 방

    public List<RoomInfo> roomInfos = new List<RoomInfo>(); // 현재 생성된 방들의 몬스터 존재 여부를 확인할 리스트

    private void Awake()
    {
        Initialized();
    }

    public void Initialized()
    {
        // 스테이지에 맞는 몬스터 리소스 가져오기
        enemyPrefabs = Resources.LoadAll<GameObject>($"Prefabs/Enemy/Stage{StageManager.Instance.CurrentStage}").ToList();
        rooms = GetComponentsInChildren<RoomBehaviour>().ToList();

        // 방에 몬스터가 존재한다면 모두 파괴
        foreach ( var roomInfo in roomInfos )
        {
            if(roomInfo.enemy != null)
            {
                Destroy(roomInfo.enemy);
            }
        }

        // 방 정보 리스트 초기화
        roomInfos.Clear();


        for (int i = 0; i < rooms.Count; i++)
        {
            // 현재 방에 몬스터가 존재하지 않고 플레이어와의 거리가 1 이상인 경우 
            if (!rooms[i].GetHasEnemy() && !(Vector3.Distance(Player.Instance.transform.position, rooms[i].transform.position) < 1f))
            {
                // 몬스터 스폰 여부 랜덤 설정(0 = false, 1 = true)
                bool isSpawnEnemy = (Random.Range(0, 2) == 1);

                if (isSpawnEnemy)
                {
                    // 현재(i) 방에 몬스터 생성
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], rooms[i].transform.position, Quaternion.identity);
                    rooms[i].SetHasEnemy(isSpawnEnemy);
                    roomInfos.Add(new RoomInfo(enemy, i));
                }
            }
        }
    }

    // 리스트에서 몬스터 삭제 함수(몬스터 사망 시 실행)
    public void RemoveEnemy(GameObject enemy)
    {
        // 현재 몬스터가 존재하는 방 정보 찾기
        RoomInfo info = roomInfos.Find(x => x.enemy == enemy);

        // 방 정보를 찾았다면
        if (info != null)
        {
            // 몬스터 여부 false로 변경 후 리스트에서 삭제
            rooms[info.roomIndex].SetHasEnemy(false);
            roomInfos.Remove(info);
        }

        // 몬스터 스폰 코루틴 실행
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            // 방 인덱스 랜덤하게 가져오기
            int randomRoomNum = Random.Range(0, rooms.Count);

            // 현재 방에 몬스터가 없다면
            if (!rooms[randomRoomNum].GetHasEnemy())
            {
                // 몬스터 생성 후 루프 탈출
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], rooms[randomRoomNum].transform.position, Quaternion.identity);
                rooms[randomRoomNum].SetHasEnemy(true);
                roomInfos.Add(new RoomInfo(enemy, randomRoomNum));

                Debug.Log($"{randomRoomNum}번 방에 몬스터가 리스폰 되었습니다.");

                break; 
            }
            // 현재 방에 몬스터가 있다면 계속해서 반복
        }
    }
}
