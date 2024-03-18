Shader "Custom/RainbowSwirl"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", float) = 1.0
        _Intensity ("Intensity", float) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Overlay+50" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Speed;
            float _Intensity;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv - 0.5;
                float angle = atan2(uv.y, uv.x);
                float radius = length(uv) * _Intensity;
                float time = _Time.y * _Speed;
                float factor = sin(angle * 10.0 + time) * cos(radius * 10.0 + time) * 0.5 + 0.5;
                fixed4 color = fixed4(factor, abs(sin(angle + time)), abs(cos(angle + time)), 1.0);
                // Apply alpha based on factor
                color.a = factor;
                return color;
            }
            ENDCG
        }
    }
}
