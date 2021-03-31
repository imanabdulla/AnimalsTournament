using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField] private Texture2D[] animalTextures;
    [SerializeField] private GameObject cubePrefab;

    GameObject cubeInstance;
    int index;

    //when player click on play button
    public void OnGameplayStart()
    {
        //Build tree of the game
        Tree<string> tree = BuildTree();

        CalculateWinner(tree.Root);

        DrawWinnerOnScreen(tree.Root.Value);
    }

    /*
     * winnerSign like "2G"
     * so, winnerSign[1] is equal to "G" and winnerSign[0] is equal to "2"
    */
    private void DrawWinnerOnScreen(string winnerSign)
    {
        switch (winnerSign[1])//animal type
        {
            case 'L':
                cubeInstance = Instantiate(cubePrefab, transform);
                cubeInstance.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", animalTextures[0]);
                break;
            case 'T':
                cubeInstance = Instantiate(cubePrefab, transform);
                cubeInstance.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", animalTextures[1]);
                break;
            case 'E':
                cubeInstance = Instantiate(cubePrefab, transform);
                cubeInstance.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", animalTextures[2]);
                break;
            case 'G':
                cubeInstance = Instantiate(cubePrefab, transform);
                cubeInstance.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", animalTextures[3]);
                break;
            case 'D':
                cubeInstance = Instantiate(cubePrefab, transform);
                cubeInstance.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", animalTextures[4]);
                break;
            default:
                break;
        }

        switch (winnerSign[0])//player number
        {
            case '1':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "1";
                break;
            case '2':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "2";
                break;
            case '3':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "3";
                break;
            case '4':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "4";
                break;
            case '5':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "5";
                break;
            case '6':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "6";
                break;
            case '7':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "7";
                break;
            case '8':
                cubeInstance.transform.GetChild(0).GetComponent<TextMesh>().text = "8";
                break;
        }
    }

    private Tree<string> BuildTree()
    {
        //define the tree
        Tree<string> tree = new Tree<string>("");

        //define tree nodes
        TreeNode<string> node1 = new TreeNode<string>("", tree.Root);
        tree.AddNode(node1);
        TreeNode<string> node2 = new TreeNode<string>("", tree.Root);
        tree.AddNode(node2);

        TreeNode<string> node3 = new TreeNode<string>("", node1);
        tree.AddNode(node3);
        TreeNode<string> node4 = new TreeNode<string>("", node1);
        tree.AddNode(node4);
        TreeNode<string> node5 = new TreeNode<string>("", node2);
        tree.AddNode(node5);
        TreeNode<string> node6 = new TreeNode<string>("", node2);
        tree.AddNode(node6);

        TreeNode<string> node7 = new TreeNode<string>("", node3);
        tree.AddNode(node7);
        TreeNode<string> node8 = new TreeNode<string>("", node3);
        tree.AddNode(node8);
        TreeNode<string> node9 = new TreeNode<string>("", node4);
        tree.AddNode(node9);
        TreeNode<string> node10 = new TreeNode<string>("", node4);
        tree.AddNode(node10);
        TreeNode<string> node11 = new TreeNode<string>("", node5);
        tree.AddNode(node11);
        TreeNode<string> node12 = new TreeNode<string>("", node5);
        tree.AddNode(node12);
        TreeNode<string> node13 = new TreeNode<string>("", node6);
        tree.AddNode(node13);
        TreeNode<string> node14 = new TreeNode<string>("", node6);
        tree.AddNode(node14);

        return tree;
    }

    private void CalculateWinner(TreeNode<string> node)
    {
        // recurse on children
        IList<TreeNode<string>> children = node.Children;
        if (children.Count > 0)
        {
            foreach (TreeNode<string> child in children)
            {
                CalculateWinner(child);
            }

            SetNodeValue(node);
        }
        else
        {
            // leaf nodes are the base case
            AssignLeafNodeValue(node);
        }
    }

    private void AssignLeafNodeValue(TreeNode<string> leafNode)
    {
        leafNode.Value = XMLManager.xMLManager.tournamantDB.informations[index].cardSign;
        index++;
    }

    private void SetNodeValue(TreeNode<string> node)
    {
        //store node children values in a queue
        Queue<string> values = new Queue<string>();
        foreach (var child in node.Children)
        {
            values.Enqueue(child.Value);
        }

        var value1 = values.Dequeue();

        var value2 = values.Dequeue();

        switch (value1[1])
        {
            case 'L': //lion

                //with elephant or giraffe
                if (value2[1] == 'E' || value2[1] == 'G')
                    node.Value = value1;//lion wins
                //with dog or tiger
                else if (value2[1] == 'D' || value2[1] == 'T')
                {
                    node.Value = value2; // opposite animal wins
                }
                //with lion
                else if (value2[1] == 'L')
                    node.Value = (value1[0] > value2[0]) ? value2 : value1;//player with lower number wins

                break;

            case 'T': //tiger

                //with lion or dog
                if (value2[1] == 'L' || value2[1] == 'D')
                    node.Value = value1;//tiger wins
                //with elephant or giraffe
                else if (value2[1] == 'E' || value2[1] == 'G')
                {
                    node.Value = value2; // opposite animal wins
                }
                //with tiger
                else if (value2[1] == 'T')
                    node.Value = (value1[0] > value2[0]) ? value2 : value1;//player with lower number wins

                break;

            case 'E'://elephant

                //with tiger or giraffe
                if (value2[1] == 'T' || value2[1] == 'G')
                    node.Value = value1;
                //with lion or dog
                else if (value2[1] == 'L' || value2[1] == 'D')
                {
                    node.Value = value2; // opposite animal wins
                }
                //with elephant
                else if (value2[1] == 'E')
                    node.Value = (value1[0] > value2[0]) ? value2 : value1;//player with lower number wins

                break;

            case 'G'://giraffe

                //with dog or tiger
                if (value2[1] == 'D' || value2[1] == 'T')
                    node.Value = value1;
                //with elephant or lion
                else if (value2[1] == 'E' || value2[1] == 'L')
                {
                    node.Value = value2; // opposite animal wins
                }
                //with giraffe
                else if (value2[1] == 'G')
                    node.Value = (value1[0] > value2[0]) ? value2 : value1;//player with lower number wins

                break;

            case 'D'://dog

                //with elephant or lion
                if (value2[1] == 'E' || value2[1] == 'L')
                    node.Value = value1;
                //with dog or giraffe
                else if (value2[1] == 'D' || value2[1] == 'G')
                {
                    node.Value = value2; // opposite animal wins
                }
                //with dog
                else if (value2[1] == 'D')
                    node.Value = (value1[0] > value2[0]) ? value2 : value1;//player with lower number wins

                break;

            default:
                break;
        }
    }
}
