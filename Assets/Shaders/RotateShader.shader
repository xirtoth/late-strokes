Shader "Custom/FullscreenRotate"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed ("Speed", Range(1, 10)) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Calculate rotation angle based on time
                float angle = _Time.y * 360 * _Speed;

                // Convert UV coordinates to range [-0.5, 0.5]
                float2 uvOffset = i.uv - 0.5;

                // Apply rotation
                float2 rotatedUV = uvOffset;
                rotatedUV.x = uvOffset.x * cos(angle) - uvOffset.y * sin(angle);
                rotatedUV.y = uvOffset.x * sin(angle) + uvOffset.y * cos(angle);

                // Convert back to range [0, 1]
                rotatedUV += 0.5;

                // Sample texture
                fixed4 col = tex2D(_MainTex, rotatedUV);

                return col;
            }
            ENDCG
        }
    }
}
