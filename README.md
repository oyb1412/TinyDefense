### Tiny Defense

---

## Description

---


- 🔊프로젝트 소개

  -구글 플레이 출시를 목적으로 제작한 게임입니다.

  -동일 타입, 레벨의 타워를 합쳐 적을 막아내는 타워 디펜스 게임입니다.

  -Firebase, GoogleAPI등 서드파티 라이브러리를 적극 사용했습니다.

  -SOLID원칙의 개방폐쇄의 원칙을 지켜 콘텐츠의 수월한 추가를 위한 설계에 집중했습니다.

       

- 개발 기간 : 2024.05.14 - 2024.06.08

- 🛠️사용 기술

   -c#

   -Unity Engine

   -Firebase Auth, Firebase Firestore

   -Google Admob

- 💻구동 화면

![스크린샷(24)](https://github.com/oyb1412/TinyDefense/assets/154235801/840eaa96-be91-4418-9232-8d7ce3ff5aa6)

## 목차

---

- 기획 의도
- 발생 및 해결한 주요 문제점
- 핵심 로직


### 기획 의도

---

- 온전히 안드로이드 환경에서 구동되는 모바일 게임을 개발해보고자 시작했습니다.

- 광고 및 회원가입, 로그인, 랭킹 시스템 등 실제 서비스되고 있는 모바일 게임의 실제 환경을 구축해 보고자 시작했습니다.

- PC에 비해 제약 사항이 많은 모바일 환경에서도 안정적으로 구동되는, 최적화된 게임을 만들어보고자 시작했습니다.


### 발생 및 해결한 주요 문제점

---

- (발생)물리 엔진 사용으로 인한 프레임 드랍

   -타일 선택 및 주변 객체 탐색, 충돌 효과 등을 리지드바디 & 콜라이더를 이용해 구현했습니다.

   ![그림1](https://github.com/oyb1412/TinyDefense/assets/154235801/a61162f9-cfbe-459a-a752-a5bbd356d51b)

   -이로 인해 물리 엔진을 사용하는 객체가 늘어나면, 안정적으로 프레임 유지가 안되는 상황이 발생했습니다.

   ![그림2](https://github.com/oyb1412/TinyDefense/assets/154235801/ce8b0f03-61f2-4b2c-98de-984c646b5c21)

- (해결)물리엔진 사용을 배제해 평균 프레임 복구

   -타일 선택, 객체 탐색, 충돌 효과 등 물리엔진을 사용하던 모든 로직을 물리엔진을 사용하지 않는 로직으로 구현했습니다.

   ![그림3](https://github.com/oyb1412/TinyDefense/assets/154235801/4f872542-7730-4da0-b6f1-5b2fe2cd7f77)

   -또한 프로젝트 설정에서 물리 엔진 설정을 비활성화 하거나 주기를 늦춰, 물리 엔진이 퍼포먼스에 영향을 거의 주지 않게 변경했습니다.

   ![그림4](https://github.com/oyb1412/TinyDefense/assets/154235801/1a836a90-65ee-4859-8395-dee97d4b09eb)

  -그 결과, 객체가 아무리 많아져도 안정적으로 프레임이 유지되는 결과를 확인했습니다. 

  ![19](https://github.com/oyb1412/TinyDefense/assets/154235801/31a51e8f-4dbb-48a1-9ea8-d0789366a76a)

- (발생)Google Admob등 서드파티 라이브러리 사용으로 인한 반복된 안드로이드 빌드 실패

  -각종 서드파티 SDK를 추가한 상태에서 안드로이드 빌드를 시도했지만 실패했습니다.

  ![10](https://github.com/oyb1412/TinyDefense/assets/154235801/b1e5acce-ad50-46d1-80c2-c2db9fce9da3)

  -구글링을 통해 나온 여러 방법을 동원해 그레이들 파일과 매니패스트 파일을 수정 후 빌드를 시도했지만 실패했습니다.

  ![그림1](https://github.com/oyb1412/TinyDefense/assets/154235801/5cbc1c65-410d-4049-b1d2-2aa8f9facb19)

- (해결)새로운 프로젝트로 이전 및 중복 SDK 미다운로드로 안드로이드 빌드 성공

  -프로젝트에서 모든 SDK를 제거한 후, 새로운 프로젝트를 생성해 모든 내용을 옮겼습니다.

  -천천히 SDK를 하나씩 설치하며, 공식 홈페이지의 설치 방법을 충실히 이행했습니다.

  ![13](https://github.com/oyb1412/TinyDefense/assets/154235801/c5c86073-effb-4916-afb9-fa7730e04b9a)

  -Firebase와 Google Admob SDK중 중복되는 부분을 임포트 하면 문제가 생긴다는 강사님의 충고를 듣고, 새롭게 임포트를 할 때 중복되는 부분은 제외했습니다.

  ![14](https://github.com/oyb1412/TinyDefense/assets/154235801/e131509b-f46a-4885-bc4a-c66bd74bc0f4)

  -또한 Minimun API및 Target API를 조정해 버전 이슈 문제를 해결했습니다.

  ![15](https://github.com/oyb1412/TinyDefense/assets/154235801/acd54713-4681-4a05-8876-ddfb08e22b67)

  -그 결과, 성공적으로 빌드 및 안드로이드 기기에서 실행에 성공했습니다.

  ![16](https://github.com/oyb1412/TinyDefense/assets/154235801/15900dfc-7853-4bc6-b329-9685654715a3)


### 핵심 로직

---
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・Firebase를 이용한 데이터 관리

게임에 필요한 모든 데이터를 로컬에 저장하면 보안 및 유지보수성에서 뒤떨어진다 판단했고, 모든 데이터를 클라우드 서버에 저장 후 게임 시작 시
리소스를 로컬 폴더에 저장하도록 했습니다.
처음으로 게임 시작시 게임에 필요한 데이터를 로드하며, 이후에는 그 과정이 스킵됩니다.
또한 한번 로그인을 진행하면 로그인 정보를 저장해, 매번 로그인을 해야하는 불편함을 줄였습니다.
데이터 로드는 모두 비동기로 진행되고 대략적인 로딩 상태를 UI로 표시합니다.

![그림6](https://github.com/oyb1412/TinyDefense/assets/154235801/43d5a1bb-e068-4f45-9fbe-8001e3c394fa)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・AES복호화를 이용한 데이터 복호화

클라우드 서버에서 내려받은 데이터를 그대로 로컬에 저장하면 보안 상 위험성이 존재하기 때문에, 모든 데이터를 복호화해 저장 및 로드하도록 했습니다.

![그림7](https://github.com/oyb1412/TinyDefense/assets/154235801/02f8e3a9-56b2-4530-9f71-d1c6ad60ef24)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・옵저버 패턴을 이용한 이벤트 위주 로직

데이터의 변경이 없음에도 주기적으로 데이터를 동기화해, 필요 없는 작업이 지속적으로 반복되어 결과적으로 퍼포먼스가 하락되었기 때문에, 최적화를 위해 사용했습니다.

![그림8](https://github.com/oyb1412/TinyDefense/assets/154235801/9e72daf5-2ec3-44d7-af59-36d6108d7778)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・Google API를 이용한 광고 시스템

광고 시스템은 무료 모바일 게임에 필수라고도 할 수 있는 요소라고 생각하였기 때문에, 
게임의 진행 속도를 증가시키는 등 부가적 요소 및 어빌리티를 재설정 할 수 있는 기능적 요소에 짧은 광고 시청을 강제했습니다.

![그림9](https://github.com/oyb1412/TinyDefense/assets/154235801/b42cdf44-ec1b-4122-8d27-42294a4cc12e)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・애니메이터와 파라미터를 이용한 유닛 애니메이션 시스템

Play() 등 단순한 애니메이션 호출 메서드로 원할 때 애니메이션을 호출할 수는 있었지만, 애니메이션이 자연스럽게 이어지는 것이 아닌 뚝뚝 끊기는 연출이 반복되는 문제를 해결하기 위해 사용했습니다.

![그림10](https://github.com/oyb1412/TinyDefense/assets/154235801/495b490e-57de-4d0e-b0f7-ccbe3bf4b42b)

![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・씬 전환 페이드 시스템

씬 전환시 아무 연출없이 즉각적으로 화면이 전환되어 화면이 갈아끼워지는듯한 느낌을 받는다는 피드백을 받아, 보다 극적인 연출을 위해 사용하였습니다.
또한 트위닝을 적극적으로 사용해, 페이드 전환 후 자연스러운 콜백함수 실행이 가능했습니다.

![그림11](https://github.com/oyb1412/TinyDefense/assets/154235801/343bf144-4cee-46f9-9ff9-9f8cee730246)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)

### ・물리엔진을 사용하지 않고 모든 로직 구현

모바일 기기에서의 성능 최적화를 위해, 콜라이더, 리지드바디 등 유니티의 물리엔진을 사용하지 않고 모든 기능을 구현했습니다.
또한 유니티 프로젝트 세팅의 물리 설정을 비활성화해 상당한 성능 최적화에 성공했습니다.

![12](https://github.com/oyb1412/TinyDefense/assets/154235801/6879138d-7fa0-4104-9256-00e6b865421d)
![Line_1_(1)](https://github.com/oyb1412/TinyDefense/assets/154235801/f664c47e-d52b-4980-95ec-9859dea11aab)
### ・풀링 오브젝트 시스템

각종 오브젝트를 필요할 때 마다 생성, 필요가 없어지면 제거해 짧은 시간 내에 다량의 객체를 생성하고 제거하는 상황이 반복되 퍼포먼스가 크게 하락하였기에 사용했습니다.
게임 시작 시 런타임시 동적으로 생성되는 객체들을 수십 ~ 수백체씩 생성 해 두고 풀링으로 사용해, 런타임 시 객체를 생성하는 일을 방지했습니다.

![그림12](https://github.com/oyb1412/TinyDefense/assets/154235801/8e830d9b-8495-44d8-9c3b-22789a20ee9a)
