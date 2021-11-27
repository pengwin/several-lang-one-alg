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
	double sqrtVal = sqrt((double)n);
	return sqrtVal - floor(sqrtVal) == 0;
}

Tree *build_tree(int n, NodesSorting *sorting)
{
	Tree *tree = new Tree(n);

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

void dfs(int n, Node *node, Path *path, Metrics *metrics, NodesSorting *sorting)
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

std::vector<int> square_sums_row(int n, Metrics *metrics)
{
	NodesSorting * sorting = new NodesSorting(NULL, n);
	Tree *tree = build_tree(n, sorting);
	delete sorting;
	if (tree == NULL)
	{
		return std::vector<int>();
	}

	for (Node *root : *tree->Roots())
	{
		Path *path = new Path(n);
		NodesSorting * sorting = new NodesSorting(path, n);
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