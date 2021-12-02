#include <algorithm>
#include <cstdio>
#include <vector>
#include <math.h>
#include <iostream>
#include <map>

#include "Node.hpp"
#include "Tree.hpp"
#include "Path.hpp"
#include "NodesSorting.hpp"
#include "Metrics.hpp"

bool is_fair_square(int n)
{
    int h = n & 0xF;  // h is the last hex "digit"
    if (h > 9)
        return false;
    // Use lazy evaluation to jump out of the if statement as soon as possible
    if (h != 2 && h != 3 && h != 5 && h != 6 && h != 7 && h != 8)
    {
        int t = (int) floor( sqrt((double) n));
        return t*t == n;
    }
    return false;
}

template<class T> Tree<T> *build_tree(int n, NodesSorting<T> *sorting)
{
	Tree<T> *tree = new Tree<T>(n);

	for (int i = 1; i <= n; i++)
	{

		for (int j = 1; j <= n; j++)
		{
			if (i == j)
			{
				continue;
			}

			int sum = i + j;
			if (!is_fair_square(sum))
			{
				continue;
			}

			tree->AddPair(i, j);
		}
	}

	if (!tree->VerifyAllNodesHavePairs())
	{
		delete tree;
		return NULL;
	}

	tree->SortPairsWithSorting(sorting);
	return tree;
}

template<class T> void dfs(int n, Node *node, Path *path, Metrics *metrics, NodesSorting<T> *sorting)
{
	if (metrics != NULL) {
		metrics->IncrementDfsCounter();
	}

	sorting->SortNodes(node->Pairs());

	for (Node *p : *node->Pairs())
	{
		int v = p->Value();

		if (path->Contains(v))
		{
			continue;
		}

		path->Push(v);

		if (path->Count() == n)
		{
			break;
		}

		dfs(n, p, path, metrics, sorting);
		if (path->Count() == n)
		{
			break;
		}

		path->Pop();
	}
}

template<class T> std::vector<int> square_sums_row(int n, Metrics *metrics)
{
	NodesSorting<T> * sorting = new NodesSorting<T>(NULL, n);
	Tree<T> *tree = build_tree<T>(n, sorting);
	delete sorting;
	if (tree == NULL)
	{
		return std::vector<int>();
	}

	for (Node *root : *tree->Roots())
	{
		Path *path = new Path(n);
		NodesSorting<T> * sorting = new NodesSorting<T>(path, n);
		path->Push(root->Value());
		dfs(n, root, path, metrics, sorting);
		if (path->Count() == n)
		{
			std::vector<int> result = path->ToVector();
			if (metrics != NULL) {
				metrics->FinalizeDfsCounter(n);
			}
			delete path;
			delete tree;
			delete sorting;
			return result;
		}
		delete sorting;
		delete path;
	}
	delete tree;
	return std::vector<int>();
}