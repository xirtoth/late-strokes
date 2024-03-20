Shader "Custom/2DRainbowSpinner"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float) = 1.0
        _Intensity ("Intensity", Range(0,1)) = 1.0
        _Color1 ("Color1", Color) = (1,0,0,1)
        _Color2 ("Color2", Color) = (0,1,0,1)
        _Color3 ("Color3", Color) = (0,0,1,1)
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
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;

            v2f vert (appdata v)
            {
                v2f o;
                float2 uv = v.uv;
                uv -= 0.5; // Center UVs
                float angle = _Time.y * _Speed; // Twist based on time
                float s = sin(angle);
                float c = cos(angle);
                // Perform rotation
                uv = float2(uv.x * c - uv.y * s, uv.x * s + uv.y * c);
                uv += 0.5; // Recenter UVs
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 tex = tex2D(_MainTex, i.uv);
                fixed4 color = lerp(lerp(_Color1, _Color2, i.uv.x), _Color3, i.uv.y);
                color.a = 0.5; // Set alpha value to 0.5
                return color;
            }
            ENDCG
        }
    }
}
