# PlanetVoid

# 🎮 [게임 이름] - Unity 기반 2D 액션 게임

## 🧑‍💻 한줄 소개
`“Unity와 C#을 활용해 실시간 장애물 회피 및 객체 생성 시스템을 구현한 2D 캐주얼 게임”`

---

## 📌 프로젝트 개요

| 항목 | 설명 |
|------|------|
| 개발 기간 | 2025.04 ~ 2025.06 (약 2개월) |
| 개발 인원 | 1명 (개인 프로젝트) |
| 사용 언어 | C#, Unity |
| 주요 기능 | 랜덤 생성, 인터페이스 기반 확장, 충돌 처리, UI 스폰 시스템 |

---

## 🎮 주요 기능 구현

### 🌞 1. Sun 오브젝트 랜덤 생성
- 일정 시간마다 `SunSpawner`가 `spawnRadius` 내에서 `Physics2D.OverlapCircle`을 이용해 충돌 없이 생성
- `Rigidbody2D.linearVelocity`를 이용해 360도 랜덤 방향 이동 구현

### 🌊 2. Wave형 방해 오브젝트
- `WaveObPlanet`은 `EnemyInterface.Spawn()`을 통해 화면 좌우 외곽에서 생성
- `sin()` 함수를 이용해 물결처럼 진입하며 `Vector2.Perpendicular` 기반 이동 구현

### ☄️ 3. Meteor(운석) 낙하형 장애물 *(기획 또는 구현 중)*
- 화면 상단에서 대각선 하향 이동
- 충돌 시 파괴 및 이펙트 구현 예정

---

## 🔍 기술 포인트

- `Interface` 기반 적 생성 시스템 설계 (`EnemyInterface`)
- 충돌 감지를 위한 `Physics2D.OverlapCircle`
- 깔끔한 생성 위치 선정 로직 (`TrySpawn()` 반복 + maxAttempts)
- 물리 기반 이동 (`Rigidbody2D.linearVelocity`)
- 객체 간 분리와 재사용성을 고려한 설계

---
