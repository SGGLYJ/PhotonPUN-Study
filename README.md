## Photon PUN2 study
### 연결 관련
 - 마스터 서버에 Connect, Disconnect 하는 부분, JoinLobby, JoinRoom, CreateRoom 등 어느정도 테스트했습니다.(PhotonManager.cs)

### 룸 내 동기화 관련
 - PhotonView 이용해서 Transform 동기화 처리를 간단히 실습했습니다.(TestMove.cs)
 - 소유권을 가진 물체에 대해 좌우 인풋으로 조작 가능하고, RPC를 통해 원하는 함수를 전체적으로 호출할 수 있는 것도 확인했습니다.