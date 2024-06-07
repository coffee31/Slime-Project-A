using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public Inventory inven;

    [SerializeField] Image img;

    public int indexnum;

    private void Start()
    {
        inven = FindObjectOfType<Inventory>();
    }

    public void SetImage(Sprite sprite)
    {
        if (img != null && sprite != null)
        {
            img.gameObject.SetActive(true);

            img.sprite = sprite; // ���Կ� �̹����� �����մϴ�.
            img.color = Color.white;

        }

    }

    public void ImageOFF()
    {
        img.gameObject.SetActive(false);
    }

    public void ActiveItem()
    {
        inven.RemoveItem(indexnum);

    }
    

}