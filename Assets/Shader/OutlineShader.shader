Shader "Custom/ThickOutlineShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0.0, 0.1)) = 0.03
    }
    SubShader
    {
        Tags {"Queue" = "Transparent"}
        LOD 100

        Pass
        {
            Name "OUTLINE"
            Tags {"LightMode" = "Always"}

            Cull Off
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
                ZFail Replace
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, IN.texcoord);
                if (color.a == 0)
                {
                    discard;
                }

                float2 offset = _OutlineThickness / _ScreenParams.xy;
                fixed4 outline = _OutlineColor;

                float alpha = (
                    tex2D(_MainTex, IN.texcoord + float2(-offset.x, -offset.y)).a +
                    tex2D(_MainTex, IN.texcoord + float2(-offset.x, offset.y)).a +
                    tex2D(_MainTex, IN.texcoord + float2(offset.x, -offset.y)).a +
                    tex2D(_MainTex, IN.texcoord + float2(offset.x, offset.y)).a +

                    tex2D(_MainTex, IN.texcoord + float2(-2.0 * offset.x, -2.0 * offset.y)).a +
                    tex2D(_MainTex, IN.texcoord + float2(-2.0 * offset.x, 2.0 * offset.y)).a +
                    tex2D(_MainTex, IN.texcoord + float2(2.0 * offset.x, -2.0 * offset.y)).a +
                    tex2D(_MainTex, IN.texcoord + float2(2.0 * offset.x, 2.0 * offset.y)).a
                ) * 0.125;

                return lerp(outline, color, alpha);
            }
            ENDCG
        }
    }
}

