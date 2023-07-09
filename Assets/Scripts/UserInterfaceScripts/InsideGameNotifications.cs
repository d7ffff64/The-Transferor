using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UserInterfaceScripts
{
    public class InsideGameNotifications : MonoBehaviour
    {
        [Header("Inside Game Notifications Settings")]
        [SerializeField] private float _delayTimeToDestroyNotification;
        [SerializeField] private GameObject _fieldPrefab;

        public void NewNotification(string text)
        {
            GameObject newField = _fieldPrefab;
            string newFieldText = text;
            newField.GetComponentInChildren<TextMeshProUGUI>().text = newFieldText;

            GameObject newFieldCreated = Instantiate(newField, gameObject.transform);
            Destroy(newFieldCreated, _delayTimeToDestroyNotification);
        }
    }
}
