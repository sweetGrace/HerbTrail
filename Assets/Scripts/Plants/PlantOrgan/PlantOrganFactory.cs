using UnityEngine;

public class PlantOrganFactory : MonoBehaviour {
    //endTwig, uniTwig, adjacencyTwig, diagonalTwig,  triTwig, quadrTwig, root, Branch, fruit, platformTree, obstacleThorn, harvestBush, harvestVine
    public GameObject platformTreeRootPrefab;
    public GameObject platformTreeBranchPrefab;
    public GameObject platformTreeFruitPrefab;
    public GameObject platformTreeUpTwigPrefab;//default direction is up-left
    //public GameObject platformTreeRightTwigPrefab;//default direction is up-right

    public GameObject obstacleThornRootPrefab;
    public GameObject obstacleThornBranchPrefab;
    public GameObject obstacleThornFruitPrefab;
    public GameObject obstacleThornUpTwigPrefab;

    public GameObject harvestBushRootPrefab;
    public GameObject harvestBushBranchPrefab;
    public GameObject harvestBushFruitPrefab;
    public GameObject harvestBushUpTwigPrefab;
    
    public GameObject harvestVineRootPrefab;
    public GameObject harvestVineBranchPrefab;
    public GameObject harvestVineFruitPrefab;
    public GameObject harvestVineUpTwigPrefab;
    public static PlantOrganFactory Instance { get; private set; } = null;
    public PlantOrgan GeneratePlantOrgan(PlantOrganType organType, PlantType type, Vector3 position, Quaternion rotation) {
        GameObject PlantOrganPrefab = null;
        switch (organType) {
            case PlantOrganType.root:
                switch(type){
                    case PlantType.platformTree:
                        PlantOrganPrefab = platformTreeRootPrefab;
                        break;
                    case PlantType.obstacleThorn:
                        PlantOrganPrefab = obstacleThornRootPrefab;
                        break;
                    case PlantType.harvestBush:
                        PlantOrganPrefab = harvestBushRootPrefab;
                        break;
                    case PlantType.harvestVine:
                        PlantOrganPrefab = harvestVineRootPrefab;
                        break;
                }
                break;
            case PlantOrganType.branch:
                switch(type){
                    case PlantType.platformTree:
                        PlantOrganPrefab = platformTreeBranchPrefab;
                        break;
                    case PlantType.obstacleThorn:
                        PlantOrganPrefab = obstacleThornBranchPrefab;
                        break;
                    case PlantType.harvestBush:
                        PlantOrganPrefab = harvestBushBranchPrefab;
                        break;
                    case PlantType.harvestVine:
                        PlantOrganPrefab = harvestVineBranchPrefab;
                        break;
                }
                break;
            case PlantOrganType.fruit:
                switch(type){
                    case PlantType.platformTree:
                        PlantOrganPrefab = platformTreeFruitPrefab;
                        break;
                    case PlantType.obstacleThorn:
                        PlantOrganPrefab = obstacleThornFruitPrefab;
                        break;
                    case PlantType.harvestBush:
                        PlantOrganPrefab = harvestBushFruitPrefab;
                        break;
                    case PlantType.harvestVine:
                        PlantOrganPrefab = harvestVineFruitPrefab;
                        break;
                }
                break;
        }
        GameObject PlantOrgan = Instantiate(PlantOrganPrefab, position, rotation);
        PlantOrgan tmp = PlantOrgan.GetComponent<PlantOrgan>();
        return PlantOrgan.GetComponent<PlantOrgan>();
    }
    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlantOrganFactory already exists.");
            return;
        }
        Instance = this;
    }

}
