Shader "Custom/SwirlingFunkyShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Frequency ("Frequency", Range(0.1, 10)) = 1
        _Speed ("Speed", Range(0.1, 10)) = 1
        _SwirlAmount ("Swirl Amount", Range(-2, 2)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        float _Frequency;
        float _Speed;
        float _SwirlAmount;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Calculate time-based offset
            float offset = _Speed * _Time.y;

            // Calculate swirl distortion
            float2 swirlOffset = float2(
                sin(offset * _Frequency * _SwirlAmount),
                cos(offset * _Frequency * _SwirlAmount)
            );

            // Apply swirl distortion to UV coordinates
            float2 distortedUV = IN.uv_MainTex + swirlOffset;

            // Sample texture with distorted UV coordinates
            fixed4 texColor = tex2D(_MainTex, distortedUV);

            // Modulate RGB using sine and cosine waves
            float r = 0.5 + 0.5 * sin(offset * _Frequency);
            float g = 0.5 + 0.5 * cos(offset * _Frequency);
            float b = 0.5 + 0.5 * sin(offset * _Frequency * 0.5); // Adjusting frequency for blue channel

            // Combine color modulation with texture color
            fixed4 funkyColor = texColor * fixed4(r, g, b, 1);

            o.Albedo = funkyColor.rgb * _Color.rgb;
            o.Alpha = texColor.a * _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
