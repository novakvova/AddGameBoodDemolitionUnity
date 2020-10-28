using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]

    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1;
    public float cloudScaleMax = 3;
    public float cloudSpeedmult = 0.5f;

    private GameObject[] cloudeInstances;

    private void Awake()
    {
        // Создать массив для хранения всех экземпляров облаков
        cloudeInstances = new GameObject[numClouds];
        // Найти родительский игровой объект CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        // Создать в цикле заданное количество облаков
        GameObject cloud;
        for(int i = 0; i < numClouds; i++)
        {
            // Создать экземпляр cloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefab);
            // Выбрать местоположение для облака
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMin.y);
            cPos.y = Random.Range(cloudPosMax.x, cloudPosMax.y);
            // Масштабировать облако
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // Меньшие облака (с меньшим значением scaleU) должны быть ближе
            // к земле
            cPos.y = Mathf.Lerp(cloudPosMax.y, cPos.y, scaleU);
            // Меньшие облака должны быть дальше
            cPos.z = 100 - 90 * scaleU;
            // Применить полученные значения координат и масштаба к облаку
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // Сделать облако дочерним по отношению к anchor
            cloud.transform.SetParent(anchor.transform);
            // Добавить облако в массив
            cloudeInstances[i] = cloud;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       foreach(GameObject cloud in cloudeInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedmult;

            if(cPos.x <= cloudPosMin.x)
            {
                cPos.x = cloudPosMax.x;
            }

            cloud.transform.position = cPos;
        }
    }
}
