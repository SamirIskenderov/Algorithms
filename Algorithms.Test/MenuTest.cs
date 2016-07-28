using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Library;
using Algorithms.Library.Menu;

namespace Algorithms.Test
{
    [TestClass]
    public class MenuTest
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

        private static MenuNode NewMenuNode
        {
            get
            {
                return new MenuNode("Some new node");
            }
        }

        private static Menu<MenuNode> NewMenu
        {
            get
            {
                Menu<MenuNode> menu = new Menu<MenuNode>();

                MenuNode node0 = new MenuNode();
                MenuNode node1 = new MenuNode();
                MenuNode node2 = new MenuNode();
                MenuNode node3 = new MenuNode();
                MenuNode node4 = new MenuNode();
                MenuNode node5 = new MenuNode();
                MenuNode node6 = new MenuNode();
                MenuNode node7 = new MenuNode();
                MenuNode node8 = new MenuNode();

                menu.Connect(node0, node1);
                menu.Connect(node1, node2);
                menu.Connect(node2, node3);
                menu.Connect(node3, node8);
                menu.Connect(node3, node4);
                menu.Connect(node4, node5);
                menu.Connect(node5, node6);
                menu.Connect(node5, node7);

                menu.AddNode(node0);

                return menu;
            }
        }

        #region correct

        [TestMethod]
        public void CreateTestMenuMustNotThrowArgExc()
        {
            Menu<MenuNode> menu = NewMenu.ShallowClone();
            Assert.IsFalse(menu == null);
        }

        #endregion correct

        #region cycles

        [TestMethod]
        public void OriginalMenuMustNotContainsCycle()
        {
            Menu<MenuNode> menu = NewMenu;

            Assert.AreEqual(false, menu.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalMenuMustContainsCycleVer1()
        {
            Menu<MenuNode> menu = NewMenu;

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

            Assert.AreEqual(false, menu.IsCycle());

            menu.State = State.CanBeCycle;

            menu.Connect(menu.Nodes[7], menu.Nodes[8]);

            Assert.AreEqual(true, menu.IsCycle());
        }

        [TestMethod]
        public void ModifyedOriginalMenuMustContainsCycleVer2()
        {
            Menu<MenuNode> menu = NewMenu;

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

            Assert.AreEqual(false, menu.IsCycle());

            menu.State = State.CanBeCycle;

            menu.Connect(menu.Nodes[4], menu.Nodes[7]);

            Assert.AreEqual(true, menu.IsCycle());
        }

        #endregion cycles

        #region loops

        [TestMethod]
        public void OriginalMenuMustNotContainsLoops()
        {
            Menu<MenuNode> menu = NewMenu;

            Assert.AreEqual(false, menu.IsLooped());
        }

        [TestMethod]
        public void CycleAndLoopIsDifferent()
        {
            Menu<MenuNode> menu = NewMenu;

            Assert.AreEqual(false, menu.IsCycle());
            Assert.AreEqual(false, menu.IsLooped());

            menu.State = State.CanBeLooped;

            menu.Connect(menu.Nodes[6], menu.Nodes[6]);

            Assert.AreEqual(false, menu.IsCycle());
            Assert.AreEqual(true, menu.IsLooped());
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
        public void ModifyedOriginalMenuMustContainsLoop()
        {
            Menu<MenuNode> menu = NewMenu;

            Assert.AreEqual(false, menu.IsLooped());

            menu.State = State.CanBeLooped;

            menu.Connect(menu.Nodes[6], menu.Nodes[6]);

            Assert.AreEqual(true, menu.IsLooped());
        }

        #endregion loops

        #region connectivity

        [TestMethod]
        public void OriginalMenuMustBeConnectivity()
        {
            Menu<MenuNode> menu = NewMenu;

            Assert.AreEqual(false, menu.IsNonConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalMenuMustBeConnectivity()
        {
            Menu<MenuNode> menu = NewMenu;

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

            Assert.AreEqual(false, menu.IsNonConnectivity());

            menu.State = State.CanBeNonConnectivly;

            menu.AddNode(NewMenuNode);
            menu.Connect(menu.Nodes[7], menu.Nodes[9]);

            Assert.AreEqual(false, menu.IsNonConnectivity());
        }

        [TestMethod]
        public void ModifyedOriginalMenuMustNotBeConnectivity()
        {
            Menu<MenuNode> menu = NewMenu;

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

            Assert.AreEqual(false, menu.IsNonConnectivity());

            menu.State = State.CanBeNonConnectivly;

            menu.AddNode(NewMenuNode);

            Assert.AreEqual(true, menu.IsNonConnectivity());
        }

        #endregion connectivity

        #region routeBetween

        [TestMethod]
        public void OriginalMenuMustHaveRouteBetweenNodes()
        {
            Menu<MenuNode> menu = NewMenu;

            Assert.AreEqual(true, menu.IsRouteBetween(menu.Nodes[2], menu.Nodes[5]));
        }

        [TestMethod]
        public void ModifyedOriginalMenuMustHaveRouteBetweenNodes()
        {
            Menu<MenuNode> menu = NewMenu;

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

            menu.State = State.CanBeNonConnectivly;

            menu.AddNode(NewMenuNode);
            menu.Connect(menu.Nodes[7], menu.Nodes[9]);

            Assert.AreEqual(true, menu.IsRouteBetween(menu.Nodes[9], menu.Nodes[6]));
        }

        [TestMethod]
        public void NonConnectivityMenuMustNotHaveRouteBetweenNonConnectiviedNodes()
        {
            Menu<MenuNode> menu = NewMenu;

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

            menu.State = State.CanBeNonConnectivly;

            menu.AddNode(NewMenuNode);

            Assert.AreEqual(false, menu.IsRouteBetween(menu.Nodes[9], menu.Nodes[6]));
        }

        #endregion routeBetween
    }
}
