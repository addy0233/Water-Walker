using System;
using StylizedWater2;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if URP
using UnityEngine.Rendering.Universal;
#endif

namespace StylizedWater2.DynamicEffects
{
    [ExecuteInEditMode]
    public class DemoController : MonoBehaviour
    {
        #pragma warning disable 0108
        private Camera camera;
        private FreeCamera freeCamera;
        #pragma warning restore 0108

        public GameObject[] exhibits = Array.Empty<GameObject>();

        private int exhibitIndex;

        public Text headerText;

        private void Start()
        {
            camera = Camera.main;
            freeCamera = camera.GetComponent<FreeCamera>();

            if (Application.isPlaying == true) SwitchToExhibit(0);

            Canvas canvas = GetComponentInChildren<Canvas>(true);
            canvas.gameObject.SetActive(true);

            Camera[] cams = this.GetComponentsInChildren<Camera>(true);
            for (int i = 0; i < cams.Length; i++)
            {
                cams[i].enabled = false;
            }
        }

        private void OnEnable()
        {
            #if URP
            if (Application.isPlaying == false)
            {
                if (PipelineUtilities.RenderFeatureMissing<WaterDynamicEffectsRenderFeature>(out var renderers))
                {
                    #if UNITY_EDITOR
                    EditorGUIUtility.PingObject(this);

                    string[] rendererNames = new string[renderers.Length];
                    for (int i = 0; i < rendererNames.Length; i++)
                    {
                        rendererNames[i] = "• " + renderers[i].name;
                    }

                    if (EditorUtility.DisplayDialog("Dynamic Effects", $"The Dynamic Effects render feature hasn't been added to the following renderers:\n\n" +
                                                                       String.Join(Environment.NewLine, rendererNames) +
                                                                       $"\n\nThis is required for rendering to take effect", "Setup", "Ignore"))
                    {
                        PipelineUtilities.SetupRenderFeature<WaterDynamicEffectsRenderFeature>(name:"Stylized Water 2: Dynamic Effects");
                    }
                    #endif
                }
            }
            #endif
        }

        //UI-controlled
        public void ToggleFreeCamera()
        {
            freeCamera.enabled = !freeCamera.enabled;
        }

        public void OpenDebugger()
        {
            #if UNITY_EDITOR
            EditorApplication.ExecuteMenuItem("Window/Analysis/Dynamic Effects debugger");
            #endif
        }

        //UI-controlled
        public void MoveNext()
        {
            exhibitIndex++;

            if (exhibitIndex == exhibits.Length) exhibitIndex = 0;

            SwitchToExhibit(exhibitIndex);

        }

        //UI-controlled
        public void MovePrevious()
        {
            exhibitIndex--;

            if (exhibitIndex < 0) exhibitIndex = exhibits.Length-1;

            SwitchToExhibit(exhibitIndex);
        }

        private void SwitchToExhibit(int index)
        {
            for (int i = 0; i < exhibits.Length; i++)
            {
                if (i == index)
                {
                    headerText.text = exhibits[i].name;
                    
                    exhibits[i].SetActive(true);
                    
                    Camera cam = (Camera)exhibits[i].GetComponentInChildren(typeof(Camera), true);
                    
                    if (cam)
                    {
                        camera.transform.SetPositionAndRotation(cam.transform.position, cam.transform.rotation);
                        camera.fieldOfView = cam.fieldOfView;
                    }

                }
                else
                {
                    exhibits[i].SetActive(false);
                }
            }
        }
    }
}
