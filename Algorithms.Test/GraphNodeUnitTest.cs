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
				GraphNode node = new GraphNode()
				{
					Color = Color.Grey
				};

                GraphNode node0 = new GraphNode();
                GraphNode node1 = new GraphNode();
                GraphNode node2 = new GraphNode();

                node.Connect(node0);
				node.Connect(node1);
				node.Connect(node2);

				return node;
			}
		}

		#region correct

		[TestMethod]
		public void CreateTestGraphNodeMustNotThrowArgExc()
		{
			GraphNode node = this.NewGraphNode;
		}

		#endregion correct
	}
}