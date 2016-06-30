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