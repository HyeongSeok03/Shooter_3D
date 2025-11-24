using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab;

    public bool item;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float xPos = Random.Range(-3.5f, 3.5f);
        float time = Random.Range(1.0f, 3.0f);
        if (item)
        {
            xPos = 0f;
            time = Random.Range(3.0f, 3.0f);
        }

        Vector3 localPos = new Vector3(xPos, 0f, 0f);

        GameObject obj = Instantiate(prefab, transform);
        obj.transform.localPosition = localPos;
        obj.transform.localRotation = Quaternion.identity;
        yield return new WaitForSeconds(time);

        StartCoroutine(Spawn());
    }
}
