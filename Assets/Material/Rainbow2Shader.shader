Shader "Unlit/Rainbow2Shader"
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
            float nsin(float ph){
                return (sin(ph)+1.0)/2.0 ;
            }
            float ncos(float ph){
                return (cos(ph)+1.0)/2.0 ;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = fixed4 (
                    nsin(i.uv.x * 20.0 + _Time.y * 30.0) ,
                    nsin(i.uv.y * 20.0 + _Time.y * 30.0) ,
                    ncos(length(i.uv) * 20.0 + _Time.y * 30.0) ,
                    1.0 
                ) ;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
