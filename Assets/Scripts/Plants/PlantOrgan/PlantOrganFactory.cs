using UnityEngine;

public class PlantOrganFactory : MonoBehaviour {
    //endTwig, uniTwig, adjacencyTwig, diagonalTwig,  triTwig, quadrTwig, root, Branch, fruit, platformTree, obstacleThorn, harvestBush, harvestVine
    public GameObject HarvestTreeRootPrefab;
    public GameObject HarvestTreeBranchPrefab;
    public GameObject HarvestTreeFruitPrefab;
    public GameObject HarvestTreeUpTwigPrefab;//default direction is up-left
    public GameObject HarvestTreeRightTwigPrefab;//default direction is up-right


    public static PlantOrganFactory Instance { get; private set; } = null;

    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlantOrganFactory already exists.");
            return;
        }
        Instance = this;
    }

}
