using UnityEngine;

public class SceneLauncher : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _ammoHUDPrfab;
    [SerializeField] private GameObject _crosshairHUDPrfab;
    [SerializeField] private GameObject _menuHUDPrfab;
    [SerializeField] private Transform _playerStartPosition;

    private PauseMenu _pauseMenu;
    private Inventory _inventory;
    private WeaponManager _weaponManager;
    private PlayerInteractorDetector _playerInteractorDetector;
    private AmmoUI _ammoUI;
    private UICollectPanelHendler _collectPanelHendler;

    private void Awake()
    {
        _gameManager.StartGameManager();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject player = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
        GameObject ammoHUD = Instantiate(_ammoHUDPrfab);
        GameObject crosshairHUD = Instantiate(_crosshairHUDPrfab);
        GameObject menuHud = Instantiate(_menuHUDPrfab);
        _inventory = player.GetComponent<Inventory>();
        _weaponManager = player.GetComponent<WeaponManager>();
        _playerInteractorDetector = player.GetComponent<PlayerInteractorDetector>();
        _ammoUI = ammoHUD.GetComponent<AmmoUI>();
        _collectPanelHendler = crosshairHUD.GetComponent<UICollectPanelHendler>();
        _pauseMenu = menuHud.GetComponent<PauseMenu>();
        SetupUIDependencies();
    }

    private void SetupUIDependencies()
    {
        Debug.Log(_weaponManager);
        Debug.Log(_playerInteractorDetector);
        Debug.Log(_pauseMenu);
        GameManager.Instance.RegisterPauseMenu(_pauseMenu);
        _ammoUI.Initialize(_weaponManager, _inventory);
        _collectPanelHendler.Initialize(_playerInteractorDetector);
        _pauseMenu.gameObject.SetActive(false);
    }
}
