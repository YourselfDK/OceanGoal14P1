using UnityEngine;

public class ShopFrontController : MonoBehaviour
    {
        public GameObject shopFront;

        public void CloseShopFront()
        {
            shopFront.SetActive(false);
        }
    }
