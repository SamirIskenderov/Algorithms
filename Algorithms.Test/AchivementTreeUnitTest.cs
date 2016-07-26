using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Library;
using Algorithms.Library.Achivement;

namespace Algorithms.Test
{
    [TestClass]
    public class AchivementTreeUnitTest
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

        private static AchivementTree<Achivement> NewAchivement
        {
            get
            {
                AchivementTree<Achivement> achivement = new AchivementTree<Achivement>();

                Achivement node0 = new Achivement();
                Achivement node1 = new Achivement();
                Achivement node2 = new Achivement();
                Achivement node3 = new Achivement();
                Achivement node4 = new Achivement();
                Achivement node5 = new Achivement();
                Achivement node6 = new Achivement();
                Achivement node7 = new Achivement();
                Achivement node8 = new Achivement();

                achivement.Connect(node0, node1);
                achivement.Connect(node1, node2);
                achivement.Connect(node2, node3);
                achivement.Connect(node3, node8);
                achivement.Connect(node3, node4);
                achivement.Connect(node4, node5);
                achivement.Connect(node5, node6);
                achivement.Connect(node5, node7);

                achivement.AddNode(node0);

                return achivement;
            }
        }

        #region correct

        [TestMethod]
        public void CreateTestAchivementMustNotThrowArgExc()
        {
            AchivementTree<Achivement> achivement = NewAchivement.CloneDirectly();
            Assert.IsFalse(achivement == null);
        }

        #endregion correct

        #region cycles

        [TestMethod]
        public void OriginalAchivementMustNotContainsCycle()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

            Assert.AreEqual(false, achivement.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalAchivementMustContainsCycleVer1()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

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

            Assert.AreEqual(false, achivement.IsCycle());

            achivement.State = State.CanBeCycle;

            achivement.Connect(achivement.Nodes[7], achivement.Nodes[8]);

            Assert.AreEqual(true, achivement.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalAchivementMustContainsCycleVer2()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

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

            Assert.AreEqual(false, achivement.IsCycle());

            achivement.State = State.CanBeCycle;

            achivement.Connect(achivement.Nodes[4], achivement.Nodes[7]);

            Assert.AreEqual(true, achivement.IsCycle());
        }

        #endregion cycles

        #region loops

        [TestMethod]
        public void OriginalAchivementMustNotContainsLoops()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

            Assert.AreEqual(false, achivement.IsLooped());
        }

        [TestMethod]
        public void CycleAndLoopIsDifferent()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

            Assert.AreEqual(false, achivement.IsCycle());
            Assert.AreEqual(false, achivement.IsLooped());

            achivement.State = State.CanBeLooped;

            achivement.Connect(achivement.Nodes[6], achivement.Nodes[6]);

            Assert.AreEqual(false, achivement.IsCycle());
            Assert.AreEqual(true, achivement.IsLooped());
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
        public void ModifyedOriginalAchivementMustContainsLoop()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

            Assert.AreEqual(false, achivement.IsLooped());

            achivement.State = State.CanBeLooped;

            achivement.Connect(achivement.Nodes[6], achivement.Nodes[6]);

            Assert.AreEqual(true, achivement.IsLooped());
        }

        #endregion loops

        #region connectivity

        [TestMethod]
        public void OriginalAchivementMustBeConnectivity()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

            Assert.AreEqual(false, achivement.IsNonConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalAchivementMustBeConnectivity()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

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

            Assert.AreEqual(false, achivement.IsNonConnectivity());

            achivement.State = State.CanBeNonConnectivly;

            achivement.AddNode();
            achivement.Connect(achivement.Nodes[7], achivement.Nodes[9]);

            Assert.AreEqual(false, achivement.IsNonConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalAchivementMustNotBeConnectivity()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

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

            Assert.AreEqual(false, achivement.IsNonConnectivity());

            achivement.State = State.CanBeNonConnectivly;

            achivement.AddNode();

            Assert.AreEqual(true, achivement.IsNonConnectivity());
        }

        #endregion connectivity

        #region routeBetween

        [TestMethod]
        public void OriginalAchivementMustHaveRouteBetweenNodes()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

            Assert.AreEqual(true, achivement.IsRouteBetween(achivement.Nodes[2], achivement.Nodes[5]));
        }

        [TestMethod]
        public void ModifyedOriginalAchivementMustHaveRouteBetweenNodes()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

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

            achivement.State = State.CanBeNonConnectivly;

            achivement.AddNode();
            achivement.Connect(achivement.Nodes[7], achivement.Nodes[9]);

            Assert.AreEqual(true, achivement.IsRouteBetween(achivement.Nodes[9], achivement.Nodes[6]));
        }

        [TestMethod]
        public void NonConnectivityAchivementMustNotHaveRouteBetweenNonConnectiviedNodes()
        {
            AchivementTree<Achivement> achivement = NewAchivement;

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

            achivement.State = State.CanBeNonConnectivly;

            achivement.AddNode();

            Assert.AreEqual(false, achivement.IsRouteBetween(achivement.Nodes[9], achivement.Nodes[6]));
        }

        #endregion routeBetween
    }
}
