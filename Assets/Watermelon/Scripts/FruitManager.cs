using UnityEngine;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;
    [Header("Settings")]
    [SerializeField] private float fruitYSpawnPos;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ManagePlayerInput();
        }
    }

    private void ManagePlayerInput()
    {
        Vector2 spawnPosition = GetClickPosition();
        spawnPosition.y = fruitYSpawnPos;

        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }
    private Vector2 GetClickPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-50, fruitYSpawnPos, 0), new Vector3(50, fruitYSpawnPos, 0));

    }
#endif
}
