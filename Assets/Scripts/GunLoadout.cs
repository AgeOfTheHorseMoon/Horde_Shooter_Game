using UnityEngine;

public class GunLoadout
{
    private Transform[] _availableGunSlots;
    private GunDataSO[] _equippedGunsList;

    public GunLoadout(GunLoadoutDataSO loadoutDataSO, Transform[] availableGunSlots)
    {
        // Don't know whick will be better use either
        //for (int i = 0; i < loadoutDataSO.Loadout.Length; i++)
        //{
        //    _equippedGunsList[i] = loadoutDataSO.Loadout[i];
        //}
        _equippedGunsList = loadoutDataSO.Loadout;

        _availableGunSlots = availableGunSlots;

        EquipGuns();
    }

    public void EquipGuns()
    {
        for (int i = 0; i < _availableGunSlots.Length; i++)
        {
            _availableGunSlots[i].TryGetComponent(out Gun gunScript);
            gunScript.EquipGun(_equippedGunsList[i]);
        }
    }

    public GunDataSO[] GetEquippedGuns()
    {
        return _equippedGunsList;
    }

    // Need another addgun method to switch weapons, adds only 1 gun per function call
    public void AddGunToList(GunDataSO gunToEquip)
    {
        for (int i = 0; i < _equippedGunsList.Length; i++)
        {
            if (_equippedGunsList[i] == null)
            {
                _equippedGunsList[i] = gunToEquip;

                _availableGunSlots[i].TryGetComponent(out Gun gunScript);
                gunScript.EquipGun(gunToEquip);
                
                return; // because of this, but required to debug for now
            }
        }
        Debug.Log("All slots are full: " + _equippedGunsList.ToString());
    }

    // Make it so that removing based on index is possible
    public void RemoveGunFromList(GunDataSO gunToRemove)
    {
        for (int i = 0; i < _equippedGunsList.Length; i++)
        {
            if (gunToRemove == _equippedGunsList[i])
            {
                _equippedGunsList[i] = null;
            }
        }
    }

    public void SwitchGunsOnIndex(int index, GunDataSO gunToSwitch)
    {
        _equippedGunsList.SetValue(gunToSwitch, index);
    }
}
