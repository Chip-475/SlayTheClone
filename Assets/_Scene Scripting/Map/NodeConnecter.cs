using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class NodeConnecter : MonoBehaviour
{
    public Dictionary<int, List<Node>> Map => MapManager.instance.map;

    #region Methods
    public void AssignConnections()
    {
        foreach(var layer in Map.Keys)
        {
            if(layer == 0)
            {
                var entryNode = Map[layer][0];
                foreach(var node in Map[layer + 1])
                {
                    entryNode.forwardConnections.Add(node);
                }
            }

            foreach(var node in Map[layer])
            {
                int nConnections = Random.Range(1, 4);
                List<Node> toConnect = new();

                List<Node> nextLayerNodes = Map[layer + 1]
                    .OrderBy(n => (n.transform.position - node.transform.position).sqrMagnitude)
                    .ToList();

                for(int i = 0; i < nConnections - 1; i++)
                {
                    if(nextLayerNodes[i] != null) toConnect.Add(nextLayerNodes[i]);
                }

                node.forwardConnections.AddRange(toConnect);
            }
        }
    }
    #endregion
}
