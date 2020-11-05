// CPlusUtility.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

//hash function
//2 sorting (Should be done)
//searching (Maybe done?)

#include <iostream>
#include "CPlusUtility.h"

int BinarySearch(int arr[], int l, int r, int num)
{
	if (l <= r)
	{
		int mid = l + r / 2;
		if (arr[mid] == num)
		{
			return mid;
			if (arr[mid] > num)
			{
				return BinarySearch(arr, l, mid - 1, num);
			}
			if (arr[mid] < num)
			{
				return BinarySearch(arr, mid + 1, r, num);
			}
		}
	}
	return -1;
}

int partition(int arr[], int low, int high)
{
	int pivot = arr[high];
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr[j] < pivot)
		{
			i++;
			Swap(&arr[i], &arr[j]);
		}
	}
	Swap(&arr[i + 1], &arr[high]);
	return(i + 1);
}

void quickSort(int arr[], int low, int high)
{
	if (low < high)
	{
		int pi = partition(arr, low, high);

		quickSort(arr, low, pi - 1);
		quickSort(arr, pi + 1, high);
	}
}

void BubbleSort(int arr[], int n)
{
	int i;
	int j;
	for (i = 0; i < n - 1; i++)
	{
		for (j = 0; j < n - i - 1; j++)
		{
			if (&arr[j] > & arr[j + 1])
			{
				Swap(&arr[j], &arr[j + 1]);
			}
		}
	}
}


void Swap(int *x, int *y)
{
	int temp = *x;
	*x = *y;
	*y = temp;
}