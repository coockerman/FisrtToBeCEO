using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : ASingleton<ObstacleFactory>
{
    private Dictionary<EObstacle, GameObject> _obstaclePrefabDictionary;
    protected override void Awake()
    {
        //base.Awake();

        _obstaclePrefabDictionary = new Dictionary<EObstacle, GameObject>();
    }
    public void RegisterObstacle(EObstacle typeObstacle, GameObject prefab)
    {
        _obstaclePrefabDictionary.TryAdd(typeObstacle, prefab);
    }

    public IObstacle CreateObstacle(DataObstacle dataObstacle)
    {
        if (_obstaclePrefabDictionary.TryGetValue(dataObstacle.TypeObstacle, out GameObject prefab))
        {
            GameObject obstacleObject = Instantiate(prefab, gameObject.transform);
            IObstacle obstacleComponent = obstacleObject.GetComponent<IObstacle>();
            if (obstacleComponent != null)
            {
                obstacleComponent.Init(dataObstacle);
                return obstacleComponent;
            }
            else
            {
                Debug.LogError("The prefab does not have an IObstacle component.");
                return null;
            }
        }
        else
        {
            Debug.LogError("Obstacle Prefab Dictionary not found for: " + dataObstacle.Name);
            return null;
        }
    }
}
