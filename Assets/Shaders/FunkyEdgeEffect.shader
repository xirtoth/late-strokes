Shader "Custom/FunkyEdgeEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeIntensity ("Edge Intensity", Range(0.0, 1.0)) = 0.5
        _PulseSpeed ("Pulse Speed", Range(0.1, 5.0)) = 1.0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
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

            sampler2D _MainTex;
            float _EdgeIntensity;
            float _PulseSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Create a pulsating effect based on time
                float pulse = 0.5 + 0.5 * sin(_Time.y * _PulseSpeed);

                // Calculate distance from the center
                float dist = length(i.uv - 0.5);

                // Create a funky effect at the edges
                float effect = pow(dist, 0.5) * pulse * _EdgeIntensity;

                // Apply the effect to the color
                fixed4 color = tex2D(_MainTex, i.uv);
                color.rgb *= 1.0 - effect;

                return color;
            }
            ENDCG
        }
    }
}
