using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum BlockType
{
    cantBuild,
    canBuild,
    AlreadyBuilded
}

public class CubeBehaviour : MonoBehaviour
{
    [SerializeField] private BlockType _type;
    private GameManager _gameManager;
    private BuildManager _buildManager;
    private Color _startColor;

    private void Start()
    {
        _gameManager = GameManager.GetInstance();
        _buildManager = BuildManager.GetInstance();
        _startColor = gameObject.GetComponent<Renderer>().material.color;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        var currentColor = gameObject.GetComponent<Renderer>().material.color;
        TowerStatsUIBehaviour.GetInstance().gameObject.SetActive(false);

        if (_type == BlockType.cantBuild || _type == BlockType.AlreadyBuilded)
        {
            _buildManager.GetBuyGUIBlock().SetActive(false);
            _buildManager.SetBlockWhereWeBuilding(null);
        }
        else
        {
            _buildManager.GetBuyGUIBlock().SetActive(true);
            //gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            gameObject.GetComponent<Renderer>().material.color = new Color(currentColor.r, currentColor.g, 255, currentColor.a);
            _buildManager.SetBlockWhereWeBuilding(gameObject);
        }


        //Instantiate(BuildManager.GetInstance()._prefub, transform.position, Quaternion.identity);
    }

    

    private void OnMouseEnter()
    {
        if (_type == BlockType.cantBuild || _type == BlockType.AlreadyBuilded)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        var currentColor = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = new Color(currentColor.r, currentColor.g, 255, currentColor.a);
    }

    private void OnMouseExit()
    {
        if (_type == BlockType.cantBuild || _type == BlockType.AlreadyBuilded)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (_buildManager.GetBlockWhereWeBuilding() != gameObject)
        {
            gameObject.GetComponent<Renderer>().material.color = _startColor;
        }
    }

    public void SetType(BlockType type)
    {
        _type = type;
    }
}