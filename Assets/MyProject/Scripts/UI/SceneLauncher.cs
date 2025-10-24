using UnityEngine;

public class SceneLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _ammoHUDPrfab;
    [SerializeField] private GameObject _crosshairHUDPrfab;
    [SerializeField] private Transform _playerStartPosition;

    private Inventory _inventory;
    private WeaponManager _weaponManager;
    private PlayerInteractorDetector _playerInteractorDetector;
    private AmmoUI _ammoUI;
    private UICollectPanelHendler _collectPanelHendler;

    private void Awake()
    {
        GameObject player = Instantiate(_playerPrefab, _playerStartPosition.position, Quaternion.identity);
        GameObject ammoHUD = Instantiate(_ammoHUDPrfab);
        GameObject crosshairHUD = Instantiate(_crosshairHUDPrfab);
        _inventory = player.GetComponent<Inventory>();
        _weaponManager = player.GetComponent<WeaponManager>();
        _playerInteractorDetector = player.GetComponent<PlayerInteractorDetector>();
        _ammoUI = ammoHUD.GetComponent<AmmoUI>();
        _collectPanelHendler = crosshairHUD.GetComponent<UICollectPanelHendler>();
        SetupUIDependencies();
    }

    private void SetupUIDependencies()
    {
        _ammoUI.Initialize(_weaponManager, _inventory);
        _collectPanelHendler.Initialize(_playerInteractorDetector);
    }
}
