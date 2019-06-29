using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    [SerializeField] GameObject cloudPrefab;
    [SerializeField] GameObject cloudsParent;

    [Header("Spawning time")]
    [Tooltip("In seconds")]
    [SerializeField] float minTimeBetweenSpawns = 1f;
    [SerializeField] float maxTimeBetweenSpawns = 1f;

    [Header("Spawing position")]
    [Tooltip("Maximum value of random ofsset which will be added to," +
    	" or substracted from, generator position to set cloud position")]
    [SerializeField] float maxOffseet = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnClouds());
    }

    private IEnumerator SpawnClouds() 
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns));

            SpawnCloud();
        }
    }

    private void SpawnCloud()
    {
        var offsetX = Random.Range(-maxOffseet, maxOffseet);
        var position = transform.position + new Vector3(offsetX, 0f, 0f);
        var cloud = Instantiate(cloudPrefab, position, Quaternion.identity);

        if (cloudsParent)
        {
            cloud.transform.SetParent(cloudsParent.transform);
        }
    }
}
