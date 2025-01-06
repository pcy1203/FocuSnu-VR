<a href="https://gece.snu.ac.kr/gecexe/index.php?mid=gece_lms&document_srl=75539" target="_blank">
  <img src="https://github.com/user-attachments/assets/44215651-97e6-4fe3-98aa-c98a6180bf81" alt="배너" width="100%"/>
</a>
🔗 <a href="https://gece.snu.ac.kr/gecexe/index.php?mid=gece_lms&document_srl=75539">발표 영상 및 시연 영상 확인</a> <br/>
🔗 <a href="https://github.com/user-attachments/files/18318763/VR.PPT_5.pdf">프로젝트 발표 자료 확인</a>

<br/>
<hr />
<br/>

# 🌟 1. Project Overview (프로젝트 개요)
- **프로젝트 이름** : 집중하샤 - 집중력 향상을 위한 VR 애플리케이션
- **프로젝트 설명** : 외부 방해 속에서 주어진 과제를 수행하며 집중력을 향상시키는 VR
- **프로젝트 기간** : 2024/10/11 ~ 2024/12/10
- **비고** : 2024-2학기 서울대학교 공과대학 'VR/AR의 개론 및 실습' 과목 팀 프로젝트
<br/>
<br/>


# 🌟 2. Team Members (팀원 소개)
| 류명현 | 박찬영 | 정용환 |
|:------:|:------:|:------:|
| VR Programmer | VR Programmer | VR Programmer |
|과제 5 (탁구공 튀기기) 구현<br/>방해 요소 구현<br/>최종 빌드 및 시연|과제 3 (그림 순서 배열하기) 구현<br/>과제 4 (장애물 피하기) 구현<br/>점수 기록, 메인화면 구현<br/>보고서 작성 및 발표|과제 1 (같은 카드 뒤집기) 구현<br/>과제 2 (물건 위치 기억하기) 구현<br/>UI 상호작용 구현<br />최종 코드 수정(통합)|
<br/>
<br/>


# 🌟 3. Application Introduction (애플리케이션 소개)
- **프로젝트 배경** :
  - 4년 동안 국내 ADHD 환자 수 92% 이상 증가(국민건강보험공단, 2022)

  - 10~20대의 80% 이상 숏폼 시청 → 짧은 형식(30초 이내)의 콘텐츠 소비 선호 (오픈서베이, 2023)

  - 선행 연구 - 행동 과제를 제시하여 집중력을 개선하는 방법 제시(문종훈·원영식, 2016)
 
  - 선행 연구 - VR 환경에서 과제를 제시하여 집중력 향상시키는 애플리케이션 제안(한민수 외, 2001) → 흥미 부족 문제 지적

- **프로젝트 소개** :
  - 외부 방해 속에서 주어진 과제를 수행하며 집중력을 향상시키는 VR

  - 기본 : 사용자에게 과제를 부여하고, 집중력을 발휘하여 과제를 잘 수행하는지 평가

  - 차별점 1 : 방해 요소 도입 → 도전적 환경 제공, 더 높은 수준의 집중력 요구

  - 차별점 2 : 게임 요소 도입 (점수) → 흥미와 집중력 향상을 위한 동기 유발, 집중력 자가진단
    
- **애플리케이션 주요 기능** :
  - **과제 수행** : 5가지 종류의 과제(다양한 특성), 몰입을 통한 집중력 향상

  - **방해 요소** : 과제 수행과 집중을 어렵게 만드는 요소 (다양한 자극), 적극적 집중력 발휘 유도
    
  - **점수 기록** : 과제 수행에 대한 피드백 (동기 유발), 과제 수행 완료 시 기록

