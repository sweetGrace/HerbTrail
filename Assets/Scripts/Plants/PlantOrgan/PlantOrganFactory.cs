using UnityEngine;

public class PlantOrganFactory : MonoBehaviour {
    
    public SpriteRenderer picRenderer;
    public Sprite[] statePics;//correspondence with PlantType
    //endTwig, uniTwig, adjacencyTwig, diagonalTwig,  triTwig, quadrTwig, root, Branch, fruit, platformTree, obstacleThorn, harvestBush, harvestVine
    public GameObject platformTreeRootPrefab;//No.0
    public GameObject platformTreeBranchPrefab;
    public GameObject platformTreeFruitPrefab;
    public GameObject platformTreeTwigPrefab;//default direction is up-left
    //public GameObject platformTreeRightTwigPrefab;//default direction is up-right

    public GameObject obstacleThornRootPrefab;//No.4
    public GameObject obstacleThornBranchPrefab;
    public GameObject obstacleThornFruitPrefab;
    public GameObject obstacleThornTwigPrefab;

    public GameObject harvestBushRootPrefab;
    public GameObject harvestBushBranchPrefab;
    public GameObject harvestBushFruitPrefab;
    public GameObject harvestBushTwigPrefab;
    
    public GameObject harvestVineRootPrefab;
    public GameObject harvestVineBranchPrefab;
    public GameObject harvestVineFruitPrefab;
    public GameObject harvestVineTwigPrefab;
    public static PlantOrganFactory Instance { get; private set; } = null;
    public GameObject GeneratePlantOrgan(PlantOrganType organType, PlantType type, Vector3 position, Quaternion rotation) {
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
        /*
        GameObject PlantOrgan = Instantiate(PlantOrganPrefab, position, rotation);
        PlantOrgan tmp = PlantOrgan.GetComponent<PlantOrgan>();
        return PlantOrgan.GetComponent<PlantOrgan>();
        */
        return Instantiate(PlantOrganPrefab, position, rotation);
    }
    public GameObject GenerateTwig(PlantType type, Vector3 position, Quaternion rotation) {
        GameObject twigPrefab = null;
        switch(type){
            case PlantType.platformTree:
                twigPrefab = platformTreeTwigPrefab;
                break;
            case PlantType.obstacleThorn:
                twigPrefab = obstacleThornTwigPrefab;
                break;
            case PlantType.harvestBush:
                twigPrefab = harvestBushTwigPrefab;
                break;
            case PlantType.harvestVine:
                twigPrefab = harvestVineTwigPrefab;
                break;
        }
        
        /*
        GameObject PlantOrgan = Instantiate(PlantOrganPrefab, position, rotation);
        PlantOrgan tmp = PlantOrgan.GetComponent<PlantOrgan>();
        return PlantOrgan.GetComponent<PlantOrgan>();
        */
        return Instantiate(twigPrefab, position, rotation);
    }
    private void Start() {
        if (Instance != null) {
            Debug.LogError("PlantOrganFactory already exists.");
            return;
        }
        Instance = this;
    }

}
