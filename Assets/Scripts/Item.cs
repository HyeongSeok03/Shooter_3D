using UnityEngine;

public enum ItemState { ATK_SPEED, ATK_COUNT };

public class Item : MonoBehaviour
{
    public GameObject[] cubes;
    ItemState[] items = new ItemState[2];

    private void Start()
    {
        bool swap = Random.value < 0.5f;

        cubes[0].transform.localPosition = swap ? new Vector3(2.5f, 0, 0) : new Vector3(-2.5f, 0, 0);
        cubes[1].transform.localPosition = swap ? new Vector3(-2.5f, 0, 0) : new Vector3(2.5f, 0, 0);
    }

    public float speed;

    private void Update()
    {
        transform.position -= transform.forward * speed * Time.deltaTime;
    }
}
