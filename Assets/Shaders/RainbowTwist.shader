Shader "Custom/2DRainbowSpinner"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float) = 1.0
        _Intensity ("Intensity", Range(0,1)) = 1.0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        
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

            sampler2D _MainTex;
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
                float2 center = 0.5;
                float2 uv = i.uv - center;
                float angle = atan2(uv.y, uv.x) / (2 * 3.14159);
                angle += _Time.y * _Speed;
                float rainbow = (sin(angle) * 0.5 + 0.5);
                fixed4 col = lerp(fixed4(1, 0, 0, 1), fixed4(0, 0, 1, 1), rainbow);
                col *= _Intensity;
                return col;
            }
            ENDCG
        }
    }
}
