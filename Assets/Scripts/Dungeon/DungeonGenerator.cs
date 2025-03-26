using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room; // 방 프리팹
        public Vector2Int minPosition; // 방이 생성 될 수 있는 최소 좌표
        public Vector2Int maxPosition; // 방이 생성 될 수 있는 최대 좌표

        public bool obligatory; // 반드시 생성 되어야 하는지 여부

        // 해당 좌표에서 위 변수에 대한 규칙이 적용되는지 확인하는 함수
        public int ProbabilityOfSpawning(int x, int y)
        {
            // 좌표가 방이 생성 될 수 있는 좌표 내에 있는지 확인
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                // obligatory가 true이면 2 (반드시 생성), false이면 1 (생성될 확률 있음) 반환
                return obligatory ? 2 : 1;
            }

            // 범위 밖이라면 생성하지 않음
            return 0;
        }

    }

    public Vector2Int size; // 맵의 크기
    public int startPos = 0; // 시작 위치
    public Rule[] rooms; // 방 생성 규칙 배열
    public Vector2 offset; // 방 간의 간격

    private List<Cell> board; // Cell 배열

    private void Start()
    {
        // 맵 생성 시작
        RoomGenerator();

        // NavMeshSurface가 있는지 검사 후 없다면 컴포넌트 추가
        if (!TryGetComponent(out NavMeshSurface surface))
            surface = gameObject.AddComponent<NavMeshSurface>();

        // NavMesh 생성 / 베이크
        surface.BuildNavMesh();

        // 적 생성을 위한 EnemySpawner 컴포넌트 추가
        gameObject.AddComponent<EnemySpawner>();
    }

    // 맵 생성 함수
    private void GenerateDungeon()
    {
        // 설정해둔 맵의 크기만큼 반복
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)]; // 현재 Cell 가져오기
                // 방문한 Cell인 경우
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();
                    // 방 규칙 배열 크기만큼 반복
                    for (int k = 0; k < rooms.Length; k++)
                    {
                        // 현재 방의 규칙 확인 후 int 값 가져오기(0, 1, 2 셋 중 하나 반환)
                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        // 반드시 생성해야 한다면
                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        // 생성될 확률이 있다면
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    // 반드시 생성해야 할 방이 없다면
                    if (randomRoom == -1)
                    {
                        // 생성될 확률이 있는 방이 있다면
                        if (availableRooms.Count > 0)
                        {
                            // 랜덤한 방 규칙 가져오기 
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        // 없다면 0번 인덱스 방 규칙 선택
                        else
                        {
                            randomRoom = 0;
                        }
                    }

                    // randomRoom 인덱스의 등록된 방 프리팹 오브젝트 생성
                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();

                    // 방의 벽 상태 업데이트(RoomGenerator 함수에서 설정한 status 값에 따라 상태 업데이트)
                    newRoom.UpdateRoom(currentCell.status);
                    // 방 오브젝트 이름 수정
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }

    }

    // 방 생성 함수
    public void RoomGenerator()
    {
        board = new List<Cell>();

        // 정해둔 맵의 크기만큼 cell 생성
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        // 방문한 Cell 경로 스택
        Stack<int> path = new Stack<int>();

        // 무한루프 방지
        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true; // 현재 Cell 방문 처리

            // 마지막 Cell에 도달한 경우 생성 종료
            if (currentCell == board.Count - 1)
            {
                break;
            }

            List<int> neighbors = CheckNeighbors(currentCell); // 주변 Cell 확인

            // 주변에 방문하지 않은 Cell이 없다면
            if (neighbors.Count == 0)
            {
                // 스택이 비어있다면
                if (path.Count == 0)
                {
                    // 더이상 진행할 곳이 없기 때문에 생성 종료
                    break;
                }
                else
                {
                    // 스택이 존재한다면 이전 Cell 가져오기
                    currentCell = path.Pop();
                }
            }
            // 방문하지 않은 Cell이 있다면
            else
            {
                // 현재 Cell을 스택에 추가
                path.Push(currentCell);

                // 주변 Cell 중 랜덤한 Cell 가져오기
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // 새로운 Cell이 현재 Cell 보다 크다면
                if (newCell > currentCell)
                {
                    //(서로 겹치는 벽 제거 ?)
                    // newCell이 currentCell의 오른쪽 Cell인 경우
                    if (newCell - 1 == currentCell)
                    {
                        // 현재 Cell의 오른쪽 벽 제거
                        board[currentCell].status[2] = true;
                                                                                
                        currentCell = newCell; // 현재 Cell을 새로운 Cell로 변경

                        // 새로운 Cell의 왼쪽 벽 제거
                        board[currentCell].status[3] = true;
                    }
                    // newCell이 currentCell의 아래쪽 Cell인 경우
                    else
                    {
                        // 현재 Cell의 아래쪽 벽 제거
                        board[currentCell].status[1] = true;

                        currentCell = newCell; // 현재 Cell을 새로운 Cell로 변경

                        // 새로운 Cell의 위쪽 벽 제거
                        board[currentCell].status[0] = true;
                    }
                }
                // 새로운 Cell이 현재 Cell 보다 작다면
                else
                {
                    // newCell이 currentCell의 왼쪽 Cell인 경우
                    if (newCell + 1 == currentCell)
                    {
                        // 현재 Cell의 왼쪽 벽 제거
                        board[currentCell].status[3] = true;

                        currentCell = newCell; // 현재 Cell을 새로운 Cell로 변경

                        // 새로운 Cell의 오른쪽 벽 제거
                        board[currentCell].status[2] = true;
                    }
                    // newCell이 currentCell의 위쪽 Cell인 경우
                    else
                    {
                        // 현재 Cell의 위쪽 벽 제거
                        board[currentCell].status[0] = true;

                        currentCell = newCell; // 현재 Cell을 새로운 Cell로 변경

                        // 새로운 Cell의 아래쪽 벽 제거
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    /// <summary>
    /// 주변의 방문하지 않은 Cell을 찾는 함수
    /// </summary>
    /// <param name="cell">현재 Cell</param>
    /// <returns>주변의 방문하지 않은 Cell List</returns>
    private List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // 위쪽 Cell 확인
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            // 방문하지 않았다면 반환할 리스트에 추가
            neighbors.Add((cell - size.x));
        }

        // 아래쪽 Cell 확인
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        // 오른쪽 Cell 확인
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        // 왼쪽 Cell 확인
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }

    // 기존 던전 파괴 함수
    public void DestroyDungeon()
    {
        RoomBehaviour[] rooms = FindObjectsOfType<RoomBehaviour>();

        foreach (RoomBehaviour room in rooms)
        {
            Destroy(room.gameObject);
        }
    }

    // 커스텀 에디터에서 GenerateDungeon 버튼 클릭 시 실행 될 함수
    public void GenerateDungeonButton()
    {
        DestroyDungeon();

        RoomGenerator();
    }

    // 커스텀 에디터에서 DestroyDungeon 버튼 클릭 시 실행 될 함수
    public void DestroyDungeonButton()
    {
        DestroyDungeon();
    }
}