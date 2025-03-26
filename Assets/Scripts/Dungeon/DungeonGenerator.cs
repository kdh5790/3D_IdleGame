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
        public GameObject room; // �� ������
        public Vector2Int minPosition; // ���� ���� �� �� �ִ� �ּ� ��ǥ
        public Vector2Int maxPosition; // ���� ���� �� �� �ִ� �ִ� ��ǥ

        public bool obligatory; // �ݵ�� ���� �Ǿ�� �ϴ��� ����

        // �ش� ��ǥ���� �� ������ ���� ��Ģ�� ����Ǵ��� Ȯ���ϴ� �Լ�
        public int ProbabilityOfSpawning(int x, int y)
        {
            // ��ǥ�� ���� ���� �� �� �ִ� ��ǥ ���� �ִ��� Ȯ��
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                // obligatory�� true�̸� 2 (�ݵ�� ����), false�̸� 1 (������ Ȯ�� ����) ��ȯ
                return obligatory ? 2 : 1;
            }

            // ���� ���̶�� �������� ����
            return 0;
        }

    }

    public Vector2Int size; // ���� ũ��
    public int startPos = 0; // ���� ��ġ
    public Rule[] rooms; // �� ���� ��Ģ �迭
    public Vector2 offset; // �� ���� ����

    private List<Cell> board; // Cell �迭

    private void Start()
    {
        // �� ���� ����
        RoomGenerator();

        // NavMeshSurface�� �ִ��� �˻� �� ���ٸ� ������Ʈ �߰�
        if (!TryGetComponent(out NavMeshSurface surface))
            surface = gameObject.AddComponent<NavMeshSurface>();

        // NavMesh ���� / ����ũ
        surface.BuildNavMesh();

        // �� ������ ���� EnemySpawner ������Ʈ �߰�
        gameObject.AddComponent<EnemySpawner>();
    }

    // �� ���� �Լ�
    private void GenerateDungeon()
    {
        // �����ص� ���� ũ�⸸ŭ �ݺ�
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)]; // ���� Cell ��������
                // �湮�� Cell�� ���
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();
                    // �� ��Ģ �迭 ũ�⸸ŭ �ݺ�
                    for (int k = 0; k < rooms.Length; k++)
                    {
                        // ���� ���� ��Ģ Ȯ�� �� int �� ��������(0, 1, 2 �� �� �ϳ� ��ȯ)
                        int p = rooms[k].ProbabilityOfSpawning(i, j);

                        // �ݵ�� �����ؾ� �Ѵٸ�
                        if (p == 2)
                        {
                            randomRoom = k;
                            break;
                        }
                        // ������ Ȯ���� �ִٸ�
                        else if (p == 1)
                        {
                            availableRooms.Add(k);
                        }
                    }

                    // �ݵ�� �����ؾ� �� ���� ���ٸ�
                    if (randomRoom == -1)
                    {
                        // ������ Ȯ���� �ִ� ���� �ִٸ�
                        if (availableRooms.Count > 0)
                        {
                            // ������ �� ��Ģ �������� 
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        // ���ٸ� 0�� �ε��� �� ��Ģ ����
                        else
                        {
                            randomRoom = 0;
                        }
                    }

                    // randomRoom �ε����� ��ϵ� �� ������ ������Ʈ ����
                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();

                    // ���� �� ���� ������Ʈ(RoomGenerator �Լ����� ������ status ���� ���� ���� ������Ʈ)
                    newRoom.UpdateRoom(currentCell.status);
                    // �� ������Ʈ �̸� ����
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }

    }

    // �� ���� �Լ�
    public void RoomGenerator()
    {
        board = new List<Cell>();

        // ���ص� ���� ũ�⸸ŭ cell ����
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        // �湮�� Cell ��� ����
        Stack<int> path = new Stack<int>();

        // ���ѷ��� ����
        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true; // ���� Cell �湮 ó��

            // ������ Cell�� ������ ��� ���� ����
            if (currentCell == board.Count - 1)
            {
                break;
            }

            List<int> neighbors = CheckNeighbors(currentCell); // �ֺ� Cell Ȯ��

            // �ֺ��� �湮���� ���� Cell�� ���ٸ�
            if (neighbors.Count == 0)
            {
                // ������ ����ִٸ�
                if (path.Count == 0)
                {
                    // ���̻� ������ ���� ���� ������ ���� ����
                    break;
                }
                else
                {
                    // ������ �����Ѵٸ� ���� Cell ��������
                    currentCell = path.Pop();
                }
            }
            // �湮���� ���� Cell�� �ִٸ�
            else
            {
                // ���� Cell�� ���ÿ� �߰�
                path.Push(currentCell);

                // �ֺ� Cell �� ������ Cell ��������
                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                // ���ο� Cell�� ���� Cell ���� ũ�ٸ�
                if (newCell > currentCell)
                {
                    //(���� ��ġ�� �� ���� ?)
                    // newCell�� currentCell�� ������ Cell�� ���
                    if (newCell - 1 == currentCell)
                    {
                        // ���� Cell�� ������ �� ����
                        board[currentCell].status[2] = true;
                                                                                
                        currentCell = newCell; // ���� Cell�� ���ο� Cell�� ����

                        // ���ο� Cell�� ���� �� ����
                        board[currentCell].status[3] = true;
                    }
                    // newCell�� currentCell�� �Ʒ��� Cell�� ���
                    else
                    {
                        // ���� Cell�� �Ʒ��� �� ����
                        board[currentCell].status[1] = true;

                        currentCell = newCell; // ���� Cell�� ���ο� Cell�� ����

                        // ���ο� Cell�� ���� �� ����
                        board[currentCell].status[0] = true;
                    }
                }
                // ���ο� Cell�� ���� Cell ���� �۴ٸ�
                else
                {
                    // newCell�� currentCell�� ���� Cell�� ���
                    if (newCell + 1 == currentCell)
                    {
                        // ���� Cell�� ���� �� ����
                        board[currentCell].status[3] = true;

                        currentCell = newCell; // ���� Cell�� ���ο� Cell�� ����

                        // ���ο� Cell�� ������ �� ����
                        board[currentCell].status[2] = true;
                    }
                    // newCell�� currentCell�� ���� Cell�� ���
                    else
                    {
                        // ���� Cell�� ���� �� ����
                        board[currentCell].status[0] = true;

                        currentCell = newCell; // ���� Cell�� ���ο� Cell�� ����

                        // ���ο� Cell�� �Ʒ��� �� ����
                        board[currentCell].status[1] = true;
                    }
                }

            }

        }
        GenerateDungeon();
    }

    /// <summary>
    /// �ֺ��� �湮���� ���� Cell�� ã�� �Լ�
    /// </summary>
    /// <param name="cell">���� Cell</param>
    /// <returns>�ֺ��� �湮���� ���� Cell List</returns>
    private List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // ���� Cell Ȯ��
        if (cell - size.x >= 0 && !board[(cell - size.x)].visited)
        {
            // �湮���� �ʾҴٸ� ��ȯ�� ����Ʈ�� �߰�
            neighbors.Add((cell - size.x));
        }

        // �Ʒ��� Cell Ȯ��
        if (cell + size.x < board.Count && !board[(cell + size.x)].visited)
        {
            neighbors.Add((cell + size.x));
        }

        // ������ Cell Ȯ��
        if ((cell + 1) % size.x != 0 && !board[(cell + 1)].visited)
        {
            neighbors.Add((cell + 1));
        }

        // ���� Cell Ȯ��
        if (cell % size.x != 0 && !board[(cell - 1)].visited)
        {
            neighbors.Add((cell - 1));
        }

        return neighbors;
    }

    // ���� ���� �ı� �Լ�
    public void DestroyDungeon()
    {
        RoomBehaviour[] rooms = FindObjectsOfType<RoomBehaviour>();

        foreach (RoomBehaviour room in rooms)
        {
            Destroy(room.gameObject);
        }
    }

    // Ŀ���� �����Ϳ��� GenerateDungeon ��ư Ŭ�� �� ���� �� �Լ�
    public void GenerateDungeonButton()
    {
        DestroyDungeon();

        RoomGenerator();
    }

    // Ŀ���� �����Ϳ��� DestroyDungeon ��ư Ŭ�� �� ���� �� �Լ�
    public void DestroyDungeonButton()
    {
        DestroyDungeon();
    }
}