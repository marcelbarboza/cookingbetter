Shader "Custom/Highlight" {
    Properties{
        _Color("Highlight Color", Color) = (1, 1, 0, 1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
    }
        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
            LOD 100
            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                };

                struct v2f {
                    float4 vertex : SV_POSITION;
                    float4 worldPos : TEXCOORD0;
                };

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                    return o;
                }

                sampler2D _MainTex;
                float4 _Color;

                bool _Hit;

                float4 frag(v2f i) : SV_Target {
                    float4 col = tex2D(_MainTex, i.worldPos.xy);
                    if (_Hit) {
                        col.rgb += _Color.rgb;
                    }
                    return col;
                }
                ENDCG
            }
    }
        FallBack "Diffuse"
}