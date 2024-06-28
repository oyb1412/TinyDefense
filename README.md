### 

# Tiny Defense


---

## Description

---


- 🔊프로젝트 소개
  
  Tiny Defense는 구글 플레이 출시를 목표로 제작된 타워 디펜스 게임입니다. 동일 타입과 레벨의 타워를 합쳐 적을 막아내는 방식을 채택했으며, 실제 모바일 게임 환경을 구축하여 최적화된 게임을 제공하는 데 중점을 두었습니다.

- 개발 기간 : 2024.05.14 - 2024.06.08

- 🛠️사용 기술 및 개발 환경

   -언어 : C#
  
   -엔진 : Unity Engine
  
   -데이터베이스 : Firebase Auth, Firebase Firestore
  
   -광고 : Google Admob
  
   -개발 환경 : Window 10, Unity 2021.3.10f1, Android SDK



- 💻구동 화면

![스크린샷(24)](https://github.com/oyb1412/TinyDefense/assets/154235801/840eaa96-be91-4418-9232-8d7ce3ff5aa6)

## 목차

---

- 기획 의도
- 발생 및 해결한 주요 문제점
- 핵심 로직


### 기획 의도

---

- 안드로이드 환경에서 안정적으로 구동되는 모바일 게임 개발.

- 광고, 회원가입, 로그인, 랭킹 시스템 등 실제 서비스 환경 구축.
  
- 모바일 환경에서도 최적화된 성능 제공.


### 발생 및 해결한 주요 문제점

---

- (발생)물리 엔진 사용으로 인한 프레임 드랍

   -타일 선택 및 주변 객체 탐색, 충돌 효과 등을 리지드바디 & 콜라이더를 이용해 구현.

   ![그림1](https://github.com/oyb1412/TinyDefense/assets/154235801/a61162f9-cfbe-459a-a752-a5bbd356d51b)

   -이로 인해 물리 엔진을 사용하는 객체가 늘어나면, 안정적으로 프레임 유지가 안되는 상황이 발생.

   ![그림2](https://github.com/oyb1412/TinyDefense/assets/154235801/ce8b0f03-61f2-4b2c-98de-984c646b5c21)

- (해결)물리엔진 사용을 배제해 평균 프레임 복구

   -타일 선택, 객체 탐색, 충돌 효과 등 물리엔진을 사용하던 모든 로직을 물리엔진을 사용하지 않는 로직으로 변경.

   ![그림3](https://github.com/oyb1412/TinyDefense/assets/154235801/4f872542-7730-4da0-b6f1-5b2fe2cd7f77)

   -프로젝트 설정에서 물리 엔진 설정을 비활성화 하거나 주기를 늦춰, 물리 엔진이 퍼포먼스에 영향을 거의 주지 않게 변경.

   ![그림4](https://github.com/oyb1412/TinyDefense/assets/154235801/1a836a90-65ee-4859-8395-dee97d4b09eb)

  -그 결과, 객체가 아무리 많아져도 안정적으로 프레임 유지 성공. 

  ![19](https://github.com/oyb1412/TinyDefense/assets/154235801/31a51e8f-4dbb-48a1-9ea8-d0789366a76a)

- (발생)Google Admob등 서드파티 라이브러리 사용으로 인한 반복된 안드로이드 빌드 실패

  -각종 서드파티 SDK를 추가한 상태에서 안드로이드 빌드를 시도했지만 실패.

  ![10](https://github.com/oyb1412/TinyDefense/assets/154235801/b1e5acce-ad50-46d1-80c2-c2db9fce9da3)

  -구글링을 통해 나온 여러 방법을 동원해 그레이들 파일과 매니패스트 파일을 수정 후 빌드를 시도했지만 실패.

  ![그림1](https://github.com/oyb1412/TinyDefense/assets/154235801/5cbc1c65-410d-4049-b1d2-2aa8f9facb19)

- (해결)새로운 프로젝트로 이전 및 중복 SDK 미다운로드로 안드로이드 빌드 성공

  -프로젝트에서 모든 SDK를 제거한 후, 새로운 프로젝트를 생성해 모든 내용을 이전.

  -천천히 SDK를 하나씩 설치하며, 공식 홈페이지의 설치 방법을 충실히 이행.

  -Firebase와 Google Admob SDK중 중복되는 부분은 임포트에서 제외.

  ![14](https://github.com/oyb1412/TinyDefense/assets/154235801/e131509b-f46a-4885-bc4a-c66bd74bc0f4)

  -Minimun API및 Target API를 조정해 버전 이슈 문제를 해결.

  ![15](https://github.com/oyb1412/TinyDefense/assets/154235801/acd54713-4681-4a05-8876-ddfb08e22b67)

  -그 결과, 성공적으로 빌드 및 안드로이드 기기에서 실행에 성공.

  ![16](https://github.com/oyb1412/TinyDefense/assets/154235801/15900dfc-7853-4bc6-b329-9685654715a3)


### 핵심 로직

---
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・Firebase를 이용한 데이터 관리

모든 데이터를 클라우드 서버에 저장 후 로컬 폴더에 로드. 초기 로딩 후 로그인 정보 저장으로 편의성 제공.

데이터 로드는 비동기로 진행되며, UI를 통해 로딩 상태 표시.

![그림6](https://github.com/oyb1412/TinyDefense/assets/154235801/43d5a1bb-e068-4f45-9fbe-8001e3c394fa)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・AES복호화를 이용한 데이터 복호화

클라우드 서버에서 데이터를 로컬에 저장 시 보안을 위해 AES 복호화 적용.

![그림7](https://github.com/oyb1412/TinyDefense/assets/154235801/02f8e3a9-56b2-4530-9f71-d1c6ad60ef24)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・옵저버 패턴을 이용한 이벤트 위주 로직

불필요한 데이터 동기화를 줄이고 최적화를 위해 이벤트 기반 로직 사용.

![그림8](https://github.com/oyb1412/TinyDefense/assets/154235801/9e72daf5-2ec3-44d7-af59-36d6108d7778)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・Google API를 이용한 광고 시스템

게임 진행 속도를 증가시키는 등의 부가적 요소에 짧은 광고 시청을 포함하여 수익 창출.

![그림9](https://github.com/oyb1412/TinyDefense/assets/154235801/b42cdf44-ec1b-4122-8d27-42294a4cc12e)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・애니메이터와 파라미터를 이용한 유닛 애니메이션 시스템

애니메이션의 자연스러운 연출을 위해 애니메이터와 파라미터를 사용.

![그림10](https://github.com/oyb1412/TinyDefense/assets/154235801/495b490e-57de-4d0e-b0f7-ccbe3bf4b42b)

![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・씬 전환 페이드 시스템

극적인 씬 전환 연출을 위해 페이드 전환 효과와 트위닝 사용.

![그림11](https://github.com/oyb1412/TinyDefense/assets/154235801/343bf144-4cee-46f9-9ff9-9f8cee730246)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・물리엔진을 사용하지 않고 모든 로직 구현

성능 최적화를 위해 유니티의 물리엔진을 배제하고 로직 구현.

![12](https://github.com/oyb1412/TinyDefense/assets/154235801/6879138d-7fa0-4104-9256-00e6b865421d)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・풀링 오브젝트 시스템

런타임 시 객체 생성과 제거를 방지하고, 성능을 높이기 위해 풀링 시스템 사용.

![그림12](https://github.com/oyb1412/TinyDefense/assets/154235801/8e830d9b-8495-44d8-9c3b-22789a20ee9a)
