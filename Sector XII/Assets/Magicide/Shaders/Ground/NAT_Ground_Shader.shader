// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32794,y:33116,varname:node_2865,prsc:2|diff-6133-OUT,spec-358-OUT,gloss-6948-OUT,normal-6222-OUT,emission-3371-OUT;n:type:ShaderForge.SFN_Slider,id:358,x:32314,y:33066,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:4086,x:30932,y:33038,ptovrint:False,ptlb:SPLAT MAP,ptin:_SPLATMAP,varname:node_4086,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:5835,x:31906,y:32364,varname:node_5835,prsc:2|A-7876-RGB,B-7387-RGB,T-4086-R;n:type:ShaderForge.SFN_Tex2d,id:7387,x:31628,y:32328,ptovrint:False,ptlb:Cracks,ptin:_Cracks,varname:node_7387,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7876,x:31628,y:32151,ptovrint:False,ptlb:Base,ptin:_Base,varname:node_7876,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:1723,x:31906,y:32498,varname:node_1723,prsc:2|A-5835-OUT,B-4919-RGB,T-4086-G;n:type:ShaderForge.SFN_Tex2d,id:4919,x:31628,y:32512,ptovrint:False,ptlb:Moss,ptin:_Moss,varname:node_4919,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:4884,x:31906,y:32628,varname:node_4884,prsc:2|A-1723-OUT,B-6057-RGB,T-4086-B;n:type:ShaderForge.SFN_Tex2d,id:6057,x:31628,y:32695,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_6057,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:6133,x:31906,y:32755,varname:node_6133,prsc:2|A-4884-OUT,B-3365-RGB,T-4086-A;n:type:ShaderForge.SFN_Tex2d,id:3365,x:31628,y:32882,ptovrint:False,ptlb:Dirt,ptin:_Dirt,varname:node_3365,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5376,x:31772,y:33066,ptovrint:False,ptlb:Glow Texture,ptin:_GlowTexture,varname:node_5376,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7776,x:31979,y:32976,varname:node_7776,prsc:2|A-4086-A,B-5376-RGB;n:type:ShaderForge.SFN_Multiply,id:3371,x:32079,y:33129,varname:node_3371,prsc:2|A-7776-OUT,B-6690-RGB;n:type:ShaderForge.SFN_Color,id:6690,x:31916,y:33215,ptovrint:False,ptlb:Glow Colour,ptin:_GlowColour,varname:node_6690,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:4493,x:32217,y:32164,varname:node_4493,prsc:2|A-7876-A,B-7387-A,T-4086-R;n:type:ShaderForge.SFN_Lerp,id:1816,x:32217,y:32300,varname:node_1816,prsc:2|A-4493-OUT,B-4919-A,T-4086-G;n:type:ShaderForge.SFN_Lerp,id:6939,x:32217,y:32430,varname:node_6939,prsc:2|A-1816-OUT,B-6057-A,T-4086-B;n:type:ShaderForge.SFN_Lerp,id:6948,x:32217,y:32557,varname:node_6948,prsc:2|A-6939-OUT,B-3365-A,T-4086-A;n:type:ShaderForge.SFN_Tex2d,id:3407,x:31622,y:33373,ptovrint:False,ptlb:Base_N,ptin:_Base_N,varname:node_3407,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2eb6d7868ae78f648ad09054679f0fdd,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3635,x:31622,y:33571,ptovrint:False,ptlb:Cracks_N,ptin:_Cracks_N,varname:_node_3407_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0451693b80e812f468d4acb691f17b40,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:2194,x:31622,y:33766,ptovrint:False,ptlb:Moss_N,ptin:_Moss_N,varname:_node_3407_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ecaba0e3b7ef9ea40adae2a5aad0618e,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:6091,x:31622,y:33968,ptovrint:False,ptlb:Glow_N,ptin:_Glow_N,varname:_node_3407_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:2eb6d7868ae78f648ad09054679f0fdd,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:3255,x:31622,y:34165,ptovrint:False,ptlb:Dirt_N,ptin:_Dirt_N,varname:_node_3407_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3541d6e909dd5424aa3a10c93fe7274a,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Lerp,id:4228,x:31918,y:33568,varname:node_4228,prsc:2|A-3407-RGB,B-3635-RGB,T-4086-R;n:type:ShaderForge.SFN_Lerp,id:6425,x:31918,y:33719,varname:node_6425,prsc:2|A-4228-OUT,B-2194-RGB,T-4086-G;n:type:ShaderForge.SFN_Lerp,id:8095,x:31918,y:33868,varname:node_8095,prsc:2|A-6425-OUT,B-6091-RGB,T-4086-B;n:type:ShaderForge.SFN_Lerp,id:6222,x:31918,y:34019,varname:node_6222,prsc:2|A-8095-OUT,B-3255-RGB,T-4086-A;n:type:ShaderForge.SFN_NormalBlend,id:1568,x:31976,y:33401,varname:node_1568,prsc:2;proporder:358-4086-7387-7876-4919-6057-3365-5376-6690-3407-3635-2194-6091-3255;pass:END;sub:END;*/

