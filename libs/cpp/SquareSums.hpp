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

Tree *build_tree(int n)
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

	auto sorting = new NodesSorting(NULL, n);
	tree->SortPairsWithSorting(sorting);
	delete sorting;
	return tree;
}

void dfs(int n, Node *node, Path *path, Metrics *metrics)
{
	if (metrics != NULL) {
		metrics->IncrementDfsCounter();
	}
	NodesSorting *sorting = new NodesSorting(path, n);

	std::vector<Node *> pairs(node->PairsCount());
	std::vector<Node *>* src = node->Pairs();
	std::copy((*src).begin(), (*src).end(), pairs.begin());

	sorting->SortNodes(&pairs);
	delete sorting;

	for (Node *p : pairs)
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

		dfs(n, p, path, metrics);
		if (path->Count() == n)
		{
			break;
		}

		path->Pop();
	}

	pairs.clear();
}

std::vector<int> square_sums_row(int n, Metrics *metrics)
{
	Tree *tree = build_tree(n);
	if (tree == NULL)
	{
		return std::vector<int>();
	}

	for (Node *root : *tree->Roots())
	{
		Path *path = new Path(n);
		path->Push(root->Value());
		dfs(n, root, path, metrics);
		if (path->Count() == n)
		{
			std::vector<int> result = path->ToVector();
			if (metrics != NULL) {
				metrics->FinalizeDfsCounter(n);
			}
			delete path;
			delete tree;
			return result;
		}
		delete path;
	}
	delete tree;
	return std::vector<int>();
}