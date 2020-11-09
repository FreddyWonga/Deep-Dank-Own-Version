#pragma once

#ifndef CPLUSUTILITY_NATIVE_LIB_H
#define CPLUSUTILITY_NATIVE_LIB_H

#define DLLExport __declspec(dllexport)

extern "C"
{
	DLLExport int BinarySearch(int arr[], int p, int r, int num);
	DLLExport void BubbleSort(int arr[], int n);
	DLLExport int partition(int arr[], int low, int high);
	DLLExport void quickSort(int arr[], int low, int high);
	DLLExport void quickSortInvert(int arr[], int low, int high);

}
#endif