Shader "Shader Forge/GroundShader" {
    Properties {
        _Metallic ("Metallic", Range(0, 1)) = 0
        _SPLATMAP ("SPLAT MAP", 2D) = "white" {}
        _Cracks ("Cracks", 2D) = "white" {}
        _Base ("Base", 2D) = "white" {}
        _Moss ("Moss", 2D) = "white" {}
        _Glow ("Glow", 2D) = "white" {}
        _Dirt ("Dirt", 2D) = "white" {}
        _GlowTexture ("Glow Texture", 2D) = "white" {}
        _GlowColour ("Glow Colour", Color) = (0.5,0.5,0.5,1)
        _Base_N ("Base_N", 2D) = "bump" {}
        _Cracks_N ("Cracks_N", 2D) = "bump" {}
        _Moss_N ("Moss_N", 2D) = "bump" {}
        _Glow_N ("Glow_N", 2D) = "bump" {}
        _Dirt_N ("Dirt_N", 2D) = "bump" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Metallic;
            uniform sampler2D _SPLATMAP; uniform float4 _SPLATMAP_ST;
            uniform sampler2D _Cracks; uniform float4 _Cracks_ST;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _Moss; uniform float4 _Moss_ST;
            uniform sampler2D _Glow; uniform float4 _Glow_ST;
            uniform sampler2D _Dirt; uniform float4 _Dirt_ST;
            uniform sampler2D _GlowTexture; uniform float4 _GlowTexture_ST;
            uniform float4 _GlowColour;
            uniform sampler2D _Base_N; uniform float4 _Base_N_ST;
            uniform sampler2D _Cracks_N; uniform float4 _Cracks_N_ST;
            uniform sampler2D _Moss_N; uniform float4 _Moss_N_ST;
            uniform sampler2D _Glow_N; uniform float4 _Glow_N_ST;
            uniform sampler2D _Dirt_N; uniform float4 _Dirt_N_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Base_N_var = UnpackNormal(tex2D(_Base_N,TRANSFORM_TEX(i.uv0, _Base_N)));
                float3 _Cracks_N_var = UnpackNormal(tex2D(_Cracks_N,TRANSFORM_TEX(i.uv0, _Cracks_N)));
                float4 _SPLATMAP_var = tex2D(_SPLATMAP,TRANSFORM_TEX(i.uv0, _SPLATMAP));
                float3 _Moss_N_var = UnpackNormal(tex2D(_Moss_N,TRANSFORM_TEX(i.uv0, _Moss_N)));
                float3 _Glow_N_var = UnpackNormal(tex2D(_Glow_N,TRANSFORM_TEX(i.uv0, _Glow_N)));
                float3 _Dirt_N_var = UnpackNormal(tex2D(_Dirt_N,TRANSFORM_TEX(i.uv0, _Dirt_N)));
                float3 normalLocal = lerp(lerp(lerp(lerp(_Base_N_var.rgb,_Cracks_N_var.rgb,_SPLATMAP_var.r),_Moss_N_var.rgb,_SPLATMAP_var.g),_Glow_N_var.rgb,_SPLATMAP_var.b),_Dirt_N_var.rgb,_SPLATMAP_var.a);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(i.uv0, _Base));
                float4 _Cracks_var = tex2D(_Cracks,TRANSFORM_TEX(i.uv0, _Cracks));
                float4 _Moss_var = tex2D(_Moss,TRANSFORM_TEX(i.uv0, _Moss));
                float4 _Glow_var = tex2D(_Glow,TRANSFORM_TEX(i.uv0, _Glow));
                float4 _Dirt_var = tex2D(_Dirt,TRANSFORM_TEX(i.uv0, _Dirt));
                float gloss = lerp(lerp(lerp(lerp(_Base_var.a,_Cracks_var.a,_SPLATMAP_var.r),_Moss_var.a,_SPLATMAP_var.g),_Glow_var.a,_SPLATMAP_var.b),_Dirt_var.a,_SPLATMAP_var.a);
                float perceptualRoughness = 1.0 - lerp(lerp(lerp(lerp(_Base_var.a,_Cracks_var.a,_SPLATMAP_var.r),_Moss_var.a,_SPLATMAP_var.g),_Glow_var.a,_SPLATMAP_var.b),_Dirt_var.a,_SPLATMAP_var.a);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = float3(_Metallic,_Metallic,_Metallic);
                float specularMonochrome;
                float3 diffuseColor = lerp(lerp(lerp(lerp(_Base_var.rgb,_Cracks_var.rgb,_SPLATMAP_var.r),_Moss_var.rgb,_SPLATMAP_var.g),_Glow_var.rgb,_SPLATMAP_var.b),_Dirt_var.rgb,_SPLATMAP_var.a); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _GlowTexture_var = tex2D(_GlowTexture,TRANSFORM_TEX(i.uv0, _GlowTexture));
                float3 emissive = ((_SPLATMAP_var.a*_GlowTexture_var.rgb)*_GlowColour.rgb);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Metallic;
            uniform sampler2D _SPLATMAP; uniform float4 _SPLATMAP_ST;
            uniform sampler2D _Cracks; uniform float4 _Cracks_ST;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _Moss; uniform float4 _Moss_ST;
            uniform sampler2D _Glow; uniform float4 _Glow_ST;
            uniform sampler2D _Dirt; uniform float4 _Dirt_ST;
            uniform sampler2D _GlowTexture; uniform float4 _GlowTexture_ST;
            uniform float4 _GlowColour;
            uniform sampler2D _Base_N; uniform float4 _Base_N_ST;
            uniform sampler2D _Cracks_N; uniform float4 _Cracks_N_ST;
            uniform sampler2D _Moss_N; uniform float4 _Moss_N_ST;
            uniform sampler2D _Glow_N; uniform float4 _Glow_N_ST;
            uniform sampler2D _Dirt_N; uniform float4 _Dirt_N_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Base_N_var = UnpackNormal(tex2D(_Base_N,TRANSFORM_TEX(i.uv0, _Base_N)));
                float3 _Cracks_N_var = UnpackNormal(tex2D(_Cracks_N,TRANSFORM_TEX(i.uv0, _Cracks_N)));
                float4 _SPLATMAP_var = tex2D(_SPLATMAP,TRANSFORM_TEX(i.uv0, _SPLATMAP));
                float3 _Moss_N_var = UnpackNormal(tex2D(_Moss_N,TRANSFORM_TEX(i.uv0, _Moss_N)));
                float3 _Glow_N_var = UnpackNormal(tex2D(_Glow_N,TRANSFORM_TEX(i.uv0, _Glow_N)));
                float3 _Dirt_N_var = UnpackNormal(tex2D(_Dirt_N,TRANSFORM_TEX(i.uv0, _Dirt_N)));
                float3 normalLocal = lerp(lerp(lerp(lerp(_Base_N_var.rgb,_Cracks_N_var.rgb,_SPLATMAP_var.r),_Moss_N_var.rgb,_SPLATMAP_var.g),_Glow_N_var.rgb,_SPLATMAP_var.b),_Dirt_N_var.rgb,_SPLATMAP_var.a);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(i.uv0, _Base));
                float4 _Cracks_var = tex2D(_Cracks,TRANSFORM_TEX(i.uv0, _Cracks));
                float4 _Moss_var = tex2D(_Moss,TRANSFORM_TEX(i.uv0, _Moss));
                float4 _Glow_var = tex2D(_Glow,TRANSFORM_TEX(i.uv0, _Glow));
                float4 _Dirt_var = tex2D(_Dirt,TRANSFORM_TEX(i.uv0, _Dirt));
                float gloss = lerp(lerp(lerp(lerp(_Base_var.a,_Cracks_var.a,_SPLATMAP_var.r),_Moss_var.a,_SPLATMAP_var.g),_Glow_var.a,_SPLATMAP_var.b),_Dirt_var.a,_SPLATMAP_var.a);
                float perceptualRoughness = 1.0 - lerp(lerp(lerp(lerp(_Base_var.a,_Cracks_var.a,_SPLATMAP_var.r),_Moss_var.a,_SPLATMAP_var.g),_Glow_var.a,_SPLATMAP_var.b),_Dirt_var.a,_SPLATMAP_var.a);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = float3(_Metallic,_Metallic,_Metallic);
                float specularMonochrome;
                float3 diffuseColor = lerp(lerp(lerp(lerp(_Base_var.rgb,_Cracks_var.rgb,_SPLATMAP_var.r),_Moss_var.rgb,_SPLATMAP_var.g),_Glow_var.rgb,_SPLATMAP_var.b),_Dirt_var.rgb,_SPLATMAP_var.a); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _Metallic;
            uniform sampler2D _SPLATMAP; uniform float4 _SPLATMAP_ST;
            uniform sampler2D _Cracks; uniform float4 _Cracks_ST;
            uniform sampler2D _Base; uniform float4 _Base_ST;
            uniform sampler2D _Moss; uniform float4 _Moss_ST;
            uniform sampler2D _Glow; uniform float4 _Glow_ST;
            uniform sampler2D _Dirt; uniform float4 _Dirt_ST;
            uniform sampler2D _GlowTexture; uniform float4 _GlowTexture_ST;
            uniform float4 _GlowColour;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _SPLATMAP_var = tex2D(_SPLATMAP,TRANSFORM_TEX(i.uv0, _SPLATMAP));
                float4 _GlowTexture_var = tex2D(_GlowTexture,TRANSFORM_TEX(i.uv0, _GlowTexture));
                o.Emission = ((_SPLATMAP_var.a*_GlowTexture_var.rgb)*_GlowColour.rgb);
                
                float4 _Base_var = tex2D(_Base,TRANSFORM_TEX(i.uv0, _Base));
                float4 _Cracks_var = tex2D(_Cracks,TRANSFORM_TEX(i.uv0, _Cracks));
                float4 _Moss_var = tex2D(_Moss,TRANSFORM_TEX(i.uv0, _Moss));
                float4 _Glow_var = tex2D(_Glow,TRANSFORM_TEX(i.uv0, _Glow));
                float4 _Dirt_var = tex2D(_Dirt,TRANSFORM_TEX(i.uv0, _Dirt));
                float3 diffColor = lerp(lerp(lerp(lerp(_Base_var.rgb,_Cracks_var.rgb,_SPLATMAP_var.r),_Moss_var.rgb,_SPLATMAP_var.g),_Glow_var.rgb,_SPLATMAP_var.b),_Dirt_var.rgb,_SPLATMAP_var.a);
                float3 specColor = float3(_Metallic,_Metallic,_Metallic);
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float roughness = 1.0 - lerp(lerp(lerp(lerp(_Base_var.a,_Cracks_var.a,_SPLATMAP_var.r),_Moss_var.a,_SPLATMAP_var.g),_Glow_var.a,_SPLATMAP_var.b),_Dirt_var.a,_SPLATMAP_var.a);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
