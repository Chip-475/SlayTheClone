using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NodeConnecter : MonoBehaviour
{
    public Dictionary<int, List<Node>> Map => MapManager.instance.map;
    public List<LineRenderer> lines;

    #region Methods
    public void AssignConnections()
    {
        foreach (var node in Map[1])
        {
            MapManager.instance.startingNode.forwardConnections.Add(node.id);
        }
        foreach (var node in Map[MapManager.instance.bossLayer - 1])
        {
            node.forwardConnections.Add(MapManager.instance.bossNode.id);
            MapManager.instance.bossNode.connected = true;
        }

        foreach (var layer in Map.Keys)
        {
            if (!Map.ContainsKey(layer + 1) || layer == 0) continue;

            foreach (var node in Map[layer])
            {
                int nConnections = Random.Range(1, 4);

                var nextLayerNodes = Map[layer + 1]
                    .OrderBy(n => (n.transform.position - node.transform.position).sqrMagnitude)
                    .Where(n => (Vector2.Distance(n.transform.position, node.transform.position) < 4))
                    .ToList();

                int count = Mathf.Min(nConnections, nextLayerNodes.Count);

                for (int i = 0; i < count; i++)
                {
                    node.forwardConnections.Add(nextLayerNodes[i].id);
                    nextLayerNodes[i].connected = true;
                }
            }
        }

        foreach (var layer in Map.Keys)
        {
            if (!Map.ContainsKey(layer + 1) || layer == 0) continue;

            foreach (var node in Map[layer])
            {
                if (node.connected) continue;

                float distance = 100;
                Node toConnect = null;
                foreach (var previousLayerNode in Map[layer - 1])
                {
                    if (Vector2.Distance(previousLayerNode.transform.position, node.transform.position) >= distance) continue;

                    distance = Vector2.Distance(previousLayerNode.transform.position, node.transform.position);
                    toConnect = previousLayerNode;
                }

                toConnect.forwardConnections.Add(node.id);
            }
        }

        foreach (var layer in Map.Keys)
        {
            if (!Map.ContainsKey(layer + 1) || layer == 0) continue;

            foreach (var node in Map[layer])
            {
                // Only repair nodes that were not given a forward connection
                // by the distance-limited pass above.
                if (node.forwardConnections.Count > 0) continue;

                float distance = 100;
                Node toConnect = null;
                foreach (var nextLayerNode in Map[layer + 1])
                {
                    if (Vector2.Distance(nextLayerNode.transform.position, node.transform.position) >= distance) continue;

                    distance = Vector2.Distance(nextLayerNode.transform.position, node.transform.position);
                    toConnect = nextLayerNode;
                }

                node.forwardConnections.Add(toConnect.id);
            }
        }
    }
    public void DrawConnections()
    {
        foreach(var line in lines)
        {
            Destroy(line);
            Destroy(line.gameObject);
        }

        foreach(var layer in Map.Values)
        {
            foreach( var node in layer)
            {
                for (int i = 0; i < node.forwardConnections.Count; i++)
                {
                    try
                    {
                        GameObject obj = new();
                        var lr = obj.AddComponent<LineRenderer>();

                        lr.positionCount = 2;
                        lr.SetPosition(0, node.transform.position);
                        lr.SetPosition(1, MapManager.GetNodeById(node.forwardConnections[i]).transform.position);

                        lr.startWidth = 0.1f;
                        lr.endWidth = 0.1f;
                        lr.material = new Material(Shader.Find("Sprites/Default"));
                        lr.startColor = Color.white;
                        lr.endColor = Color.white;
                        lr.sortingOrder = -1;

                        lines.Add(lr);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
    #endregion
}
