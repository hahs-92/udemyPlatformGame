using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Transform[] fruitPosition;
    [SerializeField] private GameObject fruitPrefab;
    private int fruitIndex = 0;

    private void Start()
    {
        fruitPosition = GetComponentsInChildren<Transform>();

        for (int i = 1; i < fruitPosition.Length; i++)
        {
            GameObject newFruit = Instantiate(fruitPrefab, fruitPosition[i]);
            fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitType)).Length);
            newFruit.GetComponent<Fruit_Item>()?.SetLayerWeight(fruitIndex);
        }
    }

}
