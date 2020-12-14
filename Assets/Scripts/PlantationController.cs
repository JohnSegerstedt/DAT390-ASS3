using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public struct PlantSlot {
    public GameObject prefab;
    public uint cost;
    public Sprite image;
}

[RequireComponent(typeof(BoardController))]
class PlantationController: MonoBehaviour
{
    public PlantSlot[] plants;
    public GameObject iconPrefab;

    private static int sSunshinesLayer;

    private List<ToggleBehaviour> toggles = new List<ToggleBehaviour>();
    private int mCurrentPlantIdx;
    private uint mAvailableMoney;
    private BoardController mBoardController;
    private TextMeshProUGUI mMoneyDisplay;

    private PlantSlot CurrentPlant => plants[mCurrentPlantIdx > 0 && mCurrentPlantIdx < plants.Length ? mCurrentPlantIdx : 0];

    private void Awake()
    {
        sSunshinesLayer = LayerMask.GetMask("Sunshines");
        if (sSunshinesLayer == 0)
            Debug.LogError("Couldn't find layer 'Sunshines'.");
        mBoardController = GetComponent<BoardController>();
        mMoneyDisplay = FindObjectOfType<Canvas>()?.transform.Find("SunshinesBanner")?.GetComponentInChildren<TextMeshProUGUI>();
        if (!mMoneyDisplay)
        {
            Debug.LogError("Could not find Canvas/SunshinesBanner/TextMeshProUGUI to display sunshines.");
        }
        mCurrentPlantIdx = 0;
        mAvailableMoney = 200;
    }

    private void Start()
    {
        var canvas = FindObjectOfType<Canvas>();
        var origin = canvas.transform.Find("PlantSelectorOrigin")?.GetComponent<RectTransform>();
        if (!origin)
        {
            Debug.LogError("No PlantSelectorOrigin object found in canvas, please add it.");
        }
        var canvasTransform = canvas.transform;
        for (var i = 0; i < plants.Length; i++)
        {
            var icon = Instantiate(iconPrefab, canvasTransform);
            var iconTransform = icon.GetComponent<RectTransform>();
            iconTransform.anchorMin = origin.anchorMin;
            iconTransform.anchorMax = origin.anchorMax;
            iconTransform.pivot = origin.pivot;
            iconTransform.anchoredPosition = new Vector2(iconTransform.rect.width * i, 0);
            var toggleComponent = icon.GetComponent<ToggleBehaviour>();
            var idx = toggles.Count;
            toggleComponent.Sprite = plants[i].image;
            toggleComponent.Cost = plants[i].cost.ToString();
            toggles.Add(toggleComponent);
            toggleComponent.OnClicked += () => Selected(idx);
        }
        Selected(mCurrentPlantIdx);
        mMoneyDisplay.text = mAvailableMoney.ToString();
    }

    private void Selected(int index)
    {
        mCurrentPlantIdx = index;
        for (var i = 0; i < toggles.Count; i++)
        {
            toggles[i].SelectedChanged(i == mCurrentPlantIdx);
        }
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        var mouseClick = Input.GetMouseButtonDown(0);
        var touchClick = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        if (mouseClick || touchClick) {
            var mousePos = Input.mousePosition;
            // Check for sunshine collection
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out var hitInfo,
                Camera.main.farClipPlane, sSunshinesLayer))
            {
                var gamePiece = hitInfo.collider.GetComponent<GamePiece>();
                gamePiece.Deactive();
                mAvailableMoney += 25;
                UpdateSunshinesDisplay();
                return;
            }

            // Plantation
            var plant = CurrentPlant;
            if (mAvailableMoney < plant.cost)
            {
                // TODO: remark the money is the problem
                return;
            }
            mBoardController.Project(mousePos, out Vector2Int cell);
            if (mBoardController.TryToPlant(plant.prefab, cell)) {
                mAvailableMoney -= plant.cost;
                UpdateSunshinesDisplay();
            }
        }
    }

    private void UpdateSunshinesDisplay()
    {
        mMoneyDisplay.text = mAvailableMoney.ToString();
    }
}
