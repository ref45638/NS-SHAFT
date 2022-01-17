using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs;

    private void Start()
    {
    }

    public void InitFloor()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // 隨機產生
        for (int i = -2; i < 4; i++)
        {
            GameObject newFloor = Instantiate(floorPrefabs[i == 3 ? 0 : Random.Range(0, floorPrefabs.Length)], transform);
            newFloor.transform.position = new Vector3(i == 3 ? 0f : Random.Range(-3.4f, 3.9f), 2f - i * 2f, 0f);
        }
    }

    public void SpawnFloor()
    {
        GameObject newFloor = Instantiate(floorPrefabs[Random.Range(0, floorPrefabs.Length)], transform);
        newFloor.transform.position = new Vector3(Random.Range(-3.4f, 3.9f), -6f, 0f);
    }
}