using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Test
{
    [TestClass]
    public class GraphUnitTest
    {
        //      0
        //      |
        //      1
        //      |
        //      2
        //      |
        //      3
        //     / \
        //    4   5
        //        |
        //        6
        //       / \
        //      7   8

        private static Graph<GraphNode> NewGraph
        {
            get
            {
                Graph<GraphNode> graph = new Graph<GraphNode>();

                GraphNode node0 = new GraphNode();
                GraphNode node1 = new GraphNode();
                GraphNode node2 = new GraphNode();
                GraphNode node3 = new GraphNode();
                GraphNode node4 = new GraphNode();
                GraphNode node5 = new GraphNode();
                GraphNode node6 = new GraphNode();
                GraphNode node7 = new GraphNode();
                GraphNode node8 = new GraphNode();

                node0.Connect(node1);
                node1.Connect(node2);
                node2.Connect(node3);
                node3.Connect(node8);
                node3.Connect(node4);
                node4.Connect(node5);
                node5.Connect(node6);
                node5.Connect(node7);

                graph.AddNode(node0);

                return graph;
            }
        }

        #region correct

        [TestMethod]
        public void CreateTestGraphMustNotThrowArgExc()
        {
            Graph<GraphNode> graph = NewGraph.CloneDirectly();
            Assert.IsFalse(graph == null);
        }

        #endregion correct

        #region cycles

        [TestMethod]
        public void OriginalGraphMustNotContainsCycle()
        {
            Graph<GraphNode> graph = NewGraph;

            Assert.AreEqual(false, graph.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustContainsCycleVer1()
        {
            Graph<GraphNode> graph = NewGraph;

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    4   5
            //        |
            //        6
            //       / \
            //      7---8
            //        ^
            //       new

            Assert.AreEqual(false, graph.IsCycle());

            graph.State = State.CanBeCycle;

            graph.Connect(graph.Nodes[7], graph.Nodes[8]);

            Assert.AreEqual(true, graph.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustContainsCycleVer2()
        {
            Graph<GraphNode> graph = NewGraph;

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    4   5
            //    |   |
            //    |   6
            //new \  / \
            //      7   8

            Assert.AreEqual(false, graph.IsCycle());

            graph.State = State.CanBeCycle;

            graph.Connect(graph.Nodes[4], graph.Nodes[7]);

            Assert.AreEqual(true, graph.IsCycle());
        }

        #endregion cycles

        #region loops

        [TestMethod]
        public void OriginalGraphMustNotContainsLoops()
        {
            Graph<GraphNode> graph = NewGraph;

            Assert.AreEqual(false, graph.IsLooped());
        }

        [TestMethod]
        public void CycleAndLoopIsDifferent()
        {
            Graph<GraphNode> graph = NewGraph;

            Assert.AreEqual(false, graph.IsCycle());
            Assert.AreEqual(false, graph.IsLooped());

            graph.State = State.CanBeLooped;

            graph.Connect(graph.Nodes[6], graph.Nodes[6]);

            Assert.AreEqual(false, graph.IsCycle());
            Assert.AreEqual(true, graph.IsLooped());
        }

        [TestMethod]
        //      0
        //      |
        //      1
        //      |
        //      2
        //      |
        //      3
        //     / \
        //    4   5
        //        |
        //        6
        //       / \
        //      7   8
        public void ModifyedOriginalGraphMustContainsLoop()
        {
            Graph<GraphNode> graph = NewGraph;

            Assert.AreEqual(false, graph.IsLooped());

            graph.State = State.CanBeLooped;

            graph.Connect(graph.Nodes[6], graph.Nodes[6]);

            Assert.AreEqual(true, graph.IsLooped());
        }

        #endregion loops

        #region connectivity

        [TestMethod]
        public void OriginalGraphMustBeConnectivity()
        {
            Graph<GraphNode> graph = NewGraph;

            Assert.AreEqual(false, graph.IsNonConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustBeConnectivity()
        {
            Graph<GraphNode> graph = NewGraph;

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    4   5
            //        |
            //        6
            //       / \
            //      7   8
            //          |
            //          9 < new

            Assert.AreEqual(false, graph.IsNonConnectivity());

            graph.State = State.CanBeNonConnectivly;

            graph.AddNode();
            graph.Connect(graph.Nodes[7], graph.Nodes[9]);

            Assert.AreEqual(false, graph.IsNonConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustNotBeConnectivity()
        {
            Graph<GraphNode> graph = NewGraph;

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    4   5
            //        |
            //        6
            //       / \
            //      7   8
            //
            //          9 < new

            Assert.AreEqual(false, graph.IsNonConnectivity());

            graph.State = State.CanBeNonConnectivly;

            graph.AddNode();

            Assert.AreEqual(true, graph.IsNonConnectivity());
        }

        #endregion connectivity

        #region routeBetween

        [TestMethod]
        public void OriginalGraphMustHaveRouteBetweenNodes()
        {
            Graph<GraphNode> graph = NewGraph;

            Assert.AreEqual(true, graph.IsRouteBetween(graph.Nodes[2], graph.Nodes[5]));
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustHaveRouteBetweenNodes()
        {
            Graph<GraphNode> graph = NewGraph;

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    4   5
            //        |
            //        6
            //       / \
            //      7   8
            //          | < new
            //          9 < new

            graph.State = State.CanBeNonConnectivly;

            graph.AddNode();
            graph.Connect(graph.Nodes[7], graph.Nodes[9]);

            Assert.AreEqual(true, graph.IsRouteBetween(graph.Nodes[9], graph.Nodes[6]));
        }

        [TestMethod]
        public void NonConnectivityGraphMustNotHaveRouteBetweenNonConnectiviedNodes()
        {
            Graph<GraphNode> graph = NewGraph;

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    4   5
            //        |
            //        6
            //       / \
            //      7   8
            //
            //          9 < new

            graph.State = State.CanBeNonConnectivly;

            graph.AddNode();

            Assert.AreEqual(false, graph.IsRouteBetween(graph.Nodes[9], graph.Nodes[6]));
        }

        #endregion routeBetween
    }
}