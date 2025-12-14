Shader "Unlit/KusoKimoShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            // make fog work
            #pragma multi_compile_fog

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
                // sample the texture
                fixed4 col = fixed4 (
                    abs(sin(length(i.uv.y)*15.0+_Time.y*15.0)) ,
                    abs(cos(length(i.uv.x)*15.0+_Time.y*15.0)) ,
                    fmod(_Time.y*15.0 ,1.0) ,
                    1.0
                );
                fixed4 col2 = fixed4 (
                    fmod(sin(atan(i.uv.x/i.uv.y) + _Time.y*15.0),1.0) ,
                    abs(cos(_Time.y*30.0)) ,
                    abs(sin(_Time.y*30.0)) ,
                    1.0
                );
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
