using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] private List<RoomBehaviour> rooms = new List<RoomBehaviour>();

    private void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        enemyPrefabs.AddRange(Resources.LoadAll<GameObject>($"Prefabs/Enemy/Stage{1}"));
        rooms.AddRange(GetComponentsInChildren<RoomBehaviour>());

        foreach (var room in rooms)
        {
            if (!room.GetHasEnemy() && !(Vector3.Distance(Player.Instance.transform.position, room.transform.position) < 1f))
            {
                bool isSpawnEnemy = (Random.Range(0, 2) == 1);

                if (isSpawnEnemy)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], room.transform.position, Quaternion.identity);
                    room.SetHasEnemy(isSpawnEnemy);
                }
            }
        }
    }
}
