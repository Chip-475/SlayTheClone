using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

#pragma warning disable
public class NodeGenerator : MonoBehaviour
{
    #region Declarations
    public MainDatabase Database => MainDatabase.instance;

    public Node startingNode => MapManager.instance.startingNode;
    public Node bossNode => MapManager.instance.bossNode;
    public Node nodePrefab => MapManager.instance.nodePrefab;

    public float distanceBetweenLayers;
    public float distanceBetweenNodes;

    public Dictionary<int, List<Node>> layers = new();
    #endregion

    #region Methods
    public void InitLayerKeys()
    {
        layers.Add(0, null);
        layers.Add(Database.bossLayer, null);

        for (int i = 1; i < Database.bossLayer; i++)
        {
            layers.Add(i, null);
        }
    }
    public void SpawnNodes()
    {
        startingNode.id = 0;

        layers[0] = new List<Node>() { startingNode };
        layers[Database.bossLayer] = new List<Node>() { bossNode };

        int autoIncrementID = 1;
        for (int i = 1; i < layers.Count - 1; i++)
        {
            layers[i] = new();

            int numberOfNodes = Random.Range(Database.minNodesPerLayer, Database.maxNodesPerLayer + 1);
            for (int j = 0; j < numberOfNodes; j++)
            {
                autoIncrementID++;
                var node = Instantiate
                    (
                        nodePrefab,
                        new Vector3(startingNode.transform.position.x + (distanceBetweenLayers * i), startingNode.transform.position.y, 0),
                        Quaternion.identity
                    );
                node.id = autoIncrementID;
                layers[i].Add(node);
            }
        }
        bossNode.id = autoIncrementID++;
    }
    public void PositionNodes()
    {
        foreach (var layer in layers)
        {
            for (int i = 0; i < layer.Value.Count; i++)
            {
                // Positions nodes one under the other.
                layer.Value[i].transform.position = new Vector3
                    (
                        layer.Value[i].transform.position.x,
                        layer.Value[i].transform.position.y - (distanceBetweenNodes * i),
                        0
                    );
            }

            // Aligns column's Y position to startingNode's Y position.
            var columnHeight = Vector2.Distance
                (
                    layer.Value[0].transform.position,
                    layer.Value[layer.Value.Count - 1].transform.position
                 );
            for (int i = 0; i < layer.Value.Count; i++)
            {
                layer.Value[i].transform.position = new Vector3
                    (
                        layer.Value[i].transform.position.x,
                        layer.Value[i].transform.position.y + columnHeight / 2,
                        0
                    );
            }

            for (int i = 0; i < layer.Value.Count; i++)
            {
                // Adds offset
                float xOffset = Random.Range(-Database.nodeOffsetX, Database.nodeOffsetX);
                float yOffset = Random.Range(-Database.nodeOffsetY, Database.nodeOffsetY);
                layer.Value[i].transform.position += new Vector3(xOffset, yOffset, 0);
            }
        }
    }
    #endregion
}
