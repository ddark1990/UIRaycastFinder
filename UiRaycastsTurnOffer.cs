#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EditorGUI = UnityEditor.Experimental.Networking.PlayerConnection.EditorGUI;

namespace EditorTools
{
    public class UiRaycastsTurnOffer : EditorWindow
    {
        public List<Image> allImageComponents;
        public List<Text> allTextComponents;

        private bool _onGetAllImageComponentsPressed;
        private bool _onGetAllTextComponentsPressed;
        private bool _onOffImageComponentsPressed;
        private bool _onOnImageComponentsPressed;
        private bool _onOnTextComponentsPressed;
        private bool _onOffTextComponentsPressed;
        private bool _onClearAllPressed;
        
        Vector2 scrollPosition = Vector2.zero;
        private float float1;
        private static EditorWindow _editorWindow;

        [MenuItem("EditorTools/UIRaycastTurnOffer")]
        public static void OpenWindow()
        {
            //GetWindow<UiRaycastsTurnOffer>("UI Raycast Finder");
            _editorWindow = GetWindowWithRect(typeof(UiRaycastsTurnOffer), new Rect(0,0,400,500), true, "UiRaycastTurnOffer");
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
            var textComponents = serialObj.FindProperty("allTextComponents");
            

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(float1));
            _onGetAllImageComponentsPressed = GUILayout.Button("Get All Image Components");
            _onGetAllTextComponentsPressed = GUILayout.Button("Get All Text Components");
            _onClearAllPressed = GUILayout.Button("Clear Component Arrays");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(float1));
            _onOnImageComponentsPressed = GUILayout.Button("Turn Rays On Image Components");
            _onOnTextComponentsPressed = GUILayout.Button("Turn Rays On Text Components");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(float1));
            _onOffImageComponentsPressed = GUILayout.Button("Turn Rays Off Image Components");
            _onOffTextComponentsPressed = GUILayout.Button("Turn Rays Off Text Components");
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, false);

            EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(float1));
            EditorGUILayout.PropertyField(imageComponents, true);
            EditorGUILayout.PropertyField(textComponents, true);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            EditorGUILayout.EndScrollView();
            
            
            if(_onGetAllImageComponentsPressed) GetAllSceneUiImageComponents();
            if(_onGetAllTextComponentsPressed) GetAllSceneUiTextComponents();
            
            if(_onOnImageComponentsPressed) RaycastsForImageComponents(true);
            if(_onOnTextComponentsPressed) RaycastsForTextComponents(true);
            
            if(_onOffImageComponentsPressed) RaycastsForImageComponents(false);
            if(_onOffTextComponentsPressed) RaycastsForTextComponents(false);
            
            if(_onClearAllPressed) ClearAll();
            
        }

        private void GetAllSceneUiImageComponents()
        {
            foreach (var obj in GetAllObjectsOnlyInScene())
            {
                var image = obj.GetComponentInChildren<Image>();
                if(image != null)
                    allImageComponents.Add(image);
            }
        }
        
        private void GetAllSceneUiTextComponents()
        {
            foreach (var obj in GetAllObjectsOnlyInScene())
            {
                var text = obj.GetComponentInChildren<Text>();
                
                if(text != null)
                    allTextComponents.Add(text);
            }
        }

        private List<GameObject> GetAllObjectsOnlyInScene()
        {
            var objectsInScene = new List<GameObject>();

            foreach (var go in (GameObject[]) Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if (!EditorUtility.IsPersistent(go.transform.root.gameObject) && !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                    objectsInScene.Add(go);
            }

            return objectsInScene;
        }

        private void RaycastsForTextComponents(bool active)
        {
            foreach (var component in allTextComponents)
            {
                component.raycastTarget = active;
            }
        }
        private void RaycastsForImageComponents(bool active)
        {
            foreach (var component in allImageComponents)
            {
                component.raycastTarget = active;
            }
        }

        private void ClearAll()
        {
            allTextComponents.Clear();
            allImageComponents.Clear();
        }
    }
    
}
#endif

