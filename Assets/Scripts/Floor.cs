using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] float UpperSpeed = 2f;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Translate(0, UpperSpeed * Time.deltaTime, 0);
        if (transform.position.y > 6f) {
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }
    }
}