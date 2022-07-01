using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace enrike.utils.text
{
    public class UniversalUtilities : MonoBehaviour
    {
        /*NOW YOU ARE NOT ABLE TO CHANGE COLOR OF THE TEXT, MAKE IT 
         */

        public static GameObject InstantiateText(Vector3 position, string importText, bool materialPerTime, int giveMaterial, float fontSize, Color32 textColor)
        {
            GameObject textPrefab = new GameObject();

            Transform temporaryObjects = GameObject.Find("Canvas World Space").transform;
            GameObject textInstance = Instantiate(textPrefab, position, Quaternion.identity, temporaryObjects);
            Destroy(textPrefab);

            Transform textTransform = textInstance.transform;

            textTransform.localPosition = new Vector2(textTransform.localPosition.x, textTransform.localPosition.y + 1f);

            textInstance.name =  textInstance.name + " (Clone)";

            textInstance.AddComponent<CanvasRenderer>();
            textInstance.AddComponent<TextMeshProUGUI>();
            textInstance.AddComponent<CanvasGroup>();
            textInstance.AddComponent<LayoutElement>();

            TextMeshProUGUI textComponent = textInstance.GetComponent<TextMeshProUGUI>();
            RectTransform textRect = textInstance.GetComponent<RectTransform>();
            
            textComponent.fontSize = fontSize;
            textComponent.color = textColor;
            textComponent.verticalAlignment = VerticalAlignmentOptions.Middle;
            textComponent.horizontalAlignment = HorizontalAlignmentOptions.Center;

            if (materialPerTime)
            {
                textComponent.text = "+" + giveMaterial + importText;
            }
            else
            {
                textComponent.text = importText;
            }

            textRect.sizeDelta = new Vector2(textComponent.preferredWidth, textComponent.preferredHeight);

            LeanTween.moveLocalY(textInstance, textInstance.transform.localPosition.y + 1f, 2f);
            LeanTween.alphaCanvas(textInstance.GetComponent<CanvasGroup>(), 0f, 1.3f);

            Destroy(textInstance, 1.4f);
            return textInstance;
        }
    }
}
