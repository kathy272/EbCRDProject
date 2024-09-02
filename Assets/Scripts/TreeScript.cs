using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject[] treePrefabsL1;
    public GameObject[] treePrefabsL2;
    public GameObject[] treePrefabsR1;
    public GameObject[] treePrefabsR2;
    public Transform playerTransform;
    private List<GameObject> activeTreesL1 = new List<GameObject>();
    private List<GameObject> activeTreesL2 = new List<GameObject>();
    private List<GameObject> activeTreesR1 = new List<GameObject>();
    private List<GameObject> activeTreesR2 = new List<GameObject>();
    public float safeZone = 50; // Additional buffer zone before deleting trees
    public float treeOffset = 10; // Offset from the center for trees
    public float treeLength = 30; // Length of the trees
    public float numberOfTrees = 5;
    private float zSpawn = 0; // Current spawn position for trees

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            SpawnTrees();
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (playerTransform.position.z > zSpawn - (numberOfTrees * treeLength))
        {
            SpawnTrees();
            if (playerTransform.position.z > activeTreesL1[0].transform.position.z + safeZone)
            {
                DeleteTrees();
            }
        }
    }

    public void SpawnTrees()
    {
        // Spawn left trees
        float leftTreeLength = SpawnTree(treePrefabsL1, activeTreesL1, new Vector3(-treeOffset, 0, zSpawn + 3));
        float leftTreeLength2 = SpawnTree(treePrefabsL2, activeTreesL2, new Vector3(-treeOffset-3, 0, zSpawn));
        // Spawn right trees
        float rightTreeLength = SpawnTree(treePrefabsR1, activeTreesR1, new Vector3(treeOffset, 0, zSpawn +3));
        float rightTreeLength2 = SpawnTree(treePrefabsR2, activeTreesR2, new Vector3(treeOffset+3, 0, zSpawn));

        // Adjust zSpawn based on the longest tree length
        zSpawn += Mathf.Max(leftTreeLength, rightTreeLength, leftTreeLength2, rightTreeLength2);
    }

    private float SpawnTree(GameObject[] treePrefabs, List<GameObject> activeTrees, Vector3 position)
    {
        int treeIndex = Random.Range(0, treePrefabs.Length);
        GameObject tree = Instantiate(treePrefabs[treeIndex], position, transform.rotation);
        //change scale of tree
        tree.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        activeTrees.Add(tree);
        return treeLength;
    }

    private void DeleteTrees()
    {
        Destroy(activeTreesL1[0]);
        activeTreesL1.RemoveAt(0);
        Destroy(activeTreesL2[0]);
        activeTreesL2.RemoveAt(0);
        Destroy(activeTreesR1[0]);
        activeTreesR1.RemoveAt(0);
        Destroy(activeTreesR2[0]);
        activeTreesR2.RemoveAt(0);
    }
}
