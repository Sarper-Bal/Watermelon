using UnityEngine;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;

    [Header("Settings")]
    [SerializeField] private float fruitYSpawnPos;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;


    void Start()
    {
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayerInput();
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCallBack();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseDragCallBack();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MousUpCallBack();
        }

    }
    private void MouseDownCallBack()
    {
        DisplayLine();

        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    private void MouseDragCallBack()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }
    private void MousUpCallBack()
    {
        HideLine();

    }
    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();


        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }
    private Vector2 GetClickPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private Vector2 GetSpawnPosition()
    {
        Vector2 clickedWorldPosition = GetClickPosition();
        clickedWorldPosition.y = fruitYSpawnPos;
        return clickedWorldPosition;
    }
    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }
    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;

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
