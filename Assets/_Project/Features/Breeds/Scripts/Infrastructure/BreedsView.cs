using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using UnityEngine;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedsView: MonoBehaviour, IBreedsView
    {
        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }

        public void GenerateBreedButtons()
        {

        }

        public void RemoveBreedsButtons()
        {
 
        }
    }
}