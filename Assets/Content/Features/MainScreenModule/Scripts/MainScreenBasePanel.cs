using UnityEngine;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreenBasePanel : MonoBehaviour
    {
        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }

        public void HidePanel()
        {
            gameObject.SetActive(false);       
        }
    }
}