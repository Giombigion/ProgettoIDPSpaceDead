using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace BlackHole
{
	public class URPDemo : MonoBehaviour
	{
		public RenderPipelineAsset m_Urp;
		public BlackHoleURP m_Feature;
		public Material m_Mat = null;
		[Range(0.01f, 0.2f)] public float m_DarkRange = 0.1f;
		[Range(-2.5f, -1f)] public float m_Distortion = -2f;
		[Range(-40f, 40f)] public float m_Warp = 30f;
		float m_MouseX = 0f;
		float m_MouseY = 0f;
		bool m_TraceMouse = false;

		void Start()
		{
			GraphicsSettings.renderPipelineAsset = m_Urp;
			m_MouseX = m_MouseY = 0.5f;
			m_Feature.m_Mat = m_Mat;
		}
		void Update()
		{
			if (Input.GetMouseButtonDown(1))
			{
				m_TraceMouse = true;
			}
			else if (Input.GetMouseButtonUp(1))
			{
				m_TraceMouse = false;
			}
			else if (Input.GetMouseButton(1))
			{
				if (m_TraceMouse)
				{
					m_MouseX = Input.mousePosition.x / Screen.width;
					m_MouseY = Input.mousePosition.y / Screen.height;
				}
			}
			m_Mat.SetVector("_Center", new Vector4(m_MouseX, m_MouseY, 0f, 0f));
			m_Mat.SetFloat("_DarkRange", m_DarkRange);
			m_Mat.SetFloat("_Distortion", m_Distortion);
			m_Mat.SetFloat("_Warp", m_Warp);
		}
		void OnGUI()
		{
			GUI.Box(new Rect(10, 10, 150, 25), "Black Hole Demo");
		}
	}
}