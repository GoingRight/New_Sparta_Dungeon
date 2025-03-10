using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public Dictionary<BuffItemData, Coroutine> nowBuffs = new Dictionary<BuffItemData, Coroutine>();
    public void StartBuffEffect(BuffItemData data)
    {
        BuffEffect target = null;
        switch (data.buffType)
        {
            case Buff.InfiniteStamina:
                target = new InfiniteStaminaBuffEffect(data);
                break;
        }
        if(nowBuffs.TryGetValue(data, out Coroutine beforeCo))
        {
            StopCoroutine(beforeCo);
            nowBuffs[data] = StartCoroutine(BuffCo(target));
        }
        else
        {
            nowBuffs.Add(data, StartCoroutine(BuffCo(target)));
        }



    }

    private IEnumerator BuffCo(BuffEffect target)
    {   
        target.StartBuff();
        yield return new WaitForSeconds(target.buffItemData.buffDuration);
        target.EndBuff();
        nowBuffs.Remove(target.buffItemData);
    }
}

public abstract class BuffEffect
{
    public BuffEffect(BuffItemData data)
    {
        buffItemData = data;
    }

    public BuffItemData buffItemData;
    public abstract void StartBuff();
    public abstract void EndBuff();
}

public class InfiniteStaminaBuffEffect : BuffEffect
{
    public InfiniteStaminaBuffEffect(BuffItemData data) : base(data)
    {

    }
    public float originJumpCost = CharacterManager.Instance.Player.controller.jumpCost;
    public float originRunCost = CharacterManager.Instance.Player.controller.runStaminaCost;
    public override void EndBuff()
    {
        CharacterManager.Instance.Player.controller.jumpCost = originJumpCost;
        CharacterManager.Instance.Player.controller.runStaminaCost = originRunCost;
    }

    public override void StartBuff()
    {
        CharacterManager.Instance.Player.controller.jumpCost = 0;
        CharacterManager.Instance.Player.controller.runStaminaCost = 0;
    }
}