using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

public class HeroAnimation : MonoBehaviour
{
    #region config
    public Animator _anim;

    public enum HeroState {Chad, Mid, Weak};
    public HeroState heroState;

    public HeroStateAnims chadAnims, midAnims, weakAnims;
    public HeroStateAnims currentAnims;
    #endregion

    #region stateClass
    public class HeroStateAnims
    {
        public HeroState state;
        private static string stateString;

        public int idle, attackPrep, attack, hit;

        public HeroStateAnims(HeroState tState)
        {
            state = tState;

            stateString = string.Concat("Hero" + state.ToString());

            Debug.Log("the string for animation " + stateString);
            idle = GetAnimHash(stateString + "Idle");
            attackPrep = GetAnimHash(stateString + "AttackPrep");
            attack = GetAnimHash(stateString + "Attack");
            hit = GetAnimHash(stateString + "Hit");
        }
    }
    #endregion

    #region onStart
    private void Awake()
    {
        _anim = GetComponent<Animator>();

        defaultMat = GetComponent<SpriteRenderer>().sharedMaterial;

        chadAnims = new HeroStateAnims(HeroState.Chad);
        midAnims = new HeroStateAnims(HeroState.Mid);
        weakAnims = new HeroStateAnims(HeroState.Weak);
    }

    private void Start()
    {
        heroState = HeroState.Weak;
        UpdateActiveAnims();
        _anim.CrossFade(currentAnims.idle, 0, 0);
    }
    #endregion

    public void UpdateActiveAnims()
    {
       switch (heroState) 
       {
            case HeroState.Chad:
                currentAnims = chadAnims;
                break;
            case HeroState.Mid:
                currentAnims = midAnims;
                break;
            case HeroState.Weak:
                currentAnims = weakAnims;
                break;
       }
    }

    public static int GetAnimHash(string name)
    {
        return Animator.StringToHash(name);
    }

    public void AttackPrep()
    {
        _anim.CrossFade(currentAnims.attackPrep,0,0);
    }

    #region temp
    public float hitTime = .4f;
    Material defaultMat;
    public Material hitMat;
    #endregion

    public async void Hit()
    {
        _anim.CrossFade(currentAnims.hit, 0, 0);

        GetComponent<SpriteRenderer>().material = hitMat;
        await Task.Delay((int)(hitTime*1000));
        GetComponent<SpriteRenderer>().material = defaultMat;
    }

    private void OnValidate()
    {
        UpdateActiveAnims();
        _anim.CrossFade(currentAnims.idle, 0, 0);
    }
}
