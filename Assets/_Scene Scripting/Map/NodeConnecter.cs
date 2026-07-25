using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

public class NodeConnecter : MonoBehaviour
{
    public Dictionary<int, List<Node>> Map => MapManager.instance.map;

    #region Methods
    public void AssignConnections()
    {
        foreach (var layer in Map.Keys.ToList())
        {
            if (!Map.ContainsKey(layer + 1)) continue;

            if (layer == 0)
            {
                var entryNode = Map[layer][0];
                foreach(var node in Map[layer + 1])
                {
                    entryNode.forwardConnections.Add(node.id);
                }
            }

            foreach (var node in Map[layer])
            {
                int nConnections = Random.Range(1, 4);

                var nextLayerNodes = Map[layer + 1]
                    .OrderBy(n => (n.transform.position - node.transform.position).sqrMagnitude)
                    .ToList();

                int count = Mathf.Min(nConnections, nextLayerNodes.Count);

                for (int i = 0; i < count; i++)
                {
                    node.forwardConnections.Add(nextLayerNodes[i].id);
                }
            }
        }
    }
    #endregion
}
