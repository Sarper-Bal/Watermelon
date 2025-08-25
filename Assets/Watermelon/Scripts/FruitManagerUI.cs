using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(FruitManager))] // Ensures that this script is attached to a GameObject with FruitManager component
public class FruitManagerUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image nextFruitImage; // Image to display the next fruit
    private FruitManager fruitManager;

    private void Awake()
    {
        FruitManager.onNextFruitIndexSet += UpdateNextFruitImage;
    }
    private void OnDestroy()
    {
        FruitManager.onNextFruitIndexSet -= UpdateNextFruitImage;
    }
    void Start()
    {
    }


    private void UpdateNextFruitImage()
    {
        if (fruitManager == null)
            fruitManager = GetComponent<FruitManager>();

        nextFruitImage.sprite = fruitManager.GetNextFruitSprite();

    }
}
