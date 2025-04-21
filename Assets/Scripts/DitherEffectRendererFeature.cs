using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;
using Unity.VisualScripting;
using UnityEngine.Rendering.RenderGraphModule.Util;

// Youtube Unity <Introduction to the Render Graph in Unity 6> 참조
// https://youtu.be/U8PygjYAF7A?si=8USTeVS6gpL7_88M

public class DitherEffectRendererFeature : ScriptableRendererFeature
{
    class DitherEffectPass : ScriptableRenderPass
    {
        const string m_Passname = "DitherEffectPass";
        Material m_BlitMaterial;

        public void Setup(Material mat)
        {
            m_BlitMaterial = mat;
            // 최종 출력인 백버퍼를 효과의 입력으로 직접 사용할 수 없기 때문에 마치 중간적인 질감으로 작업하는 것
            // 이걸 true로 하면 유니티가 자동으로 작업 사본을 생성하도록 지시한다
            requiresIntermediateTexture = true;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            var stack = VolumeManager.instance.stack;
            var customEffect = stack.GetComponent<SphereVolumeComponent>();

            if (!customEffect.IsActive()) return;

            var resourceData = frameData.Get<UniversalResourceData>();

            if (resourceData.isActiveTargetBackBuffer)
            {
                Debug.LogError($"Skipping render pass. ditherEffectRendererFeature requires an intermediate Color Texture, we can't use the BackBuffer as a texture input.");
                return;
            }

            var source = resourceData.activeColorTexture;

            var destinationDesc = renderGraph.GetTextureDesc(source);
            destinationDesc.name = $"CameraColor-{m_Passname}";
            destinationDesc.clearBuffer = false;

            TextureHandle destination = renderGraph.CreateTexture(destinationDesc);

            RenderGraphUtils.BlitMaterialParameters para = new(source, destination, m_BlitMaterial, 0);
            renderGraph.AddBlitPass(para, passName: m_Passname);

            resourceData.cameraColor = destination;
        }
    }

    public RenderPassEvent injectionPoint = RenderPassEvent.AfterRenderingPostProcessing;
    public Material material;

    DitherEffectPass m_ScriptablePass;

    public override void Create()
    {
        m_ScriptablePass = new DitherEffectPass();

        // Configures where the render pass should be injected.
        m_ScriptablePass.renderPassEvent = injectionPoint;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (material == null)
        {
            Debug.LogWarning("DitherEffectRendererFeature material is null and will be skipped.");
            return;
        }

        m_ScriptablePass.Setup(material);
        renderer.EnqueuePass(m_ScriptablePass);
    }
}