- **애플리케이션 화면** :

  |화면|이름|설명|
  |:--:|:--:|:--|
  |<img src="https://github.com/user-attachments/assets/c2b07f00-d35e-4d6d-b973-f884c1b8a17d" width=600 />|**메인 화면**|- 버튼을 클릭하여 각 과제의 수행 화면으로 이동<br/>- 방해 요소 선택 가능<br/>- 점수 기록 확인 가능|
  |<img src="https://github.com/user-attachments/assets/91cabba2-e704-4462-bd24-c7db59cc15c5" width=600 />|**과제 1: 같은 카드 뒤집기**|- 과제 : 테이블 위의 카드를 뒤집어서 같은 색 카드 짝을 맞추기<br/>- 점수 획득 : 얼마나 과제를 빠르게 수행했는지<br/>- 고유 방해 요소 : 잘못된 짝을 맞출 때마다 카드 2개가 섞임<br/>- 기대 효과 : 집중력, 단기 기억력|
  |<img src="https://github.com/user-attachments/assets/709a404a-99ae-4538-8749-dcdd3139b050" width=600 />|**과제 2: 물건 위치 기억하기**|- 과제 : 주어진 시간 동안 방 안 물건들의 위치를 기억하고, 방 중앙에 나타난 물건들의 원래 위치를 찾기 (1초 이상 제자리에 위치하면 물체가 인식됨)<br/>- 점수 획득 : 얼마나 과제를 빠르게 수행했는지<br/>- 기대 효과 : 집중력, 공간 인식 능력, 기억력|
  |<img src="https://github.com/user-attachments/assets/f2800549-96db-49bd-9f55-c89fa5988a4e" width=600 />|**과제 3: 그림 순서 배열하기**|- 과제 : 오디오를 듣고, 앞에 나타난 4개의 그림을 순서대로 배열하기<br/>- 점수 획득 : 배열이 완료되면 맞힌 그림마다 점수 부여, 하나라도 틀렸을 때 게임 종료<br/>- 고유 방해 요소 : 정신없이 움직이는 장난감<br/>- 기대 효과 : 집중력, 청각적 주의력, 순차적 기억력|
  |<img src="https://github.com/user-attachments/assets/481fe9be-9c18-427c-9cda-0ce72d3ddc10" width=600 />|**과제 4: 장애물 피하기**|- 과제 : 조이스틱을 통해 화면의 캐릭터를 움직여 새들을 피하고 코인을 획득하기<br/>- 점수 획득 : 코인을 획득할 때마다 점수 부여, 새와 충돌하면 게임 종료<br/>- 고유 방해 요소 : 일정 시간마다 조이스틱과 반대 방향으로 캐릭터가 이동<br/>- 기대 효과 : 집중력, 반응 능력|
  |<img src="https://github.com/user-attachments/assets/ed17f6ba-16d5-46f2-9b96-eb7736c68ce0" width=600 />|**과제 5: 탁구공 튀기기**|- 과제 : 탁구공 최대한 많이 튕기기<br/>- 점수 획득 : 탁구공을 튀길 때마다(탁구채에 맞힐 때마다) 점수 부여<br/>- 고유 방해 요소 : 가짜 공(빨간색 공) 존재<br/>- 기대 효과 : 집중력, 신체적 능력(손-눈 협응력)|
  |<img src="https://github.com/user-attachments/assets/b96ccbdb-9a4d-4cb3-9f9d-524e338f8a5c" width=600 />|**방해 요소**|- 모든 과제에 공통적으로 적용 (메인 화면에서 선택 가능)<br/>- 소리 : 주기적으로 주의를 빼앗는 소리 플레이<br/>- 빛 번쩍임 : 주기적으로 다른 색깔로 번쩍임<br/>- 시야 차단 : 사용자 앞에 검은색 물체가 떠다님<br/>- NPC 이동 : 사용자 주변을 NPC들이 돌아다니며 주의를 빼앗음|
  |<img src="https://github.com/user-attachments/assets/e6ca11a8-ad41-42eb-ba2b-ea8ccfbebc90" width=600 />|**점수 기록**|- 메인 화면에서 확인 가능<br/>- 과제가 종료되면 자동으로 기록<br/>- 과제마다 최근 10개의 점수, 최고 점수, 평균 점수기록|
  |<img src="https://github.com/user-attachments/assets/7c398c87-99cf-497f-826b-4c8799a80d3c" width=600 />|**UI**|- 과제 시작 전에 과제에 대한 설명이 나타남<br/>- 과제 수행 중에는 남은 시간, 점수가 화면에 표시<br/>- 과제 종료 후에는 점수와 함께 다시 시작할 수 있는 UI가 나타남|

- **기대 효과** :
  - 집중력 저하/주의력 결핍을 겪고 있는 사람들에게 현실에서보다 풍부한 집중력 향상의 기회를 제공

  - 사용자로 하여금 과제에 대한 점수 기록을 제시함으로써 본인의 집중력 수준을 자가진단 및 객관적 상태를 인지하고, 개선에 대한 의지를 강화

  - 실제 ADHD/인지 장애를 겪고 있는 환자들의 증상 완화를 위한 심리 치료 보조 수단으로도 사용

<br/>
<br/>


# 🌟 4. Tasks & Responsibilities (작업 및 역할 분담)

