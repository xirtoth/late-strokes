Shader "Custom/SwirlingRainbowShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
 
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
 
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float angle = atan2(uv.y - 0.5, uv.x - 0.5);
                float rainbowFactor = (angle / (2 * 3.14159) + _Time.y * 0.1) * 5;
                float3 rainbowColor = float3(sin(rainbowFactor), sin(rainbowFactor + 2), sin(rainbowFactor + 4));
                return float4(rainbowColor, 0.5); // Set alpha to 0.5 for semi-transparency
            }
            ENDCG
        }
    }
}
