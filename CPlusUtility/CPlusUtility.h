#pragma once

#ifndef CP_UTILITY_NATIVE_LIB_H
#define CP_UTILITY_NATIVE_LIB_H

#define DLLExport __declspec(dllexport)

extern "C"
{
	DLLExport int BinarySearch(int arr[], int l, int r, int num);
	DLLExport void BubbleSort(int arr[], int n);
	DLLExport void quickSort(int arr[], int low, int high);
	DLLExport int partition(int arr[], int low, int high);
}
#endif

void Swap(int *x, int *y);