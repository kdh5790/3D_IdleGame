using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private int currentStage = 1;
    [SerializeField] private DungeonGenerator dungeonGenerator;
    [SerializeField] private EnemySpawner enemySpawner;

    private NavMeshSurface surface;

    public int CurrentStage { get { return currentStage; } }

    public void InitializeStage()
    {
        // �� ��� �ı� �� �����
        dungeonGenerator.DestroyDungeon();
        dungeonGenerator.RoomGenerator();

        if (surface == null)
            surface = GetComponent<NavMeshSurface>();

        surface.BuildNavMesh(); // NavMesh �ٽ� ����ũ

        if (enemySpawner == null)
            enemySpawner = GetComponent<EnemySpawner>();

        enemySpawner.Initialized(); // ���������� �´� �� ����

        // �÷��̾� ��ġ�� ���� �������� �ʴ� ������ �̵�
        MovePlayerToSafeRoom(); 
    }

    public void ChangeStage(int stage)
    {
        currentStage = stage;
        InitializeStage();
    }

    // �÷��̾ ������ ������ �̵���Ű�� �Լ�
    private void MovePlayerToSafeRoom()
    {
        // ���� Ȱ��ȭ�� �� List ��������
        List<RoomBehaviour> availableRooms = dungeonGenerator.GetComponentsInChildren<RoomBehaviour>().ToList();
        List<RoomBehaviour> safeRooms = new List<RoomBehaviour>(); // ���Ͱ� �������� �ʴ� ���� ��Ƶ� List

        foreach (RoomBehaviour room in availableRooms)
        {
            // �濡 ���Ͱ� �������� �ʴ´ٸ� ����Ʈ�� �߰�
            if (!room.GetHasEnemy())
            {
                safeRooms.Add(room);
            }
        }

        // ������ ���� �ִٸ�
        if (safeRooms.Count > 0)
        {
            // ������ ���� �̾� �÷��̾ �̵���Ŵ
            RoomBehaviour selectedRoom = safeRooms[Random.Range(0, safeRooms.Count)];
            Player.Instance.transform.position = selectedRoom.transform.position;
        }
        // ������ ���� ���ٸ� ������ġ�� �̵�
        else
        {
            Debug.LogWarning("������ ���� ã�� �� �����ϴ�. �÷��̾ ���� ��ġ�� �̵��մϴ�.");
            Player.Instance.transform.position = dungeonGenerator.transform.position; 
        }
    }
}