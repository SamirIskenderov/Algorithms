using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        //    8   4
        //        |
        //        5
        //       / \
        //      6   7

        private Graph NewGraph
        {
            get
            {
                Graph graph = new Graph
                {
                    EnsureValidity = false
                };

                graph.AddNode(9);

                graph.ConnectNodes(graph.Nodes[0], graph.Nodes[1]);
                graph.ConnectNodes(graph.Nodes[1], graph.Nodes[2]);
                graph.ConnectNodes(graph.Nodes[2], graph.Nodes[3]);
                graph.ConnectNodes(graph.Nodes[3], graph.Nodes[4]);
                graph.ConnectNodes(graph.Nodes[4], graph.Nodes[5]);
                graph.ConnectNodes(graph.Nodes[5], graph.Nodes[7]);
                graph.ConnectNodes(graph.Nodes[5], graph.Nodes[6]);
                graph.ConnectNodes(graph.Nodes[3], graph.Nodes[8]);

                graph.EnsureValidity = true;

                if (!graph.CheckValidity())
                {
                    throw new ArgumentException("Graph is not valid");
                }

                return graph;
            }
        }

        #region operators

        #region hash

        [TestMethod]
        public void GetHashCodeForSameGraphMustReturnSameValue()
        {
            Assert.AreEqual(true, this.NewGraph.GetHashCode() == this.NewGraph.GetHashCode());
        }

        [TestMethod]
        public void GetHashCodeForCopyOfGraphMustReturnSameValue()
        {
            Graph rhs = new Graph
            {
                EnsureValidity = false
            };

            rhs.AddNode(9);

            rhs.ConnectNodes(rhs.Nodes[0], rhs.Nodes[1]);
            rhs.ConnectNodes(rhs.Nodes[1], rhs.Nodes[2]);
            rhs.ConnectNodes(rhs.Nodes[2], rhs.Nodes[3]);
            rhs.ConnectNodes(rhs.Nodes[3], rhs.Nodes[4]);
            rhs.ConnectNodes(rhs.Nodes[4], rhs.Nodes[5]);
            rhs.ConnectNodes(rhs.Nodes[5], rhs.Nodes[7]);
            rhs.ConnectNodes(rhs.Nodes[5], rhs.Nodes[6]);
            rhs.ConnectNodes(rhs.Nodes[3], rhs.Nodes[8]);

            rhs.EnsureValidity = true;

            Assert.AreEqual(true, this.NewGraph.GetHashCode() == rhs.GetHashCode());
        }

        [TestMethod]
        public void GetHashCodeForAlmostSameMustReturnNotSameValueVer1()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.AddNode();

            Assert.AreEqual(false, lhs.GetHashCode() == rhs.GetHashCode());
        }

        [TestMethod]
        public void GetHashCodeForAlmostSameMustReturnNotSameValueVer2()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.ConnectNodes(1, 5);

            Assert.AreEqual(false, lhs.GetHashCode() == rhs.GetHashCode());
        }

        #endregion hash

        #region equals

        [TestMethod]
        public void GraphMustBeEqualToItsetf()
        {
            Graph graph = this.NewGraph;

            Assert.AreEqual(true, graph.Equals(graph));
        }

        [TestMethod]
        public void GraphsMustBeCommutativeEquals()
        {
            Graph rhs = new Graph
            {
                EnsureValidity = false
            };

            rhs.AddNode(9);

            rhs.ConnectNodes(rhs.Nodes[0], rhs.Nodes[1]);
            rhs.ConnectNodes(rhs.Nodes[1], rhs.Nodes[2]);
            rhs.ConnectNodes(rhs.Nodes[2], rhs.Nodes[3]);
            rhs.ConnectNodes(rhs.Nodes[3], rhs.Nodes[4]);
            rhs.ConnectNodes(rhs.Nodes[4], rhs.Nodes[5]);
            rhs.ConnectNodes(rhs.Nodes[5], rhs.Nodes[7]);
            rhs.ConnectNodes(rhs.Nodes[5], rhs.Nodes[6]);
            rhs.ConnectNodes(rhs.Nodes[3], rhs.Nodes[8]);

            rhs.EnsureValidity = true;

            Graph lhs = new Graph
            {
                EnsureValidity = false
            };

            lhs.AddNode(9);

            lhs.ConnectNodes(lhs.Nodes[0], lhs.Nodes[1]);
            lhs.ConnectNodes(lhs.Nodes[1], lhs.Nodes[2]);
            lhs.ConnectNodes(lhs.Nodes[2], lhs.Nodes[3]);
            lhs.ConnectNodes(lhs.Nodes[3], lhs.Nodes[4]);
            lhs.ConnectNodes(lhs.Nodes[4], lhs.Nodes[5]);
            lhs.ConnectNodes(lhs.Nodes[5], lhs.Nodes[7]);
            lhs.ConnectNodes(lhs.Nodes[5], lhs.Nodes[6]);
            lhs.ConnectNodes(lhs.Nodes[3], lhs.Nodes[8]);

            lhs.EnsureValidity = true;

            Assert.AreEqual(lhs.Equals(rhs), rhs.Equals(lhs));
        }

        [TestMethod]
        public void GraphsMustBeAssociativeEquals()
        {
            Graph rhs = new Graph
            {
                EnsureValidity = false
            };

            rhs.AddNode(9);

            rhs.ConnectNodes(rhs.Nodes[0], rhs.Nodes[1]);
            rhs.ConnectNodes(rhs.Nodes[1], rhs.Nodes[2]);
            rhs.ConnectNodes(rhs.Nodes[2], rhs.Nodes[3]);
            rhs.ConnectNodes(rhs.Nodes[3], rhs.Nodes[4]);
            rhs.ConnectNodes(rhs.Nodes[4], rhs.Nodes[5]);
            rhs.ConnectNodes(rhs.Nodes[5], rhs.Nodes[7]);
            rhs.ConnectNodes(rhs.Nodes[5], rhs.Nodes[6]);
            rhs.ConnectNodes(rhs.Nodes[3], rhs.Nodes[8]);

            rhs.EnsureValidity = true;

            Graph mhs = new Graph
            {
                EnsureValidity = false
            };

            mhs.AddNode(9);

            mhs.ConnectNodes(mhs.Nodes[0], mhs.Nodes[1]);
            mhs.ConnectNodes(mhs.Nodes[1], mhs.Nodes[2]);
            mhs.ConnectNodes(mhs.Nodes[2], mhs.Nodes[3]);
            mhs.ConnectNodes(mhs.Nodes[3], mhs.Nodes[4]);
            mhs.ConnectNodes(mhs.Nodes[4], mhs.Nodes[5]);
            mhs.ConnectNodes(mhs.Nodes[5], mhs.Nodes[7]);
            mhs.ConnectNodes(mhs.Nodes[5], mhs.Nodes[6]);
            mhs.ConnectNodes(mhs.Nodes[3], mhs.Nodes[8]);

            mhs.EnsureValidity = true;

            Graph lhs = new Graph
            {
                EnsureValidity = false
            };

            lhs.AddNode(9);

            lhs.ConnectNodes(lhs.Nodes[0], lhs.Nodes[1]);
            lhs.ConnectNodes(lhs.Nodes[1], lhs.Nodes[2]);
            lhs.ConnectNodes(lhs.Nodes[2], lhs.Nodes[3]);
            lhs.ConnectNodes(lhs.Nodes[3], lhs.Nodes[4]);
            lhs.ConnectNodes(lhs.Nodes[4], lhs.Nodes[5]);
            lhs.ConnectNodes(lhs.Nodes[5], lhs.Nodes[7]);
            lhs.ConnectNodes(lhs.Nodes[5], lhs.Nodes[6]);
            lhs.ConnectNodes(lhs.Nodes[3], lhs.Nodes[8]);

            lhs.EnsureValidity = true;

            Assert.AreEqual(true, rhs.Equals(mhs) && (mhs.Equals(lhs) && rhs.Equals(lhs)));
        }

        [TestMethod]
        public void GraphMustNotBeEqualToNull()
        {
            Graph graph = this.NewGraph;

            Assert.AreEqual(false, graph.Equals(null));
        }

        [TestMethod]
        public void GraphMustNotBeEqualAnotherDifferentGraphVer1()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.AddNode();

            Assert.AreEqual(false, rhs.Equals(lhs));
        }

        [TestMethod]
        public void GraphMustNotBeEqualAnotherDifferentGraphVer2()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.ConnectNodes(1, 5);

            Assert.AreEqual(false, rhs.Equals(lhs));
        }

        [TestMethod]
        public void GraphMustNotBeEqualAnotherDifferentGraphVer3()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.CanBeCyclic = !rhs.CanBeCyclic;

            Assert.AreEqual(false, rhs.Equals(lhs));
        }

        [TestMethod]
        public void GraphMustNotBeEqualAnotherDifferentGraphVer4()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.CanBeLooped = !rhs.CanBeLooped;

            Assert.AreEqual(false, rhs.Equals(lhs));
        }

        [TestMethod]
        public void GraphMustNotBeEqualAnotherDifferentGraphVer5()
        {
            Graph rhs = this.NewGraph;
            Graph lhs = this.NewGraph;
            lhs.CanBeNonConnectivity = !rhs.CanBeNonConnectivity;

            Assert.AreEqual(false, rhs.Equals(lhs));
        }

        #endregion equals

        #endregion operators

        #region correct

        [TestMethod]
        public void CreateTestGraphMustNotThrowArgExc()
        {
            Graph graph = this.NewGraph.Clone();
        }

        #endregion correct

        #region cycles

        [TestMethod]
        public void OriginalGraphMustNotContainsCycle()
        {
            Graph graph = this.NewGraph.Clone();

            Assert.AreEqual(false, graph.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustContainsCycleVer1()
        {
            Graph graph = this.NewGraph.Clone();

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    8   4
            //        |
            //        5
            //       / \
            //      6---7

            graph.CanBeCyclic = true;

            graph.ConnectNodes(6, 7);

            Assert.AreEqual(true, graph.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustContainsCycleVer2()
        {
            Graph graph = this.NewGraph.Clone();

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    8   4
            //    |   |
            //    |   5
            //    \  / \
            //      6   7

            graph.CanBeCyclic = true;

            graph.ConnectNodes(6, 8);

            Assert.AreEqual(true, graph.IsCycle());
        }

        #endregion cycles

        #region loops

        [TestMethod]
        public void OriginalGraphMustNotContainsLoops()
        {
            Graph graph = this.NewGraph.Clone();

            Assert.AreEqual(false, graph.IsLooped());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustContainsLoop()
        {
            Graph graph = this.NewGraph.Clone();

            graph.CanBeLooped = true;

            graph.ConnectNodes(6, 6);

            Assert.AreEqual(true, graph.IsLooped());
        }

        #endregion loops

        #region connectivity

        [TestMethod]
        public void OriginalGraphMustBeConnectivity()
        {
            Graph graph = this.NewGraph.Clone();

            Assert.AreEqual(true, graph.IsConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustBeConnectivity()
        {
            Graph graph = this.NewGraph.Clone();

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    8   4
            //        |
            //        5
            //       / \
            //      6   7
            //          |
            //          9

            graph.CanBeNonConnectivity = true;

            graph.AddNode();
            graph.ConnectNodes(7, 9);

            Assert.AreEqual(true, graph.IsConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustNotBeConnectivity()
        {
            Graph graph = this.NewGraph.Clone();

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    8   4
            //        |
            //        5
            //       / \
            //      6   7
            //
            //          9

            graph.CanBeNonConnectivity = true;

            graph.AddNode();

            Assert.AreEqual(false, graph.IsConnectivity());
        }

        #endregion connectivity

        #region routeBetween

        [TestMethod]
        public void OriginalGraphMustHaveRouteBetweenNodes()
        {
            Graph graph = this.NewGraph.Clone();

            Assert.AreEqual(true, graph.IsRouteBetween(2, 5));
        }

        [TestMethod]
        public void ModifyedOriginalGraphMustHaveRouteBetweenNodes()
        {
            Graph graph = this.NewGraph.Clone();

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    8   4
            //        |
            //        5
            //       / \
            //      6   7
            //          |
            //          9

            graph.CanBeNonConnectivity = true;

            graph.AddNode();
            graph.ConnectNodes(7, 9);

            Assert.AreEqual(true, graph.IsRouteBetween(9, 6));
        }

        [TestMethod]
        public void NonConnectivityGraphMustNotHaveRouteBetweenNonConnectiviedNodes()
        {
            Graph graph = this.NewGraph.Clone();

            //      0
            //      |
            //      1
            //      |
            //      2
            //      |
            //      3
            //     / \
            //    8   4
            //        |
            //        5
            //       / \
            //      6   7
            //
            //          9

            graph.CanBeNonConnectivity = true;

            graph.AddNode();

            Assert.AreEqual(false, graph.IsRouteBetween(9, 6));
        }

        #endregion routeBetween
    }
}