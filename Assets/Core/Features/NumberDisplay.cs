using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Features {
	public class NumberDisplay : MonoBehaviour {
		[SerializeField] private Texture2D[] digitTextures;
		[SerializeField] private Texture2D colonTexture;
		[SerializeField] private VisualTreeAsset digitTemplate;

		public void DisplayNumber(string number1, VisualElement container, int width = 138, int height = 169, int colonWidth = 105, int colonHeight = 85, string number2 = null, int marginLeft = 0, int marginRight = 0) {
			container.Clear();

			foreach (char digitChar in number1) {
				int digit = digitChar - '0';

				TemplateContainer digitElement = digitTemplate.Instantiate();
				VisualElement digitImageElement = digitElement.Q<VisualElement>("digit-view");

				digitImageElement.style.backgroundImage = new StyleBackground(digitTextures[digit]);
				digitImageElement.style.width = width;
				digitImageElement.style.height = height;
				digitImageElement.style.marginLeft = marginLeft;
				digitImageElement.style.marginRight = marginRight;

				container.Add(digitImageElement);
			}
			
			if (number2 == null) return;
			
			TemplateContainer colonElement = digitTemplate.Instantiate();
			VisualElement colonImageElement = colonElement.Q<VisualElement>("digit-view");

			colonImageElement.style.backgroundImage = new StyleBackground(colonTexture);
			colonImageElement.style.width = colonWidth;
			colonImageElement.style.height = colonHeight;
			colonImageElement.style.marginLeft = marginLeft;
			colonImageElement.style.marginRight = marginRight;

			container.Add(colonImageElement);

			foreach (char digitChar in number2) {
				int digit = digitChar - '0';

				TemplateContainer digitElement = digitTemplate.Instantiate();
				VisualElement digitImageElement = digitElement.Q<VisualElement>("digit-view");

				digitImageElement.style.backgroundImage = new StyleBackground(digitTextures[digit]);
				digitImageElement.style.width = width;
				digitImageElement.style.height = height;
				digitImageElement.style.marginLeft = marginLeft;
				digitImageElement.style.marginRight = marginRight;

				container.Add(digitImageElement);
			}
		}
	}
}
