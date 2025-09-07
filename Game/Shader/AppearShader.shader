Shader "Custom/AppearShader"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "Red" {}  // 기본 텍스처
        _Color ("Color", Color) = (1, 1, 1, 1)
        _ClipX ("Clip X Position", Float) = 0.0 // 클리핑 기준이 되는 X 좌표
        _ClipType ("ClipType", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;        // 텍스처 샘플러
            float4 _Color;
            float _ClipX;              // 클리핑 기준 X 좌표
            float _ClipType;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            // 버텍스 셰이더
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);  // 월드 좌표를 클립 좌표로 변환
                o.uv = v.uv;                             // UV 좌표 전달
                return o;
            }

            // 프래그먼트 셰이더
            fixed4 frag (v2f i) : SV_Target
            {
                // 텍스처 색상 샘플링
                fixed4 col = tex2D(_MainTex, i.uv);

                // 클리핑 기준: X 좌표가 _ClipX보다 작을 때는 렌더링하지 않음
                if(_ClipType == 0)
                {
                      if (i.pos.x >= _ClipX)
                    {
                        discard;  // 이 픽셀은 렌더링하지 않음
                    }
                    return col * _Color;  // 클리핑 조건을 만족하지 않으면 텍스처 색상을 반환
                }
                     else
                {
                    if (i.pos.x < _ClipX)
                    {
                        discard;  // 이 픽셀은 렌더링하지 않음
                   }
                   return col * _Color;  // 클리핑 조건을 만족하지 않으면 텍스처 색상을 반환

                  
                }
               
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
