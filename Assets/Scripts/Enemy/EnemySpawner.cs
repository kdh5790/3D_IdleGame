using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<RoomBehaviour> rooms = new List<RoomBehaviour>();

    public List<RoomInfo> roomInfos = new List<RoomInfo>();

    private void Awake()
    {
        Initialized();
    }

    public void Initialized()
    {
        enemyPrefabs = Resources.LoadAll<GameObject>($"Prefabs/Enemy/Stage{StageManager.Instance.CurrentStage}").ToList();
        rooms = GetComponentsInChildren<RoomBehaviour>().ToList();

        foreach ( var roomInfo in roomInfos )
        {
            if(roomInfo.enemy != null)
            {
                Destroy(roomInfo.enemy);
            }
        }

        roomInfos.Clear();


        for (int i = 0; i < rooms.Count; i++)
        {
            if (!rooms[i].GetHasEnemy() && !(Vector3.Distance(Player.Instance.transform.position, rooms[i].transform.position) < 1f))
            {
                bool isSpawnEnemy = (Random.Range(0, 2) == 1);

                if (isSpawnEnemy)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], rooms[i].transform.position, Quaternion.identity);
                    rooms[i].SetHasEnemy(isSpawnEnemy);
                    roomInfos.Add(new RoomInfo(enemy, i));
                }
            }
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        RoomInfo info = roomInfos.Find(x => x.enemy == enemy);

        if (info != null)
        {
            rooms[info.roomIndex].SetHasEnemy(false);
            roomInfos.Remove(info);
        }

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            int randomRoomNum = Random.Range(0, rooms.Count);

            if (!rooms[randomRoomNum].GetHasEnemy())
            {
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], rooms[randomRoomNum].transform.position, Quaternion.identity);
                rooms[randomRoomNum].SetHasEnemy(true);
                roomInfos.Add(new RoomInfo(enemy, randomRoomNum));

                Debug.Log($"{randomRoomNum}번 방에 몬스터가 리스폰 되었습니다.");

                break; 
            }
        }
    }
}
