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

                GraphNode node0 = new GraphNode(7);
                GraphNode node1 = new GraphNode(0);
                GraphNode node2 = new GraphNode(2);

                node.Connections.Add(node0);
				node.Connections.Add(node1);
				node.Connections.Add(node2);

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