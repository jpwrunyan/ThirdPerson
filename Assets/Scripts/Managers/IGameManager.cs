public interface IGameManager {
	ManagerStatus status {
		get;
	}

	void StartUp(NetworkService networkService);
}