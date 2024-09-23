using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private GameManager gameManager;
    private Crop curCrop;

    public bool tilled;
    public bool HasCrop;
    public Sprite tilledSprite;
    public Sprite wateredTilledSprite;
    public Sprite grassSprite;
    public GameObject cropPrefab;
    public Color tilledColor;
    public Color wateredColor;
    public Color originalColor;



    private void Start()
    {
        //gameObject.GetComponent<SpriteRenderer>().sprite = grassSprite;
        tilled = false;
        originalColor = _renderer.color;
    }
    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        //_renderer.sprite = grassSprite;
    }

    private void Update()
    {
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Interact();
        }
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    // Called when the player interacts with the tile.
    public void Interact()
    {
        if (!tilled)
        {
            Till();
        }
        else if (HasCrop == false && GameManager.instance.CanPlantCrop() == true)
        {
            PlantNewCrop(GameManager.instance.selectedCropToPlant);
        }
        else if (HasCrop && curCrop.CanHarvest())
        {
            curCrop.Harvest();
                HasCrop = false;
        }
        else
        {
            Water();
        }
    }

    // Called when we interact with a tilled tile and we have crops to plant.
    void PlantNewCrop(CropData crop)
    {
        if (!tilled)
            return;
        curCrop = Instantiate(cropPrefab, transform).GetComponent<Crop>();
        curCrop.Plant(crop);
        curCrop.transform.position = gameObject.transform.position;
        curCrop.transform.localScale = new Vector3(2f, 3f);
        curCrop.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Foreground");
        GameManager.instance.onNewDay += OnNewDay;
        HasCrop = true;
    }

    // Called when we interact with a grass tile.
    void Till()
    {
        tilled = true;
        //_renderer.sprite = tilledSprite;
        _renderer.color = tilledColor;
    }
    // Called when we interact with a crop tile.
    void Water()
    {
        _renderer.color = wateredColor;
        if (HasCrop == true)
        {
            curCrop.Water();
        }
    }

    // Called every time a new day occurs. 
    // Only called if the tile contains a crop.
    public void OnNewDay()
    {
        if (curCrop == null)
        {
            tilled = false;
            _renderer.color = originalColor;
            GameManager.instance.onNewDay -= OnNewDay;
        }
        else if (curCrop != null)
        {
            _renderer.color = tilledColor;
            curCrop.NewDayCheck();
        }
    }
}
