using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// enemy�� �����ϴ� ���� �����ص� ���� Ŭ����
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
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>(); // �������� �� ������ ���͸� �����ص� ����Ʈ
    [SerializeField] private List<RoomBehaviour> rooms = new List<RoomBehaviour>(); // ���� ������ ��

    public List<RoomInfo> roomInfos = new List<RoomInfo>(); // ���� ������ ����� ���� ���� ���θ� Ȯ���� ����Ʈ

    private void Awake()
    {
        Initialized();
    }

    public void Initialized()
    {
        // ���������� �´� ���� ���ҽ� ��������
        enemyPrefabs = Resources.LoadAll<GameObject>($"Prefabs/Enemy/Stage{StageManager.Instance.CurrentStage}").ToList();
        rooms = GetComponentsInChildren<RoomBehaviour>().ToList();

        // �濡 ���Ͱ� �����Ѵٸ� ��� �ı�
        foreach ( var roomInfo in roomInfos )
        {
            if(roomInfo.enemy != null)
            {
                Destroy(roomInfo.enemy);
            }
        }

        // �� ���� ����Ʈ �ʱ�ȭ
        roomInfos.Clear();


        for (int i = 0; i < rooms.Count; i++)
        {
            // ���� �濡 ���Ͱ� �������� �ʰ� �÷��̾���� �Ÿ��� 1 �̻��� ��� 
            if (!rooms[i].GetHasEnemy() && !(Vector3.Distance(Player.Instance.transform.position, rooms[i].transform.position) < 1f))
            {
                // ���� ���� ���� ���� ����(0 = false, 1 = true)
                bool isSpawnEnemy = (Random.Range(0, 2) == 1);

                if (isSpawnEnemy)
                {
                    // ����(i) �濡 ���� ����
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], rooms[i].transform.position, Quaternion.identity);
                    rooms[i].SetHasEnemy(isSpawnEnemy);
                    roomInfos.Add(new RoomInfo(enemy, i));
                }
            }
        }
    }

    // ����Ʈ���� ���� ���� �Լ�(���� ��� �� ����)
    public void RemoveEnemy(GameObject enemy)
    {
        // ���� ���Ͱ� �����ϴ� �� ���� ã��
        RoomInfo info = roomInfos.Find(x => x.enemy == enemy);

        // �� ������ ã�Ҵٸ�
        if (info != null)
        {
            // ���� ���� false�� ���� �� ����Ʈ���� ����
            rooms[info.roomIndex].SetHasEnemy(false);
            roomInfos.Remove(info);
        }

        // ���� ���� �ڷ�ƾ ����
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            // �� �ε��� �����ϰ� ��������
            int randomRoomNum = Random.Range(0, rooms.Count);

            // ���� �濡 ���Ͱ� ���ٸ�
            if (!rooms[randomRoomNum].GetHasEnemy())
            {
                // ���� ���� �� ���� Ż��
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], rooms[randomRoomNum].transform.position, Quaternion.identity);
                rooms[randomRoomNum].SetHasEnemy(true);
                roomInfos.Add(new RoomInfo(enemy, randomRoomNum));

                Debug.Log($"{randomRoomNum}�� �濡 ���Ͱ� ������ �Ǿ����ϴ�.");

                break; 
            }
            // ���� �濡 ���Ͱ� �ִٸ� ����ؼ� �ݺ�
        }
    }
}
