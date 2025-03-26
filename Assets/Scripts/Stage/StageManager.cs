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
        // 방 모두 파괴 후 재생성
        dungeonGenerator.DestroyDungeon();
        dungeonGenerator.RoomGenerator();

        if (surface == null)
            surface = GetComponent<NavMeshSurface>();

        surface.BuildNavMesh(); // NavMesh 다시 베이크

        if (enemySpawner == null)
            enemySpawner = GetComponent<EnemySpawner>();

        enemySpawner.Initialized(); // 스테이지에 맞는 적 생성

        // 플레이어 위치를 적이 존재하지 않는 방으로 이동
        MovePlayerToSafeRoom(); 
    }

    public void ChangeStage(int stage)
    {
        currentStage = stage;
        InitializeStage();
    }

    // 플레이어를 안전한 곳으로 이동시키는 함수
    private void MovePlayerToSafeRoom()
    {
        // 현재 활성화된 방 List 가져오기
        List<RoomBehaviour> availableRooms = dungeonGenerator.GetComponentsInChildren<RoomBehaviour>().ToList();
        List<RoomBehaviour> safeRooms = new List<RoomBehaviour>(); // 몬스터가 존재하지 않는 방을 담아둘 List

        foreach (RoomBehaviour room in availableRooms)
        {
            // 방에 몬스터가 존재하지 않는다면 리스트에 추가
            if (!room.GetHasEnemy())
            {
                safeRooms.Add(room);
            }
        }

        // 안전한 방이 있다면
        if (safeRooms.Count > 0)
        {
            // 랜덤한 방을 뽑아 플레이어를 이동시킴
            RoomBehaviour selectedRoom = safeRooms[Random.Range(0, safeRooms.Count)];
            Player.Instance.transform.position = selectedRoom.transform.position;
        }
        // 안전한 방이 없다면 시작위치로 이동
        else
        {
            Debug.LogWarning("안전한 방을 찾을 수 없습니다. 플레이어를 시작 위치로 이동합니다.");
            Player.Instance.transform.position = dungeonGenerator.transform.position; 
        }
    }
}