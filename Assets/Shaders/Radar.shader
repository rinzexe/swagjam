Shader "Custom/Radar"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Distance ("Distance", Float) = 0
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Distance;

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

            // Better hash function for more random values
            float2 hash2(float2 p)
            {
                float3 p3 = frac(float3(p.xyx) * float3(.1031, .1030, .0973));
                p3 += dot(p3, p3.yzx + 33.33);
                return frac((p3.xx + p3.yz) * p3.zy);
            }

            // Value noise
            float noise(float2 p)
            {
                float2 i = floor(p);
                float2 f = frac(p);
                
                float2 u = f * f * (3.0 - 2.0 * f);
                
                float2 a = hash2(i);
                float2 b = hash2(i + float2(1.0, 0.0));
                float2 c = hash2(i + float2(0.0, 1.0));
                float2 d = hash2(i + float2(1.0, 1.0));
                
                return lerp(lerp(dot(a, f), dot(b, f - float2(1.0, 0.0)), u.x),
                           lerp(dot(c, f - float2(0.0, 1.0)), dot(d, f - float2(1.0, 1.0)), u.x),
                           u.y);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                // --- uv shit ---
                float2 uv = i.uv;
                float time = _Time.y;

                float steppedTime = floor(time * 4.0) / 4.0;

                float2 blockPos = floor(uv * 10) / 10;
                float blockNoise = noise(blockPos * 7.0 + steppedTime * 5111.0);

                // sine glitches
                float horizontalGlitch1 = noise(float2(0.0, uv.y * 10.5 + time * 5.0)) * 0.1;
                float horizontalGlitch2 = noise(float2(0.0, uv.y * 15.5 + time * 12.0)) * 0.1;
                float horizontalGlitch3 = noise(float2(0.0, uv.y * 5.5 + time * 4.0)) * 0.1;

                float horizontalGlitch = horizontalGlitch1 + horizontalGlitch2 + horizontalGlitch3;

                uv += lerp(float2(horizontalGlitch, 0.0) * _Distance, float2(0, 0), blockPos);

                // block glitches
                float blockGlitch = step(0.3 - _Distance * 0.05, blockNoise);
                uv = lerp(uv, blockPos + hash2(blockPos + steppedTime) / 5.0, blockGlitch * _Distance);

                // --- color shit ---
                fixed4 col = tex2D(_MainTex, uv);

                // noise
                float noiseOverlay = noise(uv * time * 1000) * _Distance + 1;

                col *= noiseOverlay;
                
                return col;
            }
            ENDCG
        }
    }
}