Shader "Custom/AppearShader"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "Red" {}  // �⺻ �ؽ�ó
        _Color ("Color", Color) = (1, 1, 1, 1)
        _ClipX ("Clip X Position", Float) = 0.0 // Ŭ���� ������ �Ǵ� X ��ǥ
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

            sampler2D _MainTex;        // �ؽ�ó ���÷�
            float4 _Color;
            float _ClipX;              // Ŭ���� ���� X ��ǥ
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

            // ���ؽ� ���̴�
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);  // ���� ��ǥ�� Ŭ�� ��ǥ�� ��ȯ
                o.uv = v.uv;                             // UV ��ǥ ����
                return o;
            }

            // �����׸�Ʈ ���̴�
            fixed4 frag (v2f i) : SV_Target
            {
                // �ؽ�ó ���� ���ø�
                fixed4 col = tex2D(_MainTex, i.uv);

                // Ŭ���� ����: X ��ǥ�� _ClipX���� ���� ���� ���������� ����
                if(_ClipType == 0)
                {
                      if (i.pos.x >= _ClipX)
                    {
                        discard;  // �� �ȼ��� ���������� ����
                    }
                    return col * _Color;  // Ŭ���� ������ �������� ������ �ؽ�ó ������ ��ȯ
                }
                     else
                {
                    if (i.pos.x < _ClipX)
                    {
                        discard;  // �� �ȼ��� ���������� ����
                   }
                   return col * _Color;  // Ŭ���� ������ �������� ������ �ؽ�ó ������ ��ȯ

                  
                }
               
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
