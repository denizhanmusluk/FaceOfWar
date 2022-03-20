using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawner : MonoBehaviour
{
    public GameObject[] coin;
    public int coinGroupCount = 10;
    public int coinGroupDistance = 10;

    private float[] xPosition = new float[2];
    private float[] yPosition = new float[2];
    int maxScore = 0;
    void Start()
    {
        _coinSpawn();
    }

    void _coinSpawn()
    {
        xPosition[0] = 0f;
        xPosition[1] = 0f;

        yPosition[0] = -0.7f;
        yPosition[1] = 0.7f;

        for (int x = 1; x <= coinGroupCount; x++)
        {
            int xIndex = Random.Range(0, 2);
            int yIndex = Random.Range(0, 2);
            //int i = Random.RandomRange(1, 6);
            int selectionCoin = Random.Range(0, coin.Length);
            //for (; i <= 1; i++)
            //{
                //yield return new WaitForSeconds(Random.Range(0.0f, 0.05f));
                float randomX;
                float randomY = 0;
                randomX = xPosition[xIndex];
                //if (xIndex == 0)
                //{
                //    randomX = xPosition[xIndex] + Mathf.Abs(3.0f - i) / 4;
                //}
                //else
                //{
                //    randomX = xPosition[xIndex] - Mathf.Abs(3.0f - i) / 4;
                //}
                maxScore++;
                float randomZ = x * coinGroupDistance;
                //float randomZ = i + x * coinGroupDistance;
                var diamond = Instantiate(coin[selectionCoin], transform.position + new Vector3(randomX, randomY, randomZ), Quaternion.identity);
                diamond.transform.parent = gameObject.transform;
            //}
        }
        //Finish.Instance.maXScore = maxScore;
        //Finish.Instance.diamondCount();
    }
}
