using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] clouds;
    void Start()
    {
        StartCoroutine(spawn());
    }

  IEnumerator spawn()
    {
  
        while (true)
        {
            int select = Random.Range(0, clouds.Length);
           GameObject cloud = Instantiate(clouds[select], transform.position, Quaternion.identity);
            cloud.transform.rotation = Quaternion.Euler(90, 90, 0);
            cloud.transform.position = transform.position + new Vector3(0, Random.Range(-30, 30), 0);
            StartCoroutine(moving(cloud));
            yield return new WaitForSeconds(10f);
        }
    }
    IEnumerator moving(GameObject cloud)
    {
        float counter = 0;
        while(counter < 40f)
        {
            counter += Time.deltaTime;
            cloud.transform.position = Vector3.MoveTowards(cloud.transform.position, cloud.transform.position+ new Vector3(1, 0, 0), 10 * Time.deltaTime);

            yield return null;
        }
    }
}
