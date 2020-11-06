// CPlusUtility.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

//hash function
//2 sorting (Should be done)
//searching (Maybe done?)

#include <iostream>
#include "CPlusUtility.h"
#include<bits/stdc++.h> 


using namespace std;

//Binary Search
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

//Sorting Systems
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
			if (arr[j] > arr[j + 1])
			{
				//Swapping without swap function
				int z;
				z = &arr[j];
				&arr[j] = &arr[j + 1];
				&arr[j + 1] = z;

				//Using swap function
				Swap(&arr[j], &arr[j + 1])
			}
		}
	}
}

void Swap(int *x, int *y)
{
	int z; 
	*z = *x;
	*x = *y;
	*y = *z;
}

//Hashing
class Hash
{
	int BUCKET; // No. of buckets 

	// Pointer to an array containing buckets 
	list<int>* table;
public:
	Hash(int V); // Constructor 

	// inserts a key into hash table 
	void insertItem(int x);

	// deletes a key from hash table 
	void deleteItem(int key);

	// hash function to map values to key 
	int hashFunction(int x) {
		return (x % BUCKET);
	}

	void displayHash();
};

Hash::Hash(int b)
{
	this->BUCKET = b;
	table = new list<int>[BUCKET];
}

void Hash::insertItem(int key)
{
	int index = hashFunction(key);
	table[index].push_back(key);
}

void Hash::deleteItem(int key)
{
	// get the hash index of key 
	int index = hashFunction(key);

	// find the key in (inex)th list 
	list <int> ::iterator i;
	for (i = table[index].begin();
		i != table[index].end(); i++) {
		if (*i == key)
			break;
	}

	// if key is found in hash table, remove it 
	if (i != table[index].end())
		table[index].erase(i);
}


//Binary Search Tree
class BST
{
	int data;
	BST* left, * right;

public:

	// Default constructor.
	BST();

	// Parameterized constructor.
	BST(int);

	// Insert function.
	BST* Insert(BST*, int);

	// Inorder traversal.
	void Inorder(BST*);
};

// Default Constructor definition.
BST::BST() : data(0), left(NULL), right(NULL) {}

// Parameterized Constructor definition.
BST::BST(int value)
{
	data = value;
	left = right = NULL;
}

// Insert function definition.
BST* BST::Insert(BST* root, int value)
{
	if (!root)
	{
		// Insert the first node, if root is NULL.
		return new BST(value);
	}

	// Insert data.
	if (value > root->data)
	{
		// Insert right node data, if the 'value'
		// to be inserted is greater than 'root' node data.

		// Process right nodes.
		root->right = Insert(root->right, value);
	}
	else
	{
		// Insert left node data, if the 'value' 
		// to be inserted is greater than 'root' node data.

		// Process left nodes.
		root->left = Insert(root->left, value);
	}

	// Return 'root' node, after insertion.
	return root;
}

// Inorder traversal function.
// This gives data in sorted order.
void BST::Inorder(BST* root)
{
	if (!root)
	{
		return;
	}
	Inorder(root->left);
	cout << root->data << endl;
	Inorder(root->right);
}