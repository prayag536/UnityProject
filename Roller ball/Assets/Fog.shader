Shader "Custom/TextureCoordinates/Fog" {

    Properties {

        _MainTex("Main Texture", 2D) = "white" {}

        _Color("Main Color", Color) = (1,1,1,1)

        _FogColor("Fog Color", Color) = (0.5,0.5,0.5,1)

        _FogStart("Fog Start", Float) = 0.0

        _FogEnd("Fog End", Float) = 50

        _FogTex("Fog Color Texture", 2D) = "white" {}

        _UseFogTex("Use Fog Texture (1=Yes, 0=No)", Float) = 0

    }

    SubShader {

        Pass {

            HLSLPROGRAM

            #pragma vertex vert

            #pragma fragment frag

            

            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            sampler2D _MainTex;

            fixed4 _Color;

            fixed4 _FogColor;

            float _FogStart;

            float _FogEnd;

            sampler2D _FogTex;

            float _UseFogTex;

            struct vertexInput {

                float4 vertex : POSITION;

                float4 texcoord0 : TEXCOORD0;

            };

            struct fragmentInput{

                float4 position : SV_POSITION;

                float4 texcoord0 : TEXCOORD0;

                UNITY_FOG_COORDS(1)

                float customFogDist : TEXCOORD2;

            };

            fragmentInput vert(vertexInput i){

                fragmentInput o;

                o.position = UnityObjectToClipPos(i.vertex);

                o.texcoord0 = i.texcoord0;

                UNITY_TRANSFER_FOG(o,o.position);

                float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;

                o.customFogDist = distance(_WorldSpaceCameraPos, worldPos);

                return o;

            }

            fixed4 frag(fragmentInput i) : SV_Target {

                // Use object texture and color

                fixed4 baseColor = tex2D(_MainTex, i.texcoord0.xy) * _Color;

                float fogFactor = 0;

                if (_FogEnd > _FogStart) {
        fogFactor = saturate((i.customFogDist - _FogStart) / (_FogEnd - _FogStart));
    }

                // Choose fog color: texture or property

                fixed4 fogCol = _FogColor;

                if (_UseFogTex > 0.5) {

                    fogCol = tex2D(_FogTex, i.texcoord0.xy);

                }

                // Lerp between object color/texture and fog color/texture

                fixed4 finalColor = lerp(baseColor, fogCol, fogFactor);

                return finalColor;

            }

            ENDHLSL

        }

    }

}