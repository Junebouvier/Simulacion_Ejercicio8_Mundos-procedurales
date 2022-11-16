using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private const int IndexOfSquareChild = 0;
    private const int IndexOfCircleChild = 1;
    
    [SerializeField] private GameObject branchPrefab;
    [SerializeField] private int totalLevels = 3;
    [SerializeField] private float initialSize = 5f;
    
    [SerializeField, Range(0f,1f)] private float reductionPerLevel = 0.1f;

    private Queue<GameObject> branchQueue = new Queue<GameObject>();

    private int currentLevel = 1;
    void Start()
    {
        GameObject rootBranch = Instantiate(branchPrefab, transform);
        ChangeBranchSize(rootBranch,initialSize);
        branchQueue.Enqueue(rootBranch);
        GenerateTree();
    }
    void Update()
    {
        
    }

    private void GenerateTree()
    {
        if (currentLevel >= totalLevels)
        {
            return;
        }
        
        Debug.Log(currentLevel);
        ++currentLevel;

        float newSize = Mathf.Max(initialSize - (initialSize * reductionPerLevel * (currentLevel - 1)), 0.1f);
        var branchesCreatedThisCycle = new List<GameObject>();

        while (branchQueue.Count > 0)
        {
            var rootBranch = branchQueue.Dequeue();
            
            //Generate branches
            var leftBranch = CreateBranch(rootBranch,  Random.Range(5f,30f));
            var rightBranch = CreateBranch(rootBranch, -Random.Range(5f,30));
            
            ChangeBranchSize(leftBranch, newSize);
            ChangeBranchSize(rightBranch, newSize);
            
            branchesCreatedThisCycle.Add(leftBranch);
            branchesCreatedThisCycle.Add(rightBranch);
        }

        foreach (var newBranches in branchesCreatedThisCycle)
        {
            branchQueue.Enqueue(newBranches);
        }
            
        GenerateTree();
        
    }

    private GameObject CreateBranch(GameObject prevBranch, float relativeAngle)
    {
        GameObject newBranch = Instantiate(branchPrefab, transform);
        newBranch.transform.localPosition = prevBranch.transform.localPosition + prevBranch.transform.up * GetBranchLenght(prevBranch);
        newBranch.transform.localRotation = prevBranch.transform.localRotation * Quaternion.Euler(0,0,relativeAngle);
        return newBranch;
    }

    private void ChangeBranchSize(GameObject branchInstance, float newSize)
    {
        var square= branchInstance.transform.GetChild(IndexOfSquareChild);
        var circle = branchInstance.transform.GetChild(IndexOfCircleChild);
        
        //Update square's scale
        var newScale = square.transform.localScale;
        newScale.y = newSize;
        square.transform.localScale = newScale;
        
        //Update square's position
        var newPosition = square.transform.localPosition;
        newPosition.y = newSize / 2f;
        square.transform.localPosition = newPosition;
        
        //Update circle's position
        var newCirclePosition = circle.transform.localPosition;
        newCirclePosition.y = newSize;
        circle.transform.localPosition = newCirclePosition;
    }

    private float GetBranchLenght(GameObject branchInstance)
    {
        return branchInstance.transform.GetChild(IndexOfSquareChild).localScale.y;
    }
}
