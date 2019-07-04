using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanksSpawner : MonoBehaviour
{
    const int MINIMUM_PLANK_ELEMENTS = 2;

    [Tooltip("GameObject that will be a parent for generated planks.")]
    [SerializeField] GameObject plankParent;

    [Tooltip("How many planks should exists in every frame")]
    [Range(0, 20)]
    [SerializeField] int planksCount = 5;

    [Tooltip("Y offset between planks")]
    [SerializeField] float yOffset = 3f;

    [Tooltip("X position boundary - every plank will be placed at x position"
         + " which is lower than generator x position + xBound")]
    [SerializeField] float xBound = 0f;

    [SerializeField] PlankData plank;
    [SerializeField] string sortingLayerName = "Default";

    [Min(MINIMUM_PLANK_ELEMENTS)]
    [SerializeField] int minPlankElements = MINIMUM_PLANK_ELEMENTS;
    [SerializeField] int maxPlankElements;

    GameObject highestPlank;

    private void Start()
    {
        SpawnStartingPlanks();
    }

    private void SpawnStartingPlanks()
    {
        var offset = Vector3.zero;
        for (int i = 0; i < planksCount; ++i)
        {
            var plankObject = CreatePlank();
            plankObject.transform.position += offset;
            offset += Vector3.down * yOffset;

            if (!highestPlank) highestPlank = plankObject;
        }
    }

    private void Update()
    {
        if (!ShouldSpawn()) return;

        var plankObject = CreatePlank();
        SetYPosition(plankObject);
        highestPlank = plankObject;
    }

    private bool ShouldSpawn()
    {
        if (!highestPlank) return true;

        var parent = GetPlankParent();
        var heightDiff = Mathf.Abs(
            highestPlank.transform.position.y - parent.transform.position.y);
        return heightDiff >= yOffset;
    }

    private GameObject CreatePlank()
    {
        var parent = GetPlankParent();
        var plankObject
             = Instantiate(plank.emptyPlankPrefab, parent.transform, false);
        BuildPlank(plankObject);
        SetXPosition(plankObject);
        return plankObject;
    }

    private void SetXPosition(GameObject plankObject)
    {
        var plankWidth = CalculatePlankWidth(plankObject);
        var maxX = Mathf.Max(0f, xBound - plankWidth);
        var xPosition = Random.Range(0f, maxX);
        plankObject.transform.position += Vector3.right * xPosition;
    }

    private void SetYPosition(GameObject plankObject)
    {
        if (!highestPlank) return;

        var y = highestPlank.transform.position.y + yOffset;
        var position = new Vector3(plankObject.transform.position.x, y, plankObject.transform.position.z);
        plankObject.transform.position = position;
    }

    private float CalculatePlankWidth(GameObject plankObject)
    {
        var middleElemsCount
             = plankObject.transform.childCount - MINIMUM_PLANK_ELEMENTS;
        var plankWidth = plank.leftSprite.bounds.size.x
            + middleElemsCount * plank.middleSprite.bounds.size.x
            + plank.rightSprite.bounds.size.x;
        return plankWidth;
    }

    private void BuildPlank(GameObject parent)
    {
        var position = Vector2.right * 0.5f * plank.leftSprite.bounds.size.x;
        position = AddPlankElement("Left", parent, plank.leftSprite, position);
        position = AddMiddleElements(parent, position);
        AddPlankElement("Right", parent, plank.rightSprite, position);
    }

    private Vector2 AddMiddleElements(GameObject parent, Vector2 position)
    {
        var middlePartsCount
            = Random.Range(minPlankElements, maxPlankElements)
            - MINIMUM_PLANK_ELEMENTS;

        for (var i = 0; i < middlePartsCount; ++i)
        {
            var objectName = "Middle (" + i + ")";
            position = AddPlankElement(
                objectName, parent, plank.middleSprite, position);
        }
        return position;
    }

    Vector2 AddPlankElement(
        string name, GameObject parent, Sprite sprite, Vector2 position)
    {
        var plankElement = new GameObject(name);
        plankElement.transform.position = position;
        plankElement.transform.SetParent(parent.transform, false);

        var spriteRenderer = plankElement.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingLayerName = sortingLayerName;

        return position + Vector2.right * sprite.bounds.size.x;
    }

    private GameObject GetPlankParent()
    {
        return plankParent ? plankParent : gameObject;
    }
}