- **프로젝트 진행 과정**

  ![image](https://github.com/user-attachments/assets/1996094a-ba53-4cec-a47b-07a349908782)

- **팀원 R&R**

  ![image](https://github.com/user-attachments/assets/2b6669af-fc28-4834-b10d-595cfaa773d7)

- **테스트 및 문제 해결**

  - 기능 테스트 (기능 정상 작동), UI/UX 테스트 (직관적 상호작용), 빌드 테스트 (기기 정상 실행)

  - 초기 기획에 대한 수정 (문제 해결)
  
  - [ Before ] 모든 과제에 제한 시간을 두고 점수는 일부 과제에만 존재 → 부자연스러움
  
  - [ After ] 일부 과제에만 제한 시간을 두고 모든 과제에 점수를 부여 → 통일성, 점수를 통한 진단 가능

<br/>
<br/>


# 🌟 5. Contributions & Results (수행 역할 & 결과물)

- **수행 역할**

  - 과제 3 (그림 순서 배열하기) 구현
  
  - 과제 4 (장애물 피하기) 구현
 
  - 점수 기록 기능 구현
 
  - 메인 화면, 로고, 버튼 UI 디자인
 
  - 최종 보고서 작성 및 발표
 
- **문서**
  
  - 최종 보고서 : [VR개론 프로젝트 최종 결과보고서_5조 류명현 박찬영 정용환.pdf](https://github.com/user-attachments/files/18318766/VR._5.pdf)
 
  - 최종 발표 자료 : [VR개론 프로젝트 발표 자료 PPT_5조 류명현 박찬영 정용환.pdf](https://github.com/user-attachments/files/18318763/VR.PPT_5.pdf)

- **개선점**
  
  - 의학적인 주의력 결핍 진단 기준 및 치료 방법에 근거하여 과제와 방해 요소를 더 정교하게 제시

  - 과제의 난이도(제한 시간, 요구 수준 등)를 다양화하고 사용자의 집중력 수준에 맞추어 난이도를 달리 제시
    
  - 피드백의 수단으로 단순히 점수만을 보여주는 것이 아니라 의학적인 소견이나 행동 지침 등을 제시
  
<br/>
<br/>


# 🌟 6. Technology Stack (기술 스택)
## 6.1 Programming & Modeling
| Language/Tool | Version |
|:-------:|:-------:|
| ![js](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) | |
| ![js](https://img.shields.io/badge/Unity-100000?style=for-the-badge&logo=unity&logoColor=white) | 2022.3.45f1 |

<br/>

## 6.2 Cooperation
| Tool | Usage |
|:---:|:---:|
|![js](https://img.shields.io/badge/Google-4285F4?logo=google&logoColor=fff&style=for-the-badge)|코드 및 에셋 관리|
|![js](https://img.shields.io/badge/Notion-000000?style=for-the-badge&logo=notion&logoColor=white)|태스크 관리 및 회의록 정리|

<br/>

## 6.3 Packages
- Packages/manifest.json에서 보다 자세히 확인 가능
  
| Package | Version |
|:---:|:---:|
|XR-Interaction-Toolkit|2.6.3|
|XR-Oculus|4.2.0|
|XR-OpenXR|1.12.1|
|AI-Navigation|1.1.5|
|2D-Sprite|1.0.0|
|Render-Pipelines-Universal|14.0.11|
|Scriptablebuildpipeline|1.21.25|

<br/>

## 6.4 Assets
- Unity Asset Store에서 활용한 Asset 목록
  - <a href="https://assetstore.unity.com/packages/3d/props/low-poly-basic-items-pack-household-items-249507">Low Poly Basic Items Pack - Household Items</a>

  - <a href="https://assetstore.unity.com/packages/3d/props/interior/low-poly-wooden-kid-s-toys-162585">Low-Poly Wooden Kid's Toys</a>
  
  - <a href="https://assetstore.unity.com/packages/2d/characters/2d-cartoon-birds-pack-149167">2D Cartoon Birds pack</a>
  
  - <a href="https://assetstore.unity.com/packages/2d/environments/2d-animated-coin-2d-rpk-22009">2D Animated Coin - 2D RPK</a>
  
  - <a href="https://assetstore.unity.com/packages/3d/props/arcade-machines-polypack-207908">Arcade Machines - PolyPack</a>
  
  - <a href="https://assetstore.unity.com/packages/2d/characters/bolt-2d-jellyfarm-assets-pack-188722">Bolt 2D JellyFarm Assets Pack</a>

  - <a href="https://assetstore.unity.com/packages/3d/props/low-poly-table-tennis-set-181749">Low-Poly Table Tennis Set</a>


<br />
<hr />

<div algin=center>
  <a display="block" href="https://gece.snu.ac.kr/gecexe/index.php?mid=gece_lms&document_srl=75539" target="_blank">
    <img src="https://github.com/user-attachments/assets/6468dc6c-980e-4f9b-9972-5626eb5b6f3e" alt="로고" width="50%"/>
  </a>
  <br /> 
  (▲ 로고를 클릭하여 발표/시연 영상 확인 가능)
</div>
