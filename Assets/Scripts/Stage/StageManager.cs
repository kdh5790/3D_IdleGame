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
        dungeonGenerator.DestroyDungeon();
        dungeonGenerator.MazeGenerator();

        if (surface == null)
            surface = GetComponent<NavMeshSurface>();

        surface.BuildNavMesh();

        if (enemySpawner == null)
            enemySpawner = GetComponent<EnemySpawner>();

        enemySpawner.Initialized();

        MovePlayerToSafeRoom();


    }

    public void ChangeStage(int stage)
    {
        currentStage = stage;
        InitializeStage();
    }

    private void MovePlayerToSafeRoom()
    {
        List<RoomBehaviour> availableRooms = dungeonGenerator.GetComponentsInChildren<RoomBehaviour>().ToList();
        List<RoomBehaviour> safeRooms = new List<RoomBehaviour>();

        foreach (RoomBehaviour room in availableRooms)
        {
            if (!room.GetHasEnemy())
            {
                safeRooms.Add(room);
            }
        }

        if (safeRooms.Count > 0)
        {
            RoomBehaviour selectedRoom = safeRooms[Random.Range(0, safeRooms.Count)];
            Player.Instance.transform.position = selectedRoom.transform.position;
        }
        else
        {
            Debug.LogWarning("안전한 방을 찾을 수 없습니다. 플레이어를 시작 위치로 이동합니다.");
            Player.Instance.transform.position = dungeonGenerator.transform.position; 
        }
    }
}