using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public struct PlantSlot {
    public GameObject prefab;
    public uint cost;
}

[RequireComponent(typeof(BoardController))]
class PlantationController: MonoBehaviour
{
    public PlantSlot[] plants;
    public GameObject iconPrefab;
    
    private List<ToggleBehaviour> toggles = new List<ToggleBehaviour>();
    private int mCurrentPlantIdx;
    private uint mAvailableMoney;
    private BoardController mBoardController;

    private PlantSlot CurrentPlant => plants[mCurrentPlantIdx > 0 && mCurrentPlantIdx < plants.Length ? mCurrentPlantIdx : 0];

    private void Awake()
    {
        mBoardController = GetComponent<BoardController>();
        mCurrentPlantIdx = 0;
    }

    private void Start()
    {
        var canvas = FindObjectOfType<Canvas>();
        var canvasTransform = canvas.transform;
        for (var i = 0; i < plants.Length; i++)
        {
            var icon = Instantiate(iconPrefab, canvasTransform);
            var iconTransform = icon.GetComponent<RectTransform>();
            iconTransform.anchoredPosition = new Vector2(iconTransform.rect.width * i, 0);
            var toggleComponent = icon.GetComponent<ToggleBehaviour>();
            var idx = toggles.Count;
            toggles.Add(toggleComponent);
            toggleComponent.OnClicked += () => Selected(idx);
        }
        Selected(mCurrentPlantIdx);
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
            var plant = CurrentPlant;
            Vector2Int cell;
            if (mouseClick) {
                mBoardController.Project(Input.mousePosition, out cell);
            } else {
                mBoardController.Project(Input.mousePosition, out cell);
            }
            if (mBoardController.TryToPlant(plant.prefab, cell)) {
                mAvailableMoney -= plant.cost;
            }
        }
    }
}
