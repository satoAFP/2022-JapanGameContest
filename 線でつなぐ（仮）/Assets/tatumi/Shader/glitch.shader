Shader "PostEffect/GlitchEffect"
{
    Properties
    {
      _MainTex("Texture", 2D) = "white" { }
      _NowTime("NowTime", float) = 0.0
      _Duration("Duration", float) = 100000000

      [Header(Glitch1)]
      _GlitchSize1("Glitch Size", Range(0, 2.0)) = 1.0
      _Amplitude1("Amplitude", float) = 0.01

      [Space(10)]
      [Header(Glitch2)]
      _GlitchSize2("Glitch Size", Range(0, 3.0)) = 1.0
      _Amplitude2("Amplitude", float) = 0.01

      [Space(10)]
      [Header(Glitch3)]
      _GlitchSize3("Glitch Size", Range(0.0, 1.0)) = 0.4
      _Amplitude3("Amplitude", float) = 0.1
    }
        SubShader
      {
        Tags { "RenderType" = "Opaque" "Queue" = "Geometry" }
        LOD 100

        Pass
        {
          CGPROGRAM

          //Unity.cgincから頂点シェーダーを読み込む
          #pragma vertex vert
          #pragma fragment frag

          #include "UnityCG.cginc"

          sampler2D _MainTex;
          float _NowTime, _Duration;//C#から受け取る
          float _WholeFrequency;
          float _GlitchSize1;
          float _GlitchSize2;
          float _GlitchSize3;
          float _Amplitude1;
          float _Amplitude2;
          float _Amplitude3;

          fixed2 random2(fixed2 st);
          float perlinNoise(fixed2 st);
          //uv.yを受け取る
          float glitch1(float y);
          float glitch2(float y);
          float amplitudeStrength1(float2 uv);
          float amplitudeStrength2();

          struct appdata
          {
            float4 vertex: POSITION;
            float2 uv: TEXCOORD0;
          };

          struct v2f
          {
            float2 uv: TEXCOORD0;
            UNITY_FOG_COORDS(1)
            float4 vertex: SV_POSITION;
          };

          v2f vert(appdata v)
          {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            UNITY_TRANSFER_FOG(o, o.vertex);
            return o;
          }

          //自作グリッチロジック
          //特定のyの領域をx方向に瞬間的にずらす。
          //三角関数の組み合わせで閾値より大きい領域をずらす。
          //ずらしの方向は三角関数の符号で決まる。
          //ずらしの振幅はプロパティ
          fixed4 frag(v2f  i) : SV_Target
          {
            float2 uv = i.uv;
            float value1 = glitch1(uv.y);
            float value2_1 = glitch2(uv.y);
            float value2_2 = glitch2(uv.y * 1.8 + (_Time.y % 0.2) * 100); //0.1秒毎にオフセット量が変わってグリッチが素早く変化する
            float threshold1 = _GlitchSize1;
            float threshold2 = _GlitchSize2;
            float glitch3Ypos = (random2((float2)_NowTime) * 0.5 + 0.5) * (1.0 - _GlitchSize3);//0~(1.0 - _GlitchSize3)

            float glitch1Flag = step(abs(value1), threshold1);//グリッチする領域は1、しない領域は0。
            float glitch2_1Flag = step(abs(value2_1), threshold2);//グリッチする領域は1、しない領域は0。
            float glitch2_2Flag = step(abs(value2_2), threshold2 / 20.0);//グリッチする領域は1、しない領域は0。
            float glitch3Flag = step(glitch3Ypos, uv.y) * step(uv.y, glitch3Ypos + _GlitchSize3);

            //グリッチのタイミングをずらすならここで調整
            int isPlaying1 = (_NowTime + (_Duration / 5.0 * 2.0) + _Duration > _Time.y) ? 1 : 0;
            int isPlaying2_1 = (_NowTime + _Duration > _Time.y) ? 1 : 0;
            int isPlaying2_2 = (_NowTime + _Duration > _Time.y) ? 1 : 0;
            int isPlaying3 = (_NowTime + _Duration > _Time.y) ? 1 : 0;

            // isPlaying1 = 1;
            // isPlaying2_1 = 1;
            // isPlaying2_2 = 1;
            // isPlaying3 = 1;


            //各グリッチ処理のグリッチのずれ量(amplitude)を格納
            float x1 = uv.x;
            float x2_1 = uv.x;
            float x2_2 = uv.x;
            float x3 = uv.x;
            //グリッチ処理1
            x1 = isPlaying1 * glitch1Flag * _Amplitude1 * amplitudeStrength1(uv);
            //グリッチ処理2
            x2_1 = isPlaying2_1 * glitch2_1Flag * _Amplitude2 * sign(value2_1);
            //グリッチ処理2.5(2のプロパティに依存した改造版)
            x2_2 = isPlaying2_2 * glitch2_2Flag * _Amplitude2 * sign(value2_2);
            //グリッチ処理3
            x3 = isPlaying3 * glitch3Flag * _Amplitude3;

            //グリッチのブレンドはここでやる
            uv.x += (1 - glitch3Flag) * (x1 + x2_1 + x2_2) + glitch3Flag * (x3 + x2_2 * 0.4);

            fixed4 col = tex2D(_MainTex, uv);
            return col;
          }

              //関数定義
              //-1~+1
              fixed2 random2(fixed2 st)
              {
                st = fixed2(dot(st, fixed2(127.1, 311.7)),
                dot(st, fixed2(269.5, 183.3)));
                return -1.0 + 2.0 * frac(sin(st) * 43758.5453123);
              }

              float perlinNoise(fixed2 st)
              {
                fixed2 p = floor(st);
                fixed2 f = frac(st);
                fixed2 u = f * f * (3.0 - 2.0 * f);

                float v00 = random2(p + fixed2(0, 0));
                float v10 = random2(p + fixed2(1, 0));
                float v01 = random2(p + fixed2(0, 1));
                float v11 = random2(p + fixed2(1, 1));

                return lerp(lerp(dot(v00, f - fixed2(0, 0)), dot(v10, f - fixed2(1, 0)), u.x),
                lerp(dot(v01, f - fixed2(0, 1)), dot(v11, f - fixed2(1, 1)), u.x),
                u.y) + 0.5f;
              }

              float glitch1(float y)
              {
                float time = _Time.y + 1000;
                float amp = 2.0;
                float syuki = 1.0;
                float frequency1 = 13;
                float frequency2 = 7;
                return amp * sin(3 * (y + time * frequency1)) *
                cos(7 * (y + time * frequency2)) *
                sin(time);
              }

              float glitch2(float y)
              {
                y *= 3;
                float t = _NowTime * 10;
                float s1 = sin(9.5 * y + t * 1.5);
                float c1 = cos(3.4 * y + t);
                float base1 = sin(3 * y);
                float base2 = sin(17 * y);
                return base1 + base2 + s1 * c1;
              }

              float amplitudeStrength1(float2 uv)
              {
                float y = uv.y;
                return sin(y * 300 + _Time.y * 2) *
                cos(y * 170 + _Time.y) *
                perlinNoise(uv * 20 + _Time.x);
              }

              float amplitudeStrength2()
              {
                float value;
                float threshold = 0.99;
                value = sin(_Time.y) * sin(_Time.y);
                return step(threshold, value);
              }
              ENDCG

            }
      }
}