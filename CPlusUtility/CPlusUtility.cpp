#include <iostream>
#include "CPlusUtility.h"

int BinarySearch(int arr[], int p, int r, int num)
{
	if (p <= r)
	{
		int mid = (p + r) / 2;
		if (arr[mid] == num)
		{
			return mid;
		}
		if (arr[mid] > num)
		{
			return BinarySearch(arr, p, mid - 1, num);
		}
		if (arr[mid] < num)
		{
			return BinarySearch(arr, mid + 1, r, num);
		}
	}
	return -1;
}

void Swap(int* xp, int* yp)
{
	int temp = *xp;
	*xp = *yp;
	*yp = temp;
}

void BubbleSort(int arr[], int n)
{
	int i, j;
	for (i = 0; i < n - 1; i++)
	{
		for (j = 0; j < n - i - 1; j++)
		{
			if (arr[j] > arr[j + 1])
			{
				Swap(&arr[j], &arr[j + 1]);
			}
		}
	}
}

int partition(int arr[], int low, int high)
{
	int pivot = arr[high];
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr[j] <= pivot)
		{
			i++;
			Swap(&arr[i], &arr[j]);
		}
	}
	Swap(&arr[i + 1], &arr[high]);
	return (i + 1);
}

int partitionInvert(int arr[], int low, int high)
{
	int pivot = arr[high];
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr[j] >= arr[low])
		{
			i++;
			Swap(&arr[i], &arr[j]);
		}
	}	
	Swap(&arr[i + 1], &arr[high]);
	return (i + 1);
}

void quickSort(int arr[], int low, int high)
{
	if (low < high)
	{
		int part = partition(arr, low, high);

		quickSort(arr, low, part - 1);
		quickSort(arr, part + 1, high);
	}
}

void quickSortInvert(int arr[], int low, int high)
{
	if (low < high)
	{
		int part = partitionInvert(arr, low, high);
		quickSortInvert(arr, low, part - 1);
		quickSortInvert(arr, part + 1, high);
	}
}