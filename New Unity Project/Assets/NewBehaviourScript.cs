using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class Utility
{
    public static void SelectionSort(int[] array)
    {
        int n = array.Length;

        for(int i =0; i < n; i++)
        {
            int minIndex = i;
            for(int c = i + 1; c< n; c++)
            {
                if(array[c] < array[minIndex])
                {
                    minIndex = c;
                }
            }

            int min = array[minIndex];
            array[minIndex] = array[i];
            array[i] = min;
        }
    }

    public static void BubbleSort(int[] array)
    {
        int n = array.Length;

        for(int i = 0; i < n; i++)
        {
            for(int c = 0; c < n - i - 1; c++)
            {
                if(array[c] > array[c + 1])
                {
                    int newMax = array[c];
                    array[c] = array[c + 1];
                    array[c + 1] = newMax
                }
            }
        }
    }

    public static bool LinearSearch<T>(T[] array, T target)
    {
        int n = array.Length;
        for(int i = 0; i < n; i++)
        {
            if(array[i].Equals(target) == true)
            {
                return true;
            }
        }
        return false;
    }

    public static bool BinarySearch(int[] array, int target, int left, int right)
    {
        if(right > left)
        {
            int mid = left + (right - left) / 2;
            if(array[mid] == target)
            {
                return true;
            }
            else if (array[mid] > target)
            {
                return BinarySearch(array, target, left, right - 1);
            }
            else
            {
                return BinarySearch(array, target, mid + 1, right);
            }
        }
        else
        {
            return false;
        }
    }
}