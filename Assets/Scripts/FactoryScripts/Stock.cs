using Assets.Scripts.FactoryScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private bool isFull;
    [SerializeField] private FactoryBase fatherFactoryBase;

    [SerializeField] private int amountOfResources;
    [SerializeField] private int sizeX;
    [SerializeField] private int maximumSizeX;

    [SerializeField] private int sizeY;
    [SerializeField] private int maximumSizeY;

    public bool IsFull => isFull;
}
