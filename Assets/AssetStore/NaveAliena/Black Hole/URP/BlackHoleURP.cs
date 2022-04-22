using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace BlackHole
{
	public class BlackHoleURP : ScriptableRendererFeature
	{
		public class Pass : ScriptableRenderPass
		{
			public Material m_Mat;
			public RenderTargetIdentifier m_Source;
			RenderTargetHandle m_TempColorTexture;

			public Pass()
			{
				this.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
				m_TempColorTexture.Init("_TemporaryColorTexture");
			}
			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer cmd = CommandBufferPool.Get("BlackHole");

				RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
				opaqueDesc.depthBufferBits = 0;
				cmd.GetTemporaryRT(m_TempColorTexture.id, opaqueDesc, FilterMode.Bilinear);

				Blit(cmd, m_Source, m_TempColorTexture.Identifier(), m_Mat);
				Blit(cmd, m_TempColorTexture.Identifier(), m_Source);

				context.ExecuteCommandBuffer(cmd);
				CommandBufferPool.Release(cmd);
			}
			public override void FrameCleanup(CommandBuffer cmd)
			{
				cmd.ReleaseTemporaryRT(m_TempColorTexture.id);
			}
		}
		///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool m_Enable = false;
		public Material m_Mat;
		Pass m_Pass;

		public override void Create()
		{
			m_Pass = new Pass();
		}
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (!m_Enable)
				return;

			if (m_Mat == null)
			{
				Debug.LogWarningFormat("Missing material. {0} pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
				return;
			}
			m_Pass.m_Mat = m_Mat;
			m_Pass.m_Source = renderer.cameraColorTarget;
			renderer.EnqueuePass(m_Pass);
		}
	}
}