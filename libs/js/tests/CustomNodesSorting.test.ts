import { CustomNodesSorting } from '../src/CustomNodesSorting';
import { Node } from '../src/Node';
import { Path } from '../src/Path';

test('WithoutPath_WithoutCollision', () => {
    // arrange
    var sorting = new CustomNodesSorting(null, 4);
    var nodes: Array<Node> = [];

    let node = new Node(3);
    node.add(new Node(2));
    node.add(new Node(3));
    node.add(new Node(1));

    nodes.push(node);

    node = new Node(4);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    // act
    sorting.sortNodes(nodes);

    // assert
    expect(nodes.map(n => n.value)).toStrictEqual([4, 3]);
});

test('WithPath_WithoutCollision', () => {
    // arrange
    var path = new Path(4);
    var sorting = new CustomNodesSorting(path, 4);
    var nodes: Array<Node> = [];

    path.push(2);

    var node = new Node(3);
    node.add(new Node(2));
    node.add(new Node(3));
    node.add(new Node(1));

    nodes.push(node);

    node = new Node(4);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    // act
    sorting.sortNodes(nodes);

    // assert
    expect(nodes.map(n => n.value)).toStrictEqual([4, 3]);
});

test('WithPath_WithCollision_onAllItems', () => {
    // arrange
    var path = new Path(9);
    var sorting = new CustomNodesSorting(path, 9);
    var nodes: Array<Node> = [];

    path.push(2);
    path.push(8);
    path.push(9);

    var node = new Node(3);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    node = new Node(4);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    node = new Node(7);
    node.add(new Node(8));
    node.add(new Node(9));
    node.add(new Node(5));

    nodes.push(node);

    // act
    sorting.sortNodes(nodes);

    // assert
    expect(nodes.map(n => n.value)).toStrictEqual([4, 3, 7]);
});

test('WithPath_WithCollision_OnTwoItems', () => {
    // arrange
    var path = new Path(9);
    var sorting = new CustomNodesSorting(path, 9);
    var nodes: Array<Node> = [];

    path.push(2);
    path.push(8);
    path.push(9);

    var node = new Node(3);
    node.add(new Node(2));
    node.add(new Node(3));
    node.add(new Node(4));

    nodes.push(node);

    node = new Node(4);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    node = new Node(7);
    node.add(new Node(8));
    node.add(new Node(9));
    node.add(new Node(5));

    nodes.push(node);

    // act
    sorting.sortNodes(nodes);

    // assert
    expect(nodes.map(n => n.value)).toStrictEqual([4, 7, 3]);
});

test('WithoutPath_WitCollision', () => {
    // arrange
    var sorting = new CustomNodesSorting(null, 4);
    var nodes: Array<Node> = [];

    var node = new Node(3); // 3 children
    node.add(new Node(2));
    node.add(new Node(3));
    node.add(new Node(1));

    nodes.push(node);

    node = new Node(4);  // 2 children
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    node = new Node(1);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    node = new Node(2);
    node.add(new Node(3));

    nodes.push(node);

    // act
    sorting.sortNodes(nodes);

    // assert
    expect(nodes.map(n => n.value)).toStrictEqual([2, 4, 1, 3]);
});

test('WithoutPath_WitCollision_SetFirst', () => {
    // arrange
    var sorting = new CustomNodesSorting(null, 4);
    var nodes: Array<Node> = [];

    var node = new Node(3); // 3 children
    node.add(new Node(2));
    node.add(new Node(3));
    node.add(new Node(1));

    nodes.push(node);

    node = new Node(1);
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    node = new Node(4);  // 2 children
    node.add(new Node(2));
    node.add(new Node(3));

    nodes.push(node);

    // act
    sorting.sortNodes(nodes);

    // assert
    expect(nodes.map(n => n.value)).toStrictEqual([4, 1, 3]);
});