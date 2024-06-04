Shader "Custom/TileShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", Range(0.0, 0.1)) = 0.03
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        fixed4 _OutlineColor;
        float _OutlineSize;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            float alpha = c.a;
            float outline = 0.0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        float2 offset = float2(i, j) * _OutlineSize;
                        outline += tex2D(_MainTex, IN.uv_MainTex + offset).a;
                    }
                }
            }

            if (alpha == 0 && outline > 0)
            {
                o.Albedo = _OutlineColor.rgb;
                o.Alpha = _OutlineColor.a;
            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}
