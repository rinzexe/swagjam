Shader "Custom/WindowCleaning"
{
    Properties
    {
        _DirtyTex ("Dirty State", 2D) = "white" {}
        _CleanTex ("Clean State", 2D) = "white" {}
        _AlphaTex ("Clean Progress", 2D) = "black" {}
        [Toggle] _DebugAlpha ("Show Alpha Only", Float) = 0
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        
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

            sampler2D _DirtyTex;
            sampler2D _CleanTex;
            sampler2D _AlphaTex;
            float _DebugAlpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float cleanProgress = tex2D(_AlphaTex, i.uv).r;
                
                // Debug mode: show the alpha texture directly
                if (_DebugAlpha > 0.5)
                {
                    return float4(cleanProgress.xxx, 1);
                }

                fixed4 dirtyColor = tex2D(_DirtyTex, i.uv);
                fixed4 cleanColor = tex2D(_CleanTex, i.uv);
                return lerp(dirtyColor, cleanColor, cleanProgress);
            }
            ENDCG
        }
    }
}