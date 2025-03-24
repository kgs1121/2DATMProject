# 프로젝트명: ATM만들기
## 프로젝트 개요
- 제작 기간: 2025/03/20 ~ 2025/03/24
- 개발 엔진 및 언어: Unity, C#
---
- 핵심 목표: ATM의 기본적인 입출금과 송금 기능 구현 및 데이터 저장

## 기능 설명
|로그인 화면|회원가입 화면|메인 화면|
|:-------:|:----:|:----:|
|<image src = https://github.com/user-attachments/assets/4bde3a76-04c2-4f1c-ae06-161b5cd51dbe width = "300" height = "200">|<image src = https://github.com/user-attachments/assets/03414da6-8224-4d3a-934e-7d6ed1a5b12b width = "300" height = "200">|<image src = https://github.com/user-attachments/assets/5b0d7622-af61-4701-a624-22b693cca849 width = "400" height = "200">

|입금|출금|송금|
|:-------:|:----:|:----:|
|<image src = https://github.com/user-attachments/assets/e24dd9a7-7d90-43d4-aec0-8cc33e1d2f65 width = "300" height = "200">|<image src = https://github.com/user-attachments/assets/7e08c442-de5e-450b-b80f-eb130bec3fb1 width = "300" height = "200">|<image src = https://github.com/user-attachments/assets/fbdb8498-7716-4f8d-8246-95fb5c5087aa width = "300" height = "200">|

---

## 시스템 설명
### 1. 로그인 화면
- 로그인 화면에서 로그인 또는 회원가입 진행
### 2. 회원가입 화면
- ID와 이름, Password를 입력하여 회원가입 진행
- 중복된 ID는 회원가입 불가능
### 3. 메인 화면
- 회원가입 시 정한 이름과 계좌 잔액, 현금 잔액, 입금 버튼, 출금 버튼, 송금 버튼으로 메인 화면 구성
### 4. 입금
- 입금 버튼을 누르고 원하는 금액을 입력하여 계좌에 입금
- 입력한 금액보다 현금 부족 시 입금 불가
### 5. 출금 
- 출금 버튼을 누르고 원하는 금액을 입력하여 계좌에서 출금
- 입력한 금액보다 계좌 잔액 부족 시 출금 불가
### 6. 송금
- 송금 버튼을 누르고 원하는 유저의 이름과 금액을 입력하여 송금 가능
