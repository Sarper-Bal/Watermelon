using UnityEngine;
using System;

using Random = UnityEngine.Random;


public class FruitManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Fruit[] fruitPrefabs;

    [SerializeField] private Fruit[] spawnableFruits;
    [SerializeField] private Transform fruitsParent;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [Header("Settings")]
    [SerializeField] private float fruitYSpawnPos;
    [SerializeField] private float spawnDelay;
    private bool canControl;
    private bool isControlling;

    [Header("Next Fruit Settings")]
    private int nextFruitIndex;

    [Header("Debug")]
    [SerializeField] private bool enableGizmos;

    [Header("Actions")]
    public static Action onNextFruitIndexSet;

    private void Awake()
    {

        MergeManager.onMegeProcessed += MergeProcessedCallBack;

    }
    private void OnDestroy()
    {
        MergeManager.onMegeProcessed -= MergeProcessedCallBack;
    }


    void Start()
    {
        SetNextFruitIndex();


        canControl = true;
        HideLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.IsGameState())
            return;

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

        if (currentFruit != null)
            currentFruit.EnablePhysics();


        canControl = false;

        StartControlTimer();

        isControlling = false;

    }
    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        Fruit fruiToInstantiate = spawnableFruits[nextFruitIndex];


        currentFruit = Instantiate(fruiToInstantiate,
        spawnPosition,
        Quaternion.identity,
        fruitsParent);


        //currentFruit.name = "Fruit_" + Random.Range(0, 1000);

        SetNextFruitIndex();
    }

    private void SetNextFruitIndex()
    {
        nextFruitIndex = Random.Range(0, spawnableFruits.Length);
        onNextFruitIndexSet?.Invoke();
    }
    public string GetNextFruitName()
    {
        return spawnableFruits[nextFruitIndex].name;
    }
    public Sprite GetNextFruitSprite()
    {
        return spawnableFruits[nextFruitIndex].GetSprite();
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
        Invoke("StopControlTimer", spawnDelay);
    }
    private void StopControlTimer()
    {
        canControl = true;
    }

    private void MergeProcessedCallBack(FruitType fruitType, Vector2 spawnPosition)
    {
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            if (fruitPrefabs[i].GetFruitType() == fruitType)
            {
                SpawnMergerdFruit(fruitPrefabs[i], spawnPosition);
                break;
            }
        }
    }
    private void SpawnMergerdFruit(Fruit fruit, Vector2 spawnPosition)
    {
        Fruit fruitInstance = Instantiate(fruit, spawnPosition, Quaternion.identity, fruitsParent);
        fruitInstance.EnablePhysics();
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
