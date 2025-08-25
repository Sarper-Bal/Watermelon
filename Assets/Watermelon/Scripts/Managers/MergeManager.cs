using System.Collections;
using UnityEngine;
using System;

public class MergeManager : MonoBehaviour
{
    [Header("Elements")]
    public static Action<FruitType, Vector2> onMegeProcessed;

    [Header("Settings")]
    Fruit lastSender;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Fruit.onCollsoionWithFruit += CollisionBetweenFruitsCallBack;
    }
    private void OnDestroy()
    {
        Fruit.onCollsoionWithFruit -= CollisionBetweenFruitsCallBack;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /*private void OnDestroy()
    {
        Fruit.onCollsoionWithFruit -= CollisionBetweenFruitsCallBack;
    }*/

    private void CollisionBetweenFruitsCallBack(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
            return;

        lastSender = sender;

        ProcessMerge(sender, otherFruit);

        Debug.Log("Fruits collided!" + sender.name);

    }
    private void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergedFruitType = sender.GetFruitType();
        mergedFruitType += 1;

        Vector2 fruitSpawnPos = (sender.transform.position + otherFruit.transform.position) / 2;


        Destroy(sender.gameObject);
        Destroy(otherFruit.gameObject);

        StartCoroutine(ResetLastSenderCoroutine());

        onMegeProcessed?.Invoke(mergedFruitType, fruitSpawnPos);
    }

    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();
        //    lastSender = null;

    }
}