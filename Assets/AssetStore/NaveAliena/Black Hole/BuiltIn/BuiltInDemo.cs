using UnityEngine;

namespace BlackHole
{
	public class BuiltInDemo : MonoBehaviour
	{
		public Material m_Mat = null;
		[Range(0.01f, 0.2f)] public float m_DarkRange = 0.1f;
		[Range(-2.5f, -1f)] public float m_Distortion = -2f;
		[Range(-40f, 40f)] public float m_Warp = 30f;
		float m_MouseX = 0f;
		float m_MouseY = 0f;
		bool m_TraceMouse = false;

		void Start()
		{
			m_MouseX = m_MouseY = 0.5f;
		}
		void OnRenderImage(RenderTexture src, RenderTexture dst)
		{
			m_Mat.SetVector("_Center", new Vector4(m_MouseX, m_MouseY, 0f, 0f));
			m_Mat.SetFloat("_DarkRange", m_DarkRange);
			m_Mat.SetFloat("_Distortion", m_Distortion);
			m_Mat.SetFloat("_Warp", m_Warp);
			Graphics.Blit(src, dst, m_Mat);
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
		}
		void OnGUI()
		{
			GUI.Box(new Rect(10, 10, 150, 25), "Black Hole Demo");
		}
	}
}