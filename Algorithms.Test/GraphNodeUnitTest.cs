using Algorithms.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms.Test
{
	[TestClass]
	public class GraphNodeUnitTest
	{
		public GraphNode NewGraphNode
		{
			get
			{
				GraphNode node = new GraphNode(13)
				{
					Color = Color.Grey
				};

				node.Connections.Add(7);
				node.Connections.Add(0);
				node.Connections.Add(2);

				return node;
			}
		}

		#region operators

		#region hash

		[TestMethod]
		public void GetHashCodeForSameGraphNodeMustReturnSameValue()
		{
			Assert.AreEqual(true, this.NewGraphNode.GetHashCode() == this.NewGraphNode.GetHashCode());
		}

		[TestMethod]
		public void GetHashCodeForCopyOfGraphNodeMustReturnSameValue()
		{
			GraphNode node = new GraphNode(13)
			{
				Color = Color.Grey
			};

			node.Connections.Add(7);
			node.Connections.Add(0);
			node.Connections.Add(2);

			Assert.AreEqual(true, this.NewGraphNode.GetHashCode() == node.GetHashCode());
		}

		[TestMethod]
		public void GetHashCodeForAlmostSameMustReturnNotSameValueVer1()
		{
			GraphNode rhs = this.NewGraphNode;
			GraphNode lhs = this.NewGraphNode;
			lhs.Connections.Add(9);

			Assert.AreEqual(false, lhs.GetHashCode() == rhs.GetHashCode());
		}

		[TestMethod]
		public void GetHashCodeForAlmostSameMustReturnNotSameValueVer2()
		{
			GraphNode rhs = this.NewGraphNode;
			GraphNode lhs = this.NewGraphNode;
			lhs.Connections.RemoveAt(0);

			Assert.AreEqual(false, lhs.GetHashCode() == rhs.GetHashCode());
		}

		#endregion hash

		#region equals

		[TestMethod]
		public void GraphNodeMustBeEqualToItsetf()
		{
			GraphNode node = this.NewGraphNode;

			Assert.AreEqual(true, node.Equals(node));
		}

		[TestMethod]
		public void GraphNodesMustBeCommutativeEquals()
		{
			GraphNode rhs = new GraphNode(13)
			{
				Color = Color.Grey
			};

			rhs.Connections.Add(7);
			rhs.Connections.Add(0);
			rhs.Connections.Add(2);

			GraphNode lhs = new GraphNode(13)
			{
				Color = Color.Grey
			};

			lhs.Connections.Add(7);
			lhs.Connections.Add(0);
			lhs.Connections.Add(2);

			Assert.AreEqual(lhs.Equals(rhs), rhs.Equals(lhs));
		}

		[TestMethod]
		public void GraphNodesMustBeAssociativeEquals()
		{
			GraphNode rhs = new GraphNode(13)
			{
				Color = Color.Grey
			};

			rhs.Connections.Add(7);
			rhs.Connections.Add(0);
			rhs.Connections.Add(2);

			GraphNode mhs = new GraphNode(13)
			{
				Color = Color.Grey
			};

			mhs.Connections.Add(7);
			mhs.Connections.Add(0);
			mhs.Connections.Add(2);

			GraphNode lhs = new GraphNode(13)
			{
				Color = Color.Grey
			};

			lhs.Connections.Add(7);
			lhs.Connections.Add(0);
			lhs.Connections.Add(2);

			Assert.AreEqual(true, rhs.Equals(mhs) && (mhs.Equals(lhs) && rhs.Equals(lhs)));
		}

		[TestMethod]
		public void GraphNodeMustNotBeEqualToNull()
		{
			GraphNode node = this.NewGraphNode;

			Assert.AreEqual(false, node.Equals(null));
		}

		#endregion equals

		#endregion operators

		#region correct

		[TestMethod]
		public void CreateTestGraphNodeMustNotThrowArgExc()
		{
			GraphNode node = this.NewGraphNode;
		}

		#endregion correct
	}
}