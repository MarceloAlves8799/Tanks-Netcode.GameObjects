using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tanks
{
    public class NameSelector : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private Button connectButton;

        [SerializeField] private int minNameLenght;
        [SerializeField] private int maxNameLenght;

        public const string PlayerNameKey = "PlayerName";


        private void OnEnable()
        {
            nameField.onValueChanged.AddListener(delegate { HandleNameChanged(); });
            connectButton.onClick.AddListener(Connect);
        }

        private void Start()
        {
            if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null)
            {
                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                SceneManager.LoadScene(nextSceneIndex);

                return;
            }

            nameField.text = PlayerPrefs.GetString(PlayerNameKey, string.Empty);
            HandleNameChanged();
        }

        public void HandleNameChanged()
        {
            connectButton.interactable = nameField.text.Length >= minNameLenght &&
                                         nameField.text.Length <= maxNameLenght;
        }

        public void Connect()
        {
            PlayerPrefs.SetString(PlayerNameKey, nameField.text);

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }

        void OnDisable()
        {
            nameField.onValueChanged.RemoveListener(delegate { HandleNameChanged(); });
            connectButton.onClick.RemoveListener(Connect);
        }
    }
}
