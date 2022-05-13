Shader "Unlit/practice"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }//���߂������Ȃ炱�ꂢ��
        Blend SrcAlpha OneMinusSrcAlpha//�d�Ȃ����I�u�W�F�N�g�̉�f�̐F�Ƃ̃u�����h���@�̎w��
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
            fixed4 _Color;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed radius = 0.0;
                fixed r = distance(i.uv, fixed2(0.5,0.5)) - _Time*4;//�~���L���鏈��

                fixed4 red = fixed4(0.5, 0.5, 0.5, 0.6);//�F�t���鏈��
                fixed4 green = fixed4(1, 1, 1, 0);

                //lerp�͈���1�ƈ���2�ɂǂ��炩��\��
                //step��0��1��Ԃ�

                return lerp(red, green, step(radius, r));
            }
            ENDCG
        }
    }
}
