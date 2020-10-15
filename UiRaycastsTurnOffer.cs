#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EditorTools
{
    public class UiRaycastsTurnOffer : EditorWindow
    {
        public Image[] allImageComponents;
        public Text[] allTextComponents;

        private bool _onGetAllImageComponentsPressed;
        private bool _onGetAllTextComponentsPressed;
        private bool _onToggleImageComponentsPressed;
        private bool _onToggleTextComponentsPressed;
        private bool _onClearAllPressed;
        
        [MenuItem("EditorTools/UIRaycastTurnOffer")]
        public static void OpenWindow()
        {
            GetWindow<UiRaycastsTurnOffer>("UI Raycast Finder");
        }
        
        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void OnGUI()
        {
            var scriptableObj = this;
            var serialObj = new SerializedObject (scriptableObj);
            var imageComponents = serialObj.FindProperty ("allImageComponents");
            var textComponents = serialObj.FindProperty ("allTextComponents");
            
            _onGetAllImageComponentsPressed = GUILayout.Button("Get All Image Components");
            _onGetAllTextComponentsPressed = GUILayout.Button("Get All Text Components");
            
            _onToggleImageComponentsPressed = GUILayout.Button("Toggle All Image Components");
            _onToggleTextComponentsPressed = GUILayout.Button("Toggle All Text Components");
            _onClearAllPressed = GUILayout.Button("Clear Arrays");

            EditorGUILayout.PropertyField(imageComponents, true);
            EditorGUILayout.PropertyField(textComponents, true);
            
            if(_onGetAllImageComponentsPressed) GetAllSceneUiImageComponents();
            if(_onGetAllTextComponentsPressed) GetAllSceneUiTextComponents();
            if(_onToggleImageComponentsPressed) ToggleRaycastsForImageComponents();
            if(_onToggleTextComponentsPressed) ToggleRaycastsForTextComponents();
            if(_onClearAllPressed) ClearAll();
        }

        private void GetAllSceneUiImageComponents()
        {
            allImageComponents = FindObjectsOfType<Image>();
            
            
        }private void GetAllSceneUiTextComponents()
        {
            allTextComponents = FindObjectsOfType<Text>();
        }

        private void ToggleRaycastsForTextComponents()
        {
            foreach (var component in allTextComponents)
            {
                component.raycastTarget = !component.raycastTarget;
            }
        }
        private void ToggleRaycastsForImageComponents()
        {
            foreach (var component in allImageComponents)
            {
                component.raycastTarget = !component.raycastTarget;
            }
        }

        private void ClearAll()
        {
            allTextComponents = new Text[0];
            allImageComponents = new Image[0];
        }
    }
    
}
#endif

