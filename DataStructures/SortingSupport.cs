﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class SortingSupport<T> where T : IComparable<T>
    {
        void Swap(T[] items, int left, int right)
        {
            if (left != right)
            {
                T temp = items[left];
                items[left] = items[right];
                items[right] = temp;
            }
        }

        #region Bubble Sort

        public void BubbleSort(T[] items)
        {
            bool swapped;

            do
            {
                swapped = false;
                for (int i = 1; i < items.Length; i++)
                {
                    if (items[i - 1].CompareTo(items[i]) > 0)
                    {
                        Swap(items, i - 1, i);
                        swapped = true;
                    }
                }
            } while (swapped != false);
        }
        #endregion

        #region Insertion Sort

        public void InsertionSort(T[] items)
        {
            int sortedRangeEndIndex = 1;

            while (sortedRangeEndIndex < items.Length)
            {
                if (items[sortedRangeEndIndex].CompareTo(items[sortedRangeEndIndex - 1]) < 0)
                {
                    int insertIndex = FindInsertionIndex(items, items[sortedRangeEndIndex]);
                    Insert(items, insertIndex, sortedRangeEndIndex);
                }

                sortedRangeEndIndex++;
            }
        }

        private int FindInsertionIndex(T[] items, T valueToInsert)
        {
            for (int index = 0; index < items.Length; index++)
            {
                if (items[index].CompareTo(valueToInsert) > 0)
                {
                    return index;
                }
            }

            throw new InvalidOperationException("The insertion index was not found");
        }

        private void Insert(T[] itemArray, int indexInsertingAt, int indexInsertingFrom)
        {
            // itemArray =       0 1 2 4 5 6 3 7
            // insertingAt =     3
            // insertingFrom =   6
            // actions
            //  1: store index at in temp     temp = 4
            //  2: set index at to index from  -> 0 1 2 3 5 6 3 7   temp = 4
            //  3: walking backwards from index from to index at + 1
            //     shift values from left to right once
            //     0 1 2 3 5 6 6 7   temp = 4
            //     0 1 2 3 5 5 6 7   temp = 4
            //  4: write temp value to index at + 1
            //     0 1 2 3 4 5 6 7   temp = 4

            // Step 1
            T temp = itemArray[indexInsertingAt];

            // Step 2

            itemArray[indexInsertingAt] = itemArray[indexInsertingFrom];

            // Step 3
            for (int current = indexInsertingFrom; current > indexInsertingAt; current--)
            {
                itemArray[current] = itemArray[current - 1];
            }

            // Step 4
            itemArray[indexInsertingAt + 1] = temp;
        }

        #endregion

        #region Selection Sort

        public void SelectionSort(T[] items)
        {
            int sortedRangeEnd = 0;

            while (sortedRangeEnd < items.Length)
            {
                int nextIndex = FindIndexOfSmallestFromIndex(items, sortedRangeEnd);
                Swap(items, sortedRangeEnd, nextIndex);

                sortedRangeEnd++;
            }
        }

        private int FindIndexOfSmallestFromIndex(T[] items, int sortedRangeEnd)
        {
            T currentSmallest = items[sortedRangeEnd];
            int currentSmallestIndex = sortedRangeEnd;

            for (int i = sortedRangeEnd + 1; i < items.Length; i++)
            {
                if (currentSmallest.CompareTo(items[i]) > 0)
                {
                    currentSmallest = items[i];
                    currentSmallestIndex = i;
                }
            }

            return currentSmallestIndex;
        }
        #endregion

        #region Merge Sort

        public void MergeSort(T[] items)
        {
            if (items.Length <= 1)
            {
                return;
            }

            int leftSize = items.Length / 2;
            int rightSize = items.Length - leftSize;

            T[] left = new T[leftSize];
            T[] right = new T[rightSize];

            Array.Copy(items, 0, left, 0, leftSize);
            Array.Copy(items, leftSize, right, 0, rightSize);

            MergeSort(left);
            MergeSort(right);
            Merge(items, left, right);
        }

        private void Merge(T[] items, T[] left, T[] right)
        {
            int leftIndex = 0;
            int rightIndex = 0;
            int targetIndex = 0;

            int remaining = left.Length + right.Length;

            while (remaining > 0)
            {
                if (leftIndex >= left.Length)
                {
                    items[targetIndex] = right[rightIndex++];
                }
                else if (rightIndex >= right.Length)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else if (left[leftIndex].CompareTo(right[rightIndex]) < 0)
                {
                    items[targetIndex] = left[leftIndex++];
                }
                else
                {
                    items[targetIndex] = right[rightIndex++];
                }
                targetIndex++;
                remaining--;
            }
        }

        #endregion

        #region Quick Sort

        Random _pivotRng = new Random();

        public void QuickSort(T[] items)
        {
            quicksort(items, 0, items.Length - 1);
        }

        private void quicksort(T[] items, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = _pivotRng.Next(left, right);
                int newPivot = partition(items, left, right, pivotIndex);

                quicksort(items, left, newPivot - 1);
                quicksort(items, newPivot + 1, right);
            }
        }

        private int partition(T[] items, int left, int right, int pivotIndex)
        {
            T pivotValue = items[pivotIndex];

            Swap(items, pivotIndex, right);

            int storeIndex = left;

            for (int i = left; i < right; i++)
            {
                if (items[i].CompareTo(pivotValue) < 0)
                {
                    Swap(items, i, storeIndex);
                    storeIndex += 1;
                }
            }

            Swap(items, storeIndex, right);
            return storeIndex;
        }

        #endregion
    }
}
