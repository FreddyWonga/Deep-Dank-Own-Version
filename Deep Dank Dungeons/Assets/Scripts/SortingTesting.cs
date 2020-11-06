using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SortingTesting : MonoBehaviour
{
    public int[] numbers;

    [DllImport("CPlusUtility")]
    public static extern int BubbleSort(int[] arr, int n);
    [DllImport("CPlusUtility")]
    public static extern int BinarySearch(int[] arr, int p, int r, int num);

    void Start()
    {
        BubbleSort(numbers, numbers.Length);
        int i; 
        if (BinarySearch(numbers, 0, numbers.Length, 35) >= 0)
        {
            Debug.Log(true);
        }
        else
        {
            Debug.Log(false);
        }
    }
    public void BubbleSort2(int []arr, int n)
    {
        int i;
        int j;
        for (i = 0; i < n - 1; i++)
        {
            for (j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int z;
                    z = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = z;
                }
            }
        }
    }
}

