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
	return tree;
}

int dfsCounter = 0;

void dfs(int n, std::shared_ptr<Node> node, std::shared_ptr<Path> path)
{
	dfsCounter++;
	NodesSorting *sorting = new NodesSorting(path, n);

	std::vector<std::shared_ptr<Node>> pairs(node->PairsCount());
	std::vector<std::shared_ptr<Node>> src = *node->Pairs();
	std::copy(src.begin(), src.end(), pairs.begin());

	sorting->SortNodes(&pairs);

	for (std::shared_ptr<Node> p : pairs)
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

		dfs(n, p, path);
		if (path->Count() == n)
		{
			break;
		}

		path->Pop();
	}

	pairs.clear();
}

std::vector<int> square_sums_row(int n)
{
	Tree *tree = build_tree(n);
	if (tree == NULL)
	{
		return std::vector<int>();
	}

	for (std::shared_ptr<Node> root : tree->Roots())
	{
		std::shared_ptr<Path> path = std::shared_ptr<Path>(new Path(n));
		path->Push(root->Value());
		dfs(n, root, path);
		if (path->Count() == n)
		{
			std::vector<int> result = path->ToVector();
			path.reset();
			if (dfsCounter / n > 1)
			{
				std::cout << "Counter for " << n << " : " << dfsCounter << "\n";
			}
			dfsCounter = 0;
			return result;
		}
		path.reset();
	}
	delete tree;
	return std::vector<int>();
}