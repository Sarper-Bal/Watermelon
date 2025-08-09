using UnityEngine;

public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Fruit fruitPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [Header("Settings")]
    [SerializeField] private float fruitYSpawnPos;
    private bool canControl;
    private bool isControlling;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;


    void Start()
    {
        canControl = true;
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
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
            if (isControlling)
            {
                MouseDragCallBack();
            }
            else
            {
                MouseDownCallBack();
            }

        }


        else if (Input.GetMouseButtonUp(0) && isControlling)
        {
            MousUpCallBack();
        }

    }
    private void MouseDownCallBack()
    {
        DisplayLine();

        PlaceLineClickedPosition();
        SpawnFruit();

        isControlling = true;
    }

    private void MouseDragCallBack()
    {
        PlaceLineClickedPosition();

        currentFruit.MoveTo(new Vector2(GetSpawnPosition().x, fruitYSpawnPos));

    }
    private void MousUpCallBack()
    {
        HideLine();
        currentFruit.EnablePhysics();

        canControl = false;

        StartControlTimer();

        isControlling = false;

    }
    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();


        currentFruit = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
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

    private void PlaceLineClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }


    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }
    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;

    }
    private void StartControlTimer()
    {
        Invoke("StopControlTimer", 1);
    }
    private void StopControlTimer()
    {
        canControl = true;
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